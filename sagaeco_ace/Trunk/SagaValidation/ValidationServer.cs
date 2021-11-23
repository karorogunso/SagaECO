using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;
using SagaDB;
using SagaLogin;
using SagaValidation.Manager;
using SagaValidation.Network.Client;
using System.Runtime.InteropServices;
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
        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        static void Main(string[] args)
        {
            string fullPath = System.Environment.CurrentDirectory + "\\SagaValidation.exe";
            int WINDOW_HANDLER = FindWindow(null, fullPath);
            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);

            Logger Log = new Logger("SagaValidation.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                     SagaECO Validation Server");
            Console.WriteLine("           (C)2012-2014 The COF Project. ");
            Console.WriteLine("           (C)2014-2016 ECOAce Project.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                   FOR ECOACE INTERNAL USE ONLY ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");

            Console.ForegroundColor = ConsoleColor.White;
            Logger.ShowInfo("Version Informations:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaValidation");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(":SVN Rev." + SagaValidation.GlobalInfo.Version + "(" + SagaValidation.GlobalInfo.ModifyDate + ")");
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

            Logger.ShowInfo(string.Format("Accept Client Version at : {0}", Configuration.Instance.ClientGameVersion));

            if (!StartDatabase())
            {
                Logger.ShowError("cannot connect to MySQL", null);
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
                Logger.ShowWarning("lost connection of MySQL...", null);
                notConnected = true;
            }
            while (notConnected)
            {
                Logger.ShowInfo("trying to connect to MySQL...", null);
                accountDB.Connect();
                if (!accountDB.isConnected())
                {
                    Logger.ShowError("connect failted. trying again in 10sec", null);
                    System.Threading.Thread.Sleep(10000);
                    notConnected = true;
                }
                else
                {
                    Logger.ShowInfo("connected to MySQL successfully!", null);
                    Logger.ShowInfo("SagaMap can connect with Client.", null);
                    notConnected = false;
                }
            }
        }
    }
}
