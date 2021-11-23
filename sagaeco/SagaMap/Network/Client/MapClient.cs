using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Map;
using SagaLib;
using SagaMap;
using SagaMap.Manager;

namespace SagaMap.Network.Client
{
    public partial class MapClient : SagaLib.Client 
    {
        private string client_Version;

        private uint frontWord, backWord;

        private Account account;
        private ActorPC chara;

        //an添加
        byte shopswitch;
        string shoptitle;

        Dictionary<uint, PlayerShopItem> soldItem = new Dictionary<uint, PlayerShopItem>();
        Dictionary<uint, PlayerShopItem> sellShop = new Dictionary<uint, PlayerShopItem>();

        /// <summary>
        /// 玩家商店变量(0为关 1为开)广播用
        /// </summary>
        public byte Shopswitch { get { return this.shopswitch; } set { this.shopswitch = value; } }
        /// <summary>
        /// 玩家商店标题
        /// </summary>
        public string Shoptitle { get { return this.shoptitle; } set { this.shoptitle = value; } }

        //end

        public Map map;
        public enum SESSION_STATE
        {
            LOGIN, AUTHENTIFICATED, REDIRECTING, DISCONNECTED, LOADING, LOADED
        }
        public SESSION_STATE state;
        public bool firstLogin = true;
        public Mob.MobAI AI;

        public ActorPC Character { get { return this.chara; } set { this.chara = value; } }
        public Map Map { get { return this.map; } set { this.map = value; } }

