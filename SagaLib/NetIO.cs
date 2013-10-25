using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
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

        private AsyncCallback callbackSize;
        private AsyncCallback callbackData;
        private AsyncCallback callbackKeyExchange;
        
        public Socket sock;
        public Encryption Crypt;
        private NetworkStream stream;

        private Client client;

        private ushort firstLevelLenth = 4;

        private bool isDisconnected;
        private bool disconnecting;

        private int lastSize;
        private int alreadyReceived;

        private ClientManager currentClientManager;

        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type
        /// </summary>
        private Dictionary<ushort, Packet> commandTable;

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
        /// Create a new netIO class using a given socket.
        /// </summary>
        /// <param name="sock">The socket for this netIO class.</param>
        public NetIO(Socket sock, Dictionary<ushort, Packet> commandTable, Client client ,ClientManager manager)
        {
            this.sock = sock;
            this.stream = new NetworkStream(sock);
            this.commandTable = commandTable;
            this.client = client;
            this.currentClientManager = manager;
            Crypt = new Encryption();
                    

            this.callbackSize = new AsyncCallback(this.ReceiveSize);
            this.callbackData = new AsyncCallback(this.ReceiveData);
            this.callbackKeyExchange= new AsyncCallback(this.ReceiveKeyExchange);
            // Use the static key untill the keys have been exchanged
            

            this.isDisconnected = false;
            
        }

        private void StartPacketParsing()
        {
            if (sock.Connected)
            {
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

        public void SetMode(Mode mode)
        {
            byte[] data;
            switch (mode)
            {
                case Mode.Server :
                    try
                    {
                        data = new byte[8];
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
                        stream.BeginRead(data, 0, 529, this.callbackKeyExchange, data);
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
                        stream.BeginRead(data, 0, 260, this.callbackKeyExchange, data);
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
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// Disconnect the client
        /// </summary>
        public void Disconnect()
        {
            try
            {
                
                if (this.isDisconnected)
                {
                    return;
                }
                try
                {
                    if (!disconnecting)
                        this.client.OnDisconnect();
                    disconnecting = true;
                }
                catch (Exception e) { Logger.ShowError(e, null); }
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

                this.isDisconnected = true;

                
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
                if (sock.Available < lastSize) size = (uint)sock.Available;
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
                catch (Exception ex)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
            }
                        
            catch (Exception e) { Logger.ShowError(e, null); }
            
        }

        private void ReceiveData(IAsyncResult ar)
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
                if (alreadyReceived < lastSize)
                {
                    int left = lastSize - alreadyReceived;
                    if (left > 1024)
                        left = 1024;
                    if (left > sock.Available) left = sock.Available;
                    try
                    {
                        stream.BeginRead(raw, 4 + alreadyReceived, left, this.callbackData, raw);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    alreadyReceived += left;
                    return;
                }
                raw = Crypt.Decrypt(raw, 8);

                Packet p = new Packet();
                p.data = raw;
                uint length = p.GetUInt(4);
                uint offset = 0;
                while (offset < length)
                {
                    uint size;
                    if(firstLevelLenth ==4)
                        size= p.GetUInt((ushort)(8 + offset));
                    else
                        size = p.GetUShort((ushort)(8 + offset));
                    
                    offset += firstLevelLenth;
                    if (size + offset > length)
                        break;
                    Packet p2 = new Packet();
                    p2.data = p.GetBytes((ushort)size, (ushort)(8 + offset));
                    offset += size;
                    ProcessPacket(p2);
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

        private void ProcessPacket(Packet p)
        {
            if (commandTable.ContainsKey(p.ID))
            {
                Packet p1 = commandTable[p.ID].New();
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
        /// Sends a packet, which is not yet encrypted, to the client.
        /// </summary>
        /// <param name="p">The packet containing all info.</param>
        public void SendPacket(Packet p, bool nolength, bool noWarper)
        {
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

            try
            {
                byte[] data;
                data = Crypt.Encrypt(p.data, 8);
                sock.BeginSend(data, 0, data.Length, SocketFlags.None, null, null);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Disconnect();
            }

        }

        public void SendPacket(Packet p, bool noWarper)
        {
            SendPacket(p, false, noWarper);
        }

        public void SendPacket(Packet p)
        {
            SendPacket(p, false);
        }
    }
}


