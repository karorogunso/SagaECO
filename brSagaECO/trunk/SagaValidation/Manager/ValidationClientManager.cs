using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading;

using SagaLib;
using SagaValidation;
using SagaValidation.Network.Client;

namespace SagaValidation.Manager
{
    public sealed class ValidationClientManager : ClientManager
    {
        List<ValidationClient> clients;
        public Thread check;
        ValidationClientManager()
        {
            this.clients = new List<ValidationClient>();
            this.commandTable = new Dictionary<ushort, Packet>();
            this.commandTable.Add(0x0001, new Packets.Client.CSMG_SEND_VERSION());
            this.commandTable.Add(0x001F, new Packets.Client.CSMG_LOGIN());
            this.commandTable.Add(0x002F, new Packets.Client.CSMG_LOGIN_REQUEST_CONFIRM());
            this.commandTable.Add(0x0031, new Packets.Client.CSMG_SERVERLET_ASK());

            this.commandTable.Add(0x000A, new Packets.Client.CSMG_PING());
            this.waitressQueue = new AutoResetEvent(true);
        }
        public static ValidationClientManager Instance
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

            internal static readonly ValidationClientManager instance = new ValidationClientManager();
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

                ValidationClient client = new ValidationClient(sock, this.commandTable);
                clients.Add(client);
            }
        }
    }

}
