//Comment this out to deactivate the dead lock check!
#define DeadLockCheck

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;


namespace SagaMap.Manager
{
    public sealed class MapClientManager : ClientManager
    {
        List<MapClient> clients;
        public Thread check;
        MapClientManager()
        {
           
            this.clients = new List<MapClient>();
            this.commandTable = new Dictionary<ushort, Packet>();

            this.commandTable.Add(0x000A, new Packets.Client.CSMG_SEND_VERSION());
            this.commandTable.Add(0x0010, new Packets.Client.CSMG_LOGIN());
            this.commandTable.Add(0x001F, new Packets.Client.CSMG_LOGOUT());
            this.commandTable.Add(0x0032, new Packets.Client.CSMG_PING());
            this.commandTable.Add(0x01FD, new Packets.Client.CSMG_CHAR_SLOT());
            this.commandTable.Add(0x020D, new Packets.Client.CSMG_ACTOR_REQUEST_PC_INFO());
            this.commandTable.Add(0x03E8, new Packets.Client.CSMG_CHAT_PUBLIC());
            this.commandTable.Add(0x07D0, new Packets.Client.CSMG_ITEM_DROP());
            this.commandTable.Add(0x07E4, new Packets.Client.CSMG_ITEM_GET());
            this.commandTable.Add(0x09E7, new Packets.Client.CSMG_ITEM_EQUIPT());
            this.commandTable.Add(0x11F8, new Packets.Client.CSMG_PLAYER_MOVE());
            this.commandTable.Add(0x11FE, new Packets.Client.CSMG_PLAYER_MAP_LOADED());
            this.commandTable.Add(0x1216, new Packets.Client.CSMG_CHAT_EMOTION());
            this.commandTable.Add(0x121B, new Packets.Client.CSMG_CHAT_MOTION());
            this.commandTable.Add(0x13BA, new Packets.Client.CSMG_CHAT_SIT());


            this.waitressQueue = new AutoResetEvent(false);
            this.waitressHasFinished = new ManualResetEvent(false);
            this.waitingWaitressesCount = 0;
            this.waitressCountLock = new Object();

            this.packetCoordinator = new Thread(new ThreadStart(this.packetCoordinationLoop));
            this.packetCoordinator.Start();

            //deadlock check
            check = new Thread(new ThreadStart(this.checkCriticalArea));
#if DeadLockCheck
            check.Start();
#endif
        }

        public static MapClientManager Instance
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

            internal static readonly MapClientManager instance = new MapClientManager();
        }


        /// <summary>
        /// Connects new clients
        /// </summary>
        public override void NetworkLoop(int maxNewConnections)
        {
            for (int i = 0; listener.Pending() && i < maxNewConnections; i++)
            {
                Socket sock = listener.AcceptSocket();
                string ip = sock.RemoteEndPoint.ToString().Substring(0, sock.RemoteEndPoint.ToString().IndexOf(':'));
                Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.NEW_CLIENT, sock.RemoteEndPoint.ToString()), null);
                MapClient client = new MapClient(sock, this.commandTable);
                clients.Add(client);
            }
        }

        public override void OnClientDisconnect(Client client_t)
        {
            clients.Remove((MapClient)client_t);
        }

    }
}
