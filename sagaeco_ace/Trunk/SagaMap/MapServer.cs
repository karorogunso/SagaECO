using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Mob;
using SagaDB.Map;
using SagaDB.Marionette;
using SagaDB.Quests;
using SagaDB.Npc;
using SagaDB.Skill;
using SagaDB.Synthese;
using SagaDB.Treasure;
using SagaDB.Ring;
using SagaDB.ECOShop;
using SagaDB.Theater;
using SagaDB.ODWar;
using SagaDB.Iris;
using SagaDB.DEMIC;
using SagaDB.Fish;
using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaMap.Manager;
using SagaMap.Mob;
using SagaMap.Network.Client;
using SagaMap.Dungeon;
using System.Runtime.InteropServices;

namespace SagaMap
{
    public class MapServer
    {
        /// <summary>
        /// The characterdatabase associated to this mapserver.
        /// </summary>
        public static ActorDB charDB;
        public static AccountDB accountDB;
        public static bool shutingdown = false;
        public static bool shouldRefreshStatistic = true;

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

        public static void EnsureCharDB()
        {
            bool notConnected = false;

            if (!charDB.isConnected())
            {
                Logger.ShowWarning("lost connection of MySQL...", null);
                notConnected = true;
            }
            while (notConnected)
            {
                Logger.ShowInfo("trying to connect to MySQL...", null);
                charDB.Connect();
                if (!charDB.isConnected())
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

        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);
        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        static void Main(string[] args)
        {
            DateTime time = DateTime.Now;
            string fullPath = System.Environment.CurrentDirectory + "\\SagaMap.exe";
            int WINDOW_HANDLER = FindWindow(null, fullPath);
            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ShutingDown);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Logger Log = new Logger("SagaMap.log");
            Logger.defaultlogger = Log;
            Logger.CurrentLogger = Log;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                         SagaECO Map Server                ");
            Console.WriteLine("         (C)2008-2011 The SagaECO Project Development Team                ");
            Console.WriteLine("         (C)2014-2016 ECOAce Project.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                   FOR ECOACE INTERNAL USE ONLY ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================================================================");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Logger.ShowInfo("Version Informations:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SagaMap");
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

            Logger.ShowInfo(LocalManager.Instance.Strings.INITIALIZATION, null);

            Configuration.Instance.Initialization("./Config/SagaMap.xml");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current Version:[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Configuration.Instance.Version);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("]");

            LocalManager.Instance.CurrentLanguage = (LocalManager.Languages)Enum.Parse(typeof(LocalManager.Languages), Configuration.Instance.Language);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current Language:[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(LocalManager.Instance);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("]");
            Console.ResetColor();

            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Configuration.Instance.LogLevel;

            Logger.ShowInfo("Initializing VirtualFileSystem...");

            VirtualFileSystemManager.Instance.Init(FileSystems.Real, ".");

            //item db
            ItemFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/", "item*.csv", System.IO.SearchOption.TopDirectoryOnly), System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //equipmentset db
            EquipmentSetFactory.Instance.Init("DB/EquipmentSet.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //hair db
            HairFactory.Instance.Init("DB/hair_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            FaceFactory.Instance.Init("DB/face_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //外部封包
            //PacketManager.Instance.LoadPacketFiles("./Packets");
            //iris db
            IrisAbilityFactory.Instance.Init("DB/iris_ability_vector_info.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //iris db
            IrisCardFactory.Instance.Init("DB/iris_card.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //dem chip
            ModelFactory.Instance.Init("DB/demic_chip_model.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //dem chip
            ChipFactory.Instance.Init("DB/demic_chip.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //treasure db 
            TreasureFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/Treasure", "*.xml", System.IO.SearchOption.AllDirectories), null);
            //fish db
            FishFactory.Instance.Init("DB/FishList.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //dropgroup db 不检查
            DropGroupFactory.Instance.Init("DB/monsterdrop.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //monster db
            MobFactory.Instance.Init("DB/monster.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //pet db
            MobFactory.Instance.InitPet("DB/pet.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //MobFactory.Instance.InitPet("DB/partner_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //pet limit db
            MobFactory.Instance.InitPetLimit("DB/pet_limit.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //marionette db 
            MarionetteFactory.Instance.Init("DB/marionette.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //skill db
            SkillFactory.Instance.InitSSP("DB/effect.ssp", System.Text.Encoding.Unicode);
            SkillFactory.Instance.LoadSkillList("DB/SkillList.xml");
            //SkillFactory.Instance.LoadSkillList2("DB/SkillDB");

            //ringfame db
            RingFameTable.Instance.Init("DB/RingFame.xml", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //quest db
            QuestFactory.Instance.Init("DB/Quests", null, true);
            //npc db
            NPCFactory.Instance.Init("DB/npc.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            NPCFactory.Instance.Init("DB/npc2.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //shop db
            ShopFactory.Instance.Init("DB/ShopDB.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //ecoshop db
            ECOShopFactory.Instance.Init("DB/ECOShop.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ChipShopFactory.Instance.Init("DB/ChipShop.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //load map
            MapInfoFactory.Instance.Init("DB/MapInfo.zip");
            //load map fish
            MapInfoFactory.Instance.LoadMapFish("DB/CanFish.xml");
            //load mapflag
            MapInfoFactory.Instance.LoadFlags("DB/MapFlags.xml");
            //pick db
            MapInfoFactory.Instance.LoadGatherInterval("DB/pick_interval.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //item synthesis db
            SyntheseFactory.Instance.Init("DB/synthe1.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //load mapobject
            MapInfoFactory.Instance.LoadMapObjects("DB/MapObjects.dat");
            MapInfoFactory.Instance.ApplyMapObject();
            MapInfoFactory.Instance.MapObjects.Clear();
            MapManager.Instance.MapInfos = MapInfoFactory.Instance.MapInfo;
            MapManager.Instance.LoadMaps();
            //load dungeon
            DungeonMapsFactory.Instance.Init("DB/Dungeon/DungeonMaps.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            DungeonFactory.Instance.Init("DB/Dungeon/Dungeons.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //load scripts
            ScriptManager.Instance.LoadScript("./Scripts");
            //mobai db
            MobAIFactory.Instance.Init("DB/MobAI/MobAI.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //spawns db
            MobSpawnManager.Instance.LoadSpawn("DB/Spawns");
            //fictitious db
            SagaDB.FictitiousActors.FictitiousActorsFactory.Instance.LoadActorsList("DB/Actors/");
            FictitiousActorsManager.Instance.regionFictitiousActors();
            //theater db
            TheaterFactory.Instance.Init("DB/TheaterSchedule.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //odwar db
            ODWarFactory.Instance.Init("DB/ODWar.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            Tasks.System.Theater.Instance.Activate();
            //init skillinstance
            Skill.SkillHandler.Instance.Init();
            //Skill.SkillHandler.Instance.LoadSkill("./Skills");
            Global.clientMananger = (ClientManager)MapClientManager.Instance;
            //load gmcommand phases
            AtCommand.Instance.LoadCommandLevelSetting("Config/GMCommand.csv");
            //make connection to the Login server.
            Network.LoginServer.LoginSession login = new SagaMap.Network.LoginServer.LoginSession();
            while (login.state != SagaMap.Network.LoginServer.LoginSession.SESSION_STATE.IDENTIFIED &&
                login.state != SagaMap.Network.LoginServer.LoginSession.SESSION_STATE.REJECTED)
            {
                System.Threading.Thread.Sleep(1000);
            }
            if (login.state == SagaMap.Network.LoginServer.LoginSession.SESSION_STATE.REJECTED)
            {
                Logger.ShowError("Shutting down in 20sec.", null);
                MapClientManager.Instance.Abort();
                System.Threading.Thread.Sleep(20000);
                return;
            }

            if (!StartDatabase())
            {
                Logger.ShowError("cannot connect to dbserver", null);
                Logger.ShowError("Shutting down in 20sec.", null);
                MapClientManager.Instance.Abort();
                System.Threading.Thread.Sleep(20000);
                return;
            }

            if (Configuration.Instance.SQLLog)
                Logger.defaultSql = charDB as MySQLActorDB;
            ScriptManager.Instance.VariableHolder = charDB.LoadServerVar();

            MapClientManager.Instance.Start();
            if (!MapClientManager.Instance.StartNetwork(Configuration.Instance.Port))
            {
                Logger.ShowError("cannot listen on port: " + Configuration.Instance.Port);
                Logger.ShowInfo("Shutting down in 20sec.");
                MapClientManager.Instance.Abort();
                System.Threading.Thread.Sleep(20000);
                return;
            }

            if (Logger.defaultSql != null)
            {
                Logger.defaultSql.SQLExecuteNonQuery("UPDATE `char` SET `online`=0;");
                Logger.ShowInfo("Clearing SQL Logs");
                Logger.defaultSql.SQLExecuteNonQuery(string.Format("DELETE FROM `log` WHERE `eventTime` < '{0}';", Logger.defaultSql.ToSQLDateTime(DateTime.Now - new TimeSpan(3, 0, 0, 0))));
            }
            Logger.ProgressBarHide("bootup progress time left："+(DateTime.Now - time).TotalMilliseconds.ToString() + "ms");
            Logger.ShowInfo(LocalManager.Instance.Strings.ACCEPTING_CLIENT);

            //激活自动保存系统变量任务
            SagaMap.Tasks.System.AutoSaveServerSvar asss = new Tasks.System.AutoSaveServerSvar();
            asss.Activate();

            //init odwarinstance
            //Tasks.System.ODWar.Instance.Activate();
            //odwar related
            //DateTime now = DateTime.Now;
            //foreach (SagaDB.ODWar.ODWar i in ODWarFactory.Instance.Items.Values)
            //{
            //    ODWarManager.Instance.StartODWar(i.MapID);
            //}
            //SagaMap.LevelLimit.LevelLimitManager.Instance.LoadLevelLimit();
            ExperienceManager.Instance.LoadTable("DB/exp.xml");
            CustomMapManager.Instance.CreateFF();
            MapManager.Instance.CreateFFInstanceOfSer();
            System.Threading.Thread console = new System.Threading.Thread(ConsoleThread);
            console.Start();
            while (true)
            {
                try
                {
                    if (shouldRefreshStatistic && Configuration.Instance.OnlineStatistics)
                    {
                        try
                        {
                            string content;
                            System.IO.StreamReader sr = new System.IO.StreamReader("Config/OnlineStatisticTemplate.htm", true);
                            content = sr.ReadToEnd();
                            sr.Close();
                            string header = content.Substring(0, content.IndexOf("<template for one row>"));
                            content = content.Substring(content.IndexOf("<template for one row>") + "<template for one row>".Length);
                            string footer = content.Substring(content.IndexOf("</template for one row>") + "</template for one row>".Length);
                            content = content.Substring(0, content.IndexOf("</template for one row>"));
                            string res = "";
                            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                            {
                                try
                                {
                                    string tmp = content;
                                    tmp = tmp.Replace("{CharName}", i.Character.Name);
                                    tmp = tmp.Replace("{Job}", i.Character.Job.ToString());
                                    tmp = tmp.Replace("{BaseLv}", i.Character.Level.ToString());
                                    if (i.Character.Job == i.Character.JobBasic)
                                        tmp = tmp.Replace("{JobLv}", i.Character.JobLevel1.ToString());
                                    else if (i.Character.Job == i.Character.Job2X)
                                        tmp = tmp.Replace("{JobLv}", i.Character.JobLevel2X.ToString());
                                    else if (i.Character.Job == i.Character.Job2T)
                                        tmp = tmp.Replace("{JobLv}", i.Character.JobLevel2T.ToString());
                                    else
                                        tmp = tmp.Replace("{JobLv}", i.Character.Job3.ToString());
                                    tmp = tmp.Replace("{Map}", i.map.Info.name);
                                    res += tmp;
                                }
                                catch { }
                            }
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(Configuration.Instance.StatisticsPagePath, false, Global.Unicode);
                            sw.Write(header);
                            sw.Write(res);
                            sw.Write(footer);
                            sw.Flush();
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                        shouldRefreshStatistic = false;
                    }
                    // keep the connections to the database servers alive
                    EnsureCharDB();
                    EnsureAccountDB();
                    // let new clients (max 10) connect
                    if (!shutingdown)
                        MapClientManager.Instance.NetworkLoop(10);
                    System.Threading.Thread.Sleep(1);

                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {

        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void ConsoleThread()
        {
            while (true)
            {
                try
                {                    
                    string cmd = Console.ReadLine();
                    string[] args = cmd.Split(' ');
                    switch (args[0].ToLower())
                    {
                        case "printthreads":
                            ClientManager.PrintAllThreads();
                            break;
                        case "printtaskinfo":
                            Logger.ShowWarning("Active AI count:" + Mob.AIThread.Instance.ActiveAI.ToString());
                            List<string> tasks = TaskManager.Instance.RegisteredTasks;
                            Logger.ShowWarning("Active Tasks:" + tasks.Count.ToString());
                            foreach(string i in tasks)
                            {
                                Logger.ShowWarning(i);
                            }
                            break;
                        case "printband":
                            int sendTotal = 0;
                            int receiveTotal = 0;
                            Logger.ShowWarning("Bandwidth usage information:");
                            try
                            {
                                foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                                {
                                    sendTotal += i.netIO.UpStreamBand;
                                    receiveTotal += i.netIO.DownStreamBand;
                                    Logger.ShowWarning(string.Format("Client:{0} Receive:{1:0.##}KB/s Send:{2:0.##}KB/s", 
                                        i.ToString(),
                                        (float)i.netIO.DownStreamBand / 1024,
                                        (float)i.netIO.UpStreamBand / 1024));
                                }
                            }
                            catch { }
                            Logger.ShowWarning(string.Format("Total: Receive:{0:0.##}KB/s Send:{1:0.##}KB/s",
                                        (float)receiveTotal / 1024,
                                        (float)sendTotal / 1024));
                            break;
                        case "announce":
                            if (args.Length > 1)
                            {
                                StringBuilder tmsg = new StringBuilder(args[1]);
                                for (int i = 2; i < args.Length; i++)
                                {
                                    tmsg.Append(" "+args[i]);
                                }
                                string msg = tmsg.ToString();
                                try
                                {
                                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                                    {
                                        i.SendAnnounce(msg);
                                    }

                                }
                                catch (Exception) { }
                            }
                            break;
                        case "kick":
                            if (args.Length > 1)
                            {
                                try
                                {
                                    MapClient client;
                                    var chr =
                                    from c in MapClientManager.Instance.OnlinePlayer
                                    where c.Character.Name == args[1]
                                    select c;
                                    client = chr.First();
                                    client.netIO.Disconnect();
                                }
                                catch (Exception) { }
                            }
                            break;
                        case "savevar":
                            charDB.SaveServerVar(ScriptManager.Instance.VariableHolder);
                            Logger.ShowInfo("Saving ....", null);
                            break;
                        case "quit":
                            Logger.ShowInfo("Closing.....", null);
                            shutingdown = true;
                            charDB.SaveServerVar(ScriptManager.Instance.VariableHolder);
                            MapClient[] clients = new MapClient[MapClientManager.Instance.Clients.Count];
                            MapClientManager.Instance.Clients.CopyTo(clients);
                            Logger.ShowInfo("Saving player's data.....", null);

                            foreach (MapClient i in clients)
                            {
                                try
                                {
                                    if (i.Character == null) continue;
                                    i.netIO.Disconnect();
                                }
                                catch (Exception) { }
                            }
                            Logger.ShowInfo("Saving golem's data.....", null);

                            foreach (Map i in MapManager.Instance.Maps.Values)
                            {
                                foreach (Actor j in i.Actors.Values)
                                {
                                    if (j.type == ActorType.GOLEM)
                                    {
                                        try
                                        {
                                            ActorGolem golem = (ActorGolem)j;
                                            charDB.SaveChar(golem.Owner, false);
                                        }
                                        catch { }
                                    }
                                }
                                if (i.IsMapInstance)
                                    i.OnDestrory();
                            }
                            System.Environment.Exit(System.Environment.ExitCode);
                            break;
                        case "who":
                            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                            {
                                byte x, y;

                                x = Global.PosX16to8(i.Character.X, i.map.Width);
                                y = Global.PosY16to8(i.Character.Y, i.map.Height);
                                Logger.ShowInfo(i.Character.Name + "(CharID:" + i.Character.CharID + ")[" + i.Map.Name + " " + x.ToString() + "," + y.ToString() + "]");
                            }
                            Logger.ShowInfo(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
                            break;
                        case "kick2":
                            if (args.Length > 1)
                            {
                                try
                                {
                                    MapClient client;
                                    var chr =
                                    from c in MapClientManager.Instance.OnlinePlayer
                                    where c.Character.CharID == uint.Parse(args[1])
                                    select c;
                                    if (chr.Count() > 0)
                                    {
                                        client = chr.First();
                                        client.netIO.Disconnect();
                                    }
                                }
                                catch (Exception) { }
                            }
                            break;
                    }
                }
                catch
                {
                }
            }
        }

        private static void ShutingDown(object sender, ConsoleCancelEventArgs args)
        {
            Logger.ShowInfo("Closing.....", null);
            shutingdown = true;
            charDB.SaveServerVar(ScriptManager.Instance.VariableHolder);
            MapClient[] clients = new MapClient[MapClientManager.Instance.Clients.Count];
            MapClientManager.Instance.Clients.CopyTo(clients);
            Logger.ShowInfo("Saving golem's data.....", null);

            Map[] maps = MapManager.Instance.Maps.Values.ToArray();
            foreach (Map i in maps)
            {
                Actor[] actors = i.Actors.Values.ToArray();
                foreach (Actor j in actors)
                {
                    if (j == null)
                        continue;
                    if (j.type == ActorType.GOLEM)
                    {
                        try
                        {
                            ActorGolem golem = (ActorGolem)j;
                            charDB.SaveChar(golem.Owner, true, false);
                        }
                        catch (Exception ex) { Logger.ShowError(ex); }
                    }
                }
                if (i.IsMapInstance)
                {
                    try
                    {
                        i.OnDestrory();
                    }
                    catch { }
                }
            }
            Logger.ShowInfo("Saving player's data.....", null);
            
            foreach (MapClient i in clients)
            {
                try
                {
                    if (i.Character == null) continue;
                    i.netIO.Disconnect();
                }
                catch(Exception) { }
            }
           

            Logger.ShowInfo("Closing MySQL connection....");
            if (charDB.GetType() == typeof(MySQLConnectivity))
            {
                MySQLConnectivity con = (MySQLConnectivity)charDB;
                while (!con.CanClose)
                    System.Threading.Thread.Sleep(100);
            }
            if (accountDB.GetType() == typeof(MySQLConnectivity))
            {
                MySQLConnectivity con = (MySQLConnectivity)accountDB;
                while (!con.CanClose)
                    System.Threading.Thread.Sleep(100);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            shutingdown = true;            
            Logger.ShowError("Fatal: An unhandled exception is thrown, terminating...");
            Logger.ShowError("Error Message:" + ex.Message);
            Logger.ShowError("Call Stack:" + ex.StackTrace);
            Logger.ShowError("Trying to save all player's data");
            charDB.SaveServerVar(ScriptManager.Instance.VariableHolder);
            
            MapClient[] clients = new MapClient[MapClientManager.Instance.Clients.Count];
            MapClientManager.Instance.Clients.CopyTo(clients);
            foreach (MapClient i in clients)
            {
                try
                {
                    if (i.Character == null) continue;
                    i.netIO.Disconnect();
                }
                catch (Exception) { }
            }
            Logger.ShowError("Trying to clear golem actor");

            Map[] maps = MapManager.Instance.Maps.Values.ToArray();
            foreach (Map i in maps)
            {
                foreach (Actor j in i.Actors.Values)
                {
                    if (j.type == ActorType.GOLEM)
                    {
                        try
                        {
                            ActorGolem golem = (ActorGolem)j;
                            charDB.SaveChar(golem.Owner, true, false);
                        }
                        catch { }
                    }
                }

                if (i.IsMapInstance)
                    i.OnDestrory();
            }

            Logger.ShowInfo("Closing MySQL connection....");
            if (charDB.GetType() == typeof(MySQLConnectivity))
            {
                MySQLConnectivity con = (MySQLConnectivity)charDB;
                while (!con.CanClose)
                    System.Threading.Thread.Sleep(100);
            }
            if (accountDB.GetType() == typeof(MySQLConnectivity))
            {
                MySQLConnectivity con = (MySQLConnectivity)accountDB;
                while (!con.CanClose)
                    System.Threading.Thread.Sleep(100);
            }
        }
    }
}
