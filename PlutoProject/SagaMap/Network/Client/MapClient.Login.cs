using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Theater;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using System.Globalization;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        bool golemLogout = false;
        bool needSendGolem = false;
        public bool fgTakeOff = false;
        PacketLogger.PacketLogger logger;
        public DateTime ping = DateTime.Now;

        public void OnPing(Packets.Client.CSMG_PING p)
        {
            if (this.chara != null)
            {
                if (this.chara.Online)
                {
                    ping = DateTime.Now;
                    if (!this.chara.Tasks.ContainsKey("Ping"))
                    {
                        Tasks.PC.Ping pa = new SagaMap.Tasks.PC.Ping(this);
                        this.chara.Tasks.Add("Ping", pa);
                        pa.Activate();
                    }
                }
            }
            Packets.Server.SSMG_PONG p2 = new SagaMap.Packets.Server.SSMG_PONG();
            this.netIO.SendPacket(p2);
        }

        public void OnSendVersion(Packets.Client.CSMG_SEND_VERSION p)
        {
            if (Configuration.Instance.ClientVersion == null || Configuration.Instance.ClientVersion == p.GetVersion())
            {
                Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.CLIENT_CONNECTING, p.GetVersion()));
                this.client_Version = p.GetVersion();

                Packets.Server.SSMG_VERSION_ACK p1 = new SagaMap.Packets.Server.SSMG_VERSION_ACK();
                p1.SetResult(SagaMap.Packets.Server.SSMG_VERSION_ACK.Result.OK);
                p1.SetVersion(this.client_Version);
                this.netIO.SendPacket(p1);
                //Official HK server will now request for Hackshield GUID check , we don't know its algorithms, so not implemented
                Packets.Server.SSMG_LOGIN_ALLOWED p2 = new SagaMap.Packets.Server.SSMG_LOGIN_ALLOWED();
                this.frontWord = (uint)Global.Random.Next();
                this.backWord = (uint)Global.Random.Next();
                p2.FrontWord = this.frontWord;
                p2.BackWord = this.backWord;
                this.netIO.SendPacket(p2);
            }
            else
            {
                Packets.Server.SSMG_VERSION_ACK p2 = new SagaMap.Packets.Server.SSMG_VERSION_ACK();
                p2.SetResult(SagaMap.Packets.Server.SSMG_VERSION_ACK.Result.VERSION_MISSMATCH);
                this.netIO.SendPacket(p2);
            }
        }

        public void OnLogin(Packets.Client.CSMG_LOGIN p)
        {
            p.GetContent();
            if (MapServer.shutingdown)
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_IPBLOCK;
                this.netIO.SendPacket(p1);
                return;
            }

            if (SagaMap.Tasks.System.AJImode.Instance.StopLogin)
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_IPBLOCK;
                this.netIO.SendPacket(p1);
                return;
            }

            if (MapServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.OK;
                p1.Unknown1 = 0x100;
                p1.TimeStamp = (uint)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

                this.netIO.SendPacket(p1);
                /*if(MapClientManager.Instance.OnlinePlayer.Count > 3)
                    System.Environment.Exit(System.Environment.ExitCode);*/


                account = MapServer.accountDB.GetUser(p.UserName);
                var check = from acc in MapClientManager.Instance.OnlinePlayer
                            where acc.account.Name == account.Name
                            select acc;
                foreach (MapClient i in check)
                {
                    i.netIO.Disconnect();
                }

                account.LastIP = this.netIO.sock.RemoteEndPoint.ToString().Split(':')[0];
                account.MacAddress = p.MacAddress;

                //这里检查同mac的已在线玩家, 如果大于或等于2个. 则断开当前请求的连接
                var players = MapClientManager.Instance.OnlinePlayer;
                var insamemac = players.Count(x => x.account.MacAddress == account.MacAddress);
                var insameip = players.Count(x => x.account.LastIP == account.LastIP);
                var onlinecount = Math.Max(insamemac, insameip);
                if (onlinecount >= Configuration.Instance.MaxCharacterInMapServer)
                {
                    netIO.Disconnect();
                    return;
                }


                //VariableHolderA<string, int> list = ScriptManager.Instance.VariableHolder.Adict["多开MAC限制"];
                //VariableHolderA<string, int> dailyban = ScriptManager.Instance.VariableHolder.Adict["多开当日限制登录的账号"];
                //if (ScriptManager.Instance.VariableHolder.AStr["多开MAC限制时间"] != DateTime.Now.ToString("yyyy-MM-dd"))
                //{
                //    ScriptManager.Instance.VariableHolder.AStr["多开MAC限制时间"] = DateTime.Now.ToString("yyyy-MM-dd");
                //    list = new VariableHolderA<string, int>();
                //    dailyban = new VariableHolderA<string, int>();
                //}

                //if(dailyban.ContainsKey(account.Name))
                //{
                //    netIO.Disconnect();
                //    return;
                //}
                //if (account.AccountID > 247)
                //{
                //    if (!list.ContainsKey(account.MacAddress))
                //        list[account.MacAddress] = 0;
                //    if (list[account.MacAddress] == 0)
                //        list[account.MacAddress] = account.AccountID;
                //    else
                //    {
                //        if ((list[account.MacAddress] != account.AccountID || dailyban.ContainsKey(account.Name)) && account.GMLevel < 20)
                //        {
                //            netIO.Disconnect();
                //            //这个ban造成了不好的影响，暂时关掉
                //            //dailyban[account.Name] = account.AccountID;
                //            return;
                //        }
                //    }
                //}

                //byte count = 0;
                //foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                //{
                //    if (i.Character.Account.LastIP == account.LastIP && i.Character.Account.GMLevel < 20)
                //    {
                //        count++;
                //    }
                //}
                //if (count > 2)
                //{
                //    netIO.Disconnect();
                //    return;
                //}

                uint[] charIDs = MapServer.charDB.GetCharIDs(account.AccountID);

                account.Characters = new List<ActorPC>();
                for (int i = 0; i < charIDs.Length; i++)
                {
                    account.Characters.Add(MapServer.charDB.GetChar(charIDs[i]));
                }
                this.state = SESSION_STATE.AUTHENTIFICATED;
            }
            else
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS;
                this.netIO.SendPacket(p1);
            }
        }

        public void OnCharSlot(Packets.Client.CSMG_CHAR_SLOT p)
        {
            if (this.state == SESSION_STATE.AUTHENTIFICATED)
            {
                var chr =
                    from c in account.Characters
                    where c.Slot == p.Slot
                    select c;
                this.Character = chr.First();
                //if (MapClientManager.Instance.OnlinePlayer.Count > 10)
                //{

                //}
                MapServer.charDB.GetDualJobInfo(this.Character);
                if (this.Character.DualJobID != 0)
                    this.Character.DualJobLevel = this.Character.PlayerDualJobList[this.Character.DualJobID].DualJobLevel;
                if (this.Character.PossessionTarget != 0)
                {
                    if (this.Character.PossessionTarget < 10000)
                    {
                        MapClient client = ((MapClient)MapClientManager.Instance.GetClient(this.Character.PossessionTarget));
                        if (client != null)
                        {
                            ActorPC pc = client.Character;
                            ActorPC found = null;
                            foreach (ActorPC i in pc.PossesionedActors)
                            {
                                if (i.CharID == this.Character.CharID)
                                {
                                    found = i;
                                    break;
                                }
                            }
                            if (found != null)
                            {
                                found.Inventory = this.Character.Inventory;
                                this.Character = found;

                                this.Character.MapID = pc.MapID;
                                this.Character.X = pc.X;
                                this.Character.Y = pc.Y;
                            }
                            else
                            {
                                this.Character.PossessionTarget = 0;
                            }
                        }
                        else
                            this.Character.PossessionTarget = 0;
                    }
                    else
                    {
                        SagaMap.Map map = MapManager.Instance.GetMap(this.Character.MapID);
                        if (map != null)
                        {
                            Actor actor = map.GetActor(this.Character.PossessionTarget);
                            if (actor == null)
                            {
                                this.Character.PossessionTarget = 0;
                            }
                            else
                            {
                                if (actor.type == ActorType.ITEM)
                                {
                                    ActorItem item = (ActorItem)actor;
                                    if (item.Item.PossessionedActor.CharID == this.Character.CharID)
                                    {
                                        ActorPC pc = (ActorPC)item.Item.PossessionedActor;
                                        pc.Inventory = this.Character.Inventory;
                                        this.Character = pc;
                                        this.Character.MapID = item.MapID;
                                        this.Character.X = item.X;
                                        this.Character.Y = item.Y;
                                    }
                                    else
                                    {
                                        this.Character.PossessionTarget = 0;
                                    }
                                }
                                else
                                    this.Character.PossessionTarget = 0;
                            }
                        }
                        else
                            this.Character.PossessionTarget = 0;
                    }
                }

                if (this.Character.Golem != null)
                {
                    SagaMap.Map map = MapManager.Instance.GetMap(this.Character.MapID);
                    if (map != null)
                    {
                        Actor actor = map.GetActor(this.Character.Golem.ActorID);
                        if (actor != null)
                        {
                            if (actor.type == ActorType.GOLEM)
                            {
                                ActorGolem golem = (ActorGolem)actor;

                                if (golem.Owner.CharID == this.Character.CharID)
                                {
                                    golem.Owner.Inventory.WareHouse = this.Character.Inventory.WareHouse;
                                    this.Character.Inventory = golem.Owner.Inventory;
                                    this.Character.Gold = golem.Owner.Gold;
                                    this.Character.Golem = golem;
                                    golem.ClearTaskAddition();
                                    map.DeleteActor(golem);
                                }
                                else
                                    this.Character.Golem = null;
                            }
                        }
                        else
                            this.Character.Golem = null;
                    }
                    else
                        this.Character.Golem = null;
                }
                if (this.Character.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.PET))
                {
                    if (this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.RIDE_PET || this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.RIDE_PARTNER)
                    {
                        this.Character.Pet = new ActorPet(this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.petID, this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET]);
                        this.Character.Pet.Ride = true;
                        this.Character.Pet.Owner = this.Character;
                    }
                }

                this.Character.e = new ActorEventHandlers.PCEventHandler(this);
                if (this.Character.Account == null) this.Character.Account = account;
                this.Character.Online = true;
                this.Character.Party = PartyManager.Instance.GetParty(this.Character.Party);
                PartyManager.Instance.PlayerOnline(this.Character.Party, this.Character);

                this.Character.Ring = RingManager.Instance.GetRing(this.Character.Ring);
                RingManager.Instance.PlayerOnline(this.Character.Ring, this.Character);

                Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.PLAYER_LOG_IN, this.Character.Name));
                Logger.ShowInfo(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count);

                ActorPC SerPC = ScriptManager.Instance.VariableHolder;
                string day = DateTime.Now.ToString("d");

                //记录最大在线人数
                if (MapClientManager.Instance.OnlinePlayer.Count > SerPC.AInt[day + "最大在线人数"])
                {
                    SerPC.AInt[day + "最大在线人数"] = MapClientManager.Instance.OnlinePlayer.Count;
                    SerPC.AStr[day + "最大在线人数日期"] = DateTime.Now.ToString("T");
                }

                //记录当日账号登陆数量
                if (!SerPC.Adict[day + "账号统计"].ContainsKey(Character.Account.Name))
                {
                    SerPC.Adict[day + "账号统计"][Character.Account.Name] = 1;
                    SerPC.AInt[day + "登陆账号数"]++;
                }
                else
                    SerPC.Adict[day + "账号统计"][Character.Account.Name]++;

                //记录当日IP登陆数量
                if (!SerPC.Adict[day + "IP统计"].ContainsKey(Character.Account.LastIP))
                {
                    SerPC.Adict[day + "IP统计"][Character.Account.LastIP] = 1;
                    SerPC.AInt[day + "登陆IP数"]++;
                }
                else
                    SerPC.Adict[day + "IP统计"][Character.Account.LastIP]++;

                MapServer.shouldRefreshStatistic = true;

                this.Map = MapManager.Instance.GetMap(this.Character.MapID);
                if (this.map == null)
                {
                    if (this.Character.SaveMap == 0)
                    {
                        this.Character.SaveMap = 10023100;
                        this.Character.SaveX = 242;
                        this.Character.SaveY = 128;
                    }
                    this.Character.MapID = this.Character.SaveMap;
                    this.map = MapManager.Instance.GetMap(this.Character.SaveMap);
                    this.Character.X = Global.PosX8to16(this.chara.SaveX, map.Width);
                    this.Character.Y = Global.PosY8to16(this.chara.SaveY, map.Height);

                }
                if (this.map.IsMapInstance && this.chara.PossessionTarget == 0)
                {
                    Map map = this.map;
                    this.Character.MapID = this.map.ClientExitMap;
                    this.map = MapManager.Instance.GetMap(this.map.ClientExitMap);
                    this.Character.X = Global.PosX8to16(map.ClientExitX, this.map.Width);
                    this.Character.Y = Global.PosY8to16(map.ClientExitY, this.map.Height);
                }

                if (this.chara.Race != PC_RACE.DEM)
                {
                    if (this.Character.CEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.Level, SagaMap.Scripting.LevelType.CLEVEL))
                        this.Character.CEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.Level, SagaMap.Scripting.LevelType.CLEVEL);
                    if (this.Character.DominionCEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.DominionLevel, SagaMap.Scripting.LevelType.CLEVEL))
                        this.Character.DominionCEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.DominionLevel, SagaMap.Scripting.LevelType.CLEVEL);
                    if (this.Character.DominionJEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.DominionJobLevel, SagaMap.Scripting.LevelType.JLEVEL2))
                        this.Character.DominionJEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.DominionJobLevel, SagaMap.Scripting.LevelType.JLEVEL2);
                    if (this.Character.Job == this.Character.JobBasic)
                    {
                        if (this.Character.JEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel1, SagaMap.Scripting.LevelType.JLEVEL))
                            this.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel1, SagaMap.Scripting.LevelType.JLEVEL);
                    }
                    else if (this.Character.Job == this.Character.Job2X)
                    {
                        if (this.Character.JEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2X, SagaMap.Scripting.LevelType.JLEVEL2))
                            this.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2X, SagaMap.Scripting.LevelType.JLEVEL2);
                    }
                    else if (this.Character.Job == this.Character.Job2T)
                    {
                        if (this.Character.JEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2T, SagaMap.Scripting.LevelType.JLEVEL2))
                            this.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2T, SagaMap.Scripting.LevelType.JLEVEL2);
                    }
                    else
                    {
                        if (this.Character.JEXP < ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel3, SagaMap.Scripting.LevelType.JLEVEL3))
                            this.Character.JEXP = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel3, SagaMap.Scripting.LevelType.JLEVEL3);
                    }
                }
                if (this.Character.DominionStr < Configuration.Instance.StartupSetting[this.Character.Race].Str)
                    this.Character.DominionStr = Configuration.Instance.StartupSetting[this.Character.Race].Str;
                if (this.Character.DominionDex < Configuration.Instance.StartupSetting[this.Character.Race].Dex)
                    this.Character.DominionDex = Configuration.Instance.StartupSetting[this.Character.Race].Dex;
                if (this.Character.DominionInt < Configuration.Instance.StartupSetting[this.Character.Race].Int)
                    this.Character.DominionInt = Configuration.Instance.StartupSetting[this.Character.Race].Int;
                if (this.Character.DominionVit < Configuration.Instance.StartupSetting[this.Character.Race].Vit)
                    this.Character.DominionVit = Configuration.Instance.StartupSetting[this.Character.Race].Vit;
                if (this.Character.DominionAgi < Configuration.Instance.StartupSetting[this.Character.Race].Agi)
                    this.Character.DominionAgi = Configuration.Instance.StartupSetting[this.Character.Race].Agi;
                if (this.Character.DominionMag < Configuration.Instance.StartupSetting[this.Character.Race].Mag)
                    this.Character.DominionMag = Configuration.Instance.StartupSetting[this.Character.Race].Mag;

                this.Character.WRPRanking = WRPRankingManager.Instance.GetRanking(this.Character);

                foreach (SagaDB.Item.Item i in this.Character.Inventory.Equipments.Values)
                {
                    if (i.BaseData.jointJob != PC_JOB.NONE)
                    {
                        this.Character.JobJoint = i.BaseData.jointJob;
                        break;
                    }
                }
                this.Map.RegisterActor(this.Character);
                this.state = SESSION_STATE.LOADING;
                if (this.Character.Golem != null)
                {
                    needSendGolem = true;
                }

                //现在,死亡的角色上线会回到记录点
                if (Character.HP == 0)
                {
                    if (this.Character.SaveMap == 0)
                    {
                        this.Character.SaveMap = 10023000;
                        this.Character.SaveX = 123;
                        this.Character.SaveY = 233;
                    }
                    this.Character.MapID = this.Character.SaveMap;
                    this.map = MapManager.Instance.GetMap(this.Character.SaveMap);
                    this.Character.X = Global.PosX8to16(this.chara.SaveX, map.Width);
                    this.Character.Y = Global.PosY8to16(this.chara.SaveY, map.Height);
                    this.Character.HP = 1;
                }

                if (this.map.ID == 90001999 || this.Character.MapID == 91000999)
                {
                    this.SendGotoFF();

                    Packet p2 = new Packet();
                    p2.data = new byte[3];
                    p2.ID = 0x122a;
                    this.netIO.SendPacket(p);
                }

                if (!this.Character.Tasks.ContainsKey("Recover"))//自然恢复
                {
                    Tasks.PC.Recover reg = new Tasks.PC.Recover(this);
                    this.Character.Tasks.Add("Recover", reg);
                    reg.Activate();
                }

                /*
                SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
                
                if (this.Character.Level >= LL.NowLevelLimit)
                {
                    this.SendAnnounce("当前等级暂时到达上限，正在接受圣塔的福利...");
                    this.SendAnnounce(string.Format("下次等级上限 {0} 级的解除时间为 {1}",LL.NextLevelLimit,LL.NextTime.ToString()));
                    SagaMap.LevelLimit.LevelLimitManager.Instance.SendReachInfo(this);
                }*/
                PC.StatusFactory.Instance.CalcStatus(this.chara);
                /*if (this.chara.EPLoginTime < DateTime.Now)
                {
                    this.chara.EP += 10;
                    if (this.chara.EP > this.chara.MaxEP)
                        this.chara.EP = this.chara.MaxEP;
                    this.chara.EPLoginTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);
                }
                else
                {
                    TimeSpan span = this.chara.EPLoginTime - DateTime.Now;
                    SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.EP_INCREASE, (int)span.Hours + 1));
                }*///改革！

                //packet logger for unnormal player
                foreach (string i in Configuration.Instance.MonitorAccounts)
                {
                    if (this.account.Name.StartsWith(i))
                    {
                        logger = new SagaMap.PacketLogger.PacketLogger(this);
                        break;
                    }
                }

                if (Logger.defaultSql != null)
                {
                    Logger.defaultSql.SQLExecuteNonQuery("UPDATE `char` SET `online` = 1 WHERE `char_id` = " + this.chara.CharID.ToString());
                }
            }
        }

        public void OnMapLoaded(Packets.Client.CSMG_PLAYER_MAP_LOADED p)
        {
            this.CheckAPI = false;
            this.Character.invisble = false;
            this.Character.VisibleActors.Clear();
            this.Map.OnActorVisibilityChange(this.Character);
            this.Map.SendVisibleActorsToActor(this.Character);

            if (Character.MapID == 21190000)
                Character.CInt["NextMoveEventID"] = 80000103;
            if (this.Character.MapID == 90001999)
                CustomMapManager.Instance.EnterFFOnMapLoaded(this);
            //if (this.Character.MapID == 90001999 || this.Character.MapID == 91000999)
            //CustomMapManager.Instance.SendGotoSerFFMap(this);

            if (this.Character.TInt["TempBGM"] > 0)
            {
                Packets.Server.SSMG_NPC_CHANGE_BGM p3 = new Packets.Server.SSMG_NPC_CHANGE_BGM();
                p3.SoundID = (uint)this.Character.TInt["TempBGM"];
                p3.Loop = 50;
                p3.Volume = 100;
                this.netIO.SendPacket(p3);
                this.Character.TInt["TempBGM"] = 0;
                this.Character.TInt.Remove("TempBGM");
            }
            if (needSendGolem)
            {
                ActorGolem golem = this.Character.Golem;
                if (golem.GolemType == GolemType.Sell)
                {
                    Packets.Server.SSMG_GOLEM_SHOP_SELL_RESULT p2 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_SELL_RESULT();
                    p2.SoldItems = golem.SoldItem;
                    this.netIO.SendPacket(p2);
                }
                if (golem.GolemType == GolemType.Buy)
                {
                    Packets.Server.SSMG_GOLEM_SHOP_BUY_RESULT p2 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_RESULT();
                    p2.BoughtItems = golem.BoughtItem;
                    this.netIO.SendPacket(p2);
                }
                if (golem.GolemType >= GolemType.Plant && golem.GolemType <= GolemType.Strange)
                {
                    this.OnGolemWarehouse(new SagaMap.Packets.Client.CSMG_GOLEM_WAREHOUSE());
                }
                this.Character.Golem.SoldItem.Clear();
                this.Character.Golem.BoughtItem.Clear();
                this.Character.Golem.SellShop.Clear();
                this.Character.Golem.BuyShop.Clear();
                needSendGolem = false;
            }

            SendFGEvent();
            SendDungeonEvent();

            if (this.Character.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.PET))
            {
                if (this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].IsPet && Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.PET)
                    this.SendPet(this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET]);
                else if (Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.PARTNER)
                {
                    SendPartner(this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET]);
                    PartnerTalking(Character.Partner, TALK_EVENT.MASTERLOGIN, 100);
                }
            }
            PC.StatusFactory.Instance.CalcStatus(this.Character);
            SendEquip();
            this.Character.e.OnActorChangeBuff(this.Character);

            if (this.Character.PossessionTarget != 0)
            {
                Actor actor = this.Map.GetActor(this.Character.PossessionTarget);
                if (actor != null)
                    this.Character.e.OnActorAppears(actor);
            }

            foreach (Actor i in this.Character.PossesionedActors)
            {
                if (i != this.Character)
                    i.e.OnActorAppears(this.Character);
            }

            SendQuestInfo();
            SendQuestPoints();
            SendQuestCount();
            SendQuestTime();
            SendQuestStatus();
            SendStamp();
            SendTamaire();
            SendTitleList();
            SendWRPRanking(this.Character);

            if (SagaDB.ODWar.ODWarFactory.Instance.Items.ContainsKey(map.ID))
            {
                if (!this.chara.Skills.ContainsKey(2457))
                {
                    SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2457, 1);
                    skill.NoSave = true;
                    this.chara.Skills.Add(2457, skill);
                }
            }
            else
            {
                if (this.chara.Skills.ContainsKey(2457))
                {
                    this.chara.Skills.Remove(2457);
                }
            }

            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
            {
                if (this.Character.WRPRanking <= 10)
                {
                    if (!this.chara.Skills.ContainsKey(10500))
                    {
                        SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(10500, 1);
                        skill.NoSave = true;
                        this.chara.Skills.Add(10500, skill);
                    }
                }
                else
                {
                    if (this.chara.Skills.ContainsKey(10500))
                    {
                        this.chara.Skills.Remove(10500);
                    }
                }
            }
            else
            {
                if (this.chara.Skills.ContainsKey(10500))
                {
                    this.chara.Skills.Remove(10500);
                }
            }

            SendPlayerInfo();


            if (this.map.Info.Flag.Test(SagaDB.Map.MapFlags.Wrp))
            {
                SendSystemMessage(LocalManager.Instance.Strings.WRP_ENTER);
            }

            SendPartyInfo();
            SendRingInfo(SagaMap.Packets.Server.SSMG_RING_INFO.Reason.NONE);
            SendRingFF();
            PartyManager.Instance.UpdateMemberPosition(this.Character.Party, this.Character);
            if (this.map.IsDungeon && this.Character.Party != null)
                PartyManager.Instance.UpdateMemberDungeonPosition(this.Character.Party, this.Character);

            if (TheaterFactory.Instance.Items.ContainsKey(this.map.ID))
            {
                Movie nextMovie = TheaterFactory.Instance.GetNextMovie(this.map.ID);
                if (nextMovie != null)
                {
                    Packets.Server.SSMG_THEATER_INFO p3 = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                    p3.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MESSAGE;
                    p3.Message = LocalManager.Instance.Strings.THEATER_WELCOME;
                    this.netIO.SendPacket(p3);
                    p3 = new SagaMap.Packets.Server.SSMG_THEATER_INFO();
                    p3.MessageType = SagaMap.Packets.Server.SSMG_THEATER_INFO.Type.MOVIE_ADDRESS;
                    p3.Message = nextMovie.URL;
                    this.netIO.SendPacket(p3);
                }
            }

            if (!this.Character.Tasks.ContainsKey("QuestTime"))
            {
                Tasks.PC.QuestTime task = new SagaMap.Tasks.PC.QuestTime(this);
                this.Character.Tasks.Add("QuestTime", task);
                task.Activate();
            }

            if (!this.Character.Tasks.ContainsKey("AutoSave"))
            {
                Tasks.PC.AutoSave task = new SagaMap.Tasks.PC.AutoSave(this.Character);
                this.Character.Tasks.Add("AutoSave", task);
                task.Activate();
            }
            //SP改革
            /*if (!this.Character.Tasks.ContainsKey("SpRecover"))
            {
                Tasks.PC.SpRecover task = new SagaMap.Tasks.PC.SpRecover(this);
                this.Character.Tasks.Add("SpRecover", task);
                task.Activate();
            }*/

            if (this.Map.Info.Healing)
            {
                if (!this.Character.Tasks.ContainsKey("CityRecover"))
                {
                    Tasks.PC.CityRecover task = new SagaMap.Tasks.PC.CityRecover(this);
                    this.Character.Tasks.Add("CityRecover", task);
                    task.Activate();
                }
            }
            else
            {
                if (this.Character.Tasks.ContainsKey("CityRecover"))
                {
                    this.Character.Tasks["CityRecover"].Deactivate();
                    this.Character.Tasks.Remove("CityRecover");
                }
            }

            if (this.Map.Info.Cold || this.map.Info.Hot || this.map.Info.Wet)
            {
                if (!this.Character.Tasks.ContainsKey("CityDown"))
                {
                    Tasks.PC.CityDown task = new SagaMap.Tasks.PC.CityDown(this);
                    this.Character.Tasks.Add("CityDown", task);
                    task.Activate();
                }
            }
            else
            {
                if (this.Character.Tasks.ContainsKey("CityDown"))
                {
                    this.Character.Tasks["CityDown"].Deactivate();
                    this.Character.Tasks.Remove("CityDown");
                }
            }

            if (this.Character.PossessionTarget != 0)
            {
                if (!this.Character.Tasks.ContainsKey("PossessionRecover"))
                {
                    Tasks.PC.PossessionRecover task = new SagaMap.Tasks.PC.PossessionRecover(this);
                    this.Character.Tasks.Add("PossessionRecover", task);
                    task.Activate();
                }
            }

            Packets.Server.SSMG_LOGIN_FINISHED p1 = new SagaMap.Packets.Server.SSMG_LOGIN_FINISHED();
            this.netIO.SendPacket(p1);
            this.state = SESSION_STATE.LOADED;

            if (this.Character.TInt["NextMapEvent"] > 0)
            {
                this.EventActivate((uint)this.Character.TInt["NextMapEvent"]);
                this.Character.TInt["NextMapEvent"] = 0;
            }
            if (this.Character.MapID == 90001999)
                CustomMapManager.Instance.EnterFFOnMapLoaded(this);

            //SendMails();
            //SendGifts();

            SendNPCStates();

            //誰的坑！！快填平！
            //SendMosterGuide();

            Character.Speed = 410;
            SendActorSpeed(this.Character, this.Character.Speed);
            if (this.Character.TranceID != 0)
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, this.Character, true);


            if (DefWarManager.Instance.IsDefWar(this.map.ID) && this.Character.DefWarShow)
            {
                Packets.Server.SSMG_DEFWAR_INFO p2 = new Packets.Server.SSMG_DEFWAR_INFO();
                p2.List = DefWarManager.Instance.GetDefWarList(this.map.ID);
                this.netIO.SendPacket(p2);
            }
            //SendGifts();

            //Check Actor is leaving Tent Map
            if (this.Character.TenkActor != null)
            {
                //檢查是否離開TentActor的所在地圖及TentMap
                if (this.Character.MapID != this.Character.TenkActor.TentMapID && this.Character.MapID != this.Character.TenkActor.MapID)
                {

                    //刪除當前地圖的TentActor
                    Map cmap = MapManager.Instance.GetMap(this.Character.TenkActor.MapID);
                    cmap.DeleteActor(this.Character.TenkActor);

                    //若TentMap 已創建則刪除地圖
                    if (this.Character.TenkActor.TentMapID != 0)
                    {
                        Map map = MapManager.Instance.GetMap(this.Character.TenkActor.Caster.MapID);
                        MapManager.Instance.DeleteMapInstance(map.ID);
                    }

                    Logger.ShowInfo("Destory Player's Tent : " + this.Character.Name + " (" + this.Character.TenkActor.EventID + ")");

                    this.Character.TenkActor = null;


                }
            }
            //Check API Item
            if (this.CheckAPI == false)
            {
                Process pr = new Process();
                pr.CheckAPIItem(this.Character.CharID, this);
                this.CheckAPI = true;
            }


            //Send Daily Stamp
            DateTime thisDay = DateTime.Today;
            if (Character.AStr["DailyStamp_DAY"] != thisDay.ToString("d"))
            {
                Packets.Server.SSMG_PLAYER_SHOW_DAILYSTAMP ds = new Packets.Server.SSMG_PLAYER_SHOW_DAILYSTAMP();
                ds.Type = 1;
                this.netIO.SendPacket(ds);
            }

            //MapDefaultScript
            if (ScriptManager.Instance.Events.ContainsKey(this.Character.MapID))
                EventActivate(this.Character.MapID);
        }

        public void OnLogout(Packets.Client.CSMG_LOGOUT p)
        {
            //竟然不清状态。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
            //Logout sequence
            //this.PossessionPrepareCancel();
            if (p.Result == (SagaMap.Packets.Client.CSMG_LOGOUT.Results)1)
            {
                golemLogout = true;
            }
            else
                golemLogout = false;
            Packets.Server.SSMG_LOGOUT p1 = new SagaMap.Packets.Server.SSMG_LOGOUT();
            p1.Result = (SagaMap.Packets.Server.SSMG_LOGOUT.Results)p.Result;
            PartnerTalking(Character.Partner, TALK_EVENT.MASTERQUIT, 100, 5000);
            this.netIO.SendPacket(p1);
        }
        public void OnSSOLogout(Packets.Client.CSMG_SSO_LOGOUT p)
        {
            //竟然不清状态。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
            //Packets.Server.SSMG_SSO_LOGOUT p1 = new Packets.Server.SSMG_SSO_LOGOUT();
            //this.netIO.SendPacket(p1);
            this.netIO.Disconnect();
        }
    }
}
