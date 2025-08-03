using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using SagaLib;

namespace SagaMap.Network.LoginServer
{
    public class LoginSession : SagaLib.Client
    {
        public enum SESSION_STATE
        {
            CONNECTED, DISCONNECTED, NOT_IDENTIFIED, IDENTIFIED, REJECTED
        }
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED.
        /// </summary>
        public SESSION_STATE state = SESSION_STATE.CONNECTED;
        private Socket sock;       

        Dictionary<ushort, Packet> commandTable;

        public LoginSession()
        {
            this.commandTable = new Dictionary<ushort, Packet>();

            this.commandTable.Add(0xFFF2, new Packets.Login.INTERN_LOGIN_REQUEST_CONFIG_ANSWER());

            
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sock = newSock;
            this.Connect();
        }

        public void Connect()
        {
            var address = System.Net.Dns.GetHostAddresses(Configuration.Instance.LoginHost)[0];
            bool Connected = false;
            int times = 5;
            do
            {
                if (times < 0)
                {
                    Logger.ShowError("Cannot connect to the loginserver,please check the configuration!", null);
                    return;
                }
                try
                {
                    sock.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(address.ToString()), Configuration.Instance.LoginPort));
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

            Logger.ShowInfo("Successfully connected to the loginserver", null);
            this.state = SESSION_STATE.CONNECTED;
            try
            {
                this.netIO = new NetIO(sock, this.commandTable, this);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.netIO.FirstLevelLength = 2;
                this.netIO.SetMode(NetIO.Mode.Client);
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
            this.state = SESSION_STATE.NOT_IDENTIFIED;
            Packets.Login.INTERN_LOGIN_REGISTER p;
            int count = Configuration.Instance.HostedMaps.Count / 200;
            List<uint> list;
            for (int i = 0; i < count; i++)
            {
                p = new SagaMap.Packets.Login.INTERN_LOGIN_REGISTER();
                p.Password = Configuration.Instance.LoginPass;
                list = new List<uint>();
                for (int j = i * 200; j < (i + 1) * 200; j++)
                {
                    list.Add(Configuration.Instance.HostedMaps[j]);
                }
                p.HostedMaps = list;
                this.netIO.SendPacket(p);
            }
            p = new SagaMap.Packets.Login.INTERN_LOGIN_REGISTER();
            p.Password = Configuration.Instance.LoginPass;
            list = new List<uint>();
            for (int i = count * 200; i < Configuration.Instance.HostedMaps.Count; i++)
            {
                list.Add(Configuration.Instance.HostedMaps[i]);
            }
            p.HostedMaps = list;
            this.netIO.SendPacket(p);

            Packets.Login.INTERN_LOGIN_REQUEST_CONFIG p1 = new SagaMap.Packets.Login.INTERN_LOGIN_REQUEST_CONFIG();
            p1.Version = Configuration.Instance.Version;
            this.netIO.SendPacket(p1);
        }

        public void OnGetConfig(Packets.Login.INTERN_LOGIN_REQUEST_CONFIG_ANSWER p)
        {
            if (p.AuthOK)
            {
                Configuration.Instance.StartupSetting = p.StartupSetting;
                Logger.ShowInfo("Got Configuration from login server:");
                foreach (SagaDB.Actor.PC_RACE i in Configuration.Instance.StartupSetting.Keys)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[Info]");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Configuration for Race[");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(i.ToString());
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(":\r\n      " + Configuration.Instance.StartupSetting[i].ToString());
                    Console.ResetColor();
                }
                this.state = SESSION_STATE.IDENTIFIED;
            }
            else
            {
                Logger.ShowError("FATAL: Request Rejected from loginserver,terminating");
                this.state = SESSION_STATE.REJECTED;
            }
        }

        public override void OnDisconnect()
        {
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect();
        }
    }
}
