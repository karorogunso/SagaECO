using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

using SagaLib;

namespace SagaProxy
{
    public class ProxyClient : Client
    {
        public List<Packet> packetContainer;
        public List<Packet> packetContainerServer;
        public List<Packet> packets;
        public ProxyServer.ServerType type;
        public ushort firstlevel;
        public byte ms = 0;

        public enum SESSION_STATE
        {
            LOGIN,MAP,REDIRECTING,DISCONNECTED
        }
        public SESSION_STATE state;
        public GameServerSession session;

        public ProxyClient(Socket mSock,Dictionary<ushort,Packet> mCommandTable,string ip,int port,List<Packet> clientp,List<Packet>serverp,List<Packet>p,ushort lvllen,ProxyServer.ServerType type)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this);
            this.netIO.FirstLevelLength = lvllen;
            this.firstlevel = lvllen;
            this.packetContainer = clientp;
            this.packetContainerServer = serverp;
            this.type = type;

            this.packets = p;
            session = new GameServerSession(ip, port,this, type);
            switch (this.type)
            {
                case ProxyServer.ServerType.Login:
                    MainWindow.Instance.CurrentLoginServer = session;
                    break;
                case ProxyServer.ServerType.Map:
                    MainWindow.Instance.CurrentMapServer = session;
                    break;
                case ProxyServer.ServerType.Validation:
                    MainWindow.Instance.CurrentValidationServer = session;
                    break;
            }
            this.netIO.SetMode(NetIO.Mode.Server);
            if (this.netIO.sock.Connected)
                this.OnConnect();
        }
        public override void OnConnect()
        {

        }

        public override void OnDisconnect()
        {
            try
            {
                if (this.state != SESSION_STATE.DISCONNECTED)
                {
                    this.state = SESSION_STATE.DISCONNECTED;
                    session.netIO.Disconnect();
                }

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
        }
        public void OnRedirectUniversal(Packets.Client.SendUniversal p)
        {
            try
            {
                waitUntilReady();
                Packets.Client.RedirectUniversal p1 = new SagaProxy.Packets.Client.RedirectUniversal();
                p1.data = new byte[p.data.Length];
                p.data.CopyTo(p1.data, 0);
                this.packets.Add(p1);
                OnRedirectPacket(p);
                /*
                MainWindow.Instance.(new Action(() => { frmMain.Instance.PacketsList.Items.Add(string.Format("0x{0:X4},{1},Client,{2},{3}", p.ID, p.data.Length, this.packetContainer.IndexOf(p1), ms)); }));
                if (frmMain.Instance.autofollow.Checked)
                    frmMain.Instance.Invoke(new Action(() => { frmMain.Instance.PacketsList.TopIndex = frmMain.Instance.PacketsList.Items.Count - 1; }));
               */
                session.netIO.SendPacket(p1);
            }
            catch (Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() =>MainWindow.Instance.Message.AppendText(ex.ToString()));
            }
        }
        public void OnRedirectVersion(Packets.Client.SendVersion p)
        {
            waitUntilReady();
            if (!ProxyServer.Instance.VersionRecorded)
            {
                Packets.Client.RedirectVersion p1 = new SagaProxy.Packets.Client.RedirectVersion();
                p.data.CopyTo(p1.data, 0);
                this.packets.Add(p1);
                OnRedirectPacket(p);
                session.netIO.SendPacket(p1);
                ProxyServer.Instance.VersionPacket = p;
                //ProxyServer.Instance.VersionRecorded = true;
            }
            else
            {
                Packets.Client.RedirectVersion p1 = new SagaProxy.Packets.Client.RedirectVersion();
                ProxyServer.Instance.VersionPacket.data.CopyTo(p1.data, 0);
                this.packets.Add(p1);
                OnRedirectPacket(p);
                session.netIO.SendPacket(p1);
            }
        }
        public void OnRedirectPacket(Packet p)
        {
            this.packetContainer.Add(p);
            PacketInfo pi = new PacketInfo("Client", this.type.ToString(), this.packetContainerServer.Count, p.ID, p.data.Length, p.DumpData().Replace("\n", ""));

            MainWindow.Instance.Dispatcher.Invoke(() =>
            {
                MainWindow.Instance.PacketList.Add(pi);
                MainWindow.Instance.UpdateGrid();
            });
        }
        void waitUntilReady()
        {
            try
            {
                ClientManager.LeaveCriticalArea();
                while (!session.netIO.Crypt.IsReady)
                {
                    System.Threading.Thread.Sleep(100);
                }
                ClientManager.EnterCriticalArea();
            }
            catch (Exception ex)
            {
                MainWindow.Instance.Dispatcher.Invoke(() => MainWindow.Instance.Message.AppendText(ex.ToString()));
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
                    tmp2 += "\r\n";
                }
            }
            return tmp2;
        }
    }
}
