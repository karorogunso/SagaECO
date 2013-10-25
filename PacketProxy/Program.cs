using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace PacketProxy
{
    class Program
    {
        public static System.IO.StreamWriter sw;
        public static ProxyClientManager pm;
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);
            ProxyClientManager.Instance.IP = "203.174.58.144";
            //ProxyClientManager.Instance.IP = "203.174.58.142";
            ProxyClientManager.Instance.port = 12000;
            ProxyClientManager.Instance.Start();
            ProxyClientManager.Instance.StartNetwork(12000);

            Global.clientMananger = (ClientManager)ProxyClientManager.Instance;
            sw = new System.IO.StreamWriter("packetlog.txt", true);
            ProxyClientManager.Instance.packetContainerClient = PacketContainer.Instance.packetsClientLogin;
            ProxyClientManager.Instance.packetContainerServer = PacketContainer.Instance.packetsLogin;
            ProxyClientManager.Instance.packets = PacketContainer.Instance.packets;
            while (true)
            {
                // keep the connections to the database servers alive
                // let new clients (max 10) connect
                ProxyClientManager.Instance.NetworkLoop(10);
                if (pm != null)
                    if (pm.ready)
                        pm.NetworkLoop(10);
                System.Threading.Thread.Sleep(1);
            }
        }
        
        private static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            Logger.ShowInfo("Closing.....", null);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BF = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            string path = DateTime.Now.ToString().Replace('/', '-');
            path = path.Replace(' ', '_');
            path = path.Replace(':', '-');
            System.IO.FileStream fs = new System.IO.FileStream(path + ".dat", System.IO.FileMode.Create);
            BF.Serialize(fs, PacketContainer.Instance);
            fs.Close();
        }
    }
}
