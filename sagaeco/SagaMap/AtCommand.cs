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
                level = lvl;
            }
        }
        private Dictionary<string, CommandInfo> commandTable;
        private static string MasterName = "Saga";

        public AtCommand()
        {
            commandTable = new Dictionary<string, CommandInfo>();

            #region "Prefixes"
            string OpenCommandPrefix = "/";
            string GMCommandPrefix = "!";
            string RemoteCommandPrefix = "~";
            #endregion

            #region "Public Commands"

            commandTable.Add(GMCommandPrefix + "buff", new CommandInfo(new ProcessCommandFunc(ProcessBuffTest), 100));

            // public commands
            //this.commandTable.Add(OpenCommandPrefix + "who", new CommandInfo(new ProcessCommandFunc(this.ProcessWho), 0));
            commandTable.Add(OpenCommandPrefix + "revive", new CommandInfo(new ProcessCommandFunc(ProcessRevive2), 0));
            commandTable.Add(OpenCommandPrefix + "home", new CommandInfo(new ProcessCommandFunc(ProcessHome), 0));
            commandTable.Add(OpenCommandPrefix + "motion", new CommandInfo(new ProcessCommandFunc(ProcessMotion), 0));
            commandTable.Add(OpenCommandPrefix + "dustbox", new CommandInfo(new ProcessCommandFunc(ProcessDustbox), 0));
            commandTable.Add(OpenCommandPrefix + "vcashshop", new CommandInfo(new ProcessCommandFunc(ProcessVcashsop), 0));
            commandTable.Add(OpenCommandPrefix + "ncshop", new CommandInfo(new ProcessCommandFunc(ProcessNCshop), 0));
            //this.commandTable.Add(OpenCommandPrefix + "user", new CommandInfo(new ProcessCommandFunc(this.ProcessUser), 0));
            commandTable.Add(OpenCommandPrefix + "commandlist", new CommandInfo(new ProcessCommandFunc(ProcessCommandList), 0));
            commandTable.Add(OpenCommandPrefix + "w", new CommandInfo(new ProcessCommandFunc(ProcessTrumpet), 0));
            commandTable.Add(OpenCommandPrefix + "openware", new CommandInfo(new ProcessCommandFunc(ProcessOpenWare), 0));
            commandTable.Add(OpenCommandPrefix + "setshop", new CommandInfo(new ProcessCommandFunc(ProcessSetShop), 0));
            commandTable.Add(OpenCommandPrefix + "autolock", new CommandInfo(new ProcessCommandFunc(ProcessAutoLock), 0));
            commandTable.Add(OpenCommandPrefix + "refuse", new CommandInfo(new ProcessCommandFunc(ProcessRefuse), 0));
            commandTable.Add(OpenCommandPrefix + "acceptpk", new CommandInfo(new ProcessCommandFunc(ProcessAccept), 0));
            commandTable.Add(OpenCommandPrefix + "praise", new CommandInfo(new ProcessCommandFunc(ProcessPraise), 0));
            commandTable.Add(OpenCommandPrefix + "bosstime", new CommandInfo(new ProcessCommandFunc(ProcessBossTime), 0));
            commandTable.Add(OpenCommandPrefix + "die", new CommandInfo(new ProcessCommandFunc(ProcessDie), 0));
            commandTable.Add(OpenCommandPrefix + "checkpartner", new CommandInfo(new ProcessCommandFunc(ProcessCheckPartner), 0));
            commandTable.Add(OpenCommandPrefix + "greetingmotion", new CommandInfo(new ProcessCommandFunc(ProcessGreetingMotion), 0));
            commandTable.Add(OpenCommandPrefix + "duang", new CommandInfo(new ProcessCommandFunc(ProcessDuang), 0));

            #endregion

            #region "GM Commands"
            // gm commands
            //this.commandTable.Add(GMCommandPrefix + "wlevel", new CommandInfo(new ProcessCommandFunc(this.ProcessWlevel), 2));



            //now working
            commandTable.Add(GMCommandPrefix + "greeting", new CommandInfo(new ProcessCommandFunc(ProcessGreeting), 200));

            commandTable.Add(GMCommandPrefix + "wing", new CommandInfo(new ProcessCommandFunc(ProcessOpenWing), 200));
            commandTable.Add(GMCommandPrefix + "ffweather", new CommandInfo(new ProcessCommandFunc(ProcessChangeFFWeather), 200));
            commandTable.Add(GMCommandPrefix + "fg", new CommandInfo(new ProcessCommandFunc(ProcessFG), 200));
            commandTable.Add(GMCommandPrefix + "joinfg", new CommandInfo(new ProcessCommandFunc(ProcessJoinFG), 200));

            commandTable.Add(GMCommandPrefix + "warp", new CommandInfo(new ProcessCommandFunc(ProcessWarp), 20));
            commandTable.Add(GMCommandPrefix + "chat", new CommandInfo(new ProcessCommandFunc(ProcessChat), 30));
            commandTable.Add(GMCommandPrefix + "announce", new CommandInfo(new ProcessCommandFunc(ProcessAnnounce), 30));
            commandTable.Add(GMCommandPrefix + "heal", new CommandInfo(new ProcessCommandFunc(ProcessHeal), 50));
            commandTable.Add(GMCommandPrefix + "level", new CommandInfo(new ProcessCommandFunc(ProcessLevel), 60));
            commandTable.Add(GMCommandPrefix + "joblv", new CommandInfo(new ProcessCommandFunc(ProcessJobLevel), 60));
            commandTable.Add(GMCommandPrefix + "gold", new CommandInfo(new ProcessCommandFunc(ProcessGold), 50));
            commandTable.Add(GMCommandPrefix + "shoppoint", new CommandInfo(new ProcessCommandFunc(ProcessShoppoint), 60));
            commandTable.Add(GMCommandPrefix + "hair", new CommandInfo(new ProcessCommandFunc(ProcessHair), 20));
            commandTable.Add(GMCommandPrefix + "hairstyle", new CommandInfo(new ProcessCommandFunc(ProcessHairstyle), 20));
            commandTable.Add(GMCommandPrefix + "haircolor", new CommandInfo(new ProcessCommandFunc(ProcessHaircolor), 20));
            commandTable.Add(GMCommandPrefix + "job", new CommandInfo(new ProcessCommandFunc(ProcessJob), 60));
            commandTable.Add(GMCommandPrefix + "statpoints", new CommandInfo(new ProcessCommandFunc(ProcessStatPoints), 60));
            commandTable.Add(GMCommandPrefix + "skillpoints", new CommandInfo(new ProcessCommandFunc(ProcessSkillPoints), 60));
            commandTable.Add(GMCommandPrefix + "hide", new CommandInfo(new ProcessCommandFunc(ProcessHide), 60));
            commandTable.Add(GMCommandPrefix + "ban", new CommandInfo(new ProcessCommandFunc(ProcessBan), 80));

            commandTable.Add(GMCommandPrefix + "event", new CommandInfo(new ProcessCommandFunc(ProcessEvent), 20));

            commandTable.Add(GMCommandPrefix + "hairext", new CommandInfo(new ProcessCommandFunc(ProcessHairext), 20));
            commandTable.Add(GMCommandPrefix + "playersize", new CommandInfo(new ProcessCommandFunc(ProcessPlayersize), 20));
            commandTable.Add(GMCommandPrefix + "item", new CommandInfo(new ProcessCommandFunc(ProcessItem), 1));
            commandTable.Add(GMCommandPrefix + "speed", new CommandInfo(new ProcessCommandFunc(ProcessSpeed), 50));
            commandTable.Add(GMCommandPrefix + "gmrevive", new CommandInfo(new ProcessCommandFunc(ProcessRevive), 50));

            commandTable.Add(GMCommandPrefix + "kick", new CommandInfo(new ProcessCommandFunc(ProcessKick), 200));
            commandTable.Add(GMCommandPrefix + "kickall", new CommandInfo(new ProcessCommandFunc(ProcessKickAll), 200));
            commandTable.Add(GMCommandPrefix + "recall", new CommandInfo(new ProcessCommandFunc(ProcessJump), 60));
            commandTable.Add(GMCommandPrefix + "recall2", new CommandInfo(new ProcessCommandFunc(ProcessJump2), 60));
            commandTable.Add(GMCommandPrefix + "jump", new CommandInfo(new ProcessCommandFunc(ProcessJumpTo), 60));
            commandTable.Add(GMCommandPrefix + "jump2", new CommandInfo(new ProcessCommandFunc(ProcessJumpTo2), 60));
            commandTable.Add(GMCommandPrefix + "mob", new CommandInfo(new ProcessCommandFunc(ProcessMob), 200));
            commandTable.Add(GMCommandPrefix + "summon", new CommandInfo(new ProcessCommandFunc(ProcessSummon), 60));
            commandTable.Add(GMCommandPrefix + "summonme", new CommandInfo(new ProcessCommandFunc(ProcessSummonMe), 60));
            commandTable.Add(GMCommandPrefix + "spawn", new CommandInfo(new ProcessCommandFunc(ProcessSpawn), 200));
            commandTable.Add(GMCommandPrefix + "effect", new CommandInfo(new ProcessCommandFunc(ProcessEffect), 60));
            commandTable.Add(GMCommandPrefix + "kickgolem", new CommandInfo(new ProcessCommandFunc(ProcessKickGolem), 200));
            commandTable.Add(GMCommandPrefix + "killallmob", new CommandInfo(new ProcessCommandFunc(ProcessKillAllMob), 200));
            commandTable.Add(GMCommandPrefix + "odwarstart", new CommandInfo(new ProcessCommandFunc(ProcessODWarStart), 200));
            //this.commandTable.Add(GMCommandPrefix + "tweet", new CommandInfo(new ProcessCommandFunc(this.ProcessTweet), 0));


            //for skill test
            commandTable.Add(GMCommandPrefix + "skill", new CommandInfo(new ProcessCommandFunc(ProcessSkill), 60));
            commandTable.Add(GMCommandPrefix + "skillclear", new CommandInfo(new ProcessCommandFunc(ProcessSkillClear), 60));
            commandTable.Add(GMCommandPrefix + "gmob", new CommandInfo(new ProcessCommandFunc(ProcessGridMob), 200));
            commandTable.Add(GMCommandPrefix + "showstatus", new CommandInfo(new ProcessCommandFunc(ProcessShowStatus), 60));

            commandTable.Add(GMCommandPrefix + "who", new CommandInfo(new ProcessCommandFunc(ProcessWho), 1));
            commandTable.Add(GMCommandPrefix + "who2", new CommandInfo(new ProcessCommandFunc(ProcessWho2), 20));
            commandTable.Add(GMCommandPrefix + "who3", new CommandInfo(new ProcessCommandFunc(ProcessWho3), 60));
            commandTable.Add(GMCommandPrefix + "who4", new CommandInfo(new ProcessCommandFunc(ProcessWho4), 60));
            commandTable.Add(GMCommandPrefix + "mode", new CommandInfo(new ProcessCommandFunc(ProcessMode), 100));
            commandTable.Add(GMCommandPrefix + "robot", new CommandInfo(new ProcessCommandFunc(ProcessRobot), 100));

            commandTable.Add(GMCommandPrefix + "go", new CommandInfo(new ProcessCommandFunc(ProcessGo), 20));
            commandTable.Add(GMCommandPrefix + "ch", new CommandInfo(new ProcessCommandFunc(ProcessCh), 20));
            //now working
            commandTable.Add(GMCommandPrefix + "info", new CommandInfo(new ProcessCommandFunc(ProcessInfo), 20));

            commandTable.Add(GMCommandPrefix + "reloadscript", new CommandInfo(new ProcessCommandFunc(ProcessReloadScript), 200));
            commandTable.Add(GMCommandPrefix + "loadscripts", new CommandInfo(new ProcessCommandFunc(ProcessloadScript), 99));
            commandTable.Add(GMCommandPrefix + "reloadconfig", new CommandInfo(new ProcessCommandFunc(ProcessReloadConfig), 200));
            commandTable.Add(GMCommandPrefix + "loadskills", new CommandInfo(new ProcessCommandFunc(ProcessloadSkills), 99));
            commandTable.Add(GMCommandPrefix + "raw", new CommandInfo(new ProcessCommandFunc(ProcessRaw), 100));
            commandTable.Add(GMCommandPrefix + "test", new CommandInfo(new ProcessCommandFunc(ProcessTest), 100));

            commandTable.Add(GMCommandPrefix + "face", new CommandInfo(new ProcessCommandFunc(ProcessFace), 100));
            commandTable.Add(GMCommandPrefix + "createff", new CommandInfo(new ProcessCommandFunc(ProcessCreateFF), 100));
            commandTable.Add(GMCommandPrefix + "openff", new CommandInfo(new ProcessCommandFunc(ProcessOpenFF), 100));

            commandTable.Add(GMCommandPrefix + "theater", new CommandInfo(new ProcessCommandFunc(ProcessTheater), 100));

            commandTable.Add(GMCommandPrefix + "metamo", new CommandInfo(new ProcessCommandFunc(ProcessMetamo), 100));

            commandTable.Add(GMCommandPrefix + "through", new CommandInfo(new ProcessCommandFunc(ProcessThrough), 100));

            commandTable.Add(GMCommandPrefix + "ta", new CommandInfo(new ProcessCommandFunc(ProcessTaskAnnounce), 100));
            commandTable.Add(GMCommandPrefix + "sta", new CommandInfo(new ProcessCommandFunc(ProcessStopTaskAnnounce), 100));

            commandTable.Add(GMCommandPrefix + "goldto", new CommandInfo(new ProcessCommandFunc(ProcessGoldTo), 100));
            commandTable.Add(GMCommandPrefix + "itemto", new CommandInfo(new ProcessCommandFunc(ProcessItemTo), 100));

            commandTable.Add(GMCommandPrefix + "ring", new CommandInfo(new ProcessCommandFunc(ProcessRing), 100));

            commandTable.Add(GMCommandPrefix + "clearbuff", new CommandInfo(new ProcessCommandFunc(ProcessClearBuff), 100));

            commandTable.Add(GMCommandPrefix + "skilldebug", new CommandInfo(new ProcessCommandFunc(ProcessSkillDebug), 100));

            //简化操作！！
            commandTable.Add(GMCommandPrefix + "var", new CommandInfo(new ProcessCommandFunc(ProcessVariable), 100));
            commandTable.Add(GMCommandPrefix + "variable", new CommandInfo(new ProcessCommandFunc(ProcessVariable), 100));
            commandTable.Add(GMCommandPrefix + "title", new CommandInfo(new ProcessCommandFunc(ProcessSetTitle), 100));
            commandTable.Add(GMCommandPrefix + "titleto", new CommandInfo(new ProcessCommandFunc(ProcessSetTitleTo), 100));
            commandTable.Add(GMCommandPrefix + "status", new CommandInfo(new ProcessCommandFunc(ProcessStatus), 100));
            commandTable.Add(GMCommandPrefix + "effect2", new CommandInfo(new ProcessCommandFunc(ProcessEffect2), 30));

            commandTable.Add(GMCommandPrefix + "golem", new CommandInfo(new ProcessCommandFunc(ProcessGolem), 100));
            commandTable.Add(GMCommandPrefix + "debug", new CommandInfo(new ProcessCommandFunc(ProcessDebug), 100));
            commandTable.Add(GMCommandPrefix + "furniture", new CommandInfo(new ProcessCommandFunc(ProcessFurniture), 100));
            commandTable.Add(GMCommandPrefix + "dialog", new CommandInfo(new ProcessCommandFunc(ProcessDialog), 100));
            commandTable.Add(GMCommandPrefix + "dialogdebug", new CommandInfo(new ProcessCommandFunc(ProcessDialogDebug), 100));
            commandTable.Add(GMCommandPrefix + "partyinfo", new CommandInfo(new ProcessCommandFunc(ProcessPartyInfo), 100));

            commandTable.Add(GMCommandPrefix + "love", new CommandInfo(new ProcessCommandFunc(ProcessLove), 100));
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
        public void ProcessRing(MapClient client, string args)
        {
            if (args != "") ;
            SagaDB.Ring.Ring ring = RingManager.Instance.CreateRing(client.Character, args);
        }


        public void ProcessClearBuff(MapClient client, string args)
        {
            client.Character.ClearTaskAddition(true);
            client.SendSystemMessage("所有状态已清除。");
        }

        public void ProcessSkillDebug(MapClient client, string args)
        {
            if (client.Character.TInt["GM技能调试模式"] == 1)
            {
                client.Character.TInt["GM技能调试模式"] = 0;
                client.SendSystemMessage("关闭了GM技能调试状态");
            }
            else
            {
                client.Character.TInt["GM技能调试模式"] = 1;
                client.SendSystemMessage("开启了GM技能调试状态，所有技能无消耗，并无视使用限制（包括CD）");
            }
        }

        public void ProcessVariable(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            try
            {
                ActorPC pc = client.Character;
                string type = arg[0];
                string varname = arg[1];
                int varval = 0;
                string varstr = "";
                int charID = 0;

                if (varname != "list")
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(varname, @"^\d+$")) //无符号整型（纯数字）
                    {
                        charID = int.Parse(varname);
                        var chr =
                            from c in MapClientManager.Instance.OnlinePlayer
                            where c.Character.CharID == charID
                            select c;
                        if (chr.Count() == 0)
                        {
                            client.SendSystemMessage("错误的CharaID");
                            return;
                        }
                        pc = chr.First().Character;
                        varname = arg[2];
                        if (arg.Count() >= 4)
                            if (type.Contains("s"))//字符串类型
                                varstr = arg[3];
                            else
                                varval = int.Parse(arg[3]);
                    }
                    else
                    {
                        if (arg.Count() >= 3)
                            if (type.Contains("s"))//字符串类型
                                varstr = arg[2];
                            else
                                varval = int.Parse(arg[2]);
                    }
                }
                bool changevalue = (charID == 0 && arg.Count() >= 3) || (charID != 0 && arg.Count() >= 4);//实际上直接判断varval是不是初始值也可以，但是严谨一点…
                if (varname == "list") changevalue = false;
                switch (type)
                {
                    case "a":
                        if (changevalue)
                            pc.AInt[varname] = varval;
                        if (varname == "list")
                        {
                            foreach (var item in pc.AInt)
                                if (item.Value != 0)
                                    client.SendSystemMessage("角色的AInt[" + item.Key + "]变量值为" + item.Value);
                        }
                        else
                            client.SendSystemMessage("角色的AInt[" + varname + "]变量值为" + pc.AInt[varname].ToString());
                        break;
                    case "c":
                        if (changevalue)
                            pc.CInt[varname] = varval;
                        if (varname == "list")
                        {
                            foreach (var item in pc.CInt)
                                if (item.Value != 0)
                                    client.SendSystemMessage("角色的CInt[" + item.Key + "]变量值为" + item.Value);
                        }
                        else
                            client.SendSystemMessage("角色的CInt[" + varname + "]变量值为" + pc.CInt[varname].ToString());
                        break;
                    case "t":
                        if (changevalue)
                            pc.TInt[varname] = varval;
                        if (varname == "list")
                        {
                            foreach (var item in pc.TInt)
                                if (item.Value != 0)
                                    client.SendSystemMessage("角色的TInt[" + item.Key + "]变量值为" + item.Value);
                        }
                        else
                            client.SendSystemMessage("角色的TInt[" + varname + "]变量值为" + pc.TInt[varname].ToString());
                        break;
                    case "as":
                        if (changevalue)
                            pc.AStr[varname] = varstr;
                        if (varname == "list")
                            foreach (var item in pc.AStr)
                                client.SendSystemMessage("角色的AStr[" + item.Key + "]变量值为" + item.Value);
                        else
                            client.SendSystemMessage("角色的AStr[" + varname + "]变量值为" + pc.AStr[varname]);
                        break;
                    case "cs":
                        if (changevalue)
                            pc.CStr[varname] = varstr;
                        if (varname == "list")
                            foreach (var item in pc.CStr)
                                client.SendSystemMessage("角色的CStr[" + item.Key + "]变量值为" + item.Value);
                        else
                            client.SendSystemMessage("角色的CStr[" + varname + "]变量值为" + pc.CStr[varname]);
                        break;
                    case "ts":
                        if (changevalue)
                            pc.TStr[varname] = varstr;
                        if (varname == "list")
                            foreach (var item in pc.TStr)
                                client.SendSystemMessage("角色的TStr[" + item.Key + "]变量值为" + item.Value);
                        else
                            client.SendSystemMessage("角色的TStr[" + varname + "]变量值为" + pc.TStr[varname]);
                        break;
                    default:
                        client.SendSystemMessage("类型错误");
                        break;
                }
            }
            catch (Exception)
            {
                client.SendSystemMessage("参数错误（注意结尾不要有多余的空格！）");
                client.SendSystemMessage("\"!variable 类型[a/c/t/as/cs/ts] 变量名\"—显示自己的对应变量名");
                client.SendSystemMessage("\"!variable 类型 变量名 变量值\"—修改自己对应的变量值");
                client.SendSystemMessage("\"!variable 类型 charID 变量名\"—显示指定角色的对应变量名");
                client.SendSystemMessage("\"!variable 类型 charID 变量名 变量值\"—修改指定角色对应的变量值");
                client.SendSystemMessage("类型：a:AInt c:CInt t:TInt as:AStr cs:CStr ts:TStr");
                return;
            }
        }

        public void ProcessSetTitleTo(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg[0] == "")
            {
                client.SendSystemMessage("参数错误");
            }
            else
            {
                int charID = int.Parse(arg[0]);
                int index = int.Parse(arg[1]);

                var chr =
    from c in MapClientManager.Instance.OnlinePlayer
    where c.Character.CharID == charID
    select c;
                MapClient tClient = chr.First();

                tClient.SetTitle(index, true);
            }
        }
        public void ProcessSetTitle(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg[0] == "")
            {
                client.SendSystemMessage("参数错误");
            }
            else
            {
                int index = int.Parse(arg[0]);
                client.SetTitle(index, true);
            }
        }
        public void ProcessBuffTest(MapClient client, string args)
        {
            string[] arg = args.Split(' ');
            if (arg[0] == "" || arg[1] == "")
            {
                client.SendSystemMessage("参数错误");
            }
            else
            {
                try
                {
                    byte list = byte.Parse(arg[0]);
                    int index = int.Parse(arg[1]);
                    string s = "";

                    byte[] IDbuf;
                    string strIDbuf = "";
                    byte[] Indexbuf;
                    string strIndexbuf = "";
                    string nullbuf = "";
                    IDbuf = BitConverter.GetBytes(client.Character.ActorID);
                    Array.Reverse(IDbuf);
                    strIDbuf = Conversions.bytes2HexString(IDbuf);
                    Indexbuf = BitConverter.GetBytes(index);
                    Array.Reverse(Indexbuf);
                    strIndexbuf = Conversions.bytes2HexString(Indexbuf);
                    nullbuf = " 00 00 00 00";
                    switch (list)
                    {
                        case 1:
                            s = "15 7C " + strIDbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 2:
                            s = "15 7C " + strIDbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 3:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 4:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 5:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 6:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 7:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 8:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 9:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf + nullbuf;
                            break;
                        case 10:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf + nullbuf;
                            break;
                        case 11:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf + nullbuf;
                            break;
                        case 12:
                            s = "15 7C " + strIDbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + nullbuf + strIndexbuf;
                            break;
                    }
                    byte[] buf = Conversions.HexStr2Bytes(s.Replace(" ", ""));
                    Packet p = new Packet();
                    p.data = buf;
                    client.netIO.SendPacket(p);
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
        }
        public void ProcessGreeting(MapClient client, string args)
        {
            if (client.Character.AInt["打招呼无动作"] != 1)
                client.Character.AInt["打招呼无动作"] = 1;
            else
                client.Character.AInt["打招呼无动作"] = 0;
            client.SendSystemMessage("打招呼回应已设置为:" + client.Character.AInt["打招呼无动作"]);
        }
        public void ProcessOpenWing(MapClient client, string args)
        {
            Packets.Server.SSMG_TEST_EVOLVE_OPEN p1 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN();
            client.netIO.SendPacket(p1);
            Packets.Server.SSMG_TEST_EVOLVE_OPEN2 p2 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN2();
            client.netIO.SendPacket(p2);
            Packets.Server.SSMG_TEST_EVOLVE_OPEN3 p3 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN3();
            client.netIO.SendPacket(p3);
        }
        public void ProcessChangeFFWeather(MapClient client, string args)
        {
            string[] arg = args.Split(' ');

            if (arg.Length > 1)
                ScriptManager.Instance.VariableHolder.AInt["服務器FF天氣"] = int.Parse(arg[1]);
            ScriptManager.Instance.VariableHolder.AInt["服務器FF背景"] = int.Parse(arg[0]);
            Map map = MapManager.Instance.GetMap(client.Character.MapID);
            foreach (var pc in map.Actors)
            {
                if (pc.Value.type == ActorType.PC)
                {
                    MapClient mc = MapClient.FromActorPC(((ActorPC)pc.Value));
                    CustomMapManager.Instance.EnterFFOnMapLoaded(mc);
                }
            }
        }
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

        public void ProcessJoinFG(MapClient client, string args)
        {



            Packet p = new Packet(10);//unknown packet
            p.ID = 0x18E3;
            p.PutUInt(client.Character.ActorID, 2);
            p.PutUInt(client.Character.MapID, 6);
            MapClient.FromActorPC(client.Character).netIO.SendPacket(p);



            Map map = MapManager.Instance.GetMap(client.Character.MapID);
            client.Character.FGarden.MapID = MapManager.Instance.CreateMapInstance(client.Character, 70000000, client.Character.MapID, Global.PosX16to8(client.Character.X, map.Width), Global.PosY16to8(client.Character.Y, map.Height));
            ActorPC pc = client.Character;
            //spawn furnitures
            map = MapManager.Instance.GetMap(pc.FGarden.MapID);
            foreach (ActorFurniture i in pc.FGarden.Furnitures[SagaDB.FGarden.FurniturePlace.GARDEN])
            {
                i.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(i);
                i.invisble = false;
            }

            pc.BattleStatus = 0;
            pc.Speed = 200;
            client.SendChangeStatus();
            Map newMap = MapManager.Instance.GetMap(pc.FGarden.MapID);
            client.Map.SendActorToMap(client.Character, newMap, Global.PosX8to16(6, newMap.Width), Global.PosY8to16(11, newMap.Height));



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

        public void ProcessLove(MapClient client, string command)
        {
            int love = 0;
            if (command != "")
                love = int.Parse(command);
            client.Character.AInt["名称后缀图标"] = love;
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, client.Character, true);
        }
            public void ProcessPartyInfo(MapClient client, string command)
        {
            List<SagaDB.Party.Party> partys = new List<SagaDB.Party.Party>();
            foreach (MapClient c in MapClientManager.Instance.OnlinePlayer)
            {
                ActorPC pc = c.Character;
                if (pc.Party != null && !partys.Contains(pc.Party))
                    partys.Add(pc.Party);
            }
            string accountIDsName = "";
            string PartyNamesName = "";
            for (int i = 0; i < partys.Count; i++)
            {
                SagaDB.Party.Party p = partys[i];
                accountIDsName = p.ID + "|" + p.Name + ":";
                PartyNamesName = p.ID + "|" + p.Name + ":";
                foreach (var item in p.Members.Values)
                {
                    accountIDsName += item.Account.AccountID + ",";
                    PartyNamesName += item.Name + ",";
                }
                client.SendSystemMessage(accountIDsName);
                client.SendSystemMessage(PartyNamesName);
            }

        }
        public void ProcessDialogDebug(MapClient client, string command)
        {
            ActorPC pc = client.Character;
            if (ScriptManager.Instance.debuger.Contains(pc))
                ScriptManager.Instance.debuger.Remove(pc);
            else ScriptManager.Instance.debuger.Add(pc);
        }
        public void ProcessDialog(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length == 1)
                {
                    ushort ID = ushort.Parse(args[0]);
                    Packets.Server.SSMG_ANO_DIALOG_BOX p = new Packets.Server.SSMG_ANO_DIALOG_BOX();
                    p.DID = ID;
                    client.netIO.SendPacket(p);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void ProcessDebug(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length == 1)
                {
                    uint Level = uint.Parse(args[0]);
                    Logger.CurrentLogger.LogLevel = (Logger.LogContent)Level;
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void ProcessFurniture(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length == 1)
                {
                    uint ActorID = uint.Parse(args[0]);
                    Actor a = client.map.GetActor(ActorID);
                    if (a != null)
                        client.map.DeleteActor(a);
                    return;
                }
                if (args.Length == 9)
                {
                    uint mapid = client.map.ID;
                    uint itemid = uint.Parse(args[0]);
                    short x = short.Parse(args[1]);
                    short y = short.Parse(args[2]);
                    short z = short.Parse(args[3]);
                    ushort dir = ushort.Parse(args[4]);
                    short xa = short.Parse(args[5]);
                    short ya = short.Parse(args[6]);
                    short za = short.Parse(args[7]);
                    ushort motion = ushort.Parse(args[8]);

                    string n = "未知家具";
                    if (ItemFactory.Instance.Items.ContainsKey(itemid))
                        n = ItemFactory.Instance.Items[itemid].name;

                    ActorFurniture item = new ActorFurniture()
                    {
                        MapID = client.map.ID,
                        Name = n,
                        X = x,
                        Y = y,
                        Z = z,
                        ItemID=itemid,
                        Dir=dir,
                        Xaxis = xa,
                        Yaxis = ya,
                        Zaxis = za,
                        Motion = motion,
                        e = new NullEventHandler()
                    };
                    client.map.RegisterActor(item);
                    item.invisble = false;
                    client.map.OnActorVisibilityChange(item);
                    client.SendSystemMessage("创建成功！ActorID：" + item.ActorID);

                    System.IO.FileStream fs = new System.IO.FileStream("家具.xml", System.IO.FileMode.Append);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                    Map map = MapManager.Instance.GetMap(client.Character.MapID);
                    sw.WriteLine("  <Actor Type=\"FI\">");
                    sw.WriteLine(string.Format("    <MapID>{0}</MapID>", client.map.ID));
                    sw.WriteLine(string.Format("    <ItemID>{0}</ItemID>", item.ItemID));
                    sw.WriteLine(string.Format("    <PictID>0</PictID>"));
                    sw.WriteLine(string.Format("    <x>{0}</x>",item.X));
                    sw.WriteLine(string.Format("    <y>{0}</y>", item.Y));
                    sw.WriteLine(string.Format("    <z>{0}</z>", item.Z));
                    sw.WriteLine(string.Format("    <dir>{0}</dir>", item.Dir));
                    sw.WriteLine(string.Format("    <Xaxis>{0}</Xxsis>",item.Xaxis));
                    sw.WriteLine(string.Format("    <Yaxis>{0}</Yxsis>", item.Yaxis));
                    sw.WriteLine(string.Format("    <Zaxis>{0}</Zxsis>", item.Zaxis));
                    sw.WriteLine(string.Format("    <Motion>{0}</Motion>", item.Motion));
                    sw.WriteLine(string.Format("    <Name> </Name>"));
                    sw.WriteLine("  </Actor>");
                    sw.Flush();
                    fs.Flush();
                    fs.Close();
                    client.SendSystemMessage("添加成功！");

                }
                else
                {
                    client.SendSystemMessage("指令错误！");
                }
            }
            catch (Exception ex)
            {
                client.SendSystemMessage("错误！");
                Logger.ShowError(ex);
            }
        }

        public void ProcessGolem(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length == 1)
                {
                    uint ActorID = uint.Parse(args[0]);
                    Actor a = client.map.GetActor(ActorID);
                    if (a != null)
                        client.map.DeleteActor(a);
                    return;
                }
                if (args.Length == 9)
                {
                    uint mapid = uint.Parse(args[0]);
                    byte x = byte.Parse(args[1]);
                    byte y = byte.Parse(args[2]);
                    uint eventid = uint.Parse(args[3]);
                    uint pictid = uint.Parse(args[4]);
                    string name = args[5];
                    byte shoptype = byte.Parse(args[6]);
                    string title = args[7];
                    byte aimode = byte.Parse(args[8]);
                    ActorGolem golem = new ActorGolem();
                    golem.MapID = mapid;
                    golem.X2 = x;
                    golem.Y2 = y;
                    golem.EventID = eventid;
                    golem.PictID = pictid;
                    golem.Name = name;
                    if (shoptype == 1)
                        golem.GolemType = GolemType.Sell;
                    else
                        golem.GolemType = GolemType.Buy;
                    golem.Title = title;
                    golem.AIMode = aimode;
                    FictitiousActorsManager.Instance.regionFictitiousSingleActor(golem);
                    client.SendSystemMessage("刷新成功！ActorID：" + golem.ActorID);
                }
                else
                {
                    client.SendSystemMessage("错误！请按照!golem mapid x y eventid pictid 名字 ShopType(0为收购 1为贩卖) 店名 AIMODE格式输入。");
                    client.SendSystemMessage("例如!golem 10054001 152 203 66000001 16470000 清姬2 1 收购木材啦！ 0");
                    client.SendSystemMessage("或者使用!golem actorID来进行删除石像。actorID使用!who3查看。");
                }
            }
            catch (Exception ex)
            {
                client.SendSystemMessage("错误！请按照!golem mapid x y eventid pictid 名字 ShopType(0为收购 1为贩卖) 店名 AIMODE格式输入。");
                client.SendSystemMessage("例如!golem 10054001 152 203 66000001 16470000 清姬2 1 收购木材啦！ 0");
                client.SendSystemMessage("或者使用!golem actorID来进行删除石像。actorID使用!who3查看。");
                Logger.ShowError(ex);
            }
        }

        public void ProcessStatus(MapClient client, string command)
        {
            try
            {
                string[] args = command.Split(' ');
                if (args.Length > 1)
                {
                    int pt = int.Parse(args[1]);
                    switch (args[0])
                    {
                        case "str":
                            client.Character.Str = (ushort)pt;
                            break;
                        case "dex":
                            client.Character.Dex = (ushort)pt;
                            break;
                        case "int":
                            client.Character.Int = (ushort)pt;
                            break;
                        case "vit":
                            client.Character.Vit = (ushort)pt;
                            break;
                        case "agi":
                            client.Character.Agi = (ushort)pt;
                            break;
                        case "mag":
                            client.Character.Mag = (ushort)pt;
                            break;
                        case "hp":
                            client.Character.TInt["临时HP"] = pt;
                            break;
                        case "mp":
                            client.Character.TInt["临时MP"] = pt;
                            break;
                        case "sp":
                            client.Character.TInt["临时SP"] = pt;
                            break;
                        case "atk":
                            client.Character.TInt["临时ATK"] = pt;
                            break;
                        case "matk":
                            client.Character.TInt["临时MATK"] = pt;
                            break;
                    }
                }
                SagaMap.PC.StatusFactory.Instance.CalcStatus(client.Character);
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
                        continue;
                    string[] sLine = line.Split(',');
                    if (!commandTable.Keys.Contains(sLine[0]))
                        continue;
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

                if (client.Character.Account.GMLevel >= 1)
                {
                    Logger log = new Logger("GM命令使用记录.txt");
                    string logtext = "\r\n" + client.Character.Name + "：" + command;
                    log.WriteLog(logtext);
                }

                if (commandTable.ContainsKey(args[0]))
                {
                    CommandInfo cInfo = commandTable[args[0]];

                    if (client.Character.Account.GMLevel >= cInfo.level)
                    {
                        if (client.Character.Account.GMLevel >= 1)
                        {
                            Logger.LogGMCommand(client.Character.Name + "(" + client.Character.CharID + ")", "",
                                string.Format("Account:{0}({1}) GMLv:{2} Command:{3}",
                                client.Character.Account.Name,
                                client.Character.Account.AccountID,
                                client.Character.Account.GMLevel, command));
                        }

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
        private void ProcessRevive2(MapClient client, string args)
        {
            //client.Character.Buff.Dead = true;
            //client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, client.Character, true);
            client.OnPlayerReturnHome(new Packets.Client.CSMG_PLAYER_RETURN_HOME());
            /*
            if (client.Character.HP == 0)
            {
                if (!client.Character.Buff.Dead)
                    client.Character.Buff.Dead = true;
                if (client.Character.TInt["副本复活标记"] == 3 && client.Character.TInt["复活次数"] < 1)
                {
                    Tasks.PC.Revive reg;
                    reg = new Tasks.PC.Revive(client, 120, client.Character.TInt["副本复活标记"]);
                    client.Character.Tasks.Add("Revive", reg);
                    reg.Activate();
                }
                if (client.Character.TInt["副本复活标记"] == 0)
                {
                    Tasks.PC.Revive reg;
                    reg = new Tasks.PC.Revive(client, 30, client.Character.TInt["副本复活标记"]);
                    client.Character.Tasks.Add("Revive", reg);
                    reg.Activate();
                }
                if (client.Character.TInt["副本复活标记"] == 4)
                {
                    Tasks.PC.Revive reg;
                    reg = new Tasks.PC.Revive(client, 10, client.Character.TInt["副本复活标记"]);
                    client.Character.Tasks.Add("Revive", reg);
                    reg.Activate();
                    List<Actor> actors = new List<Actor>();
                    foreach (var a in MapClient.FromActorPC(client.Character).map.Actors)
                    {
                        actors.Add(a.Value);
                    }
                    foreach (var item in actors)
                    {
                        if (item != null)
                            if (item.type == ActorType.MOB)
                                if (!item.Buff.Dead)
                                    item.HP = item.MaxHP;
                    }
                }
            }*/
        }
        public void ProcessHome(MapClient client, string args)
        {
            if (client.Character.Account.AccountID <= 247 && client.Character.CInt["数据转移"] != 1) return;//阻止旧账号
            //if (client.Character.MapID == 10054000 && !client.Character.Buff.Dead) return;
            Map map = MapManager.Instance.GetMap(client.Character.MapID);
            ActorPC pc = client.Character;

            pc.Buff.Dead = false;
            if (pc.HP == 0)
            {
                pc.HP = pc.MaxHP;
                pc.MP = pc.MaxMP;
                pc.SP = pc.MaxSP;
                pc.EP = pc.MaxEP;
                if (pc.Job == PC_JOB.ASTRALIST)//魔法师
                {
                    pc.EP = 0;
                }
                if(pc.Job == PC_JOB.HAWKEYE)
                {
                    //pc.EP = 0;
                    pc.SP = 0;
                }
                Skill.SkillHandler.Instance.ShowVessel(pc, (int)-pc.MaxHP);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
                Skill.SkillHandler.Instance.ShowEffectByActor(pc, 5116);
            }

            if (pc.Job == PC_JOB.CARDINAL)
                pc.EP = 5000;

            pc.TInt["副本复活标记"] = 0;
            pc.BattleStatus = 0;
            client.SendChangeStatus();
            
            pc.Buff.紫になる = false;
            pc.Motion = MotionType.STAND;
            pc.MotionLoop = false;

            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);



            Skill.SkillHandler.Instance.CastPassiveSkills(pc);
            client.SendPlayerInfo();
            if (!pc.Tasks.ContainsKey("Recover"))//自然恢复
            {
                Tasks.PC.Recover reg = new Tasks.PC.Recover(client);
                pc.Tasks.Add("Recover", reg);
                reg.Activate();
            }

            EffectArg arg = new EffectArg();
            arg.effectID = 5362;
            arg.actorID = 0xFFFFFFFF;
            arg.x = Global.PosX16to8(client.Character.X, map.Width);
            arg.y = Global.PosY16to8(client.Character.Y, map.Height);
            client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, client.Character, true);

            if (Configuration.Instance.HostedMaps.Contains(10054000))
            {
                Map newMap = MapManager.Instance.GetMap(10054000);
                client.Map.SendActorToMap(client.Character, 10054000, Global.PosX8to16(154, newMap.Width), Global.PosY8to16(146, newMap.Height));
            }

        }
        private void ProcessMotion(MapClient client, string args)
        {
            if (int.Parse(args) == 591)
            {
                client.SendSystemMessage("这个动作吵死了，不让用！");
                return;
            }
            client.SendMotion((MotionType)int.Parse(args), 1);
        }

        private void ProcessWhere(MapClient client, string args)
        {

        }

        private void ProcessDustbox(MapClient client, string args)
        {
            client.npcTrade = true;
            string name = "垃圾箱(此处不生成CP！)";

            client.SendTradeStartNPC(name);
            client.Character.CInt["垃圾箱记录"] = 1;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            /*while (client.npcTrade)
            {
                System.Threading.Thread.Sleep(500);
            }*/
            if (blocked)
                ClientManager.EnterCriticalArea();
            client.Character.CInt["垃圾箱记录"] = 0;
            List<Item> items = client.npcTradeItem;
            client.npcTradeItem = null;
        }
        private void ProcessNCshop(MapClient client, string args)
        {
            Scripting.SkillEvent.Instance.NCShopOpen(client.Character);
        }

        private void ProcessVcashsop(MapClient client, string args)
        {
            client.SendSystemMessage("这个商店正在整顿中啦！~");
            //Scripting.SkillEvent.Instance.VShopOpen(client.Character);
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

        void ProcessStatPoints(MapClient client, string args)
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

        void ProcessSkillPoints(MapClient client, string command)
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
        private void ProcessloadSkills(MapClient client, string args)
        {
            ClientManager.noCheckDeadLock = true;
            try
            {
                Skill.SkillHandler.Instance.skillHandlers.Clear();
                Skill.SkillHandler.Instance.MobskillHandlers.Clear();
                Skill.SkillHandler.Instance.Init();
                Skill.SkillHandler.Instance.LoadSkill("DB/Skills",true,client.Character.name);
            }
            catch(Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                ClientManager.noCheckDeadLock = false;
            }
            ClientManager.noCheckDeadLock = false;
        }
        //TODO:
        private void ProcessReloadConfig(MapClient client, string args)
        {
            try
            {
                switch (args.ToLower())
                {
                    case "ecoshop":
                        //ProcessAnnounce(client, "Reloading ECOShop");
                        ECOShopFactory.Instance.Reload();
                        //ProcessAnnounce(client, "Reloaded ECOShop");
                        break;
                    case "shopdb":
                        //ProcessAnnounce(client, "Reloading ShopDB");
                        ShopFactory.Instance.Reload();
                        GC.Collect();
                        //ProcessAnnounce(client, "Reloaded ShopDB");
                        break;
                    case "monster":
                        //ProcessAnnounce(client, "Reloading monster");
                        MobFactory.Instance.Mobs.Clear();
                        MobFactory.Instance.Init("./DB/monster.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        MobAIFactory.Instance.Items.Clear();
                        MobAIFactory.Instance.Init("DB/MobAI.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        SagaMap.Mob.MobAIFactory.Instance.Init(SagaLib.VirtualFileSystem.VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/TTMobAI", "*.xml", System.IO.SearchOption.AllDirectories), null);
                        //ProcessAnnounce(client, "Reloaded monster");
                        break;
                    case "quests":
                        //ProcessAnnounce(client, "Reloading Quests");
                        QuestFactory.Instance.Reload();
                        // ProcessAnnounce(client, "Reloaded Quests");
                        break;
                    case "treasure":
                        // ProcessAnnounce(client, "Reloading Treasure");
                        TreasureFactory.Instance.Reload();
                        //ProcessAnnounce(client, "Reloaded Treasure");
                        break;
                    case "spawns":
                        // ProcessAnnounce(client, "Reloading Spawns");
                        MobSpawnManager.Instance.Spawns.Clear();
                        MobSpawnManager.Instance.LoadSpawn("./DB/Spawns");
                        //ProcessAnnounce(client, "Reloaded Spawns");
                        break;
                    case "theater":
                        // ProcessAnnounce(client, "Reloading Theater");
                        TheaterFactory.Instance.Reload();
                        // ProcessAnnounce(client, "Reloaded Theater");
                        break;
                    case "synthese":
                        //ProcessAnnounce(client, "Reloading SyntheseDB");
                        SyntheseFactory.Instance.Reload();
                        //ProcessAnnounce(client, "Reloaded SyntheseDB");
                        break;
                    case "item":
                        ItemFactory.Instance.Reload();
                        ItemFactory.Instance.Init(VirtualFileSystemManager.Instance.FileSystem.SearchFile("DB/", "item*.csv", System.IO.SearchOption.TopDirectoryOnly), System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        SagaDB.Partner.PartnerFactory.Instance.ClearPartnerEquips();
                        SagaDB.Partner.PartnerFactory.Instance.InitPartnerEquipDB("DB/partner_Equip.csv", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        break;
                    case "kuji":
                        KujiListFactory.Instance.KujiList.Clear();
                        KujiListFactory.Instance.NewKujilist.Clear();
                        KujiListFactory.Instance.InitXML("DB/KujiList.xml", System.Text.Encoding.GetEncoding(Configuration.Instance.DBEncoding));
                        break;
                    case "ssp":
                        SagaDB.Skill.SkillFactory.Instance.items.Clear();
                        SagaDB.Skill.SkillFactory.Instance.InitSSP("DB/effect.ssp", System.Text.Encoding.Unicode);
                        break;
                    case "skilldb":
                        ClientManager.noCheckDeadLock = true;
                        try
                        {
                            Skill.SkillHandler.Instance.skillHandlers.Clear();
                            Skill.SkillHandler.Instance.MobskillHandlers.Clear();
                            Skill.SkillHandler.Instance.Init();
                            Skill.SkillHandler.Instance.LoadSkill("DB/Skills", true, client.Character.name);
                        }
                        catch (Exception ex)
                        {
                            SagaLib.Logger.ShowError(ex);
                            ClientManager.noCheckDeadLock = false;
                        }
                        ClientManager.noCheckDeadLock = false;
                        break;
                    default:
                        //ProcessAnnounce(client, "Reloading Configs");
                        Configuration.Instance.Initialization("./Config/SagaMap.xml");
                        //ProcessAnnounce(client, "Reloaded Configs");
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void ProcessloadScript(MapClient client, string args)
        {
            //ProcessAnnounce(client, "Reloading Scripts");
            try
            {

                ScriptManager.Instance.ReloadScript(true,client.Character.name);
            }
            catch (Exception ex)
            {
                client.SendSystemMessage(ex.ToString());
            }
            //ProcessAnnounce(client, "Reloaded Scripts");
        }

        private void ProcessReloadScript(MapClient client, string args)
        {
            //ProcessAnnounce(client, "Reloading Scripts");
            try
            {
                ScriptManager.Instance.ReloadScript();
            }
            catch (Exception ex)
            {
                client.SendSystemMessage(ex.ToString());
            }
            //ProcessAnnounce(client, "Reloaded Scripts");
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
                        ActorMob mob = client.map.SpawnMob(id,
                            (short)(client.Character.X + new Random().Next(1, 10)),
                            (short)(client.Character.Y + new Random().Next(1, 10)),
                            2500,
                            client.Character);
                        client.Character.SettledSlave.Add(mob);
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
            actor.range = 1;
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
                            null);
                    }
                }
                catch (Exception ex)
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_MOB_ERROR);

                }
            }
        }
        private void ProcessGoldTo(MapClient client, string args)
        {
            string name;
            uint gold = 0;
            //SagaLib.ClientManager.LeaveCriticalArea();
            if (args == "")
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            if (args.Split(' ').Length != 2)
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            try
            {
                name = args.Split(' ')[0];
                gold = uint.Parse(args.Split(' ')[1]);
                MapClient cp = (MapClient)SagaMap.Manager.MapClientManager.Instance.GetClientForName(name);
                if (cp == null)
                {
                    client.SendSystemMessage("错误");
                    return;
                }
                if (gold > 0)
                {
                    cp.Character.Gold += (long)gold;
                    cp.SendGoldUpdate();

                    client.SendSystemMessage("给" + name + " " + gold.ToString() + " G");
                    cp.SendSystemMessage(client.Character.Name + " 给 " + name + " " + gold.ToString() + " G");
                }
                else
                {
                    client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_NO_SUCH_ITEM);
                }
            }
            catch (Exception) { }
        }
        private void ProcessItemTo(MapClient client, string args)
        {
            string name;
            int number;
            uint id = 0;
            uint picid = 0;
            //SagaLib.ClientManager.LeaveCriticalArea();
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"(.+?) (\d+)( \d)*\z");
            if (!reg.IsMatch(args))
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            try
            {
                name = reg.Match(args).Groups[1].Value;
                id = uint.Parse(reg.Match(args).Groups[2].Value);
                if (reg.Match(args).Groups[3].Value != "")
                    number = int.Parse(reg.Match(args).Groups[3].Value);
                else
                    number = 1;
                Item item = ItemFactory.Instance.GetItem(id);
                MapClient cp = (MapClient)MapClientManager.Instance.GetClientForName(name);
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
            else if (args == "food")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.FOOD)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "ufood")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.UNION_FOOD)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "cube")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.UNION_ACTCUBE)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "partner")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.PARTNER || item.Value.itemType == ItemType.RIDE_PARTNER || item.Value.itemType == ItemType.RIDE_PET ||
                            item.Value.itemType == ItemType.RIDE_PET_ROBOT || item.Value.itemType == ItemType.PET || item.Value.itemType == ItemType.PET_NEKOMATA)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "socks")
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
                        if (item.Value.itemType == ItemType.SOCKS)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
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
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
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
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
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
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "233333")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.NONE || item.Value.itemType == ItemType.USE || item.Value.itemType == ItemType.FOOD ||
                            item.Value.itemType == ItemType.POTION || item.Value.itemType == ItemType.SEED || item.Value.itemType == ItemType.SEED ||
                            item.Value.itemType == ItemType.SCROLL)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "furniture")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.FURNITURE || item.Value.itemType == ItemType.FF_ROOM_FLOOR || item.Value.itemType == ItemType.FG_GARDEN_FLOOR || item.Value.itemType == ItemType.FG_GARDEN_MODELHOUSE
                             || item.Value.itemType == ItemType.FG_ROOM_FLOOR || item.Value.itemType == ItemType.FG_ROOM_WALL || item.Value.itemType == ItemType.FG_FLYING_SAIL)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "shoes")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.BOOTS || item.Value.itemType == ItemType.LONGBOOTS || item.Value.itemType == ItemType.SHOES ||
                            item.Value.itemType == ItemType.HALFBOOTS)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "clothes")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.ARROW || item.Value.itemType == ItemType.ARMOR_UPPER || item.Value.itemType == ItemType.ARMOR_LOWER ||
                            item.Value.itemType == ItemType.ONEPIECE || item.Value.itemType == ItemType.COSTUME || item.Value.itemType == ItemType.BODYSUIT ||
                            item.Value.itemType == ItemType.WEDDING || item.Value.itemType == ItemType.OVERALLS || item.Value.itemType == ItemType.FACEBODYSUIT ||
                            item.Value.itemType == ItemType.SLACKS)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "c1")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (
                            item.Value.itemType == ItemType.ONEPIECE || item.Value.itemType == ItemType.COSTUME)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "c2")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.BODYSUIT ||
                            item.Value.itemType == ItemType.WEDDING || item.Value.itemType == ItemType.OVERALLS || item.Value.itemType == ItemType.FACEBODYSUIT ||
                            item.Value.itemType == ItemType.SLACKS)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "c3")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.ARROW || item.Value.itemType == ItemType.ARMOR_UPPER)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "c4")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.ARMOR_LOWER)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "stamp")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.STAMP)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "accesory")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.ACCESORY_NECK || item.Value.itemType == ItemType.BACKPACK || item.Value.itemType == ItemType.ACCESORY_FINGER ||
                            item.Value.itemType == ItemType.SOCKS || item.Value.itemType == ItemType.EFFECT || item.Value.itemType == ItemType.HELM ||
                            item.Value.itemType == ItemType.JOINT_SYMBOL || item.Value.itemType == ItemType.ACCESORY_FACE || item.Value.itemType == ItemType.ACCESORY_HEAD)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "weapons")
            {
                foreach (KeyValuePair<uint, Item.ItemData> item in ItemFactory.Instance.Items)
                {
                    {
                        if (item.Value.itemType == ItemType.CLAW || item.Value.itemType == ItemType.HAMMER || item.Value.itemType == ItemType.STAFF ||
                            item.Value.itemType == ItemType.SWORD || item.Value.itemType == ItemType.AXE || item.Value.itemType == ItemType.SPEAR ||
                            item.Value.itemType == ItemType.BOW || item.Value.itemType == ItemType.GUN || item.Value.itemType == ItemType.ETC_WEAPON ||
                            item.Value.itemType == ItemType.ACCESORY_FINGER || item.Value.itemType == ItemType.SHORT_SWORD || item.Value.itemType == ItemType.RAPIER ||
                            item.Value.itemType == ItemType.STRINGS || item.Value.itemType == ItemType.BOOK || item.Value.itemType == ItemType.DUALGUN ||
                            item.Value.itemType == ItemType.RIFLE || item.Value.itemType == ItemType.THROW || item.Value.itemType == ItemType.ROPE ||
                            item.Value.itemType == ItemType.CARD || item.Value.itemType == ItemType.SHIELD)
                        {
                            Item i = new Item(item.Value);
                            i.Durability = i.BaseData.durability;
                            i.Stack = (ushort)1;
                            i.Identified = true;
                            client.AddItem(i, true, false);
                        }
                    }
                }
            }
            else if (args == "hair")
            {
                foreach (var item in HairFactory.Instance.Hairs)
                {
                    Item i = ItemFactory.Instance.GetItem(item.ItemID);
                    if (i != null)
                    {
                        i.Durability = i.BaseData.durability;
                        i.Stack = (ushort)1;
                        i.Identified = true;
                        client.AddItem(i, true, false);
                    }
                }
            }
            else if (args == "color")
            {
                List<uint> colors = new List<uint>();
                uint ids = 10031301;
                for (int i = 0; i < 32; i++)
                {
                    colors.Add(ids);
                    ids++;
                }
                colors.Add(10031364);
                colors.Add(10031365);
                colors.Add(10031366);
                colors.Add(10031367);
                colors.Add(10031368);
                foreach (var item in colors)
                {
                    Item i = ItemFactory.Instance.GetItem(item);
                    if (i != null)
                    {
                        i.Durability = i.BaseData.durability;
                        i.Stack = (ushort)1;
                        i.Identified = true;
                        client.AddItem(i, true, false);
                    }
                }
            }
            else if (args == "face")
            {
                foreach (var item in FaceFactory.Instance.FaceItemIDList)
                {
                    Item i = ItemFactory.Instance.GetItem(item);
                    if (i != null)
                    {
                        i.Durability = i.BaseData.durability;
                        i.Stack = (ushort)1;
                        i.Identified = true;
                        client.AddItem(i, true, false);
                    }
                }
            }
            else if (args == "clear")
            {
                /*Dictionary<uint, ushort> items = new Dictionary<uint, ushort>();
                foreach (var item in client.Character.Inventory.Items[ContainerType.BODY])
                {
                    items.Add(item.Slot, item.Stack);
                    client.DeleteItem(item.Slot, item.Stack, true);
                }*/
                int count = client.Character.Inventory.Items[ContainerType.BODY].Count;
                for (int i = 0; i < count; i++)
                    client.DeleteItem(client.Character.Inventory.Items[ContainerType.BODY][0].Slot, client.Character.Inventory.Items[ContainerType.BODY][0].Stack, true);
            }
            else if (args == "clearlogout")
            {
                client.Character.Inventory.Items[ContainerType.BODY].Clear();
                client.netIO.Disconnect();
            }
            else if (args == "clearware")
            {
                client.Character.Inventory.WareHouse.Clear();
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
                        client.AddItem(item, true, false);
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
            byte x, y;

            x = Global.PosX16to8(client.Character.X, client.map.Width);
            y = Global.PosY16to8(client.Character.Y, client.map.Height);
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
            client.SendSystemMessage(client.map.ID + ",[" + client.Character.X + "," + client.Character.Y + "],[" + x.ToString() + "," + y.ToString() + "]");
            client.SendSystemMessage("当前在线IP：" + MapClientManager.Instance.OnlinePlayerOnlyIP.Count.ToString());
        }


        private void ProcessWho4(MapClient client, string args)
        {
            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
            {
                byte x, y;
                x = Global.PosX16to8(i.Character.X, i.map.Width);
                y = Global.PosY16to8(i.Character.Y, i.map.Height);
                string ip = "{IP:" + i.Character.Account.LastIP + "}";
                if (i.Character.Account.GMLevel > 100)
                    ip = "{IP: 无法获取}";
                string mac = "[MAC:" + i.Character.Account.MacAddress + "]";

                byte count = 0;
                foreach (MapClient j in MapClientManager.Instance.OnlinePlayer)
                    if (j.Character.Account.LastIP == i.Character.Account.LastIP && j.Character.Account.GMLevel < 20)
                        count++;
                byte count2 = 0;
                foreach (MapClient j in MapClientManager.Instance.OnlinePlayer)
                    if (j.Character.Account.MacAddress == i.Character.Account.MacAddress && j.Character.Account.GMLevel < 20)
                        count2++;
                client.SendSystemMessage(i.Character.Name + "(AccountID:" + i.Character.Account.AccountID + "CharID:" + i.Character.CharID + "," + "ActorID:" + i.Character.ActorID + ")" +
                "[" + i.Map.Name + "("+i.Map.ID+") " + x.ToString() + "," + y.ToString() + "," + i.Map.ID + "] " + ip + mac + " 同IP号数：" + count + " 同MAC号数：" + count2);
            }
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
            client.SendSystemMessage("当前在线IP：" + MapClientManager.Instance.OnlinePlayerOnlyIP.Count.ToString());
        }
        private void ProcessWho2(MapClient client, string args)
        {
            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
            {
                byte x, y;
                x = Global.PosX16to8(i.Character.X, i.map.Width);
                y = Global.PosY16to8(i.Character.Y, i.map.Height);
                string ip = "{IP:" + i.Character.Account.LastIP + "}";
                if (i.Character.Account.GMLevel > 100)
                    ip = "{IP: 无法获取}";
                string mac = "[MAC:" + i.Character.Account.MacAddress + "]";

                byte count = 0;
                foreach (MapClient j in MapClientManager.Instance.OnlinePlayer)
                    if (j.Character.Account.LastIP == i.Character.Account.LastIP && j.Character.Account.GMLevel < 20)
                        count++;
                byte count2 = 0;
                foreach (MapClient j in MapClientManager.Instance.OnlinePlayer)
                    if (j.Character.Account.MacAddress == i.Character.Account.MacAddress && j.Character.Account.GMLevel < 20)
                        count2++;
                client.SendSystemMessage(i.Character.Name + "(AccountID:" + i.Character.Account.AccountID + "CharID:" + i.Character.CharID + "," + "ActorID:" + i.Character.ActorID + ")" +
                "[" + i.Map.Name + " " + x.ToString() + "," + y.ToString() + "," + i.Map.ID + "] "  + " 同IP：" + count + " 同MAC：" + count2);
            }
            client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
            client.SendSystemMessage("当前在线IP：" + MapClientManager.Instance.OnlinePlayerOnlyIP.Count.ToString());
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
                            client.SendSystemMessage("怪物:" + mob.Name + "(ActorID:" + mob.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.PC:
                            ActorPC pc = (ActorPC)act;
                            client.SendSystemMessage("玩家:" + pc.Name + "(ActorID:" + pc.ActorID + ")(CharID:" + pc.CharID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.PET:
                            ActorPet pet = (ActorPet)act;
                            client.SendSystemMessage("宠物:" + pet.Name + "(ActorID:" + pet.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.SHADOW:
                            ActorShadow sw = (ActorShadow)act;
                            client.SendSystemMessage("阴影:" + sw.Name + "(ActorID:" + sw.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.ITEM:
                            ActorItem itm = (ActorItem)act;
                            client.SendSystemMessage("道具:" + itm.Name + "(ActorID:" + itm.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                        case ActorType.FURNITURE:
                            ActorFurniture fi = (ActorFurniture)act;
                            client.SendSystemMessage("家具:" + fi.Name + "(ActorID:" + fi.ActorID + ")[" + x.ToString() + "," + y.ToString() + "," + fi.Z.ToString() + "]");
                            break;
                        case ActorType.GOLEM:
                            ActorGolem go = (ActorGolem)act;
                            client.SendSystemMessage("石像:" + go.Name + "(ActorID:" + go.ActorID + ")[" + x.ToString() + "," + y.ToString() + "," + "]");
                            break;
                        case ActorType.SKILL:
                            ActorSkill skill = (ActorSkill)act;
                            client.SendSystemMessage("技能:" + skill.Name + "(ActorID:" + skill.ActorID + ")[" + x.ToString() + "," + y.ToString() + "]");
                            break;
                    }
                }
                client.SendSystemMessage(string.Format("共：{0} 個Actors", actors.Count));
            }
            catch
            {

            }
        }


        private void ProcessGo(MapClient client, string args)
        {
            uint number = uint.Parse(args);
            uint mapid;
            byte x;
            byte y;
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
                    uint mapid = uint.Parse(arg[0]);
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
                    client.Character.Wig = style;
                    client.SendCharInfoUpdate();
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

                    if (client.map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        client.Character.DominionCEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.CLEVEL) + 1;
                        ExperienceManager.Instance.CheckExp(client, SagaMap.Scripting.LevelType.CLEVEL);
                        client.Character.DominionLevel = lv;
                    }
                    else
                    {
                        client.Character.CEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.CLEVEL) + 1;
                        ExperienceManager.Instance.CheckExp(client, SagaMap.Scripting.LevelType.CLEVEL);
                        client.Character.Level = lv;
                    }
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
                    if (client.map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        client.Character.DominionJEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.JLEVEL2) + 1;
                        ExperienceManager.Instance.CheckExp(client, SagaMap.Scripting.LevelType.JLEVEL2);
                        client.Character.DominionJobLevel = lv;
                    }
                    else
                    {
                        if (client.Character.Job == client.Character.JobBasic)
                        {
                            client.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.JLEVEL) + 1;
                            ExperienceManager.Instance.CheckExp(client, SagaMap.Scripting.LevelType.JLEVEL);
                            client.Character.JobLevel1 = lv;

                        }
                        else
                        {
                            client.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(lv, SagaMap.Scripting.LevelType.JLEVEL2) + 1;
                            ExperienceManager.Instance.CheckExp(client, SagaMap.Scripting.LevelType.JLEVEL2);
                            if (client.Character.Job == client.Character.Job2X)
                                client.Character.JobLevel2X = lv;
                            else if (client.Character.Job == client.Character.Job2T)
                                client.Character.JobLevel2T = lv;
                            else
                                client.Character.JobLevel3 = lv;

                        }
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
                    client.Character.TInt["playersize"] = (int)playersize;
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
                    ulong gold = ulong.Parse(args);
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
                    client.Character.CP = shopp;

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
            if (client.Character.Job == PC_JOB.ASTRALIST)//魔法师
            {
                client.Character.EP = 0;
            }
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
                catch (Exception ex)
                {
                }
            }
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
                    client.Character.SkillsReserve.Clear();
                    client.Character.Skills2_1.Clear();
                    client.Character.Skills2_2.Clear();
                    client.Character.Skills3.Clear();
                    break;
                case 1: //1轉
                    client.Character.Skills.Clear();
                    break;
                case 2: //2轉
                    client.Character.Skills2.Clear();
                    client.Character.Skills2_1.Clear();
                    client.Character.Skills2_2.Clear();
                    break;
                case 3: //保留技能
                    client.Character.Skills3.Clear();
                    client.Character.SkillsReserve.Clear();
                    break;
            }
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
        }

        private void ProcessChat(MapClient client, string args)
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
        private void ProcessSetShop(MapClient client, string args)
        {
            /*client.SendSystemMessage("店铺系统暂时关闭，正在整顿中。详细情况请见Yuki.cc公告。");
            return;
            if (client.Character.Account.AccountID <= 247)
            {
                client.SendSystemMessage("老账号暂时无法交易哦，等确认到没有BUG再开！");
                return;
            }*/
            if (client.Character.MapID != 10054000 && client.Character.Account.GMLevel < 30)
            {
                client.SendSystemMessage("该地区无法开设店铺。");
                return;
            }
            Packets.Server.SSMG_PLAYER_SETSHOP_OPEN_SETUP p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SETSHOP_OPEN_SETUP();
            Packets.Server.SSMG_PLAYER_SHOP_APPEAR p2 = new Packets.Server.SSMG_PLAYER_SHOP_APPEAR();
            p2.ActorID = client.Character.ActorID;
            p2.Title = client.Shoptitle;
            client.Shopswitch = 0;
            p2.button = 0;
            p1.Comment = client.Shoptitle;
            client.netIO.SendPacket(p1);
            client.netIO.SendPacket(p2);
        }
       
        private void ProcessDuang(MapClient client, string args)
        {
            ActorPC pc = client.Character;
            if (pc.TInt["屏蔽特效"] == 0)
            {
                pc.TInt["屏蔽特效"] = 1;
                client.SendSystemMessage("当前特效显示已设置为：只屏蔽其他玩家的额外特效");
            }
            else if(pc.TInt["屏蔽特效"] == 1)
            {
                pc.TInt["屏蔽特效"] = 2;
                client.SendSystemMessage("当前特效显示已设置为：屏蔽全部的额外特效");
            }
            else
            {
                pc.TInt["屏蔽特效"] = 0;
                client.SendSystemMessage("当前特效显示已设置为：不屏蔽特效");
            }
        }
        private void ProcessGreetingMotion(MapClient client, string args)
        {
            ActorPC pc = client.Character;
            int motionid = 0;
            if (args != "")
                motionid = int.Parse(args);
            client.Character.AInt["个人固定打招呼动作"] = motionid;
            client.SendSystemMessage("打招呼固定动作已设定为：" + motionid);
        }
        private void ProcessCheckPartner(MapClient client, string args)
        {
            ActorPC pc = client.Character;
            if (pc.Partner == null) return;
            string nextlvup = pc.Partner.reliabilityexp + "/" + ExperienceManager.Instance.PartnerReliabilityEXPChart[(byte)(pc.Partner.reliability + 1)];
            client.SendSystemMessage("搭档" + pc.Partner.Name + "的当前信赖经验值：" + nextlvup);
        }
        private void ProcessDie(MapClient client, string args)
        {
            ActorPC pc = client.Character;
            if (pc.MapID == 10054000)
            {
                client.SendSystemMessage("不可以在这里自杀！");
                return;
            }
            if (!pc.Status.Additions.ContainsKey("自杀CD"))
            {
                int damage = (int)(pc.MaxHP * 5);
                Skill.SkillHandler.Instance.CauseDamage(pc, pc, damage, true);
                Skill.SkillHandler.Instance.ShowVessel(pc, damage);
                Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5446);
                Skill.SkillHandler.Instance.StableBuffsHandler["自杀CD"].ApplyBuff(pc, 3600000);
            }
            else
            {
                client.SendSystemMessage("不可以频繁自杀，对身体不好！");
            }
        }
        private void ProcessBossTime(MapClient client, string args)
        {
            try
            {
                List<ActorMob> mobs = new List<ActorMob>();

                foreach (ActorMob item in MobFactory.Instance.BossList)
                {
                    if (item.Tasks.ContainsKey("Respawn"))
                    {
                        mobs.Add(item);
                        string str = "BOSS『" + item.Name + "』复活时间剩余：";
                        TimeSpan duration = item.Tasks["Respawn"].NextUpdateTime - DateTime.Now;
                        if (duration.Hours > 0) str += duration.Hours.ToString() + "小时";
                        if (duration.Minutes > 0) str += duration.Minutes.ToString() + "分钟";
                        if (duration.Seconds > 0) str += duration.Seconds.ToString() + "秒";
                        client.SendSystemMessage(str + "。");
                    }
                    //else
                    //client.SendSystemMessage("『" + item.Name + "』存活中。");
                }
                if (mobs.Count < 1)
                {
                    client.SendSystemMessage("当前没有BOSS等待重生。");
                    return;
                }
            }
            catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
        }
        private void ProcessPraise(MapClient client, string args)
        {
            Item item = client.Character.Inventory.GetItem(950000040, Inventory.SearchType.ITEM_ID);
            if (item == null) return;
            if (item.Stack < 1) return;
            if (client.Character.Level < 65)
            {
                client.SendSystemMessage("等级还不足65级，还不能赞扬玩家哦。");
                return;
            }
            if (args != "")
            {
                try
                {
                    var chr =
                        from c in MapClientManager.Instance.OnlinePlayer
                        where c.Character.Name == args
                        select c;
                    if (chr == null)
                    {
                        client.SendSystemMessage("指定的玩家不存在或者不在线。");
                        return;
                    }

                    MapClient tClient = chr.First();
                    if (tClient.Character.Name == client.Character.Name)
                    {
                        client.SendSystemMessage("不可以指定自己哦。");
                        return;
                    }

                    client.DeleteItemID(950000040, 1, true);
                    tClient.TitleProccess(tClient.Character, 35, 1);
                    tClient.TitleProccess(tClient.Character, 36, 1);
                    tClient.TitleProccess(tClient.Character, 37, 1);
                    item = ItemFactory.Instance.GetItem(910000115);
                    item.Stack = 30;
                    client.AddItem(item, true);

                    item = ItemFactory.Instance.GetItem(910000116);
                    item.Stack = 10;
                    client.AddItem(item, true);

                    item = ItemFactory.Instance.GetItem(950000000);
                    item.Stack = 3;
                    client.AddItem(item, true);

                    item = ItemFactory.Instance.GetItem(950000001);
                    item.Stack = 3;
                    client.AddItem(item, true);

                    item = ItemFactory.Instance.GetItem(950000025);
                    item.Stack = 20;
                    client.AddItem(item, true);

                    item = ItemFactory.Instance.GetItem(950000028);
                    item.Stack = 1;
                    client.AddItem(item, true);

                    item = ItemFactory.Instance.GetItem(910000104);
                    item.Stack = 1;
                    client.AddItem(item, true);

                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                        i.SendAnnounce("恭喜萌新玩家 " + client.Character.Name + " 顺利地达到了65级，并使用了「萌新之证」，她所赞扬的玩家是：" + tClient.Character.Name);
                }
                catch (Exception) { }
            }
        }
        private void ProcessAccept(MapClient client, string args)
        {
            if (client.Character.Status.Additions.ContainsKey("等待接受挑战") && client.Character.TInt["挑战者AID"] != 0)
            {

                if (client.Character.MapID != 10054000 || client.Character.MapID != 10054000) return;
                ActorPC target = null;
                Map map = MapManager.Instance.GetMap(client.Character.MapID);
                target = (ActorPC)map.GetActor((uint)client.Character.TInt["挑战者AID"]);
                Skill.SkillHandler.RemoveAddition(client.Character, "等待接受挑战");
                if (target == null) return;
                if (target.Mode != PlayerMode.NORMAL || client.Character.Mode != PlayerMode.NORMAL) return;

                target.Mode = PlayerMode.KNIGHT_WEST;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, target, true);

                client.Character.Mode = PlayerMode.KNIGHT_EAST;

                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, client.Character, true);

                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, target, true);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, client.Character, true);

                client.SendCharInfoUpdate();
                MapClient.FromActorPC(target).SendCharInfoUpdate();
            }
        }
        private void ProcessRefuse(MapClient client, string args)
        {
            if (client.Character.CInt["挑战开关"] == 0)
            {
                client.Character.CInt["挑战开关"] = 1;
                client.SendSystemMessage("你将自动拒绝所有挑战邀请。");
            }
            else
            {
                client.Character.CInt["挑战开关"] = 0;
                client.SendSystemMessage("你将开始接受挑战邀请。");
            }
        }
        private void ProcessAutoLock(MapClient client, string args)
        {
            if (client.Character.CInt["自动锁定模式"] == 0)
            {
                client.Character.CInt["自动锁定模式"] = 1;
                client.SendSystemMessage("自动锁定模式开启！没有锁定目标时，将会自动寻找周围目标锁定。");
            }
            else
            {
                client.Character.CInt["自动锁定模式"] = 0;
                client.SendSystemMessage("自动锁定模式关闭。");
            }
        }

        private void ProcessOpenWare(MapClient client, string args)
        {
            client.currentWarehouse = WarehousePlace.Acropolis;

            client.SendWareItems(WarehousePlace.Acropolis);
        }
        private void ProcessTrumpet(MapClient client, string args)
        {
            if (args == "")
            {
                //client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ANNOUNCE_PARA);
                client.SendSystemMessage("用法: /w 内容 ");
            }
            else
            {
                if (client.Character.Gold >= 0)
                {
                    //client.Character.Gold -= 50;
                    try
                    {
                        if (args == "我最喜欢穿小裙子！")
                            if (MapClientManager.Instance.OnlinePlayerOnlyIP.Count >= 30)
                                client.TitleProccess(client.Character, 34, 1);
                        foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                        {
                            Packets.Server.SSMG_CHAT_WHOLE p = new SagaMap.Packets.Server.SSMG_CHAT_WHOLE();
                            p.Sender = "[大喇叭]" + client.Character.Name;
                            p.Content = args;
                            i.netIO.SendPacket(p);
                        }

                    }
                    catch (Exception) { }
                }
                else
                    client.SendSystemMessage("该功能需要50G!");
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

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="client"></param>
        /// <param name="args"></param>
        private void ProcessCh(MapClient client, string args)
        {


        }

        
        
        #region "Admin commands"

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
                    i.Buff.死んだふり = true;
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

        }

        //Be careful with this command
        private void ProcessCallAll(MapClient client, string args)
        {

        }


        #endregion

        #region "Dev commands"

        private void ProcessRaw(MapClient client, string args)
        {
            if (args == "")
                args = "12 0C 00 00 01 F4 32 32 00 32 02 00 00 00 00 00 00 00 00 50 00 00 00 50";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
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
            int page = 1;
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