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
using SagaMap.Scripting;

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

            if (MapServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                Packets.Server.SSMG_LOGIN_ACK p1 = new SagaMap.Packets.Server.SSMG_LOGIN_ACK();
                p1.LoginResult = SagaMap.Packets.Server.SSMG_LOGIN_ACK.Result.OK;
                p1.Unknown1 = 0x100;
                p1.Unknown2 = 0x486EB420;

                this.netIO.SendPacket(p1);

                account = MapServer.accountDB.GetUser(p.UserName);
                var check = from acc in MapClientManager.Instance.OnlinePlayer
                            where acc.account.Name == account.Name
                            select acc;
                foreach (MapClient i in check)
                {
                    i.netIO.Disconnect();
                }

                //现在不再有多开的限制了
                //account.LastIP = this.netIO.sock.RemoteEndPoint.ToString().Split(':')[0];
                //foreach (MapClient i in Manager.MapClientManager.Instance.OnlinePlayer)
                //{
                //    if (i.Character.Account.LastIP == account.LastIP && i.Character.Account.GMLevel < 20)
                //        return;
                //}

                uint[] charIDs = MapServer.charDB.GetCharIDs(account.AccountID);

                account.Characters = new List<ActorPC>();

                for (int i = 0; i < charIDs.Length; i++)
                    account.Characters.Add(MapServer.charDB.GetChar(charIDs[i]));

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
                    if (this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET].BaseData.itemType == SagaDB.Item.ItemType.RIDE_PET)
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
                if (chara.Race != PC_RACE.DEM)
                {
                    if (!chara.Rebirth)
                        ExperienceManager.Instance.CheckLvUp(this, LevelType.CLEVEL);
                    else
                        ExperienceManager.Instance.CheckLvUp(this, LevelType.CLEVEL2);
                    if (chara.JobJoint == PC_JOB.NONE)
                    {
                        if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                            ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL2);
                        else
                        {
                            if (chara.Job == chara.JobBasic)
                                ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL);
                            else if (!chara.Rebirth)
                                ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL2);
                            else
                                ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL3);
                        }
                    }
                    else
                        ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL2);
                }

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

                //this.SendSystemMessage(string.Format("SagaECO SVN:{0}({1})", GlobalInfo.Version, GlobalInfo.ModifyDate));
                //this.SendSystemMessage("----------------------------------------------------------");
                //this.SendSystemMessage(LocalManager.Instance.Strings.VISIT_OUR_HOMEPAGE);
                foreach (string i in Configuration.Instance.Motd)
                {
                    this.SendSystemMessage(i);
                }
                //this.SendSystemMessage("----------------------------------------------------------");
                this.SendSystemMessage(string.Format("打怪经验: {0}% 任务经验: {1}% 任务金币: {2}% 打怪掉率: {3}%",
                    Configuration.Instance.EXPRate, Configuration.Instance.QuestRate, Configuration.Instance.QuestGoldRate, Configuration.Instance.GlobalDropRate * 100));
                this.SendSystemMessage(string.Format("死亡经验惩罚: {0}% 死亡职业经验惩罚: {1}%", Configuration.Instance.DeathPenaltyBaseEmil * 100, Configuration.Instance.DeathPenaltyJobEmil * 100));
                if (this.map.ID == 90001999 || this.Character.MapID == 91000999)
                {
                    this.SendGotoFF();

                    Packet p2 = new Packet();
                    p2.data = new byte[3];
                    p2.ID = 0x122a;
                    this.netIO.SendPacket(p);
                }

                if (this.Character.Race != PC_RACE.DEM)
                {
                    if (!this.Character.Rebirth)
                        Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.CLEVEL, 0);
                    else
                        Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.CLEVEL2, 0);
                    if (this.Character.JobJoint == PC_JOB.NONE)
                    {
                        if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                            Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL2, 0);
                        else
                        {
                            if (this.Character.Job == this.Character.JobBasic)
                                Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL, 0);
                            else if (!this.Character.Rebirth)
                                Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL2, 0);
                            else
                                Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL3, 0);
                        }
                    }
                    else
                        Manager.ExperienceManager.Instance.CheckLvUp(this, LevelType.JLEVEL2, 0);
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
                }*/
                //改革！

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
            this.Character.invisble = false;
            this.Character.VisibleActors.Clear();
            this.Map.OnActorVisibilityChange(this.Character);
            this.Map.SendVisibleActorsToActor(this.Character);

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
                this.SendPet(this.Character.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET]);
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
            if (Character.Ring != null)
                if (Character.Ring.FF_ID != 0)
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


            if (this.Map.Info.Cold && !this.Character.Status.Additions.ContainsKey("Heating") && !this.Character.Tasks.ContainsKey("CityDown"))
            {
                Tasks.PC.CityDown task = new SagaMap.Tasks.PC.CityDown(this);
                this.Character.Tasks.Add("CityDown", task);
                task.Activate();
            }
            else if (this.map.Info.Hot && !this.Character.Status.Additions.ContainsKey("IntenseHeatSheld") && !this.Character.Tasks.ContainsKey("CityDown"))
            {
                Tasks.PC.CityDown task = new SagaMap.Tasks.PC.CityDown(this);
                this.Character.Tasks.Add("CityDown", task);
                task.Activate();
            }
            else if (this.map.Info.Wet && !this.Character.Status.Additions.ContainsKey("Aqualung") && !this.Character.Tasks.ContainsKey("CityDown"))
            {
                Tasks.PC.CityDown task = new SagaMap.Tasks.PC.CityDown(this);
                this.Character.Tasks.Add("CityDown", task);
                task.Activate();
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

            if (this.Character.MapID == 90001999)
                CustomMapManager.Instance.EnterFFOnMapLoaded(this);

            SendNPCStates();
            SendActorSpeed(this.Character, this.Character.Speed);
            if (this.Character.TranceID != 0)
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, this.Character, true);
        }

        public void OnLogout(Packets.Client.CSMG_LOGOUT p)
        {
            //Logout sequence
            this.PossessionPrepareCancel();
            if (p.Result == (SagaMap.Packets.Client.CSMG_LOGOUT.Results)1)
            {
                golemLogout = true;
            }
            else
                golemLogout = false;
            Packets.Server.SSMG_LOGOUT p1 = new SagaMap.Packets.Server.SSMG_LOGOUT();
            p1.Result = (SagaMap.Packets.Server.SSMG_LOGOUT.Results)p.Result;
            this.netIO.SendPacket(p1);
        }
        public void OnSSOLogout(Packets.Client.CSMG_SSO_LOGOUT p)
        {
            Packets.Server.SSMG_SSO_LOGOUT p1 = new Packets.Server.SSMG_SSO_LOGOUT();
            this.netIO.SendPacket(p1);
        }
    }
}
