//Comment this out to deactivate the dead lock check!
//#define DeadLockCheck

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;


namespace SagaLogin.Manager
{
    public sealed class LoginClientManager : ClientManager
    {
        List<LoginClient> clients;
        public Thread check;
        LoginClientManager()
        {
            /*
            this.clients = new Dictionary<uint, GatewayClient>();
            this.commandTable = new Dictionary<ushort, Packet>();

            //here for packets
            this.commandTable.Add(0x0101, new Packets.Client.SendKey());
            this.commandTable.Add(0x0102, new Packets.Client.SendGUID());
            this.commandTable.Add(0x0104, new Packets.Client.SendIdentify());
            this.commandTable.Add(0x0105, new Packets.Client.RequestSession());


            
            */
            this.clients = new List<LoginClient>();
            this.commandTable = new Dictionary<ushort, Packet>();

            commandTable.Add(0xDDDF, new Packets.Client.TOOL_GIFTS());


            this.commandTable.Add(0x0001, new Packets.Client.CSMG_SEND_VERSION());
            this.commandTable.Add(0x000A, new Packets.Client.CSMG_PING());
            this.commandTable.Add(0x002A, new Packets.Client.CSMG_CHAR_STATUS());
            this.commandTable.Add(0x00A0, new Packets.Client.CSMG_CHAR_CREATE());
            this.commandTable.Add(0x00A5, new Packets.Client.CSMG_CHAR_DELETE());
            this.commandTable.Add(0x00A7, new Packets.Client.CSMG_CHAR_SELECT());
            this.commandTable.Add(0x001F, new Packets.Client.CSMG_LOGIN());
            this.commandTable.Add(0x0032, new Packets.Client.CSMG_REQUEST_MAP_SERVER());
            this.commandTable.Add(0x00C9, new Packets.Client.CSMG_CHAT_WHISPER());
            this.commandTable.Add(0x00D2, new Packets.Client.CSMG_FRIEND_ADD());
            this.commandTable.Add(0x00D4, new Packets.Client.CSMG_FRIEND_ADD_REPLY());
            this.commandTable.Add(0x00D7, new Packets.Client.CSMG_FRIEND_DELETE());
            this.commandTable.Add(0x00E1, new Packets.Client.CSMG_FRIEND_DETAIL_UPDATE());
            this.commandTable.Add(0x00E6, new Packets.Client.CSMG_FRIEND_MAP_UPDATE());
            this.commandTable.Add(0x0104, new Packets.Client.CSMG_RING_EMBLEM_NEW());
            this.commandTable.Add(0x0109, new Packets.Client.CSMG_RING_EMBLEM());
            //this.commandTable.Add(0x015F, new Packets.Client.CSMG_SEND_GUID());
            this.commandTable.Add(0x0172, new Packets.Client.CSMG_WRP_REQUEST());
            
            this.commandTable.Add(0xFFF0, new Packets.Map.INTERN_LOGIN_REGISTER());
            this.commandTable.Add(0xFFF1, new Packets.Map.INTERN_LOGIN_REQUEST_CONFIG());

            this.commandTable.Add(0x0151, new Packets.Client.CSMG_NYASHIELD_VERSION());

            this.commandTable.Add(0x0226, new Packets.Client.CSMG_TAMAIRE_LIST_REQUEST());

            this.waitressQueue = new AutoResetEvent(true);
            //deadlock check
            check = new Thread(new ThreadStart(this.checkCriticalArea));
            check.Name = string.Format("DeadLock checker({0})", check.ManagedThreadId);
#if DeadLockCheck
            check.Start();
#endif
        }

        public static LoginClientManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly LoginClientManager instance = new LoginClientManager();
        }

        /// <summary>
        /// 全部在线客户端，包括Map服务器
        /// </summary>
        public List<LoginClient> Clients { get { return this.clients; } }

        /// <summary>
        /// Connects new clients
        /// </summary>
        public override void NetworkLoop(int maxNewConnections)
        {
            for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
            {
                Socket sock = listener.AcceptSocket();
                string ip = sock.RemoteEndPoint.ToString().Substring(0, sock.RemoteEndPoint.ToString().IndexOf(':'));
                Logger.ShowInfo("New client from: " + sock.RemoteEndPoint.ToString(), null);
                LoginClient client = new LoginClient(sock, this.commandTable);
                clients.Add(client);
            }
        }

        public override void OnClientDisconnect(Client client_t)
        {
            clients.Remove((LoginClient)client_t);
        }

        public LoginClient FindClient(SagaDB.Actor.ActorPC pc)
        {
            var chr =
                from c in this.clients
                where !c.IsMapServer && c.selectedChar != null
                select c;
            chr = from c in chr.ToList()
                  where c.selectedChar.CharID == pc.CharID
                  select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }

        public LoginClient FindClient(uint charID)
        {
            var chr =
                from c in this.clients
                where !c.IsMapServer && c.selectedChar != null
                select c;
            chr = from c in chr.ToList()
                  where c.selectedChar.CharID == charID
                  select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }

        public LoginClient FindClient(string charName)
        {
            var chr =
                from c in this.clients
                where !c.IsMapServer && c.selectedChar != null
                select c;
            chr = from c in chr.ToList()
                  where c.selectedChar.Name == charName
                  select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }

        public List<LoginClient> FindAllOnlineAccounts()
        {
            var chr =
              from c in clients
              where !c.IsMapServer && c.account != null
              select c;
            if (chr.Count() != 0)
                return chr.ToList();
            else
                return null;
        }

        public LoginClient FindClientAccountID(uint accountID)
        {
            var chr =
                from c in this.clients
                where !c.IsMapServer && c.account != null
                select c;
            chr = from c in chr.ToList()
                  where c.account.AccountID == accountID
                  select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }
        public LoginClient FindClientAccount(string accountName)
        {
            var chr =
                from c in this.clients
                where !c.IsMapServer && c.account != null
                select c;
            chr = from c in chr.ToList()
                  where c.account.Name == accountName
                  select c;
            if (chr.Count() != 0)
                return chr.First();
            else
                return null;
        }
    }
}
