using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;
using SagaLib;
using System.Runtime.InteropServices;

namespace SagaAPI
{
    public class APIServer
    {
        public static bool shutingdown = false;

        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        static void Main(string[] args)
        {
            string fullPath = System.Environment.CurrentDirectory + "\\SagaAPI.exe";
            int WINDOW_HANDLER = FindWindow(null, fullPath);
            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);

            DateTime time = DateTime.Now;
            
            Logger Log = new Logger("SagaAPI.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                         SagaECO API Server                ");
            Console.WriteLine("         (C)2008-2011 The SagaECO Project Development Team                ");
            Console.WriteLine("         (C)2014-2016 ECOAce Project.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                   FOR ECOACE INTERNAL USE ONLY ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaAPI");
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


            Logger.ShowInfo("Starting APIServer...");

            Configuration.Instance.Initialization("./Config/SagaAPI.xml");
            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Configuration.Instance.LogLevel;



            string pre = Configuration.Instance.Prefixes + ":" + Configuration.Instance.Port+"/";
            
                if (Configuration.Instance.APIKey == null || Configuration.Instance.APIKey == "")
            {
                shutingdown = true;
                Logger.ShowError("APIKEY IS NOT DEFINED");
                return;
            }
            WebServer ws = new WebServer(SendResponse, pre);
            ws.Run();
            Logger.ShowInfo("Accepting Clients from: "+pre);
            Console.ReadKey();



        }
        public static string SendResponse(HttpListenerRequest request)
        {
            return null;
        }


        private static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            Logger.ShowInfo("Closing.....", null);
            shutingdown = true;
            Logger.ShowInfo("Checking unsaved command...", null);
            
        }


    }
}