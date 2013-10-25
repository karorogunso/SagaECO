using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Item;
using SagaLib;
using SagaLogin.Manager;
using SagaLogin.Network.Client;

namespace SagaLogin
{
    public class LoginServer
    {
        /// <summary>
        /// The characterdatabase associated to this mapserver.
        /// </summary>
        public static ActorDB charDB;
        public static AccountDB accountDB;

        public static bool StartDatabase()
        {
            try
            {
                charDB = new MySQLActorDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort,
                    Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
                accountDB = new MySQLAccountDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort,
                    Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
                charDB.Connect();
                accountDB.Connect();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void EnsureCharDB()
        {
            bool notConnected = false;

            if (!charDB.isConnected())
            {
                Logger.ShowWarning("LOST CONNECTION TO CHAR DB SERVER!", null);
                notConnected = true;
            }
            while (notConnected)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..", null);
                charDB.Connect();
                if (!charDB.isConnected())
                {
                    Logger.ShowError("Failed.. Trying again in 10sec", null);
                    System.Threading.Thread.Sleep(10000);
                    notConnected = true;
                }
                else
                {
                    Logger.ShowInfo("SUCCESSFULLY RE-CONNECTED to char db server...", null);
                    Logger.ShowInfo("Clients can now connect again", null);
                    notConnected = false;
                }
            }
        }
        
        public static void EnsureAccountDB()
        {
            bool notConnected = false;

            if (!accountDB.isConnected())
            {
                Logger.ShowWarning("LOST CONNECTION TO CHAR DB SERVER!", null);
                notConnected = true;
            }
            while (notConnected)
            {
                Logger.ShowInfo("Trying to reconnect to char db server ..", null);
                accountDB.Connect();
                if (!accountDB.isConnected())
                {
                    Logger.ShowError("Failed.. Trying again in 10sec", null);
                    System.Threading.Thread.Sleep(10000);
                    notConnected = true;
                }
                else
                {
                    Logger.ShowInfo("SUCCESSFULLY RE-CONNECTED to char db server...", null);
                    Logger.ShowInfo("Clients can now connect again", null);
                    notConnected = false;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Logger Log = new Logger("SagaLogin.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("           SagaECO Login Server - Internal Beta Version                ");
            Console.WriteLine("           (C)2008 The SagaECO Project Development Team                ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ResetColor();
            Logger.ShowInfo("Starting Initialization...", null);

            Configuration.Instance.Initialization("./Config/SagaLogin.xml");

            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Configuration.Instance.LogLevel;

            ItemFactory.Instance.Init("./DB/item.csv", System.Text.Encoding.GetEncoding("gb2312"));

            if (!StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver", null);
                Logger.ShowError("Shutting down in 20sec.", null);
                System.Threading.Thread.Sleep(20000);
                return;
            }

            LoginClientManager.Instance.Start();
            if (!LoginClientManager.Instance.StartNetwork(12000))
            {
                Logger.ShowError("cannot listen on port: " + 12000);
                Logger.ShowInfo("Shutting down in 20sec.");
                System.Threading.Thread.Sleep(20000);
                return;
            }


            Global.clientMananger = (ClientManager)LoginClientManager.Instance;

            Console.WriteLine("Accepting clients.");

            while (true)
            {
                // keep the connections to the database servers alive
                EnsureCharDB();
                EnsureAccountDB();
                // let new clients (max 10) connect
                LoginClientManager.Instance.NetworkLoop(10);
                System.Threading.Thread.Sleep(1);
            }
        }

        private static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            Logger.ShowInfo("Closing.....", null);            
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...");
            Logger.ShowError("Error Message:" + ex.Message);
            Logger.ShowError("Call Stack:" + ex.StackTrace);
        }
    }
}
