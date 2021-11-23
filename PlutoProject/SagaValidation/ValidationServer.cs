using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;
using SagaDB;
using SagaValidation.Manager;
using SagaValidation.Network.Client;

namespace SagaValidation
{
    public class ValidationServer
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
                switch (Configuration.Instance.DBType)
                {
                    case 0:
                        charDB = new MySQLActorDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort,
                            Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
                        accountDB = new MySQLAccountDB(Configuration.Instance.DBHost, Configuration.Instance.DBPort,
                            Configuration.Instance.DBName, Configuration.Instance.DBUser, Configuration.Instance.DBPass);
                        charDB.Connect();
                        accountDB.Connect();
                        return true;
                    case 1:
                        accountDB = new AccessAccountDB(Configuration.Instance.DBHost);
                        charDB = new AccessActorDb(Configuration.Instance.DBHost);
                        charDB.Connect();
                        accountDB.Connect();
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        static void Main(string[] args)
        {
            Logger Log = new Logger("SagaMap.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                     SagaECO Validation Server             ");
            Console.WriteLine("           (C)2013-2017 The Pluto ECO Project Development Team                  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");

            Console.ForegroundColor = ConsoleColor.White;
            Logger.ShowInfo("Version Informations:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaValidation");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + GlobalInfo.Version + "(" + GlobalInfo.ModifyDate + ")");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaLib");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + SagaLib.GlobalInfo.Version + "(" + SagaLib.GlobalInfo.ModifyDate + ")");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaDB");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + SagaDB.GlobalInfo.Version + "(" + SagaDB.GlobalInfo.ModifyDate + ")");

            Logger.ShowInfo("Starting Initialization...", null);

            Configuration.Instance.Initialization("./Config/SagaValidation.xml");
            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Configuration.Instance.LogLevel;

            if (!StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver", null);
                Logger.ShowError("Shutting down in 20sec.", null);
                System.Threading.Thread.Sleep(20000);
                return;
            }
            ValidationClientManager.Instance.Start();
            if (!ValidationClientManager.Instance.StartNetwork(Configuration.Instance.Port))
            {
                Logger.ShowError("cannot listen on port: " + Configuration.Instance.Port);
                Logger.ShowInfo("Shutting down in 20sec.");
                System.Threading.Thread.Sleep(20000);
                return;
            }

            Global.clientMananger = (ClientManager)ValidationClientManager.Instance;

            Logger.ShowInfo(string.Format("Accept Client Version at : {0}", Configuration.Instance.ClientGameVersion));

            Console.WriteLine("Accepting clients.");

            while (true)
            {
                // keep the connections to the database servers alive
                EnsureAccountDB();
                // let new clients (max 10) connect

                ValidationClientManager.Instance.NetworkLoop(10);
                System.Threading.Thread.Sleep(1);
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
    }
}
