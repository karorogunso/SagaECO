//Comment this out to deactivate the dead lock check!
//#define DeadLockCheck

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SagaLib;
using SagaWorld;
using SagaWorld.Network.Client;


namespace SagaWorld.Manager
{
    public sealed class WorldClientManager : ClientManager
    {
        List<WorldClient> clients;
        public Thread check;
        WorldClientManager()
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
            this.clients = new List<WorldClient>();
            this.commandTable = new Dictionary<ushort, Packet>();

            this.commandTable.Add(0x0001, new Packets.Client.CSMG_SEND_VERSION());
            this.commandTable.Add(0x000A, new Packets.Client.CSMG_PING());
            this.commandTable.Add(0x001F, new Packets.Client.CSMG_LOGIN());
            this.commandTable.Add(0x002A, new Packets.Client.CSMG_CHAR_STATUS());
            this.commandTable.Add(0x0032, new Packets.Client.CSMG_REQUEST_MAP_SERVER());
            this.commandTable.Add(0x00A0, new Packets.Client.CSMG_CHAR_CREATE());
            this.commandTable.Add(0x00A5, new Packets.Client.CSMG_CHAR_DELETE());
            this.commandTable.Add(0x00A7, new Packets.Client.CSMG_CHAR_SELECT());
            //this.commandTable.Add(0x00C9, new Packets.Client.WISPER());


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

        public static WorldClientManager Instance
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

            internal static readonly WorldClientManager instance = new WorldClientManager();
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
                Logger.ShowInfo("New client from: " + sock.RemoteEndPoint.ToString(), null);
                WorldClient client = new WorldClient(sock, this.commandTable);
                clients.Add(client);
            }
        }

        public override void OnClientDisconnect(Client client_t)
        {
            clients.Remove((WorldClient)client_t);
        }

    }
}
