using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SagaLib;
using System.Text.RegularExpressions;

namespace SagaProxy
{
    public class GameServerSession :Client
    {
        public enum SESSION_STATE
        {
            CONNECTED,DISCONNECTED
        }
        public SESSION_STATE state;
        public ProxyClient Client;
        private Socket sock;
        private string host;
        private int port;
        ProxyServer.ServerType type;
        bool redirected;

        Dictionary<ushort, Packet> commandTable;

        public GameServerSession(string host, int port, ProxyClient client, ProxyServer.ServerType type)
        {
            this.commandTable = new Dictionary<ushort, Packet>();
            this.commandTable.Add(0x0033, new Packets.Server.SendServer());
            this.commandTable.Add(0xFFFF, new Packets.Server.SendUniversal());
            this.Client = client;

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sock = newSock;
            this.host = host;
            this.type = type;
            this.port = port;
            this.Connect();
        }
        public void Connect()
        {
            bool Connected = false;
            int times = 5;
            while (!Connected)
            {
                if (times < 0)
                {
                    MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText("\r\nCannot connect to the game server, please check the configuration!"));
                    return;
                }
                try
                {
                    sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(host), port));
                    Connected = true;
                }
                catch(Exception ex)
                {
                    MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
                    MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText("\r\nFailed... Trying again in 5 sec"));
                    System.Threading.Thread.Sleep(5000);
                    Connected = false;
                }
                times--;
            }

            MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText($"\r\nSuccessfully connected to {this.type} Server"));

            this.state = SESSION_STATE.CONNECTED;
            try
            {
                this.netIO = new NetIO(sock, this.commandTable, this);
                this.netIO.SetMode(NetIO.Mode.Client);
                this.netIO.FirstLevelLength = this.Client.firstlevel;
                Packet p = new Packet(8);
                p.data[7] = 0x10;
                this.netIO.SendPacket(p, true, true);
            }
            catch(Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
            }
        }
        public void OnRedirectUniversal(Packets.Server.SendUniversal p)
        {
            OnRedirectUniversal(p, false);
        }
        public void OnRedirectUniversal(Packets.Server.SendUniversal p,bool s)
        {
            try
            {
                waitUntilReady();
                Packets.Server.RedirectUniversal p1 = new SagaProxy.Packets.Server.RedirectUniversal();
                p1.data = new byte[p.data.Length];
                p.data.CopyTo(p1.data, 0);
                OnRedirectPacket(p);

               /* if(p1.ID == 0x34)
                    MainWindow.Instance.LoggedIn = true;*/
                Client.netIO.SendPacket(p1);
            }
            catch (Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
            }
        }

        public void OnRedirectServer(Packets.Server.SendServer p)
        {
            waitUntilReady();
            Packets.Server.RedirectServer p1 = new Packets.Server.RedirectServer();
            if (this.type == ProxyServer.ServerType.Validation)
            {
                if (redirected)
                    return;
                
                string fullString = Encoding.UTF8.GetString(p.GetBytes((ushort)p.size, 0));
                byte size = p.GetByte(2);
                size--;
                p.offset = 3;
                string name = Encoding.UTF8.GetString(p.GetBytes(size, p.offset));
                byte ipindex = (byte)(size + 4);
                string fullIP = Encoding.UTF8.GetString(p.GetBytes(p.GetByte(ipindex), (ushort)(ipindex) ));
                fullIP=Regex.Replace(fullIP, "[a-zA-z ]", "");
                string ip = fullIP.Substring(0, fullIP.IndexOf(":"));
                int port = int.Parse(fullIP.Substring(fullIP.IndexOf(":") + 1, fullIP.IndexOf(",") - fullIP.IndexOf(":") - 1));

                ProxyClientManager.LoginInstance.IP = ip;
                ProxyClientManager.LoginInstance.port = port;
                int clientport = ((IPEndPoint)ProxyClientManager.LoginInstance.listener.LocalEndpoint).Port;

                string targetName = MainWindow.Instance.TargetServerName;
                if (targetName == String.Empty || targetName == name)
                {
                    p1.SevName = name;
                    p1.SevIP = "T" + "127.0.0.1:" + clientport + "," + "127.0.0.1:" + clientport + "," + "127.0.0.1:" + clientport + "," + "127.0.0.1:" + clientport;
                    redirected = true;
                    OnRedirectPacket(p);
                    Client.netIO.SendPacket(p1);
                }
            }
            else if(this.type == ProxyServer.ServerType.Login)
            {
                    byte serverid = p.GetByte(2);
                    byte size = p.GetByte(3);
                    size--;
                    string ip = System.Text.Encoding.ASCII.GetString(p.GetBytes(size, 4));
                    int port = p.GetInt((ushort)(5 + size));
                    ProxyClientManager.MapInstance.IP = ip;
                    ProxyClientManager.MapInstance.port = port;
                    int localport = ((IPEndPoint)ProxyClientManager.MapInstance.listener.LocalEndpoint).Port;

                    p1.ServerID = serverid;
                    p1.IP = "127.0.0.001";
                p1.Port = localport;
                OnRedirectPacket(p);
                Client.netIO.SendPacket(p1);
            }
            else if (this.type == ProxyServer.ServerType.Map)
            {
                Packets.Server.SendUniversal p2 = new Packets.Server.SendUniversal();
                p2.data = new byte[p.data.Length];
                p.data.CopyTo(p2.data, 0);
                OnRedirectUniversal(p2);
            }
        }

        void waitUntilReady()
        {
            try
            {
                ClientManager.LeaveCriticalArea();
                while (!Client.netIO.Crypt.IsReady)
                {
                    System.Threading.Thread.Sleep(100);
                }
                ClientManager.EnterCriticalArea();

                if (!PacketContainer.Instance.aesKey.Contains(Client.netIO.Crypt.AESKey))
                    PacketContainer.Instance.aesKey.Add(Client.netIO.Crypt.AESKey);
            }
            catch (Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
            }
        }

        public void OnRedirectPacket(Packet p)
        {
            this.Client.packetContainerServer.Add(p);
            
            PacketInfo pi = new PacketInfo("Server", this.type.ToString(), this.Client.packetContainerServer.Count, p.ID, p.data.Length, p.DumpData().Replace("\n", ""));

            MainWindow.Instance.Dispatcher.Invoke(() =>
            {
                MainWindow.Instance.PacketList.Add(pi);
                MainWindow.Instance.UpdateGrid();
            });

            /*
            if (frmMain.Instance.checkBox3.Checked)
                System.Threading.Thread.Sleep(300);
            if (frmMain.Instance.nextproxy == false)
            {
                while (frmMain.Instance.checkBox4.Checked)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            if (frmMain.Instance.nextproxy == true)
                frmMain.Instance.nextproxy = false;
            */
        }
        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {
            if (this.state != SESSION_STATE.DISCONNECTED)
            {
                this.state = SESSION_STATE.DISCONNECTED;
                //this.Client.netIO.Disconnect();
            }
        } 
    }
}
