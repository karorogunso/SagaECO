//#define FreeVersion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Item;
using SagaDB.Mob;
using SagaDB.Map;
using SagaDB.Skill;
using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaLogin.Manager;
using SagaLogin.Network.Client;
using System.Runtime.InteropServices;

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
                    default :
                        return false;
                }
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

        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        static void Main(string[] args)
        {
            string fullPath = System.Environment.CurrentDirectory + "\\SagaLogin.exe";
            int WINDOW_HANDLER = FindWindow(null, fullPath);
            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
            
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Logger Log = new Logger("SagaLogin.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                     SagaECO Login Server                ");
            Console.WriteLine("         (C)2008-2009 The SagaECO Project Development Team              ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Logger.ShowInfo("Version Informations:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaLogin");
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

            Configuration.Instance.Initialization("./Config/SagaLogin.xml");

            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Configuration.Instance.LogLevel;

            Logger.ShowInfo("Initializing VirtualFileSystem...");
#if FreeVersion1
            VirtualFileSystemManager.Instance.Init(FileSystems.LPK, "./DB/DB.lpk");
#else
            VirtualFileSystemManager.Instance.Init(FileSystems.Real, ".");
#endif
            ItemFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/", "item*.csv", System.IO.SearchOption.TopDirectoryOnly), System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            //MapInfoFactory.Instance.Init("DB/MapInfo.zip", false);
            
            if (!StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver", null);
                Logger.ShowError("Shutting down in 20sec.", null);
                System.Threading.Thread.Sleep(20000);
                return;
            }

            LoginClientManager.Instance.Start();
            if (!LoginClientManager.Instance.StartNetwork(Configuration.Instance.Port))
            {
                Logger.ShowError("cannot listen on port: " + Configuration.Instance.Port);
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
                System.Threading.Thread.Sleep(100);
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
