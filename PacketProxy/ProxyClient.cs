using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using SagaLib;

namespace PacketProxy
{
   public class ProxyClient : SagaLib.Client 
   {
       //public LoginSession LogingSession;
       public List<Packet> packetContainer;
       public List<Packet> packetContainerServer;
       public List<Packet> packets;
       public ushort firstlevel;
        public enum SESSION_STATE
        {
            LOGIN,MAP,REDIRECTING,DISCONNECTED
        }
        public SESSION_STATE state;

        public ServerSession session;

        public ProxyClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable, string ip, int port, List<Packet> clientp,List<Packet> serverp,List<Packet> p,ushort lv1len)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this, ProxyClientManager.Instance);
            this.netIO.FirstLevelLength = lv1len;
            this.firstlevel = lv1len;
            this.packetContainer = clientp;
            this.packetContainerServer = serverp;
            this.packets = p;
            session = new ServerSession(ip, port, this);
            //session = new ServerSession("203.174.58.144", 12000, this);
            this.netIO.SetMode(NetIO.Mode.Server);
            if (this.netIO.sock.Connected) this.OnConnect();
        }

       public override string ToString()
       {
           try
           {
               if (this.netIO != null) return this.netIO.sock.RemoteEndPoint.ToString();
               else
                   return "ProxyClient";
           }
           catch (Exception)
           {
               return "ProxyClient";
           }
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
            ClientManager.LeaveCriticalArea();
            while (!session.netIO.Crypt.IsReady)
            {
                System.Threading.Thread.Sleep(100);
            }
            ClientManager.EnterCriticalArea();
            Packets.Client.SendUniversal p1 = new PacketProxy.Packets.Client.SendUniversal();
            p1.data = new byte[p.data.Length];
            p.data.CopyTo(p1.data, 0);
            this.packetContainer.Add(p1);
            this.packets.Add(p1);
            string tmp = "Sender:{0}\r\nOpcode:0x{1:X4}\r\nName:{2}\r\n\r\n{5}\r\n\r\nLength:{3}\r\nData:\r\n{4}\r\n";
            string tmp2 = this.DumpData(p);
            tmp = string.Format(tmp, "Client" , p.ID, this.ToString(), p.data.Length, tmp2, "{0}");
            session.netIO.SendPacket(p);
            Console.WriteLine(tmp);
            
           
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
