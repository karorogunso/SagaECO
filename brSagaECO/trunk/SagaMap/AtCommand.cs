using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SagaDB.Map;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Network.Client;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.ECOShop;
using SagaDB.Npc;
using SagaDB.Synthese;
using SagaDB.Mob;
using SagaDB.Quests;
using SagaDB.Treasure;
using SagaDB.Theater;
using SagaMap.Mob;
using System.Linq;
using SagaMap.ActorEventHandlers;
using System.Reflection;
using System.IO;
using SagaLib.VirtualFileSystem;
using SagaDB.Skill;
using SagaMap.PC;
using SagaDB.Iris;

namespace SagaMap
{
    public class AtCommand : Singleton<AtCommand>
    {
        private delegate void ProcessCommandFunc(MapClient client, string args);

        private class CommandInfo
        {
            public ProcessCommandFunc func;
            public uint level;

            public CommandInfo(ProcessCommandFunc func, uint lvl)
            {
                this.func = func;
                this.level = lvl;
            }
        }
        private Dictionary<string, CommandInfo> commandTable;
        //private static string _MasterName = "Saga";

        public AtCommand()
        {
            this.commandTable = new Dictionary<string, CommandInfo>();

            #region "Prefixes"
            string OpenCommandPrefix = "/";
            string GMCommandPrefix = "!";
            //string RemoteCommandPrefix = "~";
            #endregion

            #region "Public Commands"
            // public commands
            this.commandTable.Add(OpenCommandPrefix + "who", new CommandInfo(new ProcessCommandFunc(this.ProcessWho), 0));
            this.commandTable.Add(OpenCommandPrefix + "motion", new CommandInfo(new ProcessCommandFunc(this.ProcessMotion), 0));
            this.commandTable.Add(OpenCommandPrefix + "dustbox", new CommandInfo(new ProcessCommandFunc(this.ProcessDustbox), 0));
            this.commandTable.Add(OpenCommandPrefix + "vcashshop", new CommandInfo(new ProcessCommandFunc(this.ProcessVcashsop), 0));
            this.commandTable.Add(OpenCommandPrefix + "user", new CommandInfo(new ProcessCommandFunc(this.ProcessUser), 0));
            this.commandTable.Add(OpenCommandPrefix + "commandlist", new CommandInfo(new ProcessCommandFunc(this.ProcessCommandList), 0));
            this.commandTable.Add(OpenCommandPrefix + "w", new CommandInfo(new ProcessCommandFunc(this.ProcessTrumpet), 0));
            #endregion

            #region "GM Commands"
            // gm commands
            this.commandTable.Add(GMCommandPrefix + "fg", new CommandInfo(new ProcessCommandFunc(this.ProcessFG), 20));
            this.commandTable.Add(GMCommandPrefix + "warp", new CommandInfo(new ProcessCommandFunc(this.ProcessWarp), 20));
            this.commandTable.Add(GMCommandPrefix + "announce", new CommandInfo(new ProcessCommandFunc(this.ProcessAnnounce), 100));
            this.commandTable.Add(GMCommandPrefix + "heal", new CommandInfo(new ProcessCommandFunc(this.ProcessHeal), 20));
            this.commandTable.Add(GMCommandPrefix + "level", new CommandInfo(new ProcessCommandFunc(this.ProcessLevel), 60));
            this.commandTable.Add(GMCommandPrefix + "joblv", new CommandInfo(new ProcessCommandFunc(this.ProcessJobLevel), 60));
            this.commandTable.Add(GMCommandPrefix + "gold", new CommandInfo(new ProcessCommandFunc(this.ProcessGold), 60));
            this.commandTable.Add(GMCommandPrefix + "shoppoint", new CommandInfo(new ProcessCommandFunc(this.ProcessShoppoint), 60));
            this.commandTable.Add(GMCommandPrefix + "hair", new CommandInfo(new ProcessCommandFunc(this.ProcessHair), 20));
            this.commandTable.Add(GMCommandPrefix + "hairstyle", new CommandInfo(new ProcessCommandFunc(this.ProcessHairstyle), 20));
            this.commandTable.Add(GMCommandPrefix + "haircolor", new CommandInfo(new ProcessCommandFunc(this.ProcessHaircolor), 20));
            this.commandTable.Add(GMCommandPrefix + "job", new CommandInfo(new ProcessCommandFunc(this.ProcessJob), 60));
            this.commandTable.Add(GMCommandPrefix + "statpoints", new CommandInfo(new ProcessCommandFunc(this.ProcessStatPoints), 60));
            this.commandTable.Add(GMCommandPrefix + "skillpoints", new CommandInfo(new ProcessCommandFunc(this.ProcessSkillPoints), 60));
            this.commandTable.Add(GMCommandPrefix + "hide", new CommandInfo(new ProcessCommandFunc(this.ProcessHide), 60));
            this.commandTable.Add(GMCommandPrefix + "ban", new CommandInfo(new ProcessCommandFunc(this.ProcessBan), 80));
            this.commandTable.Add(GMCommandPrefix + "event", new CommandInfo(new ProcessCommandFunc(this.ProcessEvent), 20));
            this.commandTable.Add(GMCommandPrefix + "hairext", new CommandInfo(new ProcessCommandFunc(this.ProcessHairext), 20));
            this.commandTable.Add(GMCommandPrefix + "playersize", new CommandInfo(new ProcessCommandFunc(this.ProcessPlayersize), 20));
            this.commandTable.Add(GMCommandPrefix + "item", new CommandInfo(new ProcessCommandFunc(this.ProcessItem), 60));
            this.commandTable.Add(GMCommandPrefix + "item2", new CommandInfo(new ProcessCommandFunc(this.ProcessItem2), 60));
            this.commandTable.Add(GMCommandPrefix + "speed", new CommandInfo(new ProcessCommandFunc(this.ProcessSpeed), 20));
            this.commandTable.Add(GMCommandPrefix + "revive", new CommandInfo(new ProcessCommandFunc(this.ProcessRevive), 20));
            this.commandTable.Add(GMCommandPrefix + "kick", new CommandInfo(new ProcessCommandFunc(this.ProcessKick), 100));
            this.commandTable.Add(GMCommandPrefix + "kickall", new CommandInfo(new ProcessCommandFunc(this.ProcessKickAll), 100));
            this.commandTable.Add(GMCommandPrefix + "recall", new CommandInfo(new ProcessCommandFunc(this.ProcessJump), 60));
            this.commandTable.Add(GMCommandPrefix + "recall2", new CommandInfo(new ProcessCommandFunc(this.ProcessJump2), 60));
            this.commandTable.Add(GMCommandPrefix + "jump", new CommandInfo(new ProcessCommandFunc(this.ProcessJumpTo), 60));
            this.commandTable.Add(GMCommandPrefix + "jump2", new CommandInfo(new ProcessCommandFunc(this.ProcessJumpTo2), 60));
            this.commandTable.Add(GMCommandPrefix + "mob", new CommandInfo(new ProcessCommandFunc(this.ProcessMob), 60));
            this.commandTable.Add(GMCommandPrefix + "summon", new CommandInfo(new ProcessCommandFunc(this.ProcessSummon), 60));
            this.commandTable.Add(GMCommandPrefix + "summonme", new CommandInfo(new ProcessCommandFunc(this.ProcessSummonMe), 60));
            this.commandTable.Add(GMCommandPrefix + "spawn", new CommandInfo(new ProcessCommandFunc(this.ProcessSpawn), 60));
            this.commandTable.Add(GMCommandPrefix + "effect", new CommandInfo(new ProcessCommandFunc(this.ProcessEffect), 60));
            this.commandTable.Add(GMCommandPrefix + "kickgolem", new CommandInfo(new ProcessCommandFunc(this.ProcessKickGolem), 60));
            this.commandTable.Add(GMCommandPrefix + "killallmob", new CommandInfo(new ProcessCommandFunc(this.ProcessKillAllMob), 60));
            this.commandTable.Add(GMCommandPrefix + "odwarstart", new CommandInfo(new ProcessCommandFunc(this.ProcessODWarStart), 60));
            this.commandTable.Add(GMCommandPrefix + "tweet", new CommandInfo(new ProcessCommandFunc(this.ProcessTweet), 0));
            //for skill test
            this.commandTable.Add(GMCommandPrefix + "skill", new CommandInfo(new ProcessCommandFunc(this.ProcessSkill), 60));
            this.commandTable.Add(GMCommandPrefix + "skillclear", new CommandInfo(new ProcessCommandFunc(this.ProcessSkillClear), 60));
            this.commandTable.Add(GMCommandPrefix + "gmob", new CommandInfo(new ProcessCommandFunc(this.ProcessGridMob), 60));
            this.commandTable.Add(GMCommandPrefix + "showstatus", new CommandInfo(new ProcessCommandFunc(this.ProcessShowStatus), 60));
            this.commandTable.Add(GMCommandPrefix + "who", new CommandInfo(new ProcessCommandFunc(this.ProcessWho), 1));
            this.commandTable.Add(GMCommandPrefix + "who2", new CommandInfo(new ProcessCommandFunc(this.ProcessWho2), 20));
            this.commandTable.Add(GMCommandPrefix + "who3", new CommandInfo(new ProcessCommandFunc(this.ProcessWho3), 60));
            this.commandTable.Add(GMCommandPrefix + "mode", new CommandInfo(new ProcessCommandFunc(this.ProcessMode), 100));
            this.commandTable.Add(GMCommandPrefix + "robot", new CommandInfo(new ProcessCommandFunc(this.ProcessRobot), 100));
            this.commandTable.Add(GMCommandPrefix + "go", new CommandInfo(new ProcessCommandFunc(this.ProcessGo), 20));
            this.commandTable.Add(GMCommandPrefix + "ch", new CommandInfo(new ProcessCommandFunc(this.ProcessCh), 20));
            //now working
            this.commandTable.Add(GMCommandPrefix + "info", new CommandInfo(new ProcessCommandFunc(this.ProcessInfo), 20));
            this.commandTable.Add(GMCommandPrefix + "reloadscript", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadScript), 99));
            this.commandTable.Add(GMCommandPrefix + "reloadconfig", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadConfig), 99));
            this.commandTable.Add(GMCommandPrefix + "raw", new CommandInfo(new ProcessCommandFunc(this.ProcessRaw), 100));
            this.commandTable.Add(GMCommandPrefix + "test", new CommandInfo(new ProcessCommandFunc(this.ProcessTest), 100));
            this.commandTable.Add(GMCommandPrefix + "face", new CommandInfo(new ProcessCommandFunc(this.ProcessFace), 100));
            this.commandTable.Add(GMCommandPrefix + "createff", new CommandInfo(new ProcessCommandFunc(this.ProcessCreateFF), 100));
            this.commandTable.Add(GMCommandPrefix + "openff", new CommandInfo(new ProcessCommandFunc(this.ProcessOpenFF), 100));
            this.commandTable.Add(GMCommandPrefix + "theater", new CommandInfo(new ProcessCommandFunc(this.ProcessTheater), 100));
            this.commandTable.Add(GMCommandPrefix + "metamo", new CommandInfo(new ProcessCommandFunc(this.ProcessMetamo), 100));
            this.commandTable.Add(GMCommandPrefix + "through", new CommandInfo(new ProcessCommandFunc(this.ProcessThrough), 100));
            this.commandTable.Add(GMCommandPrefix + "ta", new CommandInfo(new ProcessCommandFunc(this.ProcessTaskAnnounce), 100));
            this.commandTable.Add(GMCommandPrefix + "sta", new CommandInfo(new ProcessCommandFunc(this.ProcessStopTaskAnnounce), 100));
            this.commandTable.Add(GMCommandPrefix + "itemto", new CommandInfo(new ProcessCommandFunc(this.ProcessItemTo), 100));
            //梦美添加
            this.commandTable.Add(GMCommandPrefix + "status", new CommandInfo(new ProcessCommandFunc(this.ProcessStatus), 100));
            this.commandTable.Add(GMCommandPrefix + "effect2", new CommandInfo(new ProcessCommandFunc(this.ProcessEffect2), 60));
            //黑白照追加
            this.commandTable.Add(GMCommandPrefix + "idsearch", new CommandInfo(new ProcessCommandFunc(this.ProcessIDSearch), 60));
            this.commandTable.Add(GMCommandPrefix + "equiplist", new CommandInfo(new ProcessCommandFunc(this.ProcessEquipList), 60));
            this.commandTable.Add(GMCommandPrefix + "inventorylist", new CommandInfo(new ProcessCommandFunc(this.ProcessInventoryList), 60));
            this.commandTable.Add(GMCommandPrefix + "recallmap", new CommandInfo(new ProcessCommandFunc(this.ProcessCallMap), 60));
            this.commandTable.Add(GMCommandPrefix + "recallall", new CommandInfo(new ProcessCommandFunc(this.ProcessCallAll), 60));
            this.commandTable.Add(GMCommandPrefix + "monsterinfo", new CommandInfo(new ProcessCommandFunc(this.ProcessMonsterInfo), 60));
            this.commandTable.Add(GMCommandPrefix + "reloadskilldb", new CommandInfo(new ProcessCommandFunc(this.ProcessReloadSkillDB), 60));
            this.commandTable.Add(GMCommandPrefix + "skillall", new CommandInfo(new ProcessCommandFunc(this.ProcessSkillALL), 60));
            this.commandTable.Add(GMCommandPrefix + "fame", new CommandInfo(new ProcessCommandFunc(this.ProcessFame), 60));
            this.commandTable.Add(GMCommandPrefix + "statreset", new CommandInfo(new ProcessCommandFunc(this.ProcessStatReset), 60));
            this.commandTable.Add(GMCommandPrefix + "skreset", new CommandInfo(new ProcessCommandFunc(this.ProcessSkillReset), 60));
            //
            this.commandTable.Add(GMCommandPrefix + "rwarp", new CommandInfo(new ProcessCommandFunc(this.ProcessRWarp), 60));
            this.commandTable.Add(GMCommandPrefix + "cexprate", new CommandInfo(new ProcessCommandFunc(this.ProcessCEXPGainRate), 60));
            this.commandTable.Add(GMCommandPrefix + "jexprate", new CommandInfo(new ProcessCommandFunc(this.ProcessJEXPGainRate), 60));
            #endregion

            #region "Remote Commands"
            // remote commands
            //this.commandTable.Add( RemoteCommandPrefix + "jump", new CommandInfo( new ProcessCommandFunc( this.ProcessRJump ), 60 ) );
            //this.commandTable.Add( RemoteCommandPrefix + "cash", new CommandInfo( new ProcessCommandFunc( this.ProcessRCash ), 60 ) );
            //this.commandTable.Add( RemoteCommandPrefix + "info", new CommandInfo( new ProcessCommandFunc( this.ProcessRInfo ), 60 ) );
            //this.commandTable.Add( RemoteCommandPrefix + "res", new CommandInfo( new ProcessCommandFunc(this.ProcessRRes), 60));
            //this.commandTable.Add(RemoteCommandPrefix + "die", new CommandInfo(new ProcessCommandFunc(this.ProcessRDie), 60));
            //this.commandTable.Add(RemoteCommandPrefix + "heal", new CommandInfo(new ProcessCommandFunc(this.ProcessRHeal), 60));
            #endregion


            #region "Aliases"
            // Aliases
            //this.commandTable.Add(GMCommandPrefix + "kill", new CommandInfo(new ProcessCommandFunc(this.ProcessDie), 60));
            //this.commandTable.Add(RemoteCommandPrefix + "kill", new CommandInfo(new ProcessCommandFunc(this.ProcessRDie), 60));
            //this.commandTable.Add(GMCommandPrefix + "b", new CommandInfo(new ProcessCommandFunc(this.ProcessBroadcast), 60));
            //this.commandTable.Add(GMCommandPrefix + "gm", new CommandInfo(new ProcessCommandFunc(this.ProcessGMChat), 60));
            #endregion
        }

        public Dictionary<string, SagaLib.MultiRunTask> tasklist = new Dictionary<string, MultiRunTask>();
        public void ProcessTaskAnnounce(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg[0] == "" || arg[1] == "" || arg[2] == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_TA_PAEA);
            }
            else
            {
                try
                {
                    string taskname = arg[0];
                    if (!tasklist.ContainsKey(taskname))
                    {
                        string announce = arg[1];
                        int period = int.Parse(arg[2]) * 1000;
                        Tasks.System.TaskAnnounce ta = new Tasks.System.TaskAnnounce(taskname, announce, period);
                        ta.Activate();
                        tasklist.Add(taskname, ta);
                        client.SendSystemMessage(taskname + "添加成功");
                    }
                    else
                        client.SendSystemMessage(taskname + "已存在！");
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
        }
        public void ProcessStopTaskAnnounce(MapClient client, string args)
        {
            try
            {
                string taskname = args;
                if (tasklist.ContainsKey(taskname))
                {
                    SagaLib.MultiRunTask task = tasklist[taskname];
                    task.Deactivate();
                    tasklist.Remove(taskname);
                    client.SendSystemMessage(taskname + "已移除");
                }
                else
                    client.SendSystemMessage("未找到" + taskname);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        public void ProcessThrough(MapClient client, string args)
        {
            try
            {
                if (!client.Character.Status.Additions.ContainsKey("Through"))
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(100, 1);
                    Skill.Additions.Global.DefaultBuff Through = new Skill.Additions.Global.DefaultBuff(skill, client.Character, "Through", 600000);
                    Skill.SkillHandler.ApplyAddition(client.Character, Through);
                }
                else
                {
                    client.Character.Status.Additions["Through"].AdditionEnd();
                    client.Character.Status.Additions.Remove("Through");
                }
            }
            catch { }
        }
        public void ProcessMetamo(MapClient client, string args)
        {
            try
            {

                client.Character.TranceID = uint.Parse(args);
                client.SendCharInfoUpdate();
            }
            catch { }
        }
        public void ProcessTheater(MapClient client, string command)
        {
            try
            {
                Packets.Server.SSMG_NPC_PLAY_SOUND p = new SagaMap.Packets.Server.SSMG_NPC_PLAY_SOUND();
                p.SoundID = 2000;
                p.Loop = 0;
                p.Volume = 100;
                p.Balance = 50;
                client.netIO.SendPacket(p);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        public void ProcessStatus(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length > 1)
                {
                    ushort pt = ushort.Parse(args[1]);
                    switch (args[0])
                    {
                        case "str":
                            client.Character.Str = pt;
                            break;
                        case "dex":
                            client.Character.Dex = pt;
                            break;
                        case "int":
                            client.Character.Int = pt; ;
                            break;
                        case "vit":
                            client.Character.Vit = pt;
                            break;
                        case "agi":
                            client.Character.Agi = pt; ;
                            break;
                        case "mag":
                            client.Character.Mag = pt;
                            break;
                    }
                }
                client.SendStatus();
                client.SendStatusExtend();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 讀取設定檔
        /// </summary>
        /// <param name="path"></param>
        public void LoadCommandLevelSetting(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path);
                int count = 0;
                while (!sr.EndOfStream)
                {

                    string line = sr.ReadLine();
                    if (line[0] == '#')
                    {
                        continue;
                    }
                    string[] sLine = line.Split(',');
                    CommandInfo cmdInfo = commandTable[sLine[0]] as CommandInfo;
                    if (cmdInfo != null)
                    {
                        cmdInfo.level = uint.Parse(sLine[1]);
                        count++;
                    }

                }
                Logger.ShowInfo(string.Format("{0} GMCommand Setting Loaded.", count));
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex.ToString());
            }
        }

        public bool ProcessCommand(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(" ".ToCharArray(), 2);
                args[0] = args[0].ToLower();

                if (this.commandTable.ContainsKey(args[0]))
                {
                    CommandInfo cInfo = this.commandTable[args[0]];

                    if (client.Character.Account.GMLevel >= cInfo.level)
                    {
                        Logger.LogGMCommand(client.Character.Name + "(" + client.Character.CharID + ")", "",
                            string.Format("Account:{0}({1}) GMLv:{2} Command:{3}",
                            client.Character.Account.Name,
                            client.Character.Account.AccountID,
                            client.Character.Account.GMLevel, command));

                        if (args.Length == 2)
                            cInfo.func(client, args[1]);
                        else cInfo.func(client, "");
                    }
                    else
                        client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_NO_ACCESS);

                    return true;
                }
            }
            catch (Exception e) { Logger.ShowError(e, null); }

            return false;
        }

        #region "Command Processing"


        #region "Public Commands"
        private void ProcessMotion(MapClient client, string args)
        {
            client.SendMotion((MotionType)int.Parse(args), 1);
        }

        private void ProcessWhere(MapClient client, string args)
        {

        }

        private void ProcessDustbox(MapClient client, string args)
        {
            client.npcTrade = true;
            string name = "垃圾箱";

            client.SendTradeStartNPC(name);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            /*while (client.npcTrade)
            {
                System.Threading.Thread.Sleep(500);
            }*/
            if (blocked)
                ClientManager.EnterCriticalArea();

            List<SagaDB.Item.Item> items = client.npcTradeItem;
            client.npcTradeItem = null;
        }

        private void ProcessVcashsop(MapClient client, string args)
        {
            Scripting.SkillEvent.Instance.VShopOpen(client.Character);
        }

        private void ProcessUser(MapClient client, string args)
        {
            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
            {
                client.SendSystemMessage(i.Character.Name + " [" + i.Map.Name + "]");
            }
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
        }

        private void ProcessGetHeight(MapClient client, string args)
        {
        }
        #endregion

        public void ProcessCommandList(MapClient client, string args)
        {
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_COMMANDLIST);
            foreach (string i in commandTable.Keys)
            {
                if (client.Character.Account.GMLevel >= commandTable[i].level)
                {
                    string desc = "";
                    if (LocalManager.Instance.Strings.ATCOMMAND_DESC.ContainsKey(i))
                        desc = LocalManager.Instance.Strings.ATCOMMAND_DESC[i];
                    client.SendSystemMessage(i + " " + desc);
                }
            }
        }

        private void ProcessRevive(MapClient client, string args)
        {
            if (client.Character.Buff.Dead)
            {
                client.Character.BattleStatus = 0;
                client.SendChangeStatus();
                client.Character.TInt["Revive"] = 5;
                client.EventActivate(0xF1000000);
            }
        }

        private void ProcessSpeed(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_SPEED_PARA);
            }
            else
            {
                try
                {
                    client.Character.Speed = ushort.Parse(args);
                    client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, client.Character, true);
                }

                catch (Exception) { }

            }
        }

        private void ProcessStatPoints(MapClient client, string args)
        {
            try
            {
                ushort pt = ushort.Parse(args);
                client.Character.StatsPoint = pt;
            }
            catch
            {
            }
        }

        private void ProcessSkillPoints(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length > 1)
                {
                    ushort pt = ushort.Parse(args[1]);
                    switch (args[0])
                    {
                        case "1":
                            client.Character.SkillPoint = pt;
                            break;
                        case "2-1":
                            client.Character.SkillPoint2X = pt;
                            break;
                        case "2-2":
                            client.Character.SkillPoint2T = pt;
                            break;
                        case "3":
                            client.Character.SkillPoint3 = pt;
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void ProcessJob(MapClient client, string args)
        {
            try
            {
                MapServer.charDB.SaveSkill(client.Character);
                client.Character.Skills.Clear();
                client.Character.Skills2.Clear();
                client.Character.Skills2_1.Clear();
                client.Character.Skills2_2.Clear();
                client.Character.SkillsReserve.Clear();
                client.Character.Skills3.Clear();
                client.Character.JobLevel3 = 1;
                client.Character.JEXP = 1;
                client.Character.SkillPoint = 0;
                client.Character.SkillPoint2T = 0;
                client.Character.SkillPoint2X = 0;
                client.Character.SkillPoint3 = 0;
                int job = int.Parse(args);
                client.Character.Job = (PC_JOB)job;
                MapServer.charDB.GetSkill(client.Character);

                PC.StatusFactory.Instance.CalcStatus(client.Character);
                client.SendPlayerInfo();
            }
            catch (Exception)
            {
            }
        }

        private void ProcessEvent(MapClient client, string args)
        {
            try
            {
                uint Event = uint.Parse(args);
                client.EventActivate(Event);
            }
            catch (Exception)
            {
            }
        }

        private void ProcessReloadConfig(MapClient client, string args)
        {
            try
            {
                switch (args.ToLower())
                {
                    case "ecoshop":
                        ProcessAnnounce(client, "Reloading ECOShop");
                        ECOShopFactory.Instance.Reload();
                        ProcessAnnounce(client, "Reloaded ECOShop");
                        break;
                    case "shopdb":
                        ProcessAnnounce(client, "Reloading ShopDB");
                        ShopFactory.Instance.Reload();
                        GC.Collect();
                        ProcessAnnounce(client, "Reloaded ShopDB");
                        break;
                    case "monster":
                        ProcessAnnounce(client, "Reloading monster");
                        MobFactory.Instance.Mobs.Clear();
                        MobFactory.Instance.Init("./DB/monster.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        MobAIFactory.Instance.Items.Clear();
                        MobAIFactory.Instance.Init("DB/MobAI/MobAI.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        //MobAIFactory.Instance.Init(SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/TTMobAI", "*.xml", System.IO.SearchOption.AllDirectories), null);
                        ProcessAnnounce(client, "Reloaded monster");
                        break;
                    case "quests":
                        ProcessAnnounce(client, "Reloading Quests");
                        QuestFactory.Instance.Reload();
                        ProcessAnnounce(client, "Reloaded Quests");
                        break;
                    case "treasure":
                        ProcessAnnounce(client, "Reloading Treasure");
                        //TreasureFactory.Instance.Reload();
                        TreasureFactory.Instance.Items.Clear();
                        TreasureFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/Treasure", "*.xml", System.IO.SearchOption.AllDirectories), null);
                        ProcessAnnounce(client, "Reloaded Treasure");
                        break;
                    case "spawns":
                        ProcessAnnounce(client, "Reloading Spawns");
                        //MobSpawnManager.Instance.Spawns.Clear();
                        //MobSpawnManager.Instance.LoadSpawn("./DB/Spawns");
                        MobSpawnManager.Instance.Reload();
                        ProcessAnnounce(client, "Reloaded Spawns");
                        break;
                    case "theater":
                        ProcessAnnounce(client, "Reloading Theater");
                        TheaterFactory.Instance.Reload();
                        ProcessAnnounce(client, "Reloaded Theater");
                        break;
                    case "synthese":
                        ProcessAnnounce(client, "Reloading SyntheseDB");
                        SyntheseFactory.Instance.Reload();
                        ProcessAnnounce(client, "Reloaded SyntheseDB");
                        break;
                    case "item":
                        ProcessAnnounce(client, "Reloading ItemDB");
                        //ItemFactory.Instance.Reload();
                        ItemFactory.Instance.Items.Clear();
                        ItemFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/", "item*.csv", System.IO.SearchOption.TopDirectoryOnly), System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        ProcessAnnounce(client, "Reloaded ItemDB");
                        break;
                    case "iris":
                        ProcessAnnounce(client, "Reloading Iris Card");
                        IrisCardFactory.Instance.Reload();
                        IrisAbilityFactory.Instance.Reload();
                        ProcessAnnounce(client, "Reloaded Iris Card");
                        break;
                    default:
                        ProcessAnnounce(client, "Reloading Configs");
                        Configuration.Instance.Initialization("./Config/SagaMap.xml");
                        ProcessAnnounce(client, "Reloaded Configs");
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void ProcessReloadScript(MapClient client, string args)
        {
            ProcessAnnounce(client, "Reloading Scripts");
            try
            {
                ScriptManager.Instance.ReloadScript();
            }
            catch (Exception ex)
            {
                client.SendSystemMessage(ex.ToString());
            }
            ProcessAnnounce(client, "Reloaded Scripts");
        }

        private void ProcessReloadSkillDB(MapClient client, string args)
        {
            ProcessAnnounce(client, "Reloading SkillDB");
            try
            {
                SkillFactory.Instance.ReloadSkillDB();
            }
            catch (Exception ex)
            {
                client.SendSystemMessage(ex.ToString());
            }
            ProcessAnnounce(client, "Reloaded SkillDB");
        }

        private void ProcessEffect(MapClient client, string args)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = uint.Parse(args);
            arg.actorID = client.Character.ActorID;
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, client.Character, true);
        }

        private void ProcessEffect2(MapClient client, string args)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = uint.Parse(args);
            arg.x = Global.PosX16to8(client.Character.X, client.map.Width);
            arg.y = Global.PosY16to8(client.Character.Y, client.map.Height);
            //arg.actorID = client.Character.ActorID;
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, client.Character, true);
        }

        private void ProcessRobot(MapClient client, string args)
        {
            if (client.AI == null)
            {
                client.AI = new MobAI(client.Character);
                client.AI.Mode = new AIMode();
                client.AI.Mode.mask.SetValue(AIFlag.Active, true);
            }
            if (client.AI.Activated)
            {
                client.AI.Pause();
            }
            else
            {
                client.AI.Start();
            }
        }

        private void ProcessMode(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_MODE_PARA);
            }
            else
            {
                try
                {

                    switch (args)
                    {
                        case "1":
                            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                            {
                                i.SendPkMode();
                            }
                            ProcessAnnounce(client, LocalManager.Instance.Strings.ATCOMMAND_PK_MODE_INFO);
                            break;

                        case "2":
                            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                            {
                                i.SendNormalMode();
                            }
                            ProcessAnnounce(client, LocalManager.Instance.Strings.ATCOMMAND_NORMAL_MODE_INFO);
                            break;
                        default:

                            break;

                    }
                }
                catch (Exception) { }
            }
        }

        private void ProcessInfo(MapClient client, string args)
        {
            byte x, y;
            x = Global.PosX16to8(client.Character.X, client.map.Width);
            y = Global.PosY16to8(client.Character.Y, client.map.Height);
            client.SendSystemMessage(client.Map.Name + " [" + x.ToString() + "," + y.ToString() + "]");
            client.SendSystemMessage("Fire:" + client.map.Info.fire[x, y].ToString());
            client.SendSystemMessage("Wind:" + client.map.Info.wind[x, y].ToString());
            client.SendSystemMessage("Water:" + client.map.Info.water[x, y].ToString());
            client.SendSystemMessage("Earth:" + client.map.Info.earth[x, y].ToString());
            client.SendSystemMessage("Holy:" + client.map.Info.holy[x, y].ToString());
            client.SendSystemMessage("Dark:" + client.map.Info.dark[x, y].ToString());

        }

        private void ProcessJump(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_JUMP_PARA);
            }
            else
            {
                try
                {
                    uint n_Mapid;
                    short n_X, n_Y;
                    n_X = client.Character.X;
                    n_Y = client.Character.Y;
                    n_Mapid = client.Character.MapID;
                    var chr =
                        from c in MapClientManager.Instance.OnlinePlayer
                        where c.Character.Name == args
                        select c;
                    client = chr.First();
                    client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);

                }
                catch (Exception) { }
            }
        }

        private void ProcessJump2(MapClient client, string args)
        {
            if (args == "")
            {

            }
            else
            {
                try
                {
                    uint n_Mapid;
                    short n_X, n_Y;
                    n_X = client.Character.X;
                    n_Y = client.Character.Y;
                    n_Mapid = client.Character.MapID;
                    var chr =
                        from c in MapClientManager.Instance.OnlinePlayer
                        where c.Character.CharID == uint.Parse(args)
                        select c;
                    client = chr.First();
                    client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);

                }
                catch (Exception) { }
            }
        }

        private void ProcessBan(MapClient client, string args)
        {
            if (args != "")
            {
                try
                {
                    var chr =
                        from c in MapClientManager.Instance.OnlinePlayer
                        where c.Character.Name == args
                        select c;
                    MapClient tClient = chr.First();
                    tClient.Character.Account.Banned = true;
                    tClient.netIO.Disconnect();
                }
                catch (Exception) { }
            }
        }

        private void ProcessJumpTo(MapClient client, string args)
        {
            if (args == "")
            {

            }
            else
            {
                try
                {
                    var chr =
                        from c in MapClientManager.Instance.OnlinePlayer
                        where c.Character.Name == args
                        select c;
                    MapClient tClient = chr.First();
                    uint n_Mapid;
                    short n_X, n_Y;
                    n_X = tClient.Character.X;
                    n_Y = tClient.Character.Y;
                    n_Mapid = tClient.Character.MapID;
                    client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);
                }
                catch (Exception) { }
            }
        }

        private void ProcessJumpTo2(MapClient client, string args)
        {
            if (args == "")
            {

            }
            else
            {
                try
                {
                    var chr =
                        from c in MapClientManager.Instance.OnlinePlayer
                        where c.Character.CharID == uint.Parse(args)
                        select c;
                    MapClient tClient = chr.First();
                    uint n_Mapid;
                    short n_X, n_Y;
                    n_X = tClient.Character.X;
                    n_Y = tClient.Character.Y;
                    n_Mapid = tClient.Character.MapID;
                    client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);

                }
                catch (Exception) { }
            }
        }

        private void ProcessSummon(MapClient client, string args)
        {
            int number = 1;
            uint id = 0;
            if (args != "")
            {
                switch (args.Split(' ').Length)
                {
                    case 1:
                        number = 1;
                        id = uint.Parse(args);
                        break;
                    case 2:
                        id = uint.Parse(args.Split(' ')[0]);
                        number = int.Parse(args.Split(' ')[1]);
                        break;
                    default:
                        number = 1;
                        uint.Parse(args);
                        break;
                }
                try
                {
                    for (int i = 1; i <= number; i++)
                    {
                        client.map.SpawnMob(id,
                            (short)(client.Character.X + new Random().Next(1, 10)),
                            (short)(client.Character.Y + new Random().Next(1, 10)),
                            2500,
                            client.Character);
                    }
                }
                catch
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_MOB_ERROR);

                }
            }
        }

        private void ProcessSummonMe(MapClient client, string args)
        {
            ActorPC pc = client.Character;
            ActorShadow actor = new ActorShadow(pc);
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            actor.Name = Manager.LocalManager.Instance.Strings.SKILL_DECOY + pc.Name;
            actor.MapID = pc.MapID;
            actor.X = pc.X;
            actor.Y = pc.Y;
            actor.MaxHP = pc.MaxHP;
            actor.HP = pc.HP;
            actor.Speed = pc.Speed;
            actor.BaseData.range = 1;
            ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(actor);
            actor.e = eh;

            eh.AI.Mode = new SagaMap.Mob.AIMode(1);
            eh.AI.Master = pc;
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            map.SendVisibleActorsToActor(actor);
            eh.AI.Start();
        }

        private void ProcessGridMob(MapClient client, string args)
        {
            int number = 1;
            uint id = 0;
            if (args != "")
            {
                switch (args.Split(' ').Length)
                {
                    case 1:
                        number = 1;
                        id = uint.Parse(args);
                        break;
                    case 2:
                        id = uint.Parse(args.Split(' ')[0]);
                        number = int.Parse(args.Split(' ')[1]);
                        break;
                    default:
                        number = 1;
                        uint.Parse(args);
                        break;
                }
                try
                {
                    short X = client.Character.X;
                    short Y = client.Character.Y;
                    for (int x = X - number * 100; x <= X + number * 100; x += 100)
                    {
                        for (int y = Y - number * 100; y <= Y + number * 100; y += 100)
                        {
                            if (!(X == x && Y == y))
                            {
                                ActorMob m = client.map.SpawnMob(id,
                                    (short)(x),
                                    (short)(y),
                                    50,
                                    null);
                                MobEventHandler mh = (MobEventHandler)m.e;
                                mh.AI.Mode = new AIMode(4);
                            }
                        }
                    }
                }
                catch
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_MOB_ERROR);

                }
            }
        }

        private void ProcessMob(MapClient client, string args)
        {
            int number = 1;
            uint id = 0;
            int x = 0;
            int y = 0;
            if (args != "")
            {
                switch (args.Split(' ').Length)
                {
                    case 1:
                        number = 1;
                        id = uint.Parse(args);
                        break;
                    case 2:
                        id = uint.Parse(args.Split(' ')[0]);
                        number = int.Parse(args.Split(' ')[1]);
                        break;
                    case 4:
                        id = uint.Parse(args.Split(' ')[0]);
                        number = int.Parse(args.Split(' ')[1]);
                        x = int.Parse(args.Split(' ')[2]);
                        y = int.Parse(args.Split(' ')[3]);
                        break;
                    default:
                        number = 1;
                        uint.Parse(args);
                        break;
                }
                try
                {
                    if (args.Split(' ').Length == 4)
                    {
                        for (int i = 1; i <= number; i++)
                        {
                            short sx = Global.PosX8to16(byte.Parse(x.ToString()), client.map.Width);
                            short sy = Global.PosY8to16(byte.Parse(y.ToString()), client.map.Height);
                            client.map.SpawnMob(id,
                                sx,
                                sy,
                                2500,
                                null);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= number; i++)
                        {
                            client.map.SpawnMob(id,
                                (short)(client.Character.X + new Random().Next(1, 10)),
                                (short)(client.Character.Y + new Random().Next(1, 10)),
                                2500,
                                null);
                        }
                    }
                }
                catch (Exception)
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_MOB_ERROR);

                }
            }
        }

        private void ProcessItemTo(MapClient client, string args)
        {
            string name;
            int number;
            uint id = 0;
            uint picid = 0;
            //SagaLib.ClientManager.LeaveCriticalArea();
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            if (args.Split(' ').Length != 3)
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            try
            {
                name = args.Split(' ')[0];
                id = uint.Parse(args.Split(' ')[1]);
                number = int.Parse(args.Split(' ')[2]);
                Item item = ItemFactory.Instance.GetItem(id);
                MapClient cp = (MapClient)SagaMap.Manager.MapClientManager.Instance.GetClientForName(name);
                if (cp == null)
                {
                    client.SendSystemMessage("错误");
                    return;
                }
                if (item != null)
                {
                    item.Stack = (ushort)number;
                    if (picid != 0)
                    {
                        item.PictID = picid;
                    }
                    cp.AddItem(item, true);
                    client.SendSystemMessage("给" + name + " " + item.BaseData.name.ToString() + " " + number.ToString() + " 个");
                }
                else
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_NO_SUCH_ITEM);
                }
            }
            catch (Exception) { }
        }

        private void ProcessIDSearch(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_IDSEARCH);
                return;
            }

            try
            {
                var value = from x in ItemFactory.Instance.Items
                            where x.Value.name.Contains(args)
                            orderby x.Key descending
                            select new { ItemID = x.Key, ItemName = x.Value.name };


                var coll = value.ToList();
                if (coll.Count == 0)
                {
                    client.SendSystemMessage(string.Format("未找到任何道具名字包含: {0}", args));
                    return;
                }
                int max = coll.Count;
                if (max > 10)
                    max = 10;
                client.SendSystemMessage("-----------所查询的道具ID[最多只显示10条]-----------");
                for (int i = 0; i < max; i++)
                {
                    client.SendSystemMessage(string.Format("{0}. {1}   {2}", i + 1, coll[i].ItemName, coll[i].ItemID));
                }
                client.SendSystemMessage("----------------------------------------------------");
            }
            catch (Exception) { }
        }

        private void ProcessItem(MapClient client, string args)
        {
            int number;
            uint id = 0;
            uint picid = 0;
            //SagaLib.ClientManager.LeaveCriticalArea();
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            else if (args == "233")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        /*if (item.Value.itemType != ItemType.POTION && item.Value.itemType != ItemType.NONE && item.Value.itemType != ItemType.CSWAR_MARIO
                        && item.Value.itemType != ItemType.FOOD && item.Value.itemType != ItemType.SEED && item.Value.itemType != ItemType.FREESCROLL
                            && item.Value.itemType != ItemType.TREASURE_BOX && item.Value.itemType != ItemType.CONTAINER && item.Value.itemType != ItemType.TIMBER_BOX
                            && item.Value.itemType != ItemType.ARROW && item.Value.itemType != ItemType.USE && item.Value.itemType != ItemType.SCROLL
                        && item.Value.itemType != ItemType.STAMP && item.Value.itemType != ItemType.SCROLL && item.Value.itemType != ItemType.FG_GARDEN_MODELHOUSE
                            && item.Value.itemType != ItemType.FG_GARDEN_FLOOR && item.Value.itemType != ItemType.FG_ROOM_WALL && item.Value.itemType != ItemType.FURNITURE
                            && item.Value.itemType != ItemType.IRIS_CARD && item.Value.itemType != ItemType.DEMIC_CHIP)*/
                        if (item.Value.itemType == ItemType.IRIS_CARD)
                        {
                            Item i = new Item(item.Value);
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true);
                        }
                    }
                }
            }
            else if (args == "2333")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType != ItemType.POTION && item.Value.itemType != ItemType.NONE && item.Value.itemType != ItemType.CSWAR_MARIO
                        && item.Value.itemType != ItemType.FOOD && item.Value.itemType != ItemType.SEED && item.Value.itemType != ItemType.FREESCROLL
                            && item.Value.itemType != ItemType.TREASURE_BOX && item.Value.itemType != ItemType.CONTAINER && item.Value.itemType != ItemType.TIMBER_BOX
                            && item.Value.itemType != ItemType.ARROW && item.Value.itemType != ItemType.USE && item.Value.itemType != ItemType.SCROLL
                        && item.Value.itemType != ItemType.STAMP && item.Value.itemType != ItemType.SCROLL && item.Value.itemType != ItemType.FG_GARDEN_MODELHOUSE
                            && item.Value.itemType != ItemType.FG_GARDEN_FLOOR && item.Value.itemType != ItemType.FG_ROOM_WALL && item.Value.itemType != ItemType.FURNITURE
                            && item.Value.itemType != ItemType.IRIS_CARD && item.Value.itemType != ItemType.DEMIC_CHIP)
                        {
                            Item i = new Item(item.Value);
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true);
                        }
                    }
                }
            }
            else if (args == "23333")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (!(item.Value.itemType != ItemType.POTION
                        && item.Value.itemType != ItemType.FOOD && item.Value.itemType != ItemType.SEED
                            && item.Value.itemType != ItemType.ARROW && item.Value.itemType != ItemType.SCROLL
                        && item.Value.itemType != ItemType.STAMP))
                        {
                            Item i = new Item(item.Value);
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true);
                        }
                    }
                }
            }
            else if (args == "233333")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.NONE || item.Value.itemType == ItemType.USE)
                        {
                            Item i = new Item(item.Value);
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true);
                        }
                    }
                }
            }
            else if (args == "furniture")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.FURNITURE)
                        {
                            Item i = new Item(item.Value);
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true);
                        }
                    }
                }
            }
            else
            {
                try
                {

                    switch (args.Split(' ').Length)
                    {
                        case 1:
                            number = 1;
                            id = uint.Parse(args);
                            break;
                        case 2:
                            id = uint.Parse(args.Split(' ')[0]);
                            number = int.Parse(args.Split(' ')[1]);
                            break;
                        case 3:
                            id = uint.Parse(args.Split(' ')[0]);
                            number = int.Parse(args.Split(' ')[1]);
                            picid = uint.Parse(args.Split(' ')[2]);
                            break;
                        default:
                            number = 1;
                            uint.Parse(args);
                            break;
                    }

                    Item item = ItemFactory.Instance.GetItem(id);
                    if (item != null)
                    {
                        item.Stack = (ushort)number;
                        if (picid != 0)
                        {
                            item.PictID = picid;
                        }
                        client.AddItem(item, true);
                    }
                    else
                    {
                        client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_NO_SUCH_ITEM);
                    }
                }
                catch (Exception) { }
                //SagaLib.ClientManager.EnterCriticalArea();
            }

        }

        private void ProcessItem2(MapClient client, string args)
        {
            int number;
            uint id = 0;
            uint picid = 0;
            int refine = 0;
            int lifeench = 0;
            int powerench = 0;
            int critench = 0;
            int magench = 0;
            int carslot = 0;
            int identity = 0;
            //SagaLib.ClientManager.LeaveCriticalArea();
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            else
            {
                try
                {

                    switch (args.Split(' ').Length)
                    {
                        case 1:
                            number = 1;
                            id = uint.Parse(args);
                            break;
                        case 2:
                            id = uint.Parse(args.Split(' ')[0]);
                            number = int.Parse(args.Split(' ')[1]);
                            break;
                        case 3:
                            id = uint.Parse(args.Split(' ')[0]);
                            number = int.Parse(args.Split(' ')[1]);
                            picid = uint.Parse(args.Split(' ')[2]);
                            break;
                        case 9:
                            id = uint.Parse(args.Split(' ')[0]);
                            number = int.Parse(args.Split(' ')[1]);
                            refine = int.Parse(args.Split(' ')[2]);
                            identity = int.Parse(args.Split(' ')[3]);
                            carslot = int.Parse(args.Split(' ')[4]);
                            lifeench = int.Parse(args.Split(' ')[5]);
                            powerench = int.Parse(args.Split(' ')[6]);
                            critench = int.Parse(args.Split(' ')[7]);
                            magench = int.Parse(args.Split(' ')[8]);
                            break;
                        default:
                            number = 1;
                            uint.Parse(args);
                            break;
                    }

                    Item item = ItemFactory.Instance.GetItem(id);
                    if (item != null)
                    {
                        item.Stack = (ushort)number;
                        item.Refine = (ushort)refine;
                        item.Identified = identity == 1;
                        item.CurrentSlot = (byte)carslot;
                        item.LifeEnhance = (byte)lifeench;
                        item.PowerEnhance = (byte)powerench;
                        item.CritEnhance = (byte)critench;
                        item.MagEnhance = (byte)magench;
                        item.HP = StatusFactory.Instance.GetEnhanceBonus(item, 0);
                        item.Def = StatusFactory.Instance.GetEnhanceBonus(item, 1);
                        item.MDef = StatusFactory.Instance.GetEnhanceBonus(item, 3);
                        item.Atk1 = item.Atk2 = item.Atk3 = StatusFactory.Instance.GetEnhanceBonus(item, 1);
                        item.MAtk = StatusFactory.Instance.GetEnhanceBonus(item, 1);
                        item.HitCritical = StatusFactory.Instance.GetEnhanceBonus(item, 2);
                        item.AvoidCritical = StatusFactory.Instance.GetEnhanceBonus(item, 2);
                        if (picid != 0)
                        {
                            item.PictID = picid;
                        }
                        client.AddItem(item, true);
                        client.SendItemInfo(item);
                    }
                    else
                    {
                        client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_NO_SUCH_ITEM);
                    }
                }
                catch (Exception) { }
                //SagaLib.ClientManager.EnterCriticalArea();
            }

        }

        private void ProcessWho(MapClient client, string args)
        {
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
        }

        private void ProcessWho2(MapClient client, string args)
        {
            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
            {
                byte x, y;

                x = Global.PosX16to8(i.Character.X, i.map.Width);
                y = Global.PosY16to8(i.Character.Y, i.map.Height);
                client.SendSystemMessage(i.Character.Name + "(CharID:" + i.Character.CharID + ")[" + i.Map.Name + " " + i.Map.ID + " " + x.ToString() + "," + y.ToString() + "]");
            }
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
        }

        private void ProcessWho3(MapClient client, string args)
        {
            int ranged = -1;
            if (args != "")
            {
                try
                {
                    ranged = int.Parse(args);
                }
                catch (Exception)
                { }
            }
            try
            {
                List<Actor> actors = new List<Actor>();
                if (ranged == -1)
                {
                    foreach (var a in client.map.Actors)
                    {
                        actors.Add(a.Value);
                    }
                }
                else
                {
                    actors = client.map.GetActorsArea(client.Character, (short)(ranged * 100), true);
                }
                foreach (Actor act in actors)
                {
                    byte x, y;
                    x = Global.PosX16to8(act.X, client.map.Width);
                    y = Global.PosY16to8(act.Y, client.map.Height);
                    switch (act.type)
                    {
                        case ActorType.MOB:
                            ActorMob mob = (ActorMob)act;
                            client.SendSystemMessage(mob.BaseData.name + "(ActorID:" + mob.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.PC:
                            ActorPC pc = (ActorPC)act;
                            client.SendSystemMessage(pc.Name + "(ActorID:" + pc.ActorID + ")(CharID:" + pc.CharID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.PET:
                            ActorPet pet = (ActorPet)act;
                            client.SendSystemMessage(pet.BaseData.name + "(ActorID:" + pet.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.SHADOW:
                            ActorShadow sw = (ActorShadow)act;
                            client.SendSystemMessage(sw.Name + "(ActorID:" + sw.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.ITEM:
                            ActorItem itm = (ActorItem)act;
                            client.SendSystemMessage(itm.Name + "(ActorID:" + itm.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                    }
                }
                client.SendSystemMessage(string.Format("共：{0} 个Actors", actors.Count));
            }
            catch
            {

            }
        }

        private void ProcessGo(MapClient client, string args)
        {
            uint number = 0;
            uint mapid = 0; ;
            byte x = 0;
            byte y = 0;
            if (!uint.TryParse(args, out number))
            {
                if (args.ToLower() == "refine")
                {
                    mapid = 30082000;
                    x = 11;
                    y = 9;
                }
            }
            else
            {
                switch (number)
                {
                    case 1:
                        mapid = 10024000;
                        x = 127;
                        y = 141;
                        break;
                    case 2:
                        mapid = 10023000;
                        x = 127;
                        y = 144;
                        break;
                    default:
                        return;
                }
            }
            try
            {
                if (Configuration.Instance.HostedMaps.Contains(mapid))
                {
                    Map newMap = MapManager.Instance.GetMap(mapid);
                    client.Map.SendActorToMap(client.Character, mapid, Global.PosX8to16(x, newMap.Width), Global.PosY8to16(y, newMap.Height));
                }
            }
            catch (Exception)
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_WARP_ERROR);
            }
        }

        private void ProcessFG(MapClient client, string args)
        {
            Skill.SkillHandler.Instance.ShowVessel(client.Character, -100);
            if (client.Character.FGarden == null)
                client.Character.FGarden = new SagaDB.FGarden.FGarden(client.Character);
            Item item = ItemFactory.Instance.GetItem(10022700);
            item.Stack = 1;
            item.Identified = true;
            client.AddItem(item, true);
        }

        private void ProcessWarp(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg[0] == "" || arg[1] == "" || arg[2] == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_WARP_PARA);
            }
            else
            {
                try
                {
                    uint mapid = 0;
                    if (!uint.TryParse(arg[0], out mapid))
                    {
                        mapid = MapManager.Instance.Maps.FirstOrDefault(m => m.Value.Name == arg[0]).Key;
                    }
                    byte x = byte.Parse(arg[1]);
                    byte y = byte.Parse(arg[2]);
                    if (Configuration.Instance.HostedMaps.Contains(mapid))
                    {
                        Map newMap = MapManager.Instance.GetMap(mapid);
                        client.Map.SendActorToMap(client.Character, mapid, Global.PosX8to16(x, newMap.Width), Global.PosY8to16(y, newMap.Height));
                    }
                }
                catch (Exception)
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_WARP_ERROR);

                }
            }
        }

        private void ProcessPCall(MapClient client, string args)
        {

        }

        private void ProcessHair(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg[0] == "" || arg[1] == "" || arg[2] == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HAIR_PAEA);
            }
            else
            {
                try
                {
                    ushort haire = ushort.Parse(arg[0]);
                    ushort wig = ushort.Parse(arg[1]);
                    byte color = byte.Parse(arg[2]);
                    client.Character.HairStyle = (haire);
                    client.Character.Wig = (wig);
                    client.Character.HairColor = (color);
                    client.SendCharInfoUpdate();
                }
                catch (Exception)
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HAIR_ERROR);
                }
            }
        }

        private void ProcessHairext(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HAIREXT_PARA);
            }
            else
            {
                try
                {
                    byte style = byte.Parse(args);
                    if (style >= 1 && style <= 52)
                    {
                        client.Character.Wig = (byte)(style - 1);
                        client.SendCharInfoUpdate();
                    }
                }
                catch (Exception) { }
            }
        }

        private void ProcessLevel(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_LEVEL_PARA);
            }
            else
            {
                try
                {
                    byte lv = byte.Parse(args);
                    client.Character.CEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.CLEVEL) + 1;
                    ExperienceManager.Instance.CheckLvUp(client, SagaMap.Scripting.LevelType.CLEVEL);
                    client.Character.Level = lv;
                    client.SendEXP();
                    client.SendPlayerLevel();
                }
                catch (Exception) { }
            }
        }

        private void ProcessJobLevel(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_LEVEL_PARA);
            }
            else
            {
                try
                {
                    byte lv = byte.Parse(args);
                    if (client.Character.Job == client.Character.JobBasic)
                    {
                        client.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.JLEVEL) + 1;
                        ExperienceManager.Instance.CheckLvUp(client, SagaMap.Scripting.LevelType.JLEVEL);
                        client.Character.JobLevel1 = lv;

                    }
                    else if (client.Character.Job == client.Character.Job2X || client.Character.Job == client.Character.Job2T)
                    {
                        client.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.JLEVEL2) + 1;
                        ExperienceManager.Instance.CheckLvUp(client, SagaMap.Scripting.LevelType.JLEVEL2);
                        if (client.Character.Job == client.Character.Job2X)
                            client.Character.JobLevel2X = lv;
                        else
                            client.Character.JobLevel2T = lv;

                    }
                    else
                    {
                        client.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.JLEVEL3) + 1;
                        ExperienceManager.Instance.CheckLvUp(client, SagaMap.Scripting.LevelType.JLEVEL3);
                        client.Character.JobLevel3 = lv;
                    }
                    client.SendEXP();
                    client.SendPlayerLevel();
                }
                catch (Exception) { }
            }
        }

        private void ProcessHaircolor(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HAIRCOLOR_PARA);
            }
            else
            {
                try
                {
                    uint style = uint.Parse(args);

                    if (client.Character.HairStyle == 90 || client.Character.HairStyle == 91 || client.Character.HairStyle == 92)
                    {
                        client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HAIRCOLOR_ERROR);    //
                    }
                    else
                    {

                        switch (args)
                        {
                            case "1":
                                client.Character.HairColor = 0;
                                client.SendCharInfoUpdate();
                                break;
                            case "2":
                                client.Character.HairColor = 1;
                                client.SendCharInfoUpdate();
                                break;
                            case "3":
                                client.Character.HairColor = 2;
                                client.SendCharInfoUpdate();
                                break;
                            case "4":
                                client.Character.HairColor = 3;
                                client.SendCharInfoUpdate();
                                break;
                            case "5":
                                client.Character.HairColor = 4;
                                client.SendCharInfoUpdate();
                                break;
                            case "6":
                                client.Character.HairColor = 5;
                                client.SendCharInfoUpdate();
                                break;
                            case "7":
                                client.Character.HairColor = 6;
                                client.SendCharInfoUpdate();
                                break;
                            case "8":
                                client.Character.HairColor = 7;
                                client.SendCharInfoUpdate();
                                break;
                            case "9":
                                client.Character.HairColor = 8;
                                client.SendCharInfoUpdate();
                                break;
                            case "10":
                                client.Character.HairColor = 9;
                                client.SendCharInfoUpdate();
                                break;
                            case "11":
                                client.Character.HairColor = 10;
                                client.SendCharInfoUpdate();
                                break;
                            case "12":
                                client.Character.HairColor = 11;
                                client.SendCharInfoUpdate();
                                break;
                            case "13":
                                client.Character.HairColor = 12;
                                client.SendCharInfoUpdate();
                                break;
                            case "14":
                                client.Character.HairColor = 50;
                                client.SendCharInfoUpdate();
                                break;
                            case "15":
                                client.Character.HairColor = 51;
                                client.SendCharInfoUpdate();
                                break;
                            case "16":
                                client.Character.HairColor = 52;
                                client.SendCharInfoUpdate();
                                break;
                            case "17":
                                client.Character.HairColor = 60;
                                client.SendCharInfoUpdate();
                                break;
                            case "18":
                                client.Character.HairColor = 61;
                                client.SendCharInfoUpdate();
                                break;
                            case "19":
                                client.Character.HairColor = 62;
                                client.SendCharInfoUpdate();
                                break;
                            case "20":
                                client.Character.HairColor = 70;
                                client.SendCharInfoUpdate();
                                break;
                            case "21":
                                client.Character.HairColor = 71;
                                client.SendCharInfoUpdate();
                                break;
                            case "22":
                                client.Character.HairColor = 72;
                                client.SendCharInfoUpdate();
                                break;
                        }
                    }

                }
                catch (Exception) { }
            }
        }

        private void ProcessHairstyle(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HAIRSTYLE_PARA);
            }
            else
            {
                try
                {
                    uint style = uint.Parse(args);

                    switch (args)
                    {
                        case "1":
                            client.Character.HairStyle = 90;
                            client.Character.HairColor = 0;
                            client.SendCharInfoUpdate();
                            break;
                        case "2":
                            client.Character.HairStyle = 91;
                            client.Character.HairColor = 0;
                            client.SendCharInfoUpdate();
                            break;
                        case "3":
                            client.Character.HairStyle = 92;
                            client.Character.HairColor = 0;
                            client.SendCharInfoUpdate();
                            break;
                        case "4":
                            client.Character.HairStyle = 2;
                            client.SendCharInfoUpdate();
                            break;
                        case "5":
                            client.Character.HairStyle = 6;
                            client.SendCharInfoUpdate();
                            break;
                        case "6":
                            client.Character.HairStyle = 11;
                            client.SendCharInfoUpdate();
                            break;
                        case "7":
                            client.Character.HairStyle = 12;
                            client.SendCharInfoUpdate();
                            break;
                        case "8":
                            client.Character.HairStyle = 13;
                            client.SendCharInfoUpdate();
                            break;
                        case "9":
                            client.Character.HairStyle = 14;
                            client.SendCharInfoUpdate();
                            break;
                        case "10":
                            client.Character.HairStyle = 15;
                            client.SendCharInfoUpdate();
                            break;
                        case "11":
                            client.Character.HairStyle = 16;
                            client.SendCharInfoUpdate();
                            break;
                        case "12":
                            client.Character.HairStyle = 17;
                            client.SendCharInfoUpdate();
                            break;
                        case "13":
                            client.Character.HairStyle = 18;
                            client.SendCharInfoUpdate();
                            break;
                        case "14":
                            client.Character.HairStyle = 19;
                            client.SendCharInfoUpdate();
                            break;
                        case "15":
                            client.Character.HairStyle = 20;
                            client.SendCharInfoUpdate();
                            break;
                        //not working (3,4,5,7,8,9,)
                    }

                }
                catch (Exception) { }
            }
        }

        private void ProcessPlayersize(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_PLAYERSIZE_PARA);
            }
            else
            {
                try
                {
                    uint playersize = uint.Parse(args);
                    client.Character.Size = playersize;
                    client.SendPlayerSizeUpdate();

                }
                catch (Exception) { }
            }
        }

        private void ProcessShowStatus(MapClient client, string args)
        {
            try
            {
                client.SendSystemMessage("------------------Status----------------");
                Status s = client.Character.Status;
                client.SendSystemMessage(string.Format("mp_recover_skill:{0}", s.mp_recover_skill));
                client.SendSystemMessage("----------------------------------------");
            }
            catch (Exception) { }
        }

        private void ProcessGold(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_GOLD_PARA);
            }
            else
            {
                try
                {
                    uint gold = uint.Parse(args);
                    client.Character.Gold = (long)gold;
                    client.SendGoldUpdate();
                }
                catch (Exception) { }
            }
        }

        private void ProcessHide(MapClient client, string args)
        {
            Actor actor = client.Character;
            actor.Buff.Transparent = !client.Character.Buff.Transparent;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private void ProcessShoppoint(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_SHOPPOINT_PARA);
            }
            else
            {
                try
                {
                    uint shopp = uint.Parse(args);
                    client.Character.VShopPoints = shopp;
                }
                catch (Exception) { }
            }
        }

        private void ProcessHeal(MapClient client, string args)
        {
            client.Character.HP = client.Character.MaxHP;
            client.Character.MP = client.Character.MaxMP;
            client.Character.SP = client.Character.MaxSP;
            client.Character.EP = client.Character.MaxEP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, client.Character, true);
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_HEAL_MESSAGE);
        }

        private void ProcessSkill(MapClient client, string args)
        {
            byte lv = 0;
            uint id = 0;
            if (args != "")
            {
                switch (args.Split(' ').Length)
                {
                    case 1:
                        id = uint.Parse(args);
                        break;
                    case 2:
                        id = uint.Parse(args.Split(' ')[0]);
                        lv = byte.Parse(args.Split(' ')[1]);
                        break;
                    default:
                        uint.Parse(args);
                        break;
                }
                try
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(id, lv);
                    if (skill == null) return;
                    if (lv == 0)
                    {
                        skill.Level = skill.MaxLevel;
                    }
                    client.Character.Skills.Add(id, skill);
                    PC.StatusFactory.Instance.CalcStatus(client.Character);
                    client.SendPlayerInfo();

                }
                catch (Exception)
                {
                }
            }
        }

        private void ProcessSkillALL(MapClient client, string args)
        {
            if (args != "")
                return;
            if (client.Character.JobBasic != PC_JOB.NONE && client.Character.JobBasic != PC_JOB.NOVICE)
            {
                foreach (var item in SkillFactory.Instance.SkillList(client.Character.JobBasic).Keys)
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(item, 0);
                    if (skill == null)
                        continue;
                    skill.Level = skill.MaxLevel;
                    if (client.Character.Skills.ContainsKey(item))
                        client.Character.Skills.Remove(item);
                    client.Character.Skills.Add(item, skill);
                }
            }
            client.Character.SkillPoint = 0;
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
            if (client.Character.Job2X != PC_JOB.NONE && client.Character.Job2X != PC_JOB.NOVICE)
            {
                foreach (var item in SkillFactory.Instance.SkillList(client.Character.Job2X).Keys)
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(item, 0);
                    if (skill == null)
                        continue;
                    skill.Level = skill.MaxLevel;
                    if (client.Character.Skills2_1.ContainsKey(item))
                        client.Character.Skills2_1.Remove(item);
                    client.Character.Skills2_1.Add(item, skill);
                }
            }
            client.Character.SkillPoint2X = 0;
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
            if (client.Character.Job2T != PC_JOB.NONE && client.Character.Job2T != PC_JOB.NOVICE)
            {
                foreach (var item in SkillFactory.Instance.SkillList(client.Character.Job2T).Keys)
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(item, 0);
                    if (skill == null)
                        continue;
                    skill.Level = skill.MaxLevel;
                    if (client.Character.Skills2_2.ContainsKey(item))
                        client.Character.Skills2_2.Remove(item);
                    client.Character.Skills2_2.Add(item, skill);
                }
            }
            client.Character.SkillPoint2T = 0;
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
            if (client.Character.Job3 != PC_JOB.NONE && client.Character.Job3 != PC_JOB.NOVICE)
            {
                foreach (var item in SkillFactory.Instance.SkillList(client.Character.Job3).Keys)
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(item, 0);
                    if (skill == null)
                        continue;
                    skill.Level = skill.MaxLevel;
                    if (client.Character.Skills3.ContainsKey(item))
                        client.Character.Skills3.Remove(item);
                    client.Character.Skills3.Add(item, skill);
                }
            }
            client.Character.SkillPoint3 = 0;
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
        }

        private void ProcessSkillClear(MapClient client, string args)
        {
            int type = 0;
            if (args != "")
            {
                type = int.Parse(args);
            }
            switch (type)
            {
                case 0://all
                    client.Character.Skills.Clear();
                    client.Character.Skills2.Clear();
                    client.Character.Skills2_1.Clear();
                    client.Character.Skills2_2.Clear();
                    client.Character.Skills3.Clear();
                    client.Character.SkillsReserve.Clear();
                    break;
                case 1: //1轉
                    client.Character.Skills.Clear();
                    break;
                case 2: //2轉
                    client.Character.Skills2.Clear();
                    client.Character.Skills2_1.Clear();
                    client.Character.Skills2_2.Clear();
                    break;
                case 3:
                    client.Character.Skills3.Clear();
                    break;
                case 4: //保留技能
                    client.Character.SkillsReserve.Clear();
                    break;
            }
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
        }

        private void ProcessAnnounce(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ANNOUNCE_PARA);
            }
            else
            {
                try
                {
                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                    {
                        i.SendAnnounce(args);
                    }

                }
                catch (Exception) { }
            }
        }

        private void ProcessTrumpet(MapClient client, string args)
        {
            if (args == "")
            {
                //client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ANNOUNCE_PARA);
                client.SendSystemMessage("Useage: /w message (this function requirs 50Gold per once)");
            }
            else
            {
                if (client.Character.Gold >= 50)
                {
                    client.Character.Gold -= 50;
                    try
                    {
                        foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                        {
                            Packets.Server.SSMG_CHAT_WHOLE p = new SagaMap.Packets.Server.SSMG_CHAT_WHOLE();
                            p.Sender = "[!]" + client.Character.Name;
                            p.Content = args;
                            i.netIO.SendPacket(p);
                        }

                    }
                    catch (Exception) { }
                }
                else
                    client.SendSystemMessage("this function requirs 50Gold per once.");
            }
        }

        private void ProcessTweet(MapClient client, string args)
        {
            string TweetID = Configuration.Instance.TwitterID;
            string TweetPass = Configuration.Instance.TwitterPasswd;
            string name = client.Character.Name;
            int namesize = name.Length;
            int argssize = args.Length;
            int allsize = namesize + argssize;
            if (TweetID == null || TweetPass == null)
            {
            }
            if (args == "")
            {
                client.SendSystemMessage("Error: NoTweetComment");
            }
            else if (allsize >= 140)
            {
                client.SendSystemMessage("Error: TweetSizeOver");
            }
            else
            {
                try
                {
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("UTF-8");
                    string user = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(TweetID + ":" + TweetPass));
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes("status=" + name + ":" + args);

                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create("http://twitter.com/statuses/update.xml");

                    request.Method = "POST";
                    request.ServicePoint.Expect100Continue = false;

                    request.UserAgent = "SagaECOJP";
                    request.Headers.Add("Authorization", "Basic " + user);
                    request.ContentType = "application/x-www-form-urlencoded";

                    request.ContentLength = bytes.Length;


                    Stream reqStream = request.GetRequestStream();

                    reqStream.Write(bytes, 0, bytes.Length);

                    reqStream.Close();

                    try
                    {
                        foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                        {
                            Packets.Server.SSMG_CHAT_WHOLE p = new SagaMap.Packets.Server.SSMG_CHAT_WHOLE();
                            p.Sender = "Tweet";
                            p.Content = name + ":" + args;
                            i.netIO.SendPacket(p);
                        }

                    }
                    catch (Exception) { }

                }
                catch (Exception) { }
            }
        }

        private void ProcessCh(MapClient client, string args)
        {


        }

        private void ProcessEquipList(MapClient client, string args)
        {
            ActorPC pc = client.Character;
            Dictionary<EnumEquipSlot, Item> equips;
            if (pc.Form == DEM_FORM.NORMAL_FORM)
                equips = pc.Inventory.Equipments;
            else
                equips = pc.Inventory.Parts;

            if (equips.Count == 0)
            {
                client.SendSystemMessage("角色身上没有装备任何东西哦~");
                return;
            }
            client.SendSystemMessage("-------------[装备清单]--------------");
            foreach (SagaDB.Item.EnumEquipSlot item in equips.Keys)
            {
                Item i = equips[item];
                client.SendSystemMessage(string.Format("{0}: {1}   {2}", item.ToString(), ItemFactory.Instance.Items[i.ItemID].name, i.ItemID));
            }
            client.SendSystemMessage("-------------------------------------");
        }

        private void ProcessInventoryList(MapClient client, string args)
        {
            List<Item> list;
            Inventory inventory = client.Character.Inventory;
            list = inventory.Items[ContainerType.BODY];
            if (list.Count > 0)
            {
                client.SendSystemMessage("-------------[身体清单]--------------");
                foreach (var item in list)
                {
                    if (item.ItemID != 0)
                        client.SendSystemMessage(string.Format("{0}: {1}   堆叠:{2}   价值:{3}", ItemFactory.Instance.Items[item.ItemID].name, item.ItemID, item.Stack, ItemFactory.Instance.Items[item.ItemID].price));
                }
            }
            list = inventory.Items[ContainerType.RIGHT_BAG];
            if (list.Count > 0)
            {
                client.SendSystemMessage("-------------[右手清单]--------------");
                foreach (var item in list)
                {
                    if (item.ItemID != 0)
                        client.SendSystemMessage(string.Format("{0}: {1}   堆叠:{2}   价值:{3}", ItemFactory.Instance.Items[item.ItemID].name, item.ItemID, item.Stack, ItemFactory.Instance.Items[item.ItemID].price));
                }
            }
            list = inventory.Items[ContainerType.LEFT_BAG];
            if (list.Count > 0)
            {
                client.SendSystemMessage("-------------[左手清单]--------------");
                foreach (var item in list)
                {
                    if (item.ItemID != 0)
                        client.SendSystemMessage(string.Format("{0}: {1}   堆叠:{2}   价值:{3}", ItemFactory.Instance.Items[item.ItemID].name, item.ItemID, item.Stack, ItemFactory.Instance.Items[item.ItemID].price));
                }
            }
            list = inventory.Items[ContainerType.BACK_BAG];
            if (list.Count > 0)
            {
                client.SendSystemMessage("-------------[背包清单]--------------");
                foreach (var item in list)
                {
                    if (item.ItemID != 0)
                        client.SendSystemMessage(string.Format("{0}: {1}   堆叠:{2}   价值:{3}", ItemFactory.Instance.Items[item.ItemID].name, item.ItemID, item.Stack, ItemFactory.Instance.Items[item.ItemID].price));
                }
            }
        }

        private void ProcessFame(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage("请输入声望的变化值");
                return;
            }
            int value = 0;
            if (args.Split(' ').Length != 1 || !int.TryParse(args, out value))
            {
                client.SendSystemMessage("命令格式不正确");
                return;
            }
            client.Character.Fame += (uint)value;
            client.SendSystemMessage("当前的声望为: " + client.Character.Fame);
        }

        private void ProcessRWarp(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (string.IsNullOrEmpty(arg[0]) || string.IsNullOrEmpty(arg[1]))
            {
                client.SendSystemMessage("用法 !rwarp Relative-x Relative-y");
                return;
            }
            short xshort, yshort;
            byte x, y;
            bool isNegativeX, isNegativeY;
            if (short.TryParse(arg[0], out xshort) && short.TryParse(arg[1], out yshort))
            {
                if (xshort < 0)
                    isNegativeX = true;
                else isNegativeX = false;
                if (yshort < 0)
                    isNegativeY = true;
                else isNegativeY = false;
                arg[0]= arg[0].Replace("-","");
                arg[1]= arg[1].Replace("-", "");
                if (byte.TryParse(arg[0], out x) && byte.TryParse(arg[1], out y))
                {
                    byte NewX = Global.PosX16to8(client.Character.X, client.Map.Width), NewY = Global.PosY16to8(client.Character.Y, client.Map.Height);
                    if (isNegativeX)
                        NewX -= x;
                    else NewX += x;
                    if (isNegativeY)
                        NewY -= y;
                    else NewY += y;
                    client.Map.TeleportActor(client.Character, Global.PosX8to16(NewX, client.Map.Width), Global.PosY8to16(NewY, client.Map.Height));
                }
            }
        }

        private void ProcessCEXPGainRate (MapClient client, string rate)
        {
            float Rate = float.Parse(rate);
            if (Rate < 0)
                return;
            client.Character.CEXPGainRate= Rate;
        }

        private void ProcessJEXPGainRate(MapClient client, string rate)
        {
            float Rate = float.Parse(rate);
            if (Rate < 0)
                return;
            client.Character.JEXPGainRate = Rate;
        }

        #region "Admin commands"


        private void ProcessMonsterInfo(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage("this command require a monster id/name");
            }
            else
            {
                int id = 0;
                MobData md = null;
                if (!int.TryParse(args, out id))
                    md = MobFactory.Instance.Mobs.First(x => x.Value.name == args).Value;
                else
                    md = MobFactory.Instance.Mobs.First(x => x.Value.id == uint.Parse(args)).Value;
                if (md != null)
                {
                    client.SendSystemMessage(string.Format("编号为:{0} 的魔物:{1} 的数据如下", md.id, md.name));
                    client.SendSystemMessage(string.Format("等级:{0} 视野:{1} 种族:{2} 飞行:{3}", md.level, md.range, md.mobType, md.fly));
                    client.SendSystemMessage(string.Format("最大HP:{0} 最大SP:{1} 最大MP:{2} AI模式:{3}", md.hp, md.sp, md.mp, md.aiMode));
                    client.SendSystemMessage(string.Format("STR:{0} AGI:{1} VIT:{2} INT:{3} DEX:{4} MAG:{5}", md.str, md.agi, md.vit, md.intel, md.dex, md.mag));
                    client.SendSystemMessage(string.Format("咏唱速度:{0} 攻击速度:{1} 移动速度:{2}", md.cspd, md.aspd, md.speed));
                    client.SendSystemMessage(string.Format("物理攻击力:{0}-{1} 魔法攻击力:{2}-{3} 命中力: {4}-{5}", md.atk_min, md.atk_max, md.matk_min, md.matk_max, md.hit_melee, md.hit_ranged));
                    client.SendSystemMessage(string.Format("物理防御:{0}-{1} 魔法防御:{2}-{3},回避:{4}-{5}", md.def, md.def_add, md.mdef, md.mdef_add, md.avoid_melee, md.avoid_magic));
                    client.SendSystemMessage(string.Format("是否为BOSS:{0} 暴击发动:{1} 暴击回避:{2}", md.mobType.ToString().Contains("BOSS"), md.cri, md.criavd));
                    client.SendSystemMessage("----------------一般掉落---------------");
                    if (md.dropItems.Count == 0)
                    {
                        client.SendSystemMessage("此魔物无一般掉落...");
                    }
                    else
                    {
                        for (int i = 0; i < md.dropItems.Count; i++)
                        {
                            client.SendSystemMessage(string.Format("掉落{0}: {1}|{2} - {3}%", i + 1, md.dropItems[i].ItemID, ItemFactory.Instance.Items.FirstOrDefault(x => x.Key == md.dropItems[i].ItemID).Value.name, (float)(md.dropItems[i].Rate / 100.0f)));
                        }
                    }
                    client.SendSystemMessage("----------------特殊掉落---------------");
                    if (md.dropItemsSpecial.Count == 0)
                    {
                        client.SendSystemMessage("此魔物无特殊掉落...");
                    }
                    else
                    {
                        for (int i = 0; i < md.dropItemsSpecial.Count; i++)
                        {
                            client.SendSystemMessage(string.Format("掉落{0}: {1}|{2} - {3}%", i + 1, md.dropItemsSpecial[i].ItemID, ItemFactory.Instance.Items.FirstOrDefault(x => x.Key == md.dropItemsSpecial[i].ItemID).Value.name, (float)(md.dropItemsSpecial[i].Rate / 100.0f)));
                        }
                    }
                    //client.SendSystemMessage("------------------掉落---------------");
                    //client.SendSystemMessage(string.Format("{0}|{1}\t{2}|{3}\t{4}|{5}\t{6}|{7}\t{8}|{9}\t{10}|{11}",
                    //    md.dropItems[0].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[0].ItemID).Value.name,
                    //    md.dropItems[1].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[1].ItemID).Value.name,
                    //    md.dropItems[2].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[2].ItemID).Value.name,
                    //    md.dropItems[3].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[3].ItemID).Value.name,
                    //    md.dropItems[4].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[4].ItemID).Value.name));
                    //client.SendSystemMessage(string.Format("{0}|{1}\t{2}|{3}\t{4}|{5}\t{6}|{7}\t{8}|{9}\t{10}|{11}",
                    //    md.dropItems[5].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[5].ItemID).Value.name,
                    //    md.dropItems[6].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[6].ItemID).Value.name,
                    //    md.dropItems[7].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[7].ItemID).Value.name,
                    //    md.dropItems[8].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[8].ItemID).Value.name,
                    //    md.dropItems[9].ItemID, ItemFactory.Instance.Items.First(x => x.Key == md.dropItems[9].ItemID).Value.name));
                }
                else
                    client.SendSystemMessage("this monster is not exists.");
            }
        }

        private void ProcessKick(MapClient client, string playername)
        {
            if (playername == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_KICK_PARA);
            }
            else
            {
                try
                {
                    var chr =
                    from c in MapClientManager.Instance.OnlinePlayer
                    where c.Character.Name == playername
                    select c;
                    client = chr.First();
                    client.netIO.Disconnect();
                }
                catch (Exception) { }
            }
        }

        private void ProcessStatReset(MapClient client, string args)
        {
            client.ResetStatusPoint();
        }

        private void ProcessSkillReset(MapClient client, string args)
        {
            client.ResetSkill(1);
            client.ResetSkill(2);
            client.SendPlayerInfo();
        }
        void ProcessODWarStart(MapClient client, string arg)
        {
            uint map = uint.Parse(arg);
            Tasks.System.ODWar.Instance.StartODWar(map);
        }

        void ProcessKillAllMob(MapClient client, string arg)
        {
            bool loot = false;
            if (arg == "1")
                loot = true;
            List<Actor> actors = client.map.Actors.Values.ToList();
            int count = 0;
            foreach (Actor i in actors)
            {
                if (i.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler eh = (MobEventHandler)i.e;
                    i.Buff.PlayingDead = true;
                    eh.OnDie(loot);
                    count++;
                }
            }
            client.SendSystemMessage(count.ToString() + " mobs killed");
        }

        void ProcessKickGolem(MapClient client, string arg)
        {
            ClientManager.LeaveCriticalArea();
            try
            {
                foreach (Actor j in client.map.Actors.Values)
                {
                    if (j.type == ActorType.GOLEM)
                    {
                        try
                        {
                            ActorGolem golem = (ActorGolem)j;
                            if (golem.GolemType >= GolemType.Plant && golem.GolemType <= GolemType.Strange)
                            {
                                ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)golem.e;
                                golem.e = new ActorEventHandlers.NullEventHandler();
                                eh.AI.Pause();
                            }
                            golem.invisble = true;
                            client.map.OnActorVisibilityChange(golem);
                            golem.ClearTaskAddition();
                            MapServer.charDB.SaveChar(golem.Owner, false);
                        }
                        catch { }
                    }
                }
            }
            catch { }
            ClientManager.EnterCriticalArea();
        }

        private void ProcessKickAll(MapClient client, string args)
        {
            if (args != "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_KICKALL_PARA);
            }
            else
            {
                try
                {
                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                    {
                        i.netIO.Disconnect();

                    }

                }
                catch (Exception) { }
            }
        }

        private void ProcessSpawn(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg.Length < 4)
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_SPAWN_PARA);
                return;
            }
            System.IO.FileStream fs = new System.IO.FileStream("autospawn.xml", System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            Map map = MapManager.Instance.GetMap(client.Character.MapID);
            sw.WriteLine("  <spawn>");
            sw.WriteLine(string.Format("    <id>{0}</id>", arg[0]));
            sw.WriteLine(string.Format("    <map>{0}</map>", client.Character.MapID));
            sw.WriteLine(string.Format("    <x>{0}</x>", Global.PosX16to8(client.Character.X, map.Width)));
            sw.WriteLine(string.Format("    <y>{0}</y>", Global.PosY16to8(client.Character.Y, map.Height)));
            sw.WriteLine(string.Format("    <amount>{0}</amount>", arg[1]));
            sw.WriteLine(string.Format("    <range>{0}</range>", arg[2]));
            sw.WriteLine(string.Format("    <delay>{0}</delay>", arg[3]));
            sw.WriteLine("  </spawn>");
            sw.Flush();
            fs.Flush();
            fs.Close();
            client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ATCOMMAND_SPAWN_SUCCESS, arg[0], arg[1], arg[2], arg[3]));
        }

        private void ProcessCallMap(MapClient client, string args)
        {
            if (args == "")
                client.SendSystemMessage("用法 !RecallMap MapID");
            else
                foreach (var item in MapClientManager.Instance.OnlinePlayer)
                {
                    if (item.Map.ID == args[0])
                        item.Map.SendActorToMap(item.Character, client.Map.ID, client.Character.X, client.Character.Y);
                }
        }

        //Be careful with this command
        private void ProcessCallAll(MapClient client, string args)
        {
            foreach (var item in MapClientManager.Instance.OnlinePlayer)
            {
                item.Map.SendActorToMap(item.Character, client.Map.ID, client.Character.X, client.Character.Y);
            }
        }


        #endregion

        #region "Dev commands"

        private void ProcessRaw(MapClient client, string args)
        {
            if (args == "")
                args = "02 17 13 01 A4 02 0A 01 C7 02 0A 02 0F 01 DB 02 0F 00 10 00 1C 00 1C 00 1E 00 1B 00 06 01 05 00 C8 03 20 03 20 01 D2 01 2D";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            string fuck = "";
            for (int i = 0; i < buf.Length; i++)
            {
                fuck += buf[i].ToString() + " ";
            }
            Packet p = new Packet();
            p.data = buf;
            client.netIO.SendPacket(p);

            //client.Map.SendActorToMap(client.Character, 90000001,0,0);

        }

        private void ProcessTest(MapClient client, string args)
        {
            /*Packets.Server.SSMG_THEATER_INFO p = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
            p.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MESSAGE;
            p.Message = "欢迎光临！";
            client.netIO.SendPacket(p);
            p = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
            p.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MOVIE_ADDRESS;
            p.Message = "mms://www.sagaeco.com/clannad";
            client.netIO.SendPacket(p);
            p = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
            p.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.PLAY;
            p.Message = "";
            client.netIO.SendPacket(p);*/
            Packet p = new Packet(24);
            p.ID = 0x1dc5;
            p.offset += 6;
            p.PutByte(7);
            client.netIO.SendPacket(p);
            p = new Packet(38);
            p.ID = 0x1dc5;
            p.PutByte(1);
            p.offset += 5;
            p.PutByte(7);
            p.PutUShort(1);
            p.PutUShort(2);
            p.PutUShort(3);
            p.PutUShort(4);
            p.PutUShort(5);
            p.PutUShort(6);
            p.PutUShort(7);
            p.PutByte(7);
            p.PutUShort(1);
            p.PutUShort(2);
            p.PutUShort(3);
            p.PutUShort(4);
            p.PutUShort(5);
            p.PutUShort(6);
            p.PutUShort(7);
            client.netIO.SendPacket(p);
            p = new Packet(59);
            p.ID = 0x1dc5;
            p.PutByte(2);
            p.PutByte(1);
            p.PutUInt(1);
            p.PutByte(1);
            p.PutUShort(1);
            p.PutByte(1);
            p.PutByte(1);
            p.PutByte(1);
            p.PutUInt(1);
            p.PutByte(1);
            p.PutUInt(1);
            p.PutByte(7);
            p.PutUShort(1);
            p.PutUShort(2);
            p.PutUShort(9);
            p.PutUShort(4);
            p.PutUShort(5);
            p.PutUShort(6);
            p.PutUShort(7);
            p.PutByte(7);
            p.PutUShort(1);
            p.PutUShort(2);
            p.PutUShort(3);
            p.PutUShort(4);
            p.PutUShort(5);
            p.PutUShort(6);
            p.PutUShort(7);
            client.netIO.SendPacket(p);
        }
        private void ProcessFace(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage("用法 !face ID");
            }
            else
            {
                ushort face = ushort.Parse(args);
                client.Character.Face = face;
                //client.SendCharInfo();
                client.SendCharInfoUpdate();
            }
        }
        private void ProcessCreateFF(MapClient client, string args)
        {
            if (client.Character.FGarden == null)//如果當前帳號還沒創建過飛空庭
                client.Character.FGarden = new SagaDB.FGarden.FGarden(client.Character);//創建新的飛空庭

            if (client.Character.Ring == null)
            {
                RingManager.Instance.CreateRing(client.Character, args);
            }
            /*-------------------服務器專有--------------------
            MapClient s = MapClient.FromActorPC(ScriptManager.Instance.VariableHolder);
            SagaDB.Ring.Ring ring = RingManager.Instance.CreateRing(ScriptManager.Instance.VariableHolder, "番茄會");
            if (s.Character.Ring.FFarden == null)
            {
                s.Character.Ring.FFarden = new SagaDB.FFarden.FFarden();
                s.Character.Ring.FFarden.Name = args;
                s.Character.Ring.FFarden.RingID = s.Character.Ring.ID;
                s.Character.Ring.FFarden.ObMode = 3;
                s.Character.Ring.FFarden.Content = "飛空城";
                SagaDB.FFarden.FFarden r = new SagaDB.FFarden.FFarden();
                s.Character.Ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.GARDEN, new List<ActorFurniture>());
                s.Character.Ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.ROOM, new List<ActorFurniture>());
                MapServer.charDB.CreateFF(s.Character);
            }
            /*-------------------服務器專有--------------------*/

            if (args == "")
            {
                client.SendSystemMessage("请输入飞空城名字");
            }
            else if (client.Character.Ring == null)
            {
                client.SendSystemMessage("没有工会");
            }
            else if (client.Character.Ring.Leader != client.Character)
            {
                client.SendSystemMessage("不是工会队长");
            }
            else
            {
                if (client.Character.Ring.FFarden == null)
                {
                    client.Character.Ring.FFarden = new SagaDB.FFarden.FFarden();
                    client.Character.Ring.FFarden.Name = args;
                    client.Character.Ring.FFarden.RingID = client.Character.Ring.ID;
                    client.Character.Ring.FFarden.ObMode = 3;
                    client.Character.Ring.FFarden.Content = "测试内容";
                    SagaDB.FFarden.FFarden r = new SagaDB.FFarden.FFarden();
                    client.Character.Ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.GARDEN, new List<ActorFurniture>());
                    client.Character.Ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.ROOM, new List<ActorFurniture>());

                }
                MapServer.charDB.CreateFF(client.Character);

                client.SendRingFF();
            }
        }
        private void ProcessOpenFF(MapClient client, string args)
        {
            //CustomMapManager.Instance.EnterMap(client.Character);
            int maxPage;
            //int page = 1;
            List<SagaDB.FFarden.FFarden> res = FFardenManager.Instance.GetFFList(0, out maxPage);
            Packets.Server.SSMG_FF_LIST p1 = new SagaMap.Packets.Server.SSMG_FF_LIST();
            p1.ActorID = client.Character.ActorID;
            p1.Page = 0;
            p1.MaxPaga = (uint)maxPage;
            p1.Entries = res;
            client.netIO.SendPacket(p1);
        }
        #endregion

        #endregion

    }

}