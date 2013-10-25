using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

using SagaLib;

namespace PacketProxy
{
    public class ServerSession : Client
    {
        public enum SESSION_STATE
        {
            CONNECTED,DISCONNECTED
        }
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED.
        /// </summary>
        public SESSION_STATE state;
        public ProxyClient Client;
        private Socket sock;
        private string host;
        private int port;
        
        Dictionary<ushort, Packet> commandTable;

        public ServerSession(string host, int port, ProxyClient client)
        {
            this.commandTable = new Dictionary<ushort, Packet>();

            this.commandTable.Add(0xFFFF, new Packets.Server.SendUniversal());

            this.Client = client;

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sock = newSock;
            this.host = host;
            this.port = port;
            this.Connect();
        }

        public void Connect()
        {
            bool Connected = false;
            int times = 5;
            do
            {
                if (times < 0)
                {
                    Logger.ShowError("Cannot connect to the server,please check the configuration!", null);
                    return;
                }
                try
                {
                    sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(host), port));
                    Connected = true;
                }
                catch (Exception e)
                {
                    Logger.ShowError("Failed... Trying again in 5sec", null);
                    Logger.ShowError(e.ToString(), null);
                    System.Threading.Thread.Sleep(5000);
                    Connected = false;
                }
                times--;
            } while (!Connected);

            Logger.ShowInfo("Successfully connected to server", null);
            this.state = SESSION_STATE.CONNECTED;
            try
            {
                this.netIO = new NetIO(sock, this.commandTable, this, ProxyClientManager.Instance);
                this.netIO.SetMode(NetIO.Mode.Client);
                this.netIO.FirstLevelLength = this.Client.firstlevel;
                Packet p = new Packet(8);
                p.data[7] = 0x10;
                this.netIO.SendPacket(p, true, true);
                
            }
            catch (Exception ex)
            {
                Logger.ShowWarning(ex.StackTrace, null);
            }
        }


        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {
            if (this.state != SESSION_STATE.DISCONNECTED)
            {
                this.state = SESSION_STATE.DISCONNECTED;
                this.Client.netIO.Disconnect();
            }
        }      

        public void OnSendUniversal(Packets.Server.SendUniversal p)
        {
            ClientManager.LeaveCriticalArea();
            while (!Client.netIO.Crypt.IsReady)
            {
                System.Threading.Thread.Sleep(100);
            }
            ClientManager.EnterCriticalArea();
            string tmp = "Sender:{0}\r\nOpcode:0x{1:X4}\r\nName:{2}\r\n\r\n{5}\r\n\r\nLength:{3}\r\nData:\r\n{4}\r\n";
            string tmp2 = this.DumpData(p);
            tmp = string.Format(tmp, "Server" , p.ID, this.ToString(), p.data.Length, tmp2, "{0}");
            Console.WriteLine(tmp);
            Packets.Server.SendUniversal p1 = new PacketProxy.Packets.Server.SendUniversal();
            p1.data = new byte[p.data.Length];
            p.data.CopyTo(p1.data, 0);
            this.Client.packetContainerServer.Add(p1);
            this.Client.packets.Add(p1);
            if (p.ID == 0x33 && this.Client.firstlevel != 2)
            {
                byte serverid = p.GetByte(2);
                byte size = p.GetByte(3);
                size--;
                string ip = System.Text.Encoding.ASCII.GetString(p.GetBytes(size, 4));
                int port = p.GetInt((ushort)(5 + size));
                if (Program.pm != null)
                {
                    Program.pm.Stop();
                }
                Program.pm = new ProxyClientManager();
                Program.pm.firstlvlen = 2;
                Program.pm.IP = ip;
                Program.pm.port = port;
                Program.pm.packetContainerServer = PacketContainer.Instance.packetsMap;
                Program.pm.packetContainerClient = PacketContainer.Instance.packetsClient;
                Program.pm.packets = PacketContainer.Instance.packets2;
                Program.pm.StartNetwork(port);
                Program.pm.ready = true;
                byte[] buf = System.Text.Encoding.UTF8.GetBytes("127.0.0.001");
                p.data = new byte[buf.Length + 9];
                p.ID = 0x33;
                p.PutByte(serverid, 2);
                p.PutByte((byte)(buf.Length + 1), 3);
                p.PutBytes(buf, 4);
                p.PutInt(port, (ushort)(5 + buf.Length));
            }
            if (p.ID == 0x157C)
            {

            }
            
            Client.netIO.SendPacket(p);
        }

        public string DumpData(Packet p)
        {
            string tmp2 = "";
            for (int i = 0; i < p.data.Length; i++)
            {
                tmp2 += (String.Format("{0:X2} ", p.data[i]));
                if (((i + 1) % 16 == 0) && (i != 0))
                {
                    tmp2 += "\r\n";
                }
            }
            return tmp2;
        }
       
    }
}