        public MapClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this);
            this.netIO.SetMode(NetIO.Mode.Server);
            this.netIO.FirstLevelLength = 2;
            if (this.netIO.sock.Connected) this.OnConnect();
        }

        public override string ToString()
        {
            try
            {
                string ip = "";
                string name = "";
                if (this.netIO != null) 
                    ip = this.netIO.sock.RemoteEndPoint.ToString();
                if (chara != null)
                {
                    name = chara.Name;
                }
                if (ip != "" || name != "")
                {
                    return string.Format("{0}({1})", name, ip);
                }
                else
                    return "MapClient";
            }
            catch (Exception)
            {
                return "MapClient";
            }
        }

        public static MapClient FromActorPC(ActorPC pc)
        {
            ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)pc.e;
            return eh.Client;
        }

        public override void OnConnect()
        {

        }

        void SendHack()
        {
            /*try
            {
                if ((this.hackStamp - DateTime.Now).TotalMinutes >= 10)
                {
                    hackCount = 0;
                    hackStamp = DateTime.Now;
                }
                hackCount++;
                if (hackCount > 10)
                {
                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                    {
                        i.SendAnnounce("WPE防卫娘：" + this.Character.Name + "酱，H(ack)是不行的哦！WPE什么的最讨厌了>_<");
                    }
                    //this.netIO.Disconnect();
                }
                else if (hackCount > 2)
                {
                    this.SendSystemMessage("WPE防卫娘：你、你、你好像在H(ack)了!那、那个，要是现在就停止的话，暂时就放过你,哼～~(ˇˍˇ）");
                }
            }
            catch { }*///暂时关闭HACK
        }

        public override void OnDisconnect()
        {
            this.npcSelectResult = 0;
            this.npcShopClosed = true;
            try
            {
                this.state = SESSION_STATE.DISCONNECTED;
                MapClientManager.Instance.Clients.Remove(this);
                //如果脚本线程不为空，则强制中断
                if (this.scriptThread != null)
                {
                    try
                    {
                        this.scriptThread.Abort();
                    }
                    catch { }
                }

                if (this.Character == null)
                    return;

                this.Character.VisibleActors.Clear();

                Logger.ShowInfo(string.Format(LocalManager.Instance.Strings.PLAYER_LOG_OUT, this.Character.Name));
                Logger.ShowInfo(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count);
                MapServer.shouldRefreshStatistic = true;

                if (Logger.defaultSql != null)
                {
                    Logger.defaultSql.SQLExecuteNonQuery("UPDATE `char` SET `online` = 0 WHERE `char_id` = " + this.chara.CharID.ToString());
                }

                if (this.Character.HP == 0)
                {
                    /*this.Character.HP = 1;
                    if (this.Character.SaveMap == 0)
                    {
                        this.Character.SaveMap = 10023100;
                        this.Character.SaveX = 242;
                        this.Character.SaveY = 128;
                    }
                    if (Configuration.Instance.HostedMaps.Contains(this.Character.SaveMap))
                    {
                        MapInfo info = MapInfoFactory.Instance.MapInfo[this.Character.SaveMap];
                        this.Character.MapID = this.Character.SaveMap;
                        this.Character.X = Global.PosX8to16(this.Character.SaveX, info.width);
                        this.Character.Y = Global.PosY8to16(this.Character.SaveY, info.height);
                    }*/
                }
                if (this.Character.TenkActor != null)
                {
                    SagaMap.Map map = MapManager.Instance.GetMap(this.Character.TenkActor.MapID);
                    map.DeleteActor(this.Character.TenkActor);
                    if (ScriptManager.Instance.Events.ContainsKey(this.Character.TenkActor.EventID))
                    {
                        ScriptManager.Instance.Events.Remove(this.Character.TenkActor.EventID);
                    }
                    this.Character.TenkActor = null;
                }
                if (this.Character.FGarden != null)
                {
                    if (this.Character.FGarden.RopeActor != null)
                    {
                        SagaMap.Map map = MapManager.Instance.GetMap(this.Character.FGarden.RopeActor.MapID);
                        map.DeleteActor(this.Character.FGarden.RopeActor);
                        if (ScriptManager.Instance.Events.ContainsKey(this.Character.FGarden.RopeActor.EventID))
                        {
                            ScriptManager.Instance.Events.Remove(this.Character.FGarden.RopeActor.EventID);
                        }
                        this.Character.FGarden.RopeActor = null;
                    }
                    if (this.Character.FGarden.RoomMapID != 0)
                    {
                        SagaMap.Map roomMap = MapManager.Instance.GetMap(this.Character.FGarden.RoomMapID);
                        SagaMap.Map gardenMap = MapManager.Instance.GetMap(this.Character.FGarden.MapID);
                        roomMap.ClientExitMap = gardenMap.ClientExitMap;
                        roomMap.ClientExitX = gardenMap.ClientExitX;
                        roomMap.ClientExitY = gardenMap.ClientExitY;
                        MapManager.Instance.DeleteMapInstance(roomMap.ID);
                        this.Character.FGarden.RoomMapID = 0;
                    }
                    if (this.Character.FGarden.MapID != 0)
                    {
                        MapManager.Instance.DeleteMapInstance(this.Character.FGarden.MapID);
                        this.Character.FGarden.MapID = 0;
                    }
                }

                if (this.Map.IsMapInstance && this.Character.PossessionTarget == 0 && !golemLogout)
                {
                    this.Character.MapID = this.Map.ClientExitMap;
                    SagaMap.Map map = MapManager.Instance.GetMap(this.Map.ClientExitMap);
                    this.Character.X = Global.PosX8to16(this.Map.ClientExitX, map.Width);
                    this.Character.Y = Global.PosY8to16(this.Map.ClientExitY, map.Width);
                }

                this.Character.Online = false;
                if (logger != null)
                {
                    logger.Dispose();
                    logger = null;
                }

                if (this.Character.Marionette != null)
                    this.MarionetteDeactivate(true);

                RecruitmentManager.Instance.DeleteRecruitment(this.Character);

                PartyManager.Instance.PlayerOffline(this.Character.Party, this.Character);
                RingManager.Instance.PlayerOffline(this.Character.Ring, this.Character);

                List<ActorPC> possessioned = this.Character.PossesionedActors;
                foreach (ActorPC i in possessioned)
                {
                    Item item = this.GetPossessionItem(this.Character, i.PossessionPosition);
                    if (item.PossessionOwner != this.Character && item.PossessionOwner != null)
                    {
                        ActorItem actor = PossessionItemAdd(i, i.PossessionPosition, "");
                        i.PossessionTarget = actor.ActorID;

                        this.Character.Inventory.DeleteItem(item.Slot, 1);
                        PossessionArg arg = new PossessionArg();
                        arg.fromID = i.ActorID;
                        arg.toID = i.ActorID;
                        arg.result = (int)i.PossessionPosition;
                        this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg, i, true);
                    }
                    else
                    {
                        if (i != this.Character)
                        {
                            i.PossessionTarget = 0;
                            if (i.Online)
                            {
                                PossessionArg arg = new PossessionArg();
                                arg.fromID = i.ActorID;
                                arg.toID = i.PossessionTarget;
                                arg.cancel = true;
                                arg.result = (int)i.PossessionPosition;
                                arg.x = Global.PosX16to8(i.X, Map.Width);
                                arg.y = Global.PosY16to8(i.Y, Map.Height);
                                arg.dir = (byte)(i.Dir / 45);
                                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.POSSESSION, arg, i, true);
                            }
                            else
                            {
                                if (MapManager.Instance.GetMap(i.MapID) != null)
                                    MapManager.Instance.GetMap(i.MapID).DeleteActor(i);
                                MapServer.charDB.SaveChar(i, false, false);
                                MapServer.accountDB.WriteUser(i.Account);
                                MapClient.FromActorPC(i).DisposeActor();
                                i.Account = null;
                                continue;
                            }
                        }
                    }
                    MapServer.charDB.SaveChar(i, false, false);
                    MapServer.accountDB.WriteUser(i.Account);
                }

                if (golemLogout && this.chara.PossessionTarget == 0)
                {
                    this.Character.Golem.MapID = this.Character.MapID;
                    this.Character.Golem.X = this.Character.X;
                    this.Character.Golem.Y = this.Character.Y;
                    this.Character.Golem.Dir = this.Character.Dir;
                    this.Character.Golem.Owner = this.Character;
                    this.Character.Golem.e = new ActorEventHandlers.NullEventHandler();
                    if (this.Character.Golem.BuyLimit > this.Character.Gold)
                        this.Character.Golem.BuyLimit = (uint)this.Character.Gold;
                    if (this.Character.Golem.GolemType >= GolemType.Plant && this.Character.Golem.GolemType <= GolemType.Strange)
                    {
                        ActorEventHandlers.MobEventHandler eh = new ActorEventHandlers.MobEventHandler(this.Character.Golem);
                        this.Character.Golem.e = eh;
                        eh.AI.Mode = new SagaMap.Mob.AIMode(0);
                        eh.AI.X_Ori = this.Character.X;
                        eh.AI.Y_Ori = this.Character.Y;
                        eh.AI.X_Spawn = this.Character.X;
                        eh.AI.Y_Spawn = this.Character.Y;
                        eh.AI.MoveRange = (short)(this.map.Width * 100);
                        eh.AI.Start();
                        if (this.Character.Golem.GolemType != GolemType.Buy)
                        {
                            Tasks.Golem.GolemTask task = new SagaMap.Tasks.Golem.GolemTask(this.Character.Golem);
                            task.Activate();
                            this.Character.Golem.Tasks.Add("GolemTask", task);
                        }
                    }
                    this.map.RegisterActor(this.Character.Golem);
                    this.Character.Golem.invisble = false;
                    this.Character.Golem.Item.Durability--;
                    this.map.OnActorVisibilityChange(this.Character.Golem);
                }

                if (this.Character.Pet != null)
                    this.DeletePet();
                if (this.Character.Partner != null)
                    this.DeletePartner();

                //release resource
                this.chara.VisibleActors.Clear();
                MapManager.Instance.DisposeMapInstanceOnLogout(this.chara.CharID);

                this.Map.DeleteActor(this.Character);
                MapServer.charDB.SaveChar(this.Character);
                MapServer.accountDB.WriteUser(this.Character.Account);

                //防止下线后还存取仓库
                this.Character.Inventory.WareHouse = null;


                //release resource
                if (!golemLogout && this.Character.PossessionTarget == 0)
                {
                    DisposeActor();
                }
                else
                {
                    foreach (KeyValuePair<string, MultiRunTask> i in chara.Tasks)
                    {
                        if (i.Value is Scripting.Timer)
                        {
                            ScriptManager.Instance.Timers.Remove(i.Value.Name);
                        }
                        i.Value.Deactivate();
                    }
                    this.chara.Tasks.Clear();
                }

                //退出副本
                OnPProtectCreatedOut(null);

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void DisposeActor()
        {
            MapManager.Instance.DisposeMapInstanceOnLogout(chara.CharID);
            foreach (KeyValuePair<string, MultiRunTask> i in chara.Tasks)
            {
                if (i.Value is Scripting.Timer)
                {
                    ScriptManager.Instance.Timers.Remove(i.Value.Name);
                }
                i.Value.Deactivate();
            }
            this.chara.Tasks.Clear();
            this.Character.ClearTaskAddition();
            //this.chara.e = null;
            this.chara.Inventory = null;
            this.chara.Golem = null;

            this.chara.Stamp.Dispose();
            this.chara.FGarden = null;
            this.chara.Status = null;
            this.chara.ClearVarialbes();
            this.chara.Marionette = null;
            this.chara.NPCStates.Clear();
            this.chara.Skills = null;
            this.chara.Skills2 = null;
            this.chara.SkillsReserve = null;
            this.chara.Elements.Clear();
            this.chara.Pet = null;
            this.chara.Partner = null;
            this.chara.Quest = null;
            this.chara.Ring = null;
            this.chara.Slave.Clear();
            this.chara.Account = null;
            this.chara = null;
        }
    }
}
