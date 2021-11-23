using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace SagaLib
{
    public class NetIO
    {
        public enum Mode
        {
            Server,
            Client
        }
        private byte[] buffer = new byte[4];
        byte[] restBuffer;
        int restBufferLength;

        private AsyncCallback callbackSize;
        private AsyncCallback callbackData;
        private AsyncCallback callbackKeyExchange;
        private AsyncCallback callbackSend;
        
        public Socket sock;
        /// <summary>
        /// 加密算法实例
        /// </summary>
        public Encryption Crypt;
        private NetworkStream stream;

        /// <summary>
        /// 绑定的客户端
        /// </summary>
        public Client client;

        private ushort firstLevelLenth = 4;

        private bool isDisconnected;
        private bool disconnecting;

        private int keyAlreadyReceived;

        private int lastSize;
        private int alreadyReceived;

        internal int waitCounter = 0;
        DateTime receiveStamp = DateTime.Now;
        DateTime sendStamp = DateTime.Now;
        int receivedBytes = 0;
        int sentBytes = 0;
        int avarageReceive = 0;
        int avarageSend = 0;

        public delegate void PacketEventArg(Packet p);
        public event PacketEventArg OnReceivePacket;
        public event PacketEventArg OnSendPacket;
                
        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type
        /// </summary>
        private Dictionary<ushort, Packet> commandTable;

        /// <summary>
        /// 已断开的
        /// </summary>
        public bool Disconnected { get { return this.isDisconnected; } }

        /// <summary>
        /// 封包头的长度
        /// </summary>
        public ushort FirstLevelLength
        {
            get
            {
                return firstLevelLenth;
            }
            set
            {
                firstLevelLenth = value;
            }
        }

        /// <summary>
        /// 当前平均使用的上行带宽，以字节为单位
        /// </summary>
        public int UpStreamBand { get { return avarageSend; } }

        /// <summary>
        /// 当前平均使用的下行带宽，以字节为单位
        /// </summary>
        public int DownStreamBand { get { return avarageReceive; } }

        /// <summary>
        /// 是新的连接
        /// </summary>
        /// <param name="sock"></param>
        /// <param name="commandTable"></param>
        /// <param name="client"></param>
        public NetIO(Socket sock, Dictionary<ushort, Packet> commandTable, Client client)
        {
            this.sock = sock;
            this.stream = new NetworkStream(sock);
            this.commandTable = commandTable;
            this.client = client;
            Crypt = new Encryption();
                    

            this.callbackSize = new AsyncCallback(this.ReceiveSize);
            this.callbackData = new AsyncCallback(this.ReceiveData);
            this.callbackKeyExchange= new AsyncCallback(this.ReceiveKeyExchange);
            this.callbackSend = new AsyncCallback(this.OnSent);
            // Use the static key untill the keys have been exchanged
            
            this.isDisconnected = false;
            
        }

        private void StartPacketParsing()
        {
            if (sock.Connected)
            {
                client.OnConnect();
                try { stream.BeginRead(buffer, 0, 4, this.callbackSize, null); }
                catch (Exception ex)
                {
                    Logger.ShowError(ex, null);
                    try//this could crash the gateway somehow,so better ignore the Exception
                    {
                        this.Disconnect();
                    }
                    catch (Exception)
                    {
                    }
                    Logger.ShowWarning("Invalid packet head from:" + sock.RemoteEndPoint.ToString(), null);
                    return;
                }
            }
            else { this.Disconnect(); return; }
        }
        /// <summary>
        /// 设置当前网络层模式，客户端或服务器端
        /// </summary>
        /// <param name="mode">需要设定的模式</param>
        public void SetMode(Mode mode)
        {
            byte[] data;
            switch (mode)
            {
                case Mode.Server :
                    try
                    {
                        data = new byte[8];
                        
                        keyAlreadyReceived = 8;
                        
                        stream.BeginRead(data, 0, 8, this.callbackKeyExchange, data);
                    }
                    catch (Exception)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    break;
                case Mode.Client :
                    try
                    {
                        data = new byte[529];
                        int size;
                        if (sock.Available < 529)
                            size = sock.Available;
                        else
                            size = 529;

                        keyAlreadyReceived = size;
                        stream.BeginRead(data, 0, size, this.callbackKeyExchange, data);
                    }
                    catch (Exception)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    break;
            }
        }

        private void ReceiveKeyExchange(IAsyncResult ar)
        {
            try
            {
                if (this.isDisconnected)
                {
                    return;
                }
                if (!sock.Connected)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                try { stream.EndRead(ar); }
                catch (Exception)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                
                byte[] raw = (byte[])ar.AsyncState;

                if (keyAlreadyReceived  < raw.Length)
                {
                    int left = raw.Length - keyAlreadyReceived;
                    if (left > 1024)
                        left = 1024;
                    if (left > sock.Available) left = sock.Available;
                    try
                    {
                        stream.BeginRead(raw, keyAlreadyReceived, left, this.callbackKeyExchange, raw);
                    }
                    catch (Exception)
                    {
                        //Logger.ShowError(ex);
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    keyAlreadyReceived += left;
                    return;
                }

                if (raw.Length == 8)
                {
                    Packet p1 = new Packet(529);
                    p1.PutUInt(1, 4);
                    p1.PutByte(0x32, 8);
                    p1.PutUInt(0x100, 9);
                    Crypt.MakePrivateKey();
                    string bufstring = Conversions.bytes2HexString(Encryption.Module.getBytes());
                    p1.PutBytes(System.Text.Encoding.ASCII.GetBytes(bufstring.ToLower()), 13);
                    p1.PutUInt(0x100, 269);
                    bufstring = Conversions.bytes2HexString(Crypt.GetKeyExchangeBytes());
                    p1.PutBytes(System.Text.Encoding.ASCII.GetBytes(bufstring), 273);
                    SendPacket(p1, true, true);
                    try
                    {
                        byte[] data = new byte[260];
                        int size;
                        if (sock.Available < 260)
                            size = sock.Available;
                        else
                            size = 260;

                        keyAlreadyReceived = size;
                        stream.BeginRead(data, 0, size, this.callbackKeyExchange, data);
                    }
                    catch (Exception)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                }
                else if (raw.Length == 260)
                {
                    Packet p1 = new Packet();
                    p1.data = raw;
                    byte[] keyBuf = p1.GetBytes(256, 4);
                    Crypt.MakeAESKey(System.Text.Encoding.ASCII.GetString(keyBuf));
                    StartPacketParsing();
                }
                else if (raw.Length == 529)
                {
                    Packet p1 = new Packet();
                    p1.data = raw;
                    byte[] keyBuf = p1.GetBytes(256, 273);
                    Crypt.MakePrivateKey();                    
                    Packet p2 = new Packet(260);
                    p2.PutUInt(0x100, 0);
                    string bufstring = Conversions.bytes2HexString(Crypt.GetKeyExchangeBytes());
                    p2.PutBytes(System.Text.Encoding.ASCII.GetBytes(bufstring), 4);
                    SendPacket(p2, true, true);
                    Crypt.MakeAESKey(System.Text.Encoding.ASCII.GetString(keyBuf));
                    StartPacketParsing();
                }
            }
            catch (Exception)
            {
                this.Disconnect();
                //Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            try
            {
                
                if (this.isDisconnected)
                {
                    return;
                }
                this.isDisconnected = true;
                try
                {
                    if (!disconnecting)
                        this.client.OnDisconnect();                   
                }
                catch (Exception e) { Logger.ShowError(e, null); }
                disconnecting = true;
                try
                {
                    Logger.ShowInfo(sock.RemoteEndPoint.ToString() + " disconnected", null);
                }
                catch (Exception)
                {
                }
                try { stream.Close(); }
                catch (Exception) { }

                try { sock.Close(); }
                catch (Exception) { }
            }
            catch (Exception e)
            {
                Logger.ShowError(e, null); 
                try { stream.Close(); }
                catch (Exception) { }

                //try { sock.Disconnect(true); }
                try { sock.Close(); }
                catch (Exception) { }
                //Logger.ShowInfo(sock.RemoteEndPoint.ToString() + " disconnected", null);
            }
            //this.nlock.ReleaseWriterLock(); 
        }


        private void ReceiveSize(IAsyncResult ar)
        {
            try
            {
                if (this.isDisconnected)
                {
                    return;
                }
                
                if (buffer[0] == 0xFF && buffer[1] == 0xFF & buffer[2] == 0xFF && buffer[3] == 0xFF)
                {
                    // if the buffer is marked as "empty", there was an error during reading
                    // normally happens if the client disconnects
                    // note: this is required as sock.Connected still can be true, even the client
                    // is already disconnected
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();   
                    return;
                }
                /*if (!sock.Connected)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();   
                    return;
                }*/

                try { stream.EndRead(ar); }
                catch (Exception)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea(); 
                    return;
                }
                Array.Reverse(buffer);
                uint size = BitConverter.ToUInt32(buffer, 0) + 4;
                

                if (size < 4)
                {
                   Logger.ShowWarning(sock.RemoteEndPoint.ToString() + " error: packet size is < 4",null);
                   return;
                }

                /*while (sock.Available+4 < size)
                {
                    //Logger.ShowWarning(sock.RemoteEndPoint.ToString() + string.Format(" error: packet data is too short, should be {0:G}", size - 2), null);
                    Thread.Sleep(100);
                }*/

                byte[] data = new byte[size + 4];
                
                // mark buffer as "empty"
                buffer[0] = 0xFF;
                buffer[1] = 0xFF;
                buffer[2] = 0xFF;
                buffer[3] = 0xFF;

                lastSize = (int)size;
                if (sock.Available < lastSize) 
                    size = (uint)sock.Available;
                else
                    size = (uint)lastSize;
                if (size > 1024)
                {
                    size = 1024;
                    alreadyReceived = 1024;
                }
                else
                {
                    alreadyReceived = (int)size;
                }
                // Receive the data from the packet and call the receivedata function
                // The packet is stored in AsyncState
                //Console.WriteLine("New packet with size " + p.size);
                try
                {
                    stream.BeginRead(data, 4, (int)(size), this.callbackData, data);
                }
                catch (Exception)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
            }
                        
            catch (Exception e) { Logger.ShowError(e, null); }
            
        }

        /// <summary>
        /// 分析封包
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                if (this.isDisconnected)
                {
                    return;
                }
                /*if (!sock.Connected)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }*/
                try { stream.EndRead(ar); }
                catch (Exception)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                byte[] raw = (byte[])ar.AsyncState;
                if (alreadyReceived < lastSize && lastSize > 0)
                {
                    int left = lastSize - alreadyReceived;
                    if (left > 1024)
                        left = 1024;
                    if (sock.Available == 0)
                    {
                        waitCounter = 0;
                        while (sock.Available == 0)
                        {
                            if (waitCounter > 300)
                            {
                                Logger.ShowWarning("Receive Timeout for client:" + client.ToString());
                                ClientManager.EnterCriticalArea();
                                this.Disconnect();
                                ClientManager.LeaveCriticalArea();
                                return;
                            }
                            Thread.Sleep(100);
                            waitCounter++;
                        }
                    }
                    if (left > sock.Available) left = sock.Available;
                    alreadyReceived += left; 
                    try
                    {
                        stream.BeginRead(raw, 4 + alreadyReceived - left, left, this.callbackData, raw);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    return;
                }
                Packet a = new Packet();
                a.data = raw;
                raw = Crypt.Decrypt(raw, 8);
                DateTime now = DateTime.Now;

                receivedBytes += raw.Length;
                if ((now - receiveStamp).TotalSeconds > 10)
                {
                    avarageReceive = (int)(receivedBytes / (now - receiveStamp).TotalSeconds);
                    receivedBytes = 0;
                    receiveStamp = now;
                }

                Packet p = new Packet();
                p.data = raw;
                uint length = p.GetUInt(4);
                uint offset = 0;
                if (length > 0 && length < 1024000)
                {
                    while (offset < length)
                    {
                        uint size;
                        if (restBuffer != null && restBufferLength > 0)
                        {
                            Packet p3 = new Packet((uint)(restBufferLength + restBuffer.Length));
                            p3.PutBytes(restBuffer, 0);
                            p3.PutBytes(p.GetBytes((ushort)restBufferLength, (ushort)(8 + offset)));
                            offset += (uint)restBufferLength;
                            restBuffer = null;
                            restBufferLength = 0;
                            ProcessPacket(p3);
                        }
                        if (firstLevelLenth == 4)
                            size = p.GetUInt((ushort)(8 + offset));
                        else
                            size = p.GetUShort((ushort)(8 + offset));

                        offset += firstLevelLenth;
                        if (size + offset > length)
                        {
                            restBuffer = new byte[length - offset];
                            Array.Copy(p.data, offset + 8, restBuffer, 0, restBuffer.Length);
                            restBufferLength = (int)(size - restBuffer.Length);
                            break;
                        }
                        Packet p2 = new Packet();
                        p2.data = p.GetBytes((ushort)size, (ushort)(8 + offset));
                        offset += size;
                        ProcessPacket(p2);
                    }
                }
                try
                {
                    stream.BeginRead(buffer, 0, 4, this.callbackSize, null);
                }
                catch (Exception)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
            }
            catch (Exception e)
            {
                Logger.ShowError(e, null);
            }
           
        }

        /// <summary>
        /// 当拆分完封包后呼叫，调用此方法后将自动根据封包分派函数交由事先指定的处理函数处理该封包
        /// </summary>
        /// <param name="p">需要处理的封包</param>
        private void ProcessPacket(Packet p)
        {
            if (p.data.Length < 2) return;
            ClientManager.AddThread(string.Format("PacketParser({0}),Opcode:0x{1:X4}", Thread.CurrentThread.ManagedThreadId, p.ID), Thread.CurrentThread);
            Packet command;
            commandTable.TryGetValue(p.ID, out command);
            if (command != null)
            {
                Packet p1 = command.New();
                p1.data = p.data;
                p1.size = (ushort)(p.data.Length);
                ClientManager.EnterCriticalArea();
                try
                {
                    p1.Parse(this.client);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
                if (OnSendPacket != null)
                    OnReceivePacket(p1);             
            }
            else
            {
                if (commandTable.ContainsKey(0xFFFF))
                {
                    Packet p1 = commandTable[0xFFFF].New();
                    p1.data = p.data;
                    p1.size = (ushort)(p.data.Length);
                    ClientManager.EnterCriticalArea();
                    try
                    {
                        p1.Parse(this.client);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    ClientManager.LeaveCriticalArea();
                }
                else
                {
                    Logger.ShowDebug(string.Format("Unknown Packet:0x{0:X4}\r\n       Data:{1}", p.ID, DumpData(p)), Logger.CurrentLogger);
                }
            }
            ClientManager.RemoveThread(string.Format("PacketParser({0}),Opcode:0x{1:X4}", Thread.CurrentThread.ManagedThreadId, p.ID));
        }

        public string DumpData(Packet p)
        {
            string tmp2 = "";
            for (int i = 0; i < p.data.Length; i++)
            {
                tmp2 += (String.Format("{0:X2} ", p.data[i]));
                if (((i + 1) % 16 == 0) && (i != 0))
                {
                    tmp2 += "\r\n            ";
                }
            }
            return tmp2;
        }

        /// <summary>
        /// 发送封包
        /// </summary>
        /// <param name="p">需要发送的封包</param>
        /// <param name="nolength">不知道是干嘛的233</param>
        /// <param name="noWarper">是否不需要封装封包头，仅用于交换密钥，其余时候不建议使用</param>
        public void SendPacket(Packet p, bool nolength, bool noWarper)
        {
            //if (sendCounter >= 50)
            //    Logger.ShowDebug("Recurssion over 50 times!", Logger.defaultlogger);
            //Debug.Assert(sendCounter < 50, "Recurssion over 50 times!");
            //sendCounter++;
            if (isDisconnected)
                return;
            if (OnSendPacket != null)
                OnSendPacket(p);
            if (!noWarper)
            {
                byte[] buf = new byte[p.data.Length + firstLevelLenth];
                Array.Copy(p.data, 0, buf, firstLevelLenth, p.data.Length);
                p.data = buf;
                if (firstLevelLenth == 4)
                    p.SetLength();
                else
                    p.PutUShort((ushort)(p.data.Length - 2), 0);
                buf = new byte[p.data.Length + 4];
                Array.Copy(p.data, 0, buf, 4, p.data.Length);
                p.data = buf;
                p.SetLength();
                buf = new byte[p.data.Length + 4];
                Array.Copy(p.data, 0, buf, 4, p.data.Length);
                p.data = buf;                
            }
            if (!nolength)
            {
                int mod = 16-((p.data.Length - 8) % 16);
                if (mod != 0)
                {
                    byte[] buf = new byte[p.data.Length + mod];
                    Array.Copy(p.data, 0, buf, 0, p.data.Length);
                    p.data = buf;                    
                }
                p.PutUInt((uint)(p.data.Length - 8), 0);
            }

            sentBytes += p.data.Length;
            DateTime now = DateTime.Now;
            if ((now - sendStamp).TotalSeconds > 10)
            {
                avarageSend = (int)(sentBytes / (now - sendStamp).TotalSeconds);
                sentBytes = 0;
                sendStamp = now;
            }

            try
            {
                byte[] data;
                data = Crypt.Encrypt(p.data, 8);
                stream.BeginWrite(data, 0, data.Length, this.callbackSend, null);
            }
            catch (Exception ex)
            {
                if (this.client != null)
                {
                    Logger.ShowError(ex);
                    this.Disconnect();
                    this.client = null;
                }
            }
            //sendCounter--;
        }

        void OnSent(IAsyncResult ar)
        {
            try
            {
                stream.EndWrite(ar);
            }
            catch
            {
            }
        }

        public void SendPacket(Packet p, bool noWarper)
        {
            try
            {
                SendPacket(p, false, noWarper);
            }
            catch(Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void SendPacket(Packet p)
        {
            SendPacket(p, false);
        }
    }
}


