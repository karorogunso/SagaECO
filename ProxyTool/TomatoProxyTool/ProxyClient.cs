using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

using SagaLib;

namespace TomatoProxyTool
{
    public class ProxyClient : Client
    {
        public List<Packet> packetContainer;
        public List<Packet> packetContainerServer;
        public List<Packet> packets;
        public ushort firstlevel;
        public byte ms = 0;

        public enum SESSION_STATE
        {
            LOGIN,MAP,REDIRECTING,DISCONNECTED
        }
        public SESSION_STATE state;
        public ServerSession session;

        public ProxyClient(Socket mSock,Dictionary<ushort,Packet> mCommandTable,string ip,int port,List<Packet> clientp,List<Packet>serverp,List<Packet>p,ushort lvllen,bool mapserver)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this);
            this.netIO.FirstLevelLength = lvllen;
            this.firstlevel = lvllen;
            this.packetContainer = clientp;
            this.packetContainerServer = serverp;

            this.packets = p;
            session = new ServerSession(ip, port,this, mapserver);
            Form1.client = session;
            if (mapserver) ms = 1; else ms = 0;
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
        public void OnSendUniversal(Packets.Client.SendUniversal p)
        {
            try
            {
                ClientManager.LeaveCriticalArea();
                while (!session.netIO.Crypt.IsReady)
                {
                    System.Threading.Thread.Sleep(100);
                }
                ClientManager.EnterCriticalArea();
                Packets.Client.SendUniversal p1 = new TomatoProxyTool.Packets.Client.SendUniversal();
                p1.data = new byte[p.data.Length];
                p.data.CopyTo(p1.data, 0);
                this.packetContainer.Add(p1);
                this.packets.Add(p1);
                /*string tmp = "Sender:{0}\r\nOpcode:0x{1:X4}\r\nName:{2}\r\n\r\n{5}\r\n\r\nLength:{3}\r\nData:\r\n{4}\r\n";
                string tmp2 = this.DumpData(p);
                tmp = string.Format(tmp, "Client", p.ID, this.ToString(), p.data.Length, tmp2, "{0}");*/

                Form1.Instance.Invoke(new Action(() => { Form1.Instance.PacketsList.Items.Add(string.Format("0x{0:X4},{1},Client,{2},{3}", p.ID, p.data.Length, this.packetContainer.IndexOf(p1), ms)); }));
                if (Form1.Instance.autofollow.Checked)
                    Form1.Instance.Invoke(new Action(() => { Form1.Instance.PacketsList.TopIndex = Form1.Instance.PacketsList.Items.Count - 1; }));

                session.netIO.SendPacket(p);
            }
            catch (Exception ex)
            {
                Form1.Instance.Invoke(new Action(() => { Form1.Instance.PacketInfoBox.Text += ex.ToString(); }));
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
