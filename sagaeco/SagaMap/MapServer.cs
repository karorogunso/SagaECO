//#define FreeVersion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Mob;
using SagaDB.Partner;
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
using SagaDB.Title;
using SagaDB.DEMIC;
using SagaDB.DefWar;
using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaMap.Manager;
using SagaMap.Mob;
using SagaMap.Network.Client;
using SagaMap.Dungeon;
using System.Runtime.InteropServices;
using System.Diagnostics;

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
            Console.WriteLine("         (C)2008-2009 The SagaECO Project Development Team                ");
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
            Console.Write("Current Packet Version:[");
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

            //int item = (int)ContainerType.HEAD_ACCE2;

            Logger.CurrentLogger.LogLevel = (Logger.LogContent)Configuration.Instance.LogLevel;

            Logger.ShowInfo("Initializing VirtualFileSystem...");
#if FreeVersion1
            VirtualFileSystemManager.Instance.Init(FileSystems.LPK, "./DB/DB.lpk");
#else
            VirtualFileSystemManager.Instance.Init(FileSystems.Real, ".");
#endif
            ItemFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/", "item*.csv", System.IO.SearchOption.TopDirectoryOnly), System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //ItemFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/", "item**.csv", System.IO.SearchOption.TopDirectoryOnly), System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            HairFactory.Instance.Init("DB/hair_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            FaceFactory.Instance.Init("DB/face_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ExchangeFactory.Instance.Init("DB/exchange.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //PacketManager.Instance.LoadPacketFiles("./Packers");

            //Logger.ShowError("卡片系统暂时关闭，iris_ability_vector_info.csv没有加载");
            IrisAbilityFactory.Instance.Init("DB/iris_ability_vector_info.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            IrisCardFactory.Instance.Init("DB/iris_card.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            IrisGachaFactory.Instance.InitBlack("DB/iris_gacha_blank.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            IrisGachaFactory.Instance.InitWindow("DB/iris_gacha_window.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            ModelFactory.Instance.Init("DB/demic_chip_model.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ChipFactory.Instance.Init("DB/demic_chip.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            //SyntheseFactory.Instance.Init("DB/SyntheseDB.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            SyntheseFactory.Instance.Init("DB/synthe1.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            TreasureFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/Treasure", "*.xml", System.IO.SearchOption.AllDirectories), null);
            SagaDB.Fish.FishFactory.Instance.Init("DB/FishList.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));


            OriMobSetting.Instance.InitOriMonsterSetting("DB/OriMonsterSetting.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            MobFactory.Instance.Init("DB/monster.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //MobFactory.Instance.InitPet("DB/pet.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));也许不需要pet。csv了
            MobFactory.Instance.InitPartner("DB/partner_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            MobFactory.Instance.InitPetLimit("DB/pet_limit.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerDB("DB/partner_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerRankDB("DB/partner_base_rank.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerFoodDB("DB/partner_food.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerEquipDB("DB/partner_Equip.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerTalksInfo("DB/partner_talks_db.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerMotions("DB/partner_motion_together.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitActCubeDB("DB/partner_actcube.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            PartnerFactory.Instance.InitPartnerPicts("DB/monsterpict.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            

            MarionetteFactory.Instance.Init("DB/marionette.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            SkillFactory.Instance.InitSSP("DB/effect.ssp", System.Text.Encoding.Unicode);
            //SkillFactory.Instance.LoadSkillList("DB/SkillList.xml");
            SkillFactory.Instance.LoadSkillList2("DB/SkillDB");
            
            RingFameTable.Instance.Init("DB/RingFame.xml", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            QuestFactory.Instance.Init("DB/Quests", null, true);

            NPCFactory.Instance.Init("DB/npc.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ShopFactory.Instance.Init("DB/ShopDB.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ECOShopFactory.Instance.Init("DB/ECOShop.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ChipShopFactory.Instance.Init("DB/ChipShop.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            NCShopFactory.Instance.Init("DB/NCShop.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            GShopFactory.Instance.Init("DB/GShop.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            KujiListFactory.Instance.InitXML("DB/KujiList.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            KujiListFactory.Instance.BuildNotInKujiItemsList();
            KujiListFactory.Instance.InitEventKujiList("DB/EventKujiList.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            KujiListFactory.Instance.InitTransformList("DB/item_transform.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            KujiListFactory.Instance.InitChoiceList("DB/item_choice_list.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            NPCSpecialFactory.Instance.Init("DB/NpcSpecialEvent.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            MapInfoFactory.Instance.Init("DB/MapInfo.zip");
            MapInfoFactory.Instance.LoadMapFish("DB/CanFish.xml");
            MapInfoFactory.Instance.LoadFlags("DB/MapFlags.xml");
            MapInfoFactory.Instance.LoadMapName("DB/mapname.csv",Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            MapInfoFactory.Instance.LoadGatherInterval("DB/pick_interval.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            MapInfoFactory.Instance.LoadMapObjects("DB/MapObjects.dat");
            MapInfoFactory.Instance.ApplyMapObject();
            MapInfoFactory.Instance.MapObjects.Clear();
            MapInfoFactory.Instance.LoadMapSkills("DB/mapskills.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            MapManager.Instance.MapInfos = MapInfoFactory.Instance.MapInfo;
            MapManager.Instance.LoadMaps();

            DungeonMapsFactory.Instance.Init("DB/Dungeon/DungeonMaps.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            DungeonFactory.Instance.Init("DB/Dungeon/Dungeons.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            MobAIFactory.Instance.Init("DB/MobAI.xml", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            MobAIFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/TTMobAI", "*.xml", System.IO.SearchOption.AllDirectories), null);
            MobAIFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/AnMobAI", "*.xml", System.IO.SearchOption.AllDirectories), null);
            //MobSpawnManager.Instance.LoadAnAI("DB/AnMobAI");
            MobSpawnManager.Instance.LoadSpawn("DB/Spawns");
            SagaDB.FictitiousActors.FictitiousActorsFactory.Instance.LoadActorsList("DB/Actors");
            //SagaDB.FictitiousActors.FictitiousActorsFactory.Instance.LoadShopLists("DB/GolemShop");
            FictitiousActorsManager.Instance.regionFictitiousActors();
            TheaterFactory.Instance.Init("DB/TheaterSchedule.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            ODWarFactory.Instance.Init("DB/ODWar.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            Tasks.System.Theater.Instance.Activate();
            //Tasks.System.AutoRunSystemScript runscript = new Tasks.System.AutoRunSystemScript(3235125);
            //runscript.Activate();

            AnotherFactory.Instance.Init("DB/another_page.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            //PlayerTitleFactory.Instance.Init("DB/playertitle.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            KujiListFactory.Instance.InitZeroCPList("DB/CP0List.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            TitleFactory.Instance.Init("DB/title_info.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));
            TitleFactory.Instance.InitB("DB/title_Bounds.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            //NPCBuyFactory.Instance.Init("DB/NpcBuyList.csv", Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            Skill.SkillHandler.Instance.Init();
            Skill.SkillHandler.Instance.LoadSkill("DB/Skills");


            //加载BUFF
            Skill.SkillHandler.Instance.InitBuffs();

            Global.clientMananger = MapClientManager.Instance;

            //目前无用
            //DefWarFactory.Instance.Init("DB/odwar_order_info.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));

            AtCommand.Instance.LoadCommandLevelSetting("./Config/GMCommand.csv");
            
            Network.LoginServer.LoginSession login = new SagaMap.Network.LoginServer.LoginSession();//Make connection to the Login server.

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

            ScriptManager.Instance.LoadScript("./Scripts");
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
                Logger.defaultSql.SQLExecuteNonQuery(string.Format("DELETE FROM `log` WHERE `eventTime` < '{0}';", Logger.defaultSql.ToSQLDateTime(DateTime.Now - new TimeSpan(15, 0, 0, 0))));
            }
            Logger.ProgressBarHide("加载总耗时："+(DateTime.Now - time).TotalMilliseconds.ToString() + "ms");
            Logger.ShowInfo(LocalManager.Instance.Strings.ACCEPTING_CLIENT);

            //激活AJIMODE線程
            //SagaMap.Tasks.System.AJImode aji = new Tasks.System.AJImode();
            //aji.Activate();
            //激活自動保存系統變量線程
            Tasks.System.AutoSaveServerSvar asss = new Tasks.System.AutoSaveServerSvar();
            asss.Activate();

            //关攻防
            //Tasks.System.ODWar.Instance.Activate();

            Configuration.Instance.InitAnnounce("./DB/Announce.csv");
            //OD War related

            //蓝莓活动，记得关！！
            //Tasks.System.BlueBerryActivity.Instance.Activate();

            //中秋活动，记得关！！
            //Tasks.System.RedBeanActivity.Instance.Activate();

            //保存BOSS复活时间的线程
            Tasks.Mob.AutoSaveRespawnTime.Instance.Activate();

            Tasks.System.南牢列车.Instance.Activate();
            Tasks.System.NPC逛店.Instance.Activate();

            //自动清理副本地图
            MapManager.Instance.InstanceMapLifeHour = 6;//设置副本6小时坍塌
            //Tasks.System.AutoDeleteInstanceMap.Instance.Activate();

            DateTime now = DateTime.Now;
            foreach (SagaDB.ODWar.ODWar i in ODWarFactory.Instance.Items.Values)
            {
                //ODWarManager.Instance.StartODWar(i.MapID);
            }
            //SagaMap.LevelLimit.LevelLimitManager.Instance.LoadLevelLimit();
            ExperienceManager.Instance.LoadTable("DB/exp.xml");
            CustomMapManager.Instance.CreateFF();
            //MapManager.Instance.CreateFFInstanceOfSer();
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
                                        tmp = tmp.Replace("{JobLv}", i.Character.JobLevel3.ToString());
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
#if FreeVersion
                if (MapClientManager.Instance.OnlinePlayer.Count < int.Parse("15"))
#endif
                    if (!shutingdown)
                        MapClientManager.Instance.NetworkLoop(10);
                    System.Threading.Thread.Sleep(110);

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
                        case "mem":
                            {
                                Logger.ShowWarning(string.Format("Total Managed Ram:{0:0.00} MB, Total Process Ram:{1:0.00}MB", (float)GC.GetTotalMemory(false) / 1024 / 1024, (float)Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024));
                            }
                            break;
                        case "toptasks":
                            {
                                Logger.ShowWarning(string.Format("TaskManager:\r\n       Total Tasks:{0} Execution/s:{1} Avg Exection Time:{2}ms \r\n       Avg Schedule Time:{3}ms", TaskManager.Instance.RegisteredCount, TaskManager.Instance.ExecutionCountPerMinute / 60, TaskManager.Instance.AverageExecutionTime, TaskManager.Instance.AverageScheduleTime));

                                Logger.ShowWarning("Top 10 Tasks:");
                                Dictionary<string, int> T10tasks = TaskManager.Instance.TenTasksWithMostInstances;
                                foreach (KeyValuePair<string, int> i in T10tasks)
                                {
                                    Logger.ShowWarning(string.Format("{0} : {1} instances", i.Key, i.Value));
                                }
                            }
                            break;

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
                                Logger.ShowInfo(i.Character.Name + "(CharID:" + i.Character.CharID + ")[" + i.Map.Name + " " + x.ToString() + "," + y.ToString() + "] IP："+ i.Character.Account.LastIP);
                            }
                            Logger.ShowInfo(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
                            Logger.ShowInfo("当前IP在线："+ MapClientManager.Instance.OnlinePlayerOnlyIP.Count.ToString());
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
                    /*if (j.type == ActorType.GOLEM)取消石像
                    {
                        try
                        {
                            ActorGolem golem = (ActorGolem)j;
                            charDB.SaveChar(golem.Owner, true, false);
                        }
                        catch (Exception ex) { Logger.ShowError(ex); }
                    }*/
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
            Logger.ShowError("Error Message:" + ex.ToString());
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
