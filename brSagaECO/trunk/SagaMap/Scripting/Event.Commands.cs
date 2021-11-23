using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaMap.Manager;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.Item;
using SagaDB.Skill;
using SagaDB.Quests;
using SagaDB.Npc;
using SagaDB.Ring;
using SagaDB.Synthese;
using SagaDB.Treasure;
using SagaDB.FGarden;
using SagaDB.ECOShop;
using SagaDB.Theater;
using SagaDB.DEMIC;
using SagaMap.Skill;

namespace SagaMap.Scripting
{
    public abstract partial class Event
    {
        protected void ChangeMessageBox(ActorPC pc)
        {
            ChangeMessageBox(pc, true, 0, 0, 0);
        }

        protected void ChangeMessageBox(ActorPC pc, bool GalGameMode, UIType type, uint x, uint y)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_MESSAGE_GALMODE p = new Packets.Server.SSMG_NPC_MESSAGE_GALMODE();
            if (GalGameMode)
                p.Mode = 1;
            else
                p.Mode = 0;
            p.UIType = type;
            p.X = x;
            p.Y = y;
            client.netIO.SendPacket(p);
        }

        /// <summary>
        /// 显示发型预览UI
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">2为显示全部介绍，其他参数未知</param>
        protected void ShowHairPreview(ActorPC pc, int type)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SHOW_HAIR_PREVIEW p = new Packets.Server.SSMG_NPC_SHOW_HAIR_PREVIEW();
            p.type = (byte)type;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 面对玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void FaceToPC(ActorPC pc)
        {

        }
        /// <summary>
        /// 立刻保存系統變量
        /// </summary>
        protected void SaveServerSvar()
        {
            MapServer.charDB.SaveServerVar(ScriptManager.Instance.VariableHolder);
        }
        /// <summary>
        /// 隐藏周围玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void HidePlayer(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_HIDE_PLAYERS p = new Packets.Server.SSMG_NPC_HIDE_PLAYERS();
            p.unknown1 = 0x02;
            p.unknown2 = 0x0C;
            p.unknown3 = 0x00;
            client.netIO.SendPacket(p);
            p = new Packets.Server.SSMG_NPC_HIDE_PLAYERS();
            p.unknown1 = 0x02;
            p.unknown2 = 0x50;
            p.unknown3 = 0x01;
            client.netIO.SendPacket(p);
        }

        /// <summary>
        /// 指定位置显示Pict
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NpcID">NPCID</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="dir">方向</param>
        /// <param name="PictID">模型ID</param>
        protected void ShowPict(ActorPC pc, uint NpcID, byte x, byte y, byte dir, uint PictID)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SHOWPICT_LOCATION p = new Packets.Server.SSMG_NPC_SHOWPICT_LOCATION();
            p.NPCID = NpcID;
            p.X = x;
            p.Y = y;
            p.Dir = dir;
            client.netIO.SendPacket(p);

            Packets.Server.SSMG_NPC_SHOWPICT_VIEW p1 = new Packets.Server.SSMG_NPC_SHOWPICT_VIEW();
            p1.NPCID = NpcID;
            p1.PictID = PictID;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 取消显示的Pict
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NpcID">NPCID</param>
        protected void ShowPictCancel(ActorPC pc, uint NpcID)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SHOWPICT_CANCEL p = new Packets.Server.SSMG_NPC_SHOWPICT_CANCEL();
            p.NPCID = NpcID;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 为对象设置定时说话
        /// </summary>
        /// <param name="actor">对象</param>
        /// <param name="Millisecond">毫秒</param>
        /// <param name="message">说话内容</param>
        protected void SetTimingSpeak(Actor actor, int Millisecond, string message)
        {
            Tasks.Mob.TimingSpeak ts = new Tasks.Mob.TimingSpeak(actor, Millisecond, message);
            actor.Tasks.Add(actor.ActorID.ToString() + message + SagaLib.Global.Random.Next(0, 90000000).ToString(), ts);
            ts.Activate();
        }
        /// <summary>
        /// 设置玩家死亡时触发的ID
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="EventID">事件ID</param>
        /// <param name="MapID">触发的地图ID </param>
        protected void SetOnDieEvent(ActorPC pc, int EventID, int MapID)
        {
            pc.TInt["死亡事件ID"] = EventID;
            pc.TInt["死亡事件地图ID"] = MapID;
        }
        /// <summary>
        /// 设置玩家下一次移动触发的脚本
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="EventID">事件ID</param>
        protected void SetNextMoveEvent(ActorPC pc, uint EventID)
        {
            pc.CInt["NextMoveEventID"] = (int)EventID;
        }
        /// <summary>
        /// 根据NaviID获得步骤列表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">导航ID</param>
        /// <returns>SagaDB.Navi.Navi</returns>
        protected SagaDB.Navi.Event GetNavi(ActorPC pc, uint NaviID)
        {
            if (pc.Navi.UniqueSteps.ContainsKey(NaviID))
            {
                return pc.Navi.UniqueSteps[NaviID].BelongEvent;
            }
            return null;
        }
        /// <summary>
        /// 根据ListID和QuestID获得步骤列表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ListID">列表ID</param>
        /// <param name="QuestID">任务ID</param>
        /// <returns>SagaDB.Navi.Navi</returns>
        protected SagaDB.Navi.Event GetNavi(ActorPC pc, byte ListID, byte QuestID)
        {
            return pc.Navi.Categories[ListID].Events[QuestID];
        }

        /// <summary>
        /// 调整某个导航的状态
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">导航ID</param>
        /// <param name="state">状态（00不显示，01显示，03完成，07未领奖）</param>
        protected void OpenNavi(ActorPC pc, uint NaviID, byte state)
        {
            this.GetNavi(pc, NaviID).State = state;
        }
        /// <summary>
        /// 调整某个导航的状态（00不显示，01显示，03完成，07未领奖）
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ListID">列表ID</param>
        /// <param name="QuestID">导航ID</param>
        /// <param name="state">状态（00不显示，01显示，03完成，07未领奖）</param>
        protected void OpenNavi(ActorPC pc, byte ListID, byte QuestID, byte state)
        {
            pc.Navi.Categories[ListID].Events[QuestID].State = state;
        }
        /// <summary>
        /// 完成导航的某个步骤
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">步骤ID</param>
        /*protected void NaviQuestCompletedUP(ActorPC pc, uint NaviID)
        {
            if (!this.GetNavi(pc, NaviID).Steps[NaviID].Finished)
            {
                this.GetNavi(pc, NaviID).Steps[NaviID].Finished = true;
            }
            Packets.Server.SSMG_NAVI_PROGRESS_UP p = new Packets.Server.SSMG_NAVI_PROGRESS_UP();
            p.pc = pc;
            p.NaviID = NaviID;
            GetMapClient(pc).netIO.SendPacket(p);
        }
        /// <summary>
        /// 完成导航的某个标记
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">步骤ID</param>
        protected void NaviQuestMarkUP(ActorPC pc, uint NaviID)
        {
            if (!this.GetNavi(pc, NaviID).Steps[NaviID].Display)
                this.GetNavi(pc, NaviID).Steps[NaviID].Display = true;
            Packets.Server.SSMG_NAVI_PROGRESS_UP p = new Packets.Server.SSMG_NAVI_PROGRESS_UP();
            p.pc = pc;
            p.NaviID = NaviID;
            GetMapClient(pc).netIO.SendPacket(p);
        }*/
        /// <summary>
        /// 给玩家打开recycle活动界面
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void OpenRecycle(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_ACTIVITY_RECYCLE_WINDOW p = new Packets.Server.SSMG_ACTIVITY_RECYCLE_WINDOW();
            p.Percent = (ushort)SInt["Recycle_Percent"];
            p.PCount = (uint)pc.CInt["PC_Recycle_P"];
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 打开玩家的外观设置见面
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void OpenChangePCForm(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            /*
            if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
            {
                Packets.Server.SSMG_TEST_EVOLVE_OPEN p1 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN();
                client.netIO.SendPacket(p1);
            }
            
            else
            */
            {
                Packets.Server.SSMG_TEST_EVOLVE_OPEN p1 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN();
                client.netIO.SendPacket(p1);
                Packets.Server.SSMG_TEST_EVOLVE_OPEN2 p2 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN2();
                client.netIO.SendPacket(p2);
                Packets.Server.SSMG_TEST_EVOLVE_OPEN3 p3 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN3();
                client.netIO.SendPacket(p3);
            }
        }
        /// <summary>
        /// 进入飞空城
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="FFMaster">飞空城主人</param>
        protected void EnterFFarden(ActorPC pc, ActorPC FFMaster)
        {
            /*if (pc.FFarden == null)
            {
                return;
            }

            if (pc.FFarden.MapID == 0)
            {
                Map map = MapManager.Instance.GetMap(pc.MapID);
                pc.FFarden.MapID = MapManager.Instance.CreateMapInstance(pc, 90000000, pc.MapID, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height));
                map = MapManager.Instance.GetMap(pc.FFarden.MapID);
                foreach (ActorFurniture i in pc.FFarden.Furnitures[SagaDB.FFarden.FurniturePlace.GARDEN])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false;
                }
            }
            pc.BattleStatus = 0;
            pc.Speed = 200;
            MapClient.FromActorPC(pc).SendChangeStatus();
            Warp(pc, pc.FFarden.MapID, 6, 11);*/
        }
        /// <summary>
        /// 打开飞空城界面
        /// </summary>
        /// <param name="pc">玩家</param>
        /*protected void OpenFFList(ActorPC pc)
        {
            int maxPage;
            int page = 1;
            List<SagaDB.FFarden.FFarden> res = FFGardenManager.Instance.GetFFGarden(0, out maxPage);
            Packets.Server.SSMG_FF_LIST p1 = new SagaMap.Packets.Server.SSMG_FF_LIST();
            p1.ActorID = pc.ActorID;
            p1.Page = 0;
            p1.MaxPaga = (uint)maxPage;
            p1.Entries = res;
            MapClient client = GetMapClient(pc);
            client.netIO.SendPacket(p1);
        }*/
        /// <summary>
        /// 传送玩家到指定地图
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="mapID">地图ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <example>Warp(pc, 10024000, 150,130);</example>
        protected void Warp(ActorPC pc, uint mapID, byte x, byte y)
        {
            MapClient client = GetMapClient(pc);
            if (Configuration.Instance.HostedMaps.Contains(mapID))
            {
                Map newMap = MapManager.Instance.GetMap(mapID);
                if (client.Character.Marionette != null)
                    client.MarionetteDeactivate();
                client.Map.SendActorToMap(pc, mapID, Global.PosX8to16(x, newMap.Width), Global.PosY8to16(y, newMap.Height));
                //client.map.SendActorToMap(pc, mapID, x, y);
            }
        }

        /// <summary>
        /// NPC对话框
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="motion">NPC动作</param>
        /// <param name="message">消息内容</param>
        protected void Say(ActorPC pc, ushort motion, string message)
        {
            if (NPCFactory.Instance.Items.ContainsKey(this.eventID))
            {
                Say(pc, motion, message, NPCFactory.Instance.Items[this.eventID].Name);
            }
            else
            {
                Say(pc, motion, message, "");
            }
        }

        /// <summary>
        /// NPC对话框
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="motion">NPC动作</param>
        /// <param name="message">消息内容</param>
        /// <param name="title">消息标题</param>
        /// <example> Say(pc, 65535, "跟前輩學了很多東西$R;" +
        ///    "$R您要一起學嗎？$R;", "初階冒險者");</example>
        protected void Say(ActorPC pc, ushort motion, string message, string title)
        {
            Say(pc, this.EventID, motion, message, title);
        }

        /// <summary>
        /// 让指定NPC对话
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPC ID</param>
        /// <param name="motion">动作</param>
        /// <param name="message">信息</param>
        protected void Say(ActorPC pc, uint npcID, ushort motion, string message)
        {
            if (NPCFactory.Instance.Items.ContainsKey(npcID))
            {
                Say(pc, npcID, motion, message, NPCFactory.Instance.Items[npcID].Name);
            }
            else
            {
                Say(pc, npcID, motion, message, "");
            }
        }

        /// <summary>
        /// NPC对话框，指定作用NPCID
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        /// <param name="motion">NPC动作</param>
        /// <param name="message">消息内容</param>
        /// <param name="title">消息标题</param>
        protected void Say(ActorPC pc, uint npcID, ushort motion, string message, string title)
        {
            MapClient client = GetMapClient(pc);
            client.SendNPCMessageStart();
            string[] messages = message.Split(';');
            foreach (string i in messages)
            {
                if (i == "") continue;
                client.SendNPCMessage(npcID, i, motion, title);
            }
            client.SendNPCMessageEnd();
        }

        /// <summary>
        /// 让客户端延迟一定事件
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="wait">延迟事件，单位为毫秒</param>
        protected void Wait(ActorPC pc, uint wait)
        {
            MapClient client = GetMapClient(pc);
            client.SendNPCWait(wait);
        }

        /// <summary>
        /// 显示选择框，并等待玩家选择
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="title">标题</param>
        /// <param name="confirm">确认信息，可以为空字符串</param>
        /// <param name="options">选项</param>
        /// <returns>玩家选择的结果，第一个选项为1</returns>
        protected int Select(ActorPC pc, string title, string confirm, params string[] options)
        {
            return Select(pc, title, confirm, false, options);
        }

        /// <summary>
        /// 显示选择框，并等待玩家选择
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="title">标题</param>
        /// <param name="confirm">确认信息，可以为空字符串</param>
        /// <param name="canCancel">是否可取消</param>
        /// <param name="options">选项</param>
        /// <returns>玩家选择的结果，第一个选项为1</returns>
        protected int Select(ActorPC pc, string title, string confirm, bool canCancel, params string[] options)
        {
            Packets.Server.SSMG_NPC_SELECT p = new SagaMap.Packets.Server.SSMG_NPC_SELECT();
            p.SetSelect(title, confirm, options, canCancel);
            MapClient client = GetMapClient(pc);
            client.npcSelectResult = -1;
            client.netIO.SendPacket(p);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.npcSelectResult == -1)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
            return client.npcSelectResult;
        }

        /// <summary>
        /// 添加道具到商店
        /// </summary>
        /// <param name="goods">道具ID</param>
        protected void AddGoods(params uint[] goods)
        {
            foreach (uint i in goods)
            {
                if (this.goods.Count == 12)
                {
                    Logger.ShowWarning(this.ToString() + ":Maximal shop items(12) reached, skiping");
                }
                else
                    this.goods.Add(i);
            }
        }
        /// <summary>
        /// 打开商店
        /// </summary>
        /// <param name="pc">pc</param>
        /// <param name="Goods">商品</param>
        protected void OpenShopByList(ActorPC pc, params uint[] Goods)
        {
            OpenShopByList(pc, 1000, ShopType.None, Goods);
        }
        /// <summary>
        /// 打开商店
        /// </summary>
        /// <param name="pc">pc</param>
        /// <param name="rate">价格倍率(1000为原价，500为50%)</param>
        /// <param name="type">交易货币类型</param>
        /// <param name="Goods">商品</param>
        protected void OpenShopByList(ActorPC pc, uint rate, ShopType type, params uint[] Goods)
        {
            MapClient client = GetMapClient(pc);
            List<uint> items = Goods.ToList();
            Packets.Server.SSMG_NPC_SHOP_BUY p = new SagaMap.Packets.Server.SSMG_NPC_SHOP_BUY(items.Count);
            p.Rate = rate;
            p.Goods = items;
            switch (type)
            {
                case ShopType.None:
                    p.Gold = (uint)pc.Gold;
                    p.Bank = pc.Account.Bank;
                    break;
                case ShopType.CP:
                    p.Gold = pc.CP;
                    break;
                case ShopType.ECoin:
                    p.Gold = pc.ECoin;
                    break;
            }
            p.Type = type;
            client.netIO.SendPacket(p);

            client.npcShopClosed = false;
            client.currentShop = null;

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.npcShopClosed)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();


            client.currentShop = null;
        }
        /// <summary>
        /// 根据Shop ID打开商店
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="shopID">商店ID</param>
        protected void OpenShopBuy(ActorPC pc, uint shopID)
        {
            MapClient client = GetMapClient(pc);
            Shop shop = ShopFactory.Instance.Items[shopID];
            Packets.Server.SSMG_NPC_SHOP_BUY p = new SagaMap.Packets.Server.SSMG_NPC_SHOP_BUY(shop.Goods.Count);
            p.Rate = shop.SellRate * 5;
            p.Goods = shop.Goods;
            switch (shop.ShopType)
            {
                case ShopType.None:
                    p.Gold = (uint)pc.Gold;
                    p.Bank = pc.Account.Bank;
                    break;
                case ShopType.CP:
                    p.Gold = pc.CP;
                    break;
                case ShopType.ECoin:
                    p.Gold = pc.ECoin;
                    break;
            }
            p.Type = shop.ShopType;
            client.netIO.SendPacket(p);

            client.npcShopClosed = false;
            client.currentShop = shop;

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.npcShopClosed)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();


            client.currentShop = null;
        }

        /// <summary>
        /// 打开购买窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void OpenShopBuy(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SHOP_BUY p = new SagaMap.Packets.Server.SSMG_NPC_SHOP_BUY(goods.Count);
            p.Rate = (uint)(100 + pc.Status.buy_rate);
            p.Goods = this.goods;
            p.Gold = (uint)pc.Gold;
            p.Bank = pc.Account.Bank;
            client.netIO.SendPacket(p);
            client.npcShopClosed = false;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.npcShopClosed)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 根据商店ID打开购买窗口
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="shopID"></param>
        protected void OpenShopSell(ActorPC pc, uint shopID)
        {
            Packets.Server.SSMG_NPC_SHOP_SELL p = new SagaMap.Packets.Server.SSMG_NPC_SHOP_SELL();
            Shop shop = ShopFactory.Instance.Items[shopID];
            MapClient client = GetMapClient(pc);
            p.Rate = shop.BuyRate * 10;
            //p.Rate = (uint)(10 + pc.Status.sell_rate);
            //p.Rate = 0;
            p.ShopLimit = shop.BuyLimit;
            p.Bank = 0;
            client.netIO.SendPacket(p);


            client.npcShopClosed = false;
            client.currentShop = shop;

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.npcShopClosed)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
            client.currentShop = null;
        }

        /// <summary>
        /// 打开贩卖窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void OpenShopSell(ActorPC pc)
        {
            Packets.Server.SSMG_NPC_SHOP_SELL p = new SagaMap.Packets.Server.SSMG_NPC_SHOP_SELL();
            MapClient client = GetMapClient(pc);
            p.Rate = (uint)(10 + pc.Status.sell_rate);
            p.ShopLimit = this.buyLimit;
            p.Bank = 0;
            client.netIO.SendPacket(p);
            client.npcShopClosed = false;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.npcShopClosed)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 在客户端播放一个音效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="soundID">音效ID</param>
        /// <param name="loop">是否循环播放</param>
        /// <param name="volume">音量，100为满</param>
        /// <param name="balance">声道平衡，0为左，50为中间，100为右</param>
        protected void PlaySound(ActorPC pc, uint soundID, bool loop, uint volume, byte balance)
        {
            MapClient client = GetMapClient(pc);
            byte ifloop;
            if (loop)
                ifloop = 1;
            else
                ifloop = 0;
            client.SendNPCPlaySound(soundID, ifloop, volume, balance);
        }

        /// <summary>
        /// 修改BGM
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="soundID">BGMID</param>
        /// <param name="loop">是否循环</param>
        /// <param name="volume">音量</param>
        /// <param name="balance">均衡</param>
        protected void ChangeBGM(ActorPC pc, uint soundID, bool loop, uint volume, byte balance)
        {
            MapClient client = GetMapClient(pc);
            byte ifloop;
            if (loop)
                ifloop = 1;
            else
                ifloop = 0;
            client.SendChangeBGM(soundID, ifloop, volume, balance);
        }

        /// <summary>
        /// 在玩家处显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="effectID">特效ID</param>
        protected void ShowEffect(ActorPC pc, uint effectID)
        {
            ShowEffect(pc, pc, effectID);
        }

        /// <summary>
        /// 在指定NPC处显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="target">NPCID</param>
        /// <param name="effectID">特效ID</param>
        protected void ShowEffect(ActorPC pc, uint target, uint effectID)
        {
            ShowEffect(pc, target, 0xffff, effectID, false);
        }
        protected void ShowEffect(ActorPC pc, uint target, ushort height, uint effectID, bool oneTime)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = target;
            arg.height = height;
            MapClient client = GetMapClient(pc);
            client.SendNPCShowEffect(arg.actorID, arg.x, arg.y, arg.height, arg.effectID, arg.oneTime);
        }

        /// <summary>
        /// 在指定坐标显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="effectID">特效ID</param>
        /// 
        protected void ShowEffect(ActorPC pc, byte x, byte y, uint effectID)
        {
            ShowEffect(pc, x,y, 0xffff, effectID, false);
        }
        protected void ShowEffect(ActorPC pc, byte x, byte y, ushort height, uint effectID, bool oneTime)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = 0xFFFFFFFF;
            arg.x = x;
            arg.y = y;
            arg.height = height;
            MapClient client = GetMapClient(pc);
            client.SendNPCShowEffect(arg.actorID, arg.x, arg.y, arg.height, arg.effectID, arg.oneTime);
        }

        /// <summary>
        /// 在指定对象处显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="target">对象</param>
        /// <param name="effectID">特效ID</param>
        protected void ShowEffect(ActorPC pc, Actor target, uint effectID)
        {
            ShowEffect(pc, target.ActorID, effectID);
        }

        /// <summary>
        /// 开始Event
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="eventID"></param>
        protected void StartEvent(ActorPC pc, uint eventID)
        {
            MapClient client = GetMapClient(pc);
            client.SendCurrentEvent(eventID);
        }

        /// <summary>
        /// 取得地图名称
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <returns></returns>
        protected string GetMapName(uint mapID)
        {
            if (Configuration.Instance.HostedMaps.Contains(mapID))
            {
                Map newMap = MapManager.Instance.GetMap(mapID);
                return newMap.Name;
            }
            return "";
        }

        /// <summary>
        /// 设置玩家存储地址
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="mapID">地图ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        protected void SetHomePoint(ActorPC pc, uint mapID, byte x, byte y)
        {
            pc.SaveMap = mapID;
            pc.SaveX = x;
            pc.SaveY = y;
        }

        /// <summary>
        /// 取得玩家道具栏中指定道具的个数
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <returns></returns>
        protected int CountItem(ActorPC pc, uint itemID)
        {
            SagaDB.Item.Item item = pc.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (item != null)
            {
                return item.Stack;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得玩家身上指定道具的信息
        /// </summary>
        /// <param name="ID">道具ID</param>
        /// <returns>道具清单</returns>
        protected List<SagaDB.Item.Item> GetItem(ActorPC pc, uint ID)
        {
            List<SagaDB.Item.Item> result = new List<SagaDB.Item.Item>();
            for (int i = 2; i < 6; i++)
            {
                List<SagaDB.Item.Item> list = pc.Inventory.Items[(ContainerType)i];
                var query = from it in list
                            where it.ItemID == ID
                            select it;
                result.AddRange(query);
            }
            return result;
        }

        /// <summary>
        /// 给予玩家指定道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="item">道具</param>
        protected void GiveItem(ActorPC pc, SagaDB.Item.Item item)
        {
            MapClient client = GetMapClient(pc);
            Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + pc.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                    string.Format("ScriptGive Count:{0}", item.Stack), true);
            client.AddItem(item, true);

        }

        /// <summary>
        /// 给予玩家指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count)
        {
            GiveItem(pc, itemID, count, true, 0);
        }

        /// <summary>
        /// 给玩家指定个数的租凭道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">数量</param>
        /// <param name="rentalMinutes">租凭分钟</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, int rentalMinutes)
        {
            GiveItem(pc, itemID, count, true, 0, rentalMinutes);
        }

        /// <summary>
        /// 给予玩家指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        /// <param name="identified">是否鉴定</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified)
        {
            GiveItem(pc, itemID, count, identified, 0);
        }


        /// <summary>
        /// 给予玩家指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        /// <param name="identified">是否鉴定</param>
        /// <param name="pictID">外观ID或者画板包含的怪物ID</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified, uint pictID)
        {
            GiveItem(pc, itemID, count, identified, pictID, 0);
        }

        /// <summary>
        /// 给予玩家指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        /// <param name="identified">是否鉴定</param>
        /// <param name="pictID">外观ID或者画板包含的怪物ID</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified, uint pictID, int rentalMinutes)
        {
            MapClient client = GetMapClient(pc);
            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(itemID);
            item.PictID = pictID;
            if (rentalMinutes > 0)
            {
                item.Rental = true;
                item.RentalTime = DateTime.Now + new TimeSpan(0, rentalMinutes, 0);
            }
            if (item.Stackable)
            {
                item.Stack = count;
                item.Identified = true;//免鉴定
                client.AddItem(item, true);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    item.Stack = 1;
                    item.Identified = true;//免鉴定
                    client.AddItem(item, true);
                }
            }
            Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + pc.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                 string.Format("ScriptGive Count:{0}", count), true);

        }

        /// <summary>
        /// 取得空余道具数
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns></returns>
        protected int ItemFreeSlotCount(ActorPC pc)
        {
            int count = 0;
            count += GetItemCount(pc, ContainerType.BODY);
            count += GetItemCount(pc, ContainerType.BACK_BAG);
            count += GetItemCount(pc, ContainerType.RIGHT_BAG);
            count += GetItemCount(pc, ContainerType.LEFT_BAG);
            if (count >= 100)
                return 0;
            else
                return 100 - count;
        }

        /// <summary>
        /// 取得道具欄的道具數量
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="container">哪一個道具欄</param>
        /// <returns></returns>
        protected int GetItemCount(ActorPC pc, ContainerType container)
        {
            try
            {

                return pc.Inventory.Items[container].Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 从玩家身上拿走指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        protected SagaDB.Item.Item TakeItem(ActorPC pc, uint itemID, ushort count)
        {
            MapClient client = GetMapClient(pc);
            Logger.LogItemLost(Logger.EventType.ItemNPCLost, pc.Name + "(" + pc.CharID + ")", "(" + itemID + ")",
                    string.Format("ScriptTake Count:{0}", count), true);
            return client.DeleteItemID(itemID, count, true);
        }
        /// <summary>
        /// 从玩家身上拿走指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="slotID">道具SlotID</param>
        /// <param name="count">个数</param>
        protected void TakeItemBySlot(ActorPC pc, uint slotID, ushort count)
        {
            MapClient client = GetMapClient(pc);
            client.DeleteItem(slotID, count, true);
        }
        /// <summary>
        /// 从玩家身上拿走指定部位的装备
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="eqSlot">装备部位</param>
        /// <returns>是否成功拿走道具</returns>
        protected bool TakeEquipment(ActorPC pc, EnumEquipSlot eqSlot)
        {
            MapClient client = GetMapClient(pc);
            try
            {
                SagaDB.Item.Item item = pc.Inventory.Equipments[eqSlot];
                uint slot = item.Slot;
                InventoryDeleteResult result = pc.Inventory.DeleteItem(slot, 1);
                client.SendEquip();
                PC.StatusFactory.Instance.CalcStatus(pc);
                client.SendPlayerInfo();
                Packets.Server.SSMG_ITEM_DELETE p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                p2.InventorySlot = slot;
                client.netIO.SendPacket(p2);

                client.SendAttackType();
                Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                p4.InventorySlot = 0xffffffff;
                p4.Target = ContainerType.NONE;
                p4.Result = 1;
                p4.Range = pc.Range;
                client.netIO.SendPacket(p4);
                client.Character.Inventory.CalcPayloadVolume();
                client.SendCapacity();
                client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_DELETED, item.BaseData.name, 1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 强制脱掉装备
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="eqSlot">装备部位</param>
        protected void RemoveEquipment(ActorPC pc, EnumEquipSlot eqSlot)
        {
            if (pc.Inventory.Equipments.ContainsKey(eqSlot))
            {
                MapClient client = GetMapClient(pc);
                SagaDB.Item.Item item = pc.Inventory.Equipments[eqSlot];
                item = pc.Inventory.Equipments[item.EquipSlot[0]];
                Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                p.data = new byte[20];
                p.Target = ContainerType.BODY;
                p.InventoryID = item.Slot;
                p.Count = item.Stack;
                client.OnItemMove(p);
            }
        }

        /// <summary>
        /// 变身为活动木偶
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="id">活动木偶ID</param>
        protected void ActivateMarionette(ActorPC pc, uint id)
        {
            MapClient client = GetMapClient(pc);
            client.MarionetteActivate(id, false, true);
        }

        /// <summary>
        /// 让某特定对象（玩家，怪物）的HP/MP/SP全满
        /// </summary>
        /// <param name="actor">对象</param>
        protected void Heal(Actor actor)
        {
            MapClient client = GetMapClient(this.currentPC);
            actor.HP = actor.MaxHP;
            actor.MP = actor.MaxMP;
            actor.SP = actor.MaxSP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }

        /// <summary>
        /// 检查地图副本是否正确
        /// </summary>
        /// <param name="creator">创造者</param>
        /// <param name="mapID">地图ID</param>
        /// <returns></returns>
        protected bool CheckMapInstance(ActorPC creator, uint mapID)
        {
            Map map = MapManager.Instance.GetMap(mapID);
            if (map != null)
            {
                if (map.IsMapInstance)
                {
                    if (map.Creator.CharID == creator.CharID)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 创建一个地图副本
        /// </summary>
        /// <param name="template">模板地图</param>
        /// <param name="exitMap">玩家退出时返回的地图</param>
        /// <param name="exitX">玩家退出时返回的X坐标</param>
        /// <param name="exitY">玩家退出时返回的Y坐标</param>
        /// <returns>新建地图副本的ID</returns>
        protected int CreateMapInstance(int template, uint exitMap, byte exitX, byte exitY)
        {
            return CreateMapInstance(template, exitMap, exitX, exitY, false);
        }
        /// <summary>
        /// 创建一个地图副本
        /// </summary>
        /// <param name="template">模板地图</param>
        /// <param name="exitMap">玩家退出时返回的地图</param>
        /// <param name="exitX">玩家退出时返回的X坐标</param>
        /// <param name="exitY">玩家退出时返回的Y坐标</param>
        /// <param name="autoDispose">是否在玩家登出的时候自动删除</param>
        /// <returns>新建地图副本的ID</returns>
        protected int CreateMapInstance(int template, uint exitMap, byte exitX, byte exitY, bool autoDispose)
        {
            return (int)MapManager.Instance.CreateMapInstance(this.currentPC, (uint)template, exitMap, exitX, exitY, autoDispose);
        }

        /// <summary>
        /// 删除一个地图副本
        /// </summary>
        /// <param name="id">地图ID</param>
        /// <returns></returns>
        protected bool DeleteMapInstance(int id)
        {
            return MapManager.Instance.DeleteMapInstance((uint)id);
        }

        /// <summary>
        /// 为某个地图添加刷怪点
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="spawnFile">刷怪文件</param>
        protected void LoadSpawnFile(int mapID, string spawnFile)
        {
            Mob.MobSpawnManager.Instance.LoadOne(spawnFile, (uint)mapID);
        }

        /// <summary>
        /// 改变某个玩家的职业
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="job">职业</param>
        protected void ChangePlayerJob(ActorPC pc, PC_JOB job)
        {
            MapServer.charDB.SaveSkill(pc);
            pc.Skills.Clear();
            pc.Skills2.Clear();
            pc.Skills2_1.Clear();
            pc.Skills2_2.Clear();
            pc.SkillsReserve.Clear();
            pc.Skills3.Clear();
            pc.SkillPoint = 0;
            pc.SkillPoint2T = 0;
            pc.SkillPoint2X = 0;
            pc.SkillPoint3 = 0;
            pc.Job = job;
            MapServer.charDB.GetSkill(pc);
            PC.StatusFactory.Instance.CalcStatus(pc);
            pc.HP = pc.MaxHP;
            pc.MP = pc.MaxMP;
            pc.SP = pc.MaxSP;
            GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// 让玩家学会该职业的某个技能
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="skillID">技能ID</param>
        protected void LearnSkill(ActorPC pc, uint skillID)
        {
            if (!SkillFactory.Instance.SkillList(pc.Job).ContainsKey(skillID))
                return;
            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
            byte jobLV = SkillFactory.Instance.SkillList(pc.Job)[skillID];
            if (skill == null) return;
            if (pc.Job == pc.JobBasic)
            {
                if (pc.JobLevel1 < jobLV)
                    return;
                if (!pc.Skills.ContainsKey(skillID))
                {
                    pc.Skills.Add(skillID, skill);
                }
            }
            if (pc.Job == pc.Job2X)
            {
                if (pc.JobLevel2X < jobLV)
                    return;
                if (!pc.Skills2.ContainsKey(skillID))
                {
                    pc.Skills2.Add(skillID, skill);
                }
            }
            if (pc.Job == pc.Job2T)
            {
                if (pc.JobLevel2T < jobLV)
                    return;
                if (!pc.Skills2.ContainsKey(skillID))
                {
                    pc.Skills2.Add(skillID, skill);
                }
            }
            if (pc.Job == pc.Job3)
            {
                if (pc.JobLevel3 < jobLV)
                    return;
                if (!pc.Skills3.ContainsKey(skillID))
                {
                    pc.Skills3.Add(skillID, skill);
                }
            }
            PC.StatusFactory.Instance.CalcStatus(pc);
            GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// 向玩家打开NPC交易窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>玩家交易的道具</returns>
        protected List<SagaDB.Item.Item> NPCTrade(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            client.npcTrade = true;
            string name = "";
            if (NPCFactory.Instance.Items.ContainsKey(this.eventID))
            {
                name = NPCFactory.Instance.Items[this.eventID].Name;
            }
            client.SendTradeStartNPC(name);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.npcTrade)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();

            List<SagaDB.Item.Item> items = client.npcTradeItem;
            client.npcTradeItem = null;
            return items;
        }

        /// <summary>
        /// 检查玩家背包空间是否够装某道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="amount">个数</param>
        /// <returns></returns>
        protected bool CheckInventory(ActorPC pc, uint itemID, int amount)
        {
            SagaDB.Item.Item.ItemData item = ItemFactory.Instance.Items[itemID];
            int volume = item.volume * amount;
            int weight = item.weight * amount;
            //Logger.ShowInfo(string.Format("道具:{0} 数量:{1} 重量:{2} 体积:{3}", item.name, amount, weight, volume));

            //Logger.ShowInfo(string.Format("身体重量: {0}/{1} 身体体积:{2}/{3}", pc.Inventory.Payload[ContainerType.BODY], pc.Inventory.MaxPayload[ContainerType.BODY],
            //    pc.Inventory.Volume[ContainerType.BODY], pc.Inventory.MaxVolume[ContainerType.BODY]));

            //Logger.ShowInfo(string.Format("容积: {0}", pc.Inventory.Items[ContainerType.BODY].Count));

            if (pc.Inventory.Payload[ContainerType.BODY] + weight < pc.Inventory.MaxPayload[ContainerType.BODY] &&
                pc.Inventory.Volume[ContainerType.BODY] + volume < pc.Inventory.MaxVolume[ContainerType.BODY] && pc.Inventory.Items[ContainerType.BODY].Count < 100)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 增加某个玩家的技能点数
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="val">值</param>
        protected void SkillPointBonus(ActorPC pc, byte val)
        {
            pc.SkillPoint += val;
            GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// 2-1スキルポイントボーナス
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="val">值</param>
        protected void SkillPointBonus2T(ActorPC pc, byte val)
        {
            pc.SkillPoint2T += val;
            GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// 2-2スキルポイントボーナス
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="val">值</param>
        protected void SkillPointBonus2X(ActorPC pc, byte val)
        {
            pc.SkillPoint2X += val;
            GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// 复活某个玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="level">复活术等级</param>
        protected void Revive(ActorPC pc, byte level)
        {
            if (level > 0)
                GetMapClient(pc).SendRevive(level);
        }

        /// <summary>
        /// 取得当前服务器的在线玩家
        /// </summary>
        protected List<ActorPC> OnlinePlayers
        {
            get
            {
                List<ActorPC> list = new List<ActorPC>();
                foreach (MapClient i in MapClientManager.Instance.Clients)
                {
                    if (i.Character.Online)
                        list.Add(i.Character);
                }
                return list;
            }
        }

        /// <summary>
        /// 创建一个新道具的实例
        /// </summary>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">数量</param>
        /// <param name="identified">是否鉴定</param>
        /// <returns>道具</returns>
        protected SagaDB.Item.Item CreateItem(uint itemID, ushort count, bool identified)
        {
            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(itemID, true); //免鉴定
            item.Stack = count;
            return item;
        }

        /// <summary>
        /// 创建一个道具列表
        /// </summary>
        /// <param name="itemID">道具ID</param>
        /// <returns>道具列表</returns>
        protected List<SagaDB.Item.Item> CreateItemList(params uint[] itemID)
        {
            List<SagaDB.Item.Item> list = new List<SagaDB.Item.Item>();
            foreach (uint i in itemID)
            {
                list.Add(CreateItem(itemID[i], 1, true));
            }
            return list;
        }

        /// <summary>
        /// 向玩家发送导航箭头
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        protected void Navigate(ActorPC pc, byte x, byte y)
        {
            Packets.Server.SSMG_NPC_NAVIGATION p = new SagaMap.Packets.Server.SSMG_NPC_NAVIGATION();
            p.X = x;
            p.Y = y;
            GetMapClient(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 取消导航箭头
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void NavigateCancel(ActorPC pc)
        {
            Packets.Server.SSMG_NPC_NAVIGATION_CANCEL p = new SagaMap.Packets.Server.SSMG_NPC_NAVIGATION_CANCEL();
            GetMapClient(pc).netIO.SendPacket(p);
        }

        public void Synthese(ActorPC pc, ushort skillID, byte skillLv)
        {
            Synthese(pc, skillID, skillLv, false);
        }

        /// <summary>
        /// 显示精炼窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="skillID">精炼技能</param>
        /// <param name="skillLv">技能等级</param>
        public void Synthese(ActorPC pc, ushort skillID, byte skillLv, bool noMoney)
        {
            Packets.Server.SSMG_NPC_SYNTHESE_NEWINFO p = new Packets.Server.SSMG_NPC_SYNTHESE_NEWINFO();
            p.SkillID = skillID;
            p.SkillLevel = skillLv;
            p.Unknown1 = 1;
            p.Unknown2 = 0;
            MapClient client = GetMapClient(pc);
            client.netIO.SendPacket(p);
            //client.SendSkillDummy(skillID, skillLv);
            /*var item =
                from c in SyntheseFactory.Instance.Items.Values
                where c.SkillID == skillID && c.SkillLv <= skillLv && HasMaterial(pc, c)
                select c;
            List<SyntheseInfo> res = item.ToList();
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SYNTHESE_HEADER p = new SagaMap.Packets.Server.SSMG_NPC_SYNTHESE_HEADER();
            p.Count = (byte)res.Count;
            client.netIO.SendPacket(p);
            foreach (SyntheseInfo i in res)
            {
                Packets.Server.SSMG_NPC_SYNTHESE_INFO p1 = new SagaMap.Packets.Server.SSMG_NPC_SYNTHESE_INFO();
                p1.Synthese = i;
                client.netIO.SendPacket(p1);
            }
            */
            client.syntheseItem = new Dictionary<uint, uint>();
            client.syntheseFinished = false;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.syntheseFinished)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();

            bool complete = true;
            bool sketch = false;
            uint mobID = 0;
            foreach (uint i in client.syntheseItem.Keys)
            {
                if (!SyntheseFactory.Instance.Items.ContainsKey(i))
                    continue;
                SyntheseInfo info = SyntheseFactory.Instance.Items[i];
                foreach (ItemElement j in info.Materials)
                {
                    int count = 0;
                    while (CountItem(pc, j.ID) > 0 && (count < (j.Count * client.syntheseItem[i])))
                    {
                        int tmp = CountItem(pc, j.ID);
                        if (!sketch)
                            sketch = (j.ID == 10020758);

                        if (count + tmp > (j.Count * client.syntheseItem[i]))
                            tmp = (int)((j.Count * client.syntheseItem[i]) - count);
                        SagaDB.Item.Item item2 = TakeItem(pc, j.ID, (ushort)tmp);
                        if (item2 != null && sketch && mobID == 0)
                            mobID = item2.PictID;
                        count += tmp;
                    }
                    if (count < (j.Count * client.syntheseItem[i]))
                    {
                        complete = false;
                    }
                    //else
                    //    TakeItem(pc, j.ID, (ushort)(j.Count * client.syntheseItem[i]));
                }
                if (complete && (pc.Gold >= info.Gold || noMoney))
                {
                    for (uint k = 0; k < client.syntheseItem[i]; k++)
                    {
                        int ran = Global.Random.Next(0, 99);
                        int baseValue = 0, maxVlaue = 0;
                        foreach (ItemElement j in info.Products)
                        {
                            maxVlaue = baseValue + j.Rate;
                            if (ran >= baseValue && ran < maxVlaue)
                            {
                                if (sketch && mobID != 0)
                                    GiveItem(pc, j.ID, (ushort)(j.Count), true, mobID);
                                else
                                    GiveItem(pc, j.ID, (ushort)(j.Count));
                            }
                            baseValue = maxVlaue;
                        }
                    }
                    if (!noMoney)
                        pc.Gold -= (int)info.Gold;
                }
            }

            client.SendSkillDummy(skillID, skillLv);

            Packets.Server.SSMG_NPC_SYNTHESE_RESULT p2 = new SagaMap.Packets.Server.SSMG_NPC_SYNTHESE_RESULT();
            p2.Result = 1;
            client.netIO.SendPacket(p2);
        }

        bool HasMaterial(ActorPC pc, SyntheseInfo info)
        {
            foreach (ItemElement i in info.Materials)
            {
                if (CountItem(pc, i.ID) > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 向银行存钱
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void BankDeposit(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            string input = InputBox(pc, string.Format(LocalManager.Instance.Strings.NPC_INPUT_BANK, client.Character.Account.Bank), InputType.Bank);

            if (input == "") return;
            uint amount = uint.Parse(input);
            if (pc.Gold < amount)
            {
                Say(pc, 131, LocalManager.Instance.Strings.NPC_BANK_NOT_ENOUGH_GOLD);
                return;
            }
            pc.Gold -= (int)amount;
            pc.Account.Bank += amount;
        }

        /// <summary>
        /// 从银行取钱
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void BankWithdraw(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            string input = InputBox(pc, string.Format(LocalManager.Instance.Strings.NPC_INPUT_BANK, client.Character.Account.Bank), InputType.Bank);

            if (input == "") return;
            uint amount = uint.Parse(input);
            if (pc.Account.Bank < amount)
            {
                Say(pc, 131, LocalManager.Instance.Strings.NPC_BANK_NOT_ENOUGH_GOLD);
                return;
            }
            pc.Account.Bank -= amount;
            pc.Gold += (int)amount;
        }

        /// <summary>
        /// 显示输入框要求玩家输入
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">类型</param>
        /// <returns>输入的内容，取消则为""</returns>
        protected string InputBox(ActorPC pc, string title, InputType type)
        {
            MapClient client = GetMapClient(pc);
            client.inputContent = "\0";

            Packets.Server.SSMG_NPC_INPUTBOX p = new SagaMap.Packets.Server.SSMG_NPC_INPUTBOX();
            p.Title = title;
            p.Type = type;
            client.netIO.SendPacket(p);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.inputContent == "\0")
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();

            return client.inputContent;
        }

        /// <summary>
        /// 打开指定地点仓库
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="place">地点</param>
        protected void OpenWareHouse(ActorPC pc, WarehousePlace place)
        {
            MapClient client = GetMapClient(pc);
            client.currentWarehouse = place;
            PlaySound(pc, 2060, false, 100, 50);
            client.SendWareItems(place);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.currentWarehouse != WarehousePlace.Current)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 根据宝物组名随机取得一样道具给玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="treasureGroup">宝物组</param>
        protected void GiveRandomTreasure(ActorPC pc, string treasureGroup)
        {
            TreasureItem res = TreasureFactory.Instance.GetRandomItem(treasureGroup);
            //SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("treasureGroup:" + treasureGroup + ",ItemID:" + res.ID);
            bool identified;
            if (Global.Random.Next(0, 99) <= 5)
                identified = true;
            else
                identified = false;
            GiveItem(pc, res.ID, (ushort)res.Count, identified);
        }

        /// <summary>
        /// 根据宝物组名随机取得一样道具
        /// </summary>
        /// <param name="treasureGroup">宝物组</param>
        /// <returns>道具</returns>
        protected SagaDB.Item.Item GetRandomTreasure(string treasureGroup)
        {
            TreasureItem res = TreasureFactory.Instance.GetRandomItem(treasureGroup);
            bool identified;
            /*if (Global.Random.Next(0, 99) <= 5)
                identified = true;
            else
                identified = false;
             */
            identified = true; //免鉴定
            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(res.ID, identified);
            item.Stack = (ushort)res.Count;
            return item;
        }

        /// <summary>
        /// 鉴定道具
        /// </summary>
        /// <param name="pc">玩家</param>
        public void Identify(ActorPC pc)
        {
            List<SagaDB.Item.Item> inv = pc.Inventory.GetContainer(ContainerType.BODY);
            List<SagaDB.Item.Item> list;
            var box =
                from c in inv
                where !c.Identified
                select c;
            list = box.ToList();

            if (list.Count > 0)
            {
                string[] names = new string[list.Count];
                int i = 0;
                foreach (SagaDB.Item.Item j in list)
                {
                    names[i] = GetItemNameByType(j.BaseData.itemType);
                    i++;
                }
                int sel = Select(pc, LocalManager.Instance.Strings.ITEM_IDENTIFY, "", true, names);
                if (sel != 255)
                {
                    MapClient client = GetMapClient(pc);
                    SagaDB.Item.Item item = list[sel - 1];
                    item.Identified = true;
                    client.SendItemIdentify(item.Slot);
                    client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_IDENTIFY_RESULT, names[sel - 1].Replace("\0", ""), item.BaseData.name));
                    client.SendSkillDummy(903, 1);
                }
            }
            else
            {
                MapClient client = GetMapClient(pc);
                client.SendSystemMessage(LocalManager.Instance.Strings.ITEM_IDENTIFY_NO_NEED);
                client.SendSkillDummy(903, 1);
            }
        }

        /// <summary>
        /// 打开玩家身上的宝物箱
        /// </summary>
        /// <param name="pc">玩家</param>
        public void OpenTreasureBox(ActorPC pc, int cost = 100)
        {
            OpenTreasureBox(pc, true, true, true, cost);
        }

        /// <summary>
        /// 打开玩家身上的宝物箱
        /// </summary>
        /// <param name="pc">玩家</param>
        public void OpenTreasureBox(ActorPC pc, bool timber, bool treasure, bool container, int cost)
        {
            List<SagaDB.Item.Item> inv = pc.Inventory.GetContainer(ContainerType.BODY);
            List<SagaDB.Item.Item> list;
            var box =
                 from c in inv
                 where (c.BaseData.itemType == ItemType.TIMBER_BOX && timber) ||
                 (c.BaseData.itemType == ItemType.TREASURE_BOX && treasure) ||
                 (c.BaseData.itemType == ItemType.CONTAINER && container)
                 select c;
            list = box.ToList();

            if (list.Count > 0)
            {
                if (pc.Gold >= cost)
                {
                    pc.Gold -= cost;
                    string[] names = new string[list.Count];
                    int i = 0;
                    foreach (SagaDB.Item.Item j in list)
                    {
                        names[i] = j.BaseData.name;
                        i++;
                    }
                    int sel = Select(pc, LocalManager.Instance.Strings.ITEM_TREASURE_OPEN, "", true, names);
                    if (sel != 255)
                    {
                        SagaDB.Item.Item item = list[sel - 1];
                        uint num = item.BaseData.id - item.BaseData.iconID;
                        TreasureItem res = TreasureFactory.Instance.GetRandomItem(item.BaseData.itemType.ToString() + num.ToString());
                        TakeItem(pc, item.ItemID, 1);
                        bool identified;
                        if (Global.Random.Next(0, 99) <= 5)
                            identified = true;
                        else
                            identified = false;
                        GiveItem(pc, res.ID, (ushort)res.Count, identified);
                    }
                }
                else
                {
                    MapClient client = GetMapClient(pc);
                    client.SendSystemMessage(LocalManager.Instance.Strings.NPC_BANK_NOT_ENOUGH_GOLD);
                }
            }
            else
            {
                MapClient client = GetMapClient(pc);
                client.SendSystemMessage(LocalManager.Instance.Strings.ITEM_TREASURE_NO_NEED);
            }
        }

        /// <summary>
        /// 显示屏幕特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">特效类型，Out=渐出，In=渐进</param>
        /// <param name="effect">特效效果，黑或者白</param>
        protected void Fade(ActorPC pc, FadeType type, FadeEffect effect)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_FADE p = new SagaMap.Packets.Server.SSMG_NPC_FADE();
            p.FadeEffect = effect;
            p.FadeType = type;
            client.netIO.SendPacket(p);
        }

        /// <summary>
        /// 2-1与2-2职业间互换
        /// </summary>
        /// <param name="pc"></param>
        /// <returns>转职结果</returns>
        protected bool JobSwitch(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_JOB_SWITCH p = new SagaMap.Packets.Server.SSMG_NPC_JOB_SWITCH();
            p.Job = pc.Job;
            int max = 0;
            if (pc.Job == pc.Job2X)
            {
                p.Level = pc.JobLevel2T;
                p.Job = pc.Job2T;
                p.LevelReduced = (byte)(pc.JobLevel2T - (pc.JobLevel2T / 5));
                p.PossibleReserveSkills = (ushort)(pc.JobLevel2X / 10);
                max = pc.JobLevel2T / 5;
            }
            else if (pc.Job == pc.Job2T)
            {
                p.Level = pc.JobLevel2X;
                p.Job = pc.Job2X;
                p.LevelReduced = (byte)(pc.JobLevel2X - (pc.JobLevel2X / 5));
                p.PossibleReserveSkills = (ushort)(pc.JobLevel2T / 10);
                max = pc.JobLevel2X / 5;
            }
            p.LevelItem = Configuration.Instance.JobSwitchReduceItem;
            SagaDB.Item.Item item = pc.Inventory.GetItem(Configuration.Instance.JobSwitchReduceItem, Inventory.SearchType.ITEM_ID);
            if (item != null)
            {
                if (item.Stack < max)
                    p.ItemCount = item.Stack;
                else
                    p.ItemCount = (uint)max;
            }

            p.PossibleSkills = pc.Skills2.Values.ToList();

            client.netIO.SendPacket(p);

            client.npcJobSwitch = true;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.npcJobSwitch)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
            return client.npcJobSwitchRes;
        }

        /// <summary>
        /// 重置技能点
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="job">1为1转，2为2转</param>
        protected void ResetSkill(ActorPC pc, byte job)
        {
            GetMapClient(pc).ResetSkill(job);
            GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// 重置属性点
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void ResetStatusPoint(ActorPC pc)
        {
            GetMapClient(pc).ResetStatusPoint();
        }

        /// <summary>
        /// 打开指定ID的揭示版
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="bbsID">揭示版ID</param>
        /// <param name="cost">发贴费用</param>
        protected void OpenBBS(ActorPC pc, uint bbsID, uint cost)
        {
            MapClient client = GetMapClient(pc);

            client.bbsClose = false;
            client.bbsCost = cost;
            client.bbsID = bbsID;

            Packets.Server.SSMG_COMMUNITY_BBS_OPEN p = new SagaMap.Packets.Server.SSMG_COMMUNITY_BBS_OPEN();
            p.Gold = cost;
            client.netIO.SendPacket(p);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.bbsClose)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();

        }

        /// <summary>
        /// 创建军团
        /// </summary>
        /// <param name="pc">创建者</param>
        /// <param name="name">军团名</param>
        /// <returns>创建成功返回true,如果名字已存在返回false</returns>
        protected bool CreateRing(ActorPC pc, string name)
        {
            MapClient client = MapClient.FromActorPC(pc);
            Ring ring = RingManager.Instance.CreateRing(pc, name);
            return (ring != null);
        }

        /// <summary>
        /// 发送飞空庭制作材料
        /// </summary>
        /// <param name="parts"></param>
        protected void SendFGardenCreateMaterial(ActorPC pc, BitMask<FGardenParts> parts)
        {
            Packets.Server.SSMG_FG_CREATE_MATERIAL p = new SagaMap.Packets.Server.SSMG_FG_CREATE_MATERIAL();
            p.Parts = parts;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 进入飞空庭
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="rope">绳子的Actor</param>
        protected void EnterFGarden(ActorPC pc, ActorEvent rope)
        {
            if (rope.Caster.FGarden == null)
            {
                return;
            }

            Packet p = new Packet(10);//unknown packet
            p.ID = 0x18E3;
            p.PutUInt(pc.ActorID, 2);
            p.PutUInt(pc.MapID, 6);
            MapClient.FromActorPC(pc).netIO.SendPacket(p);

            if (rope.Caster.FGarden.MapID == 0)
            {
                Map map = MapManager.Instance.GetMap(pc.MapID);
                rope.Caster.FGarden.MapID = MapManager.Instance.CreateMapInstance(rope.Caster, 70000000, pc.MapID, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height));

                //spawn furnitures
                map = MapManager.Instance.GetMap(rope.Caster.FGarden.MapID);
                foreach (ActorFurniture i in rope.Caster.FGarden.Furnitures[FurniturePlace.GARDEN])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false;
                }
            }
            pc.BattleStatus = 0;
            pc.Speed = 200;
            MapClient.FromActorPC(pc).SendChangeStatus();
            Warp(pc, rope.Caster.FGarden.MapID, 6, 11);
        }

        /// <summary>
        /// 离开飞空庭，如果不在飞空庭中则什么都不做
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void ExitFGarden(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            pc.Speed = 350;
            if (map.IsMapInstance && (map.ID / 10 * 10) == 70000000)
            {
                Warp(pc, map.ClientExitMap, map.ClientExitX, map.ClientExitY);
            }
        }

        /// <summary>
        /// 进入飞空庭房间
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void EnterFGRoom(ActorPC pc)
        {
            ActorPC owner = GetFGardenOwner(pc);
            if (owner == null)
                return;
            if (owner.FGarden.RoomMapID == 0)
            {
                owner.FGarden.RoomMapID = MapManager.Instance.CreateMapInstance(owner, 75000000, owner.FGarden.MapID, 6, 7);
                //spawn furnitures
                Map map = MapManager.Instance.GetMap(owner.FGarden.RoomMapID);
                foreach (ActorFurniture i in owner.FGarden.Furnitures[FurniturePlace.ROOM])
                {
                    i.e = new ActorEventHandlers.NullEventHandler();
                    map.RegisterActor(i);
                    i.invisble = false;
                }
            }
            Warp(pc, owner.FGarden.RoomMapID, 5, 11);
        }

        /// <summary>
        /// 离开飞空庭房间
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void ExitFGRoom(ActorPC pc)
        {
            ActorPC owner = GetFGardenOwner(pc);
            if (owner == null)
                return;
            Warp(pc, owner.FGarden.MapID, 5, 8);
        }


        /// <summary>
        /// 离开飞空城房间
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void ExitFFRoom(ActorPC pc)
        {
            ActorPC owner = GetFGardenOwner(pc);
            if (owner == null)
                return;
            Warp(pc, pc.Ring.FFarden.MapID, 10, 10);
        }

        /// <summary>
        /// 取得当前飞空庭地图的主人
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        protected ActorPC GetFGardenOwner(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            if (map.IsMapInstance && (map.ID / 10 * 10) == 70000000 || map.IsMapInstance && (map.ID / 10 * 10) == 75000000)
            {
                return map.Creator;
            }
            return null;
        }

        /// <summary>
        /// 取得指定玩家的飞空庭绳子，如果没有飞空庭或者没有召唤过飞空庭，则返回null
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>如果没有飞空庭或者没有召唤过飞空庭，则返回null</returns>
        protected ActorEvent GetRopeActor(ActorPC pc)
        {
            if (pc.FGarden != null)
            {
                if (pc.FGarden.RopeActor != null)
                {
                    return pc.FGarden.RopeActor;
                }
            }
            return null;
        }

        /// <summary>
        /// 收回飞空庭绳子
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void ReturnRope(ActorPC pc)
        {
            if (pc.FGarden != null)
            {
                if (pc.FGarden.RopeActor != null)
                {
                    SagaMap.Map map = MapManager.Instance.GetMap(pc.FGarden.RopeActor.MapID);
                    map.DeleteActor(pc.FGarden.RopeActor);
                    if (ScriptManager.Instance.Events.ContainsKey(pc.FGarden.RopeActor.EventID))
                    {
                        ScriptManager.Instance.Events.Remove(pc.FGarden.RopeActor.EventID);
                    }
                    pc.FGarden.RopeActor = null;
                }
                if (pc.FGarden.RoomMapID != 0)
                {
                    SagaMap.Map roomMap = MapManager.Instance.GetMap(pc.FGarden.RoomMapID);
                    SagaMap.Map gardenMap = MapManager.Instance.GetMap(pc.FGarden.MapID);
                    roomMap.ClientExitMap = gardenMap.ClientExitMap;
                    roomMap.ClientExitX = gardenMap.ClientExitX;
                    roomMap.ClientExitY = gardenMap.ClientExitY;
                    MapManager.Instance.DeleteMapInstance(roomMap.ID);
                    pc.FGarden.RoomMapID = 0;
                }
                if (pc.FGarden.MapID != 0)
                {
                    MapManager.Instance.DeleteMapInstance(pc.FGarden.MapID);
                    pc.FGarden.MapID = 0;
                }
            }
        }

        /// <summary>
        /// 飞空庭起飞
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="mapID">目的地ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        public void FGTakeOff(ActorPC pc, uint mapID, byte x, byte y)
        {
            ActorPC owner = GetFGardenOwner(pc);
            if (owner != pc)
                return;
            if (owner.FGarden.MapID != 0)
            {
                //spawn furnitures
                Map map = MapManager.Instance.GetMap(owner.FGarden.MapID);
                List<Actor> list = new List<Actor>();
                foreach (Actor i in map.Actors.Values)
                {
                    if (i.type == ActorType.PC)
                    {
                        ActorPC pc2 = (ActorPC)i;
                        if (pc2.Online)
                        {
                            Packets.Server.SSMG_FG_TAKEOFF p = new SagaMap.Packets.Server.SSMG_FG_TAKEOFF();
                            p.ActorID = pc2.ActorID;
                            p.MapID = owner.FGarden.MapID;
                            MapClient.FromActorPC(pc2).netIO.SendPacket(p);
                            list.Add(pc2);
                        }
                    }
                }
                foreach (Actor i in list)
                {
                    i.Speed = 350;
                    MapClient.FromActorPC((ActorPC)i).fgTakeOff = true;
                    Warp((ActorPC)i, mapID, x, y);
                }
            }

            if (owner.FGarden.RoomMapID != 0)
            {
                //spawn furnitures
                Map map = MapManager.Instance.GetMap(owner.FGarden.RoomMapID);
                List<Actor> list = new List<Actor>();
                foreach (Actor i in map.Actors.Values)
                {
                    if (i.type == ActorType.PC)
                    {
                        ActorPC pc2 = (ActorPC)i;
                        if (pc2.Online)
                        {
                            Packets.Server.SSMG_FG_TAKEOFF p = new SagaMap.Packets.Server.SSMG_FG_TAKEOFF();
                            p.ActorID = pc2.ActorID;
                            p.MapID = owner.FGarden.MapID;
                            MapClient.FromActorPC(pc2).netIO.SendPacket(p);
                            list.Add(pc2);
                        }
                    }
                }
                foreach (Actor i in list)
                {
                    i.Speed = 350;
                    MapClient.FromActorPC((ActorPC)i).fgTakeOff = true;
                    Warp((ActorPC)i, mapID, x, y);
                }
            }

            ReturnRope(pc);
        }

        /// <summary>
        /// 显示指定UI界面
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">界面种类</param>
        protected void ShowUI(ActorPC pc, UIType type)
        {
            Packets.Server.SSMG_NPC_SHOW_UI p = new SagaMap.Packets.Server.SSMG_NPC_SHOW_UI();
            p.UIType = type;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 让NPC做动作
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPC ID</param>
        /// <param name="motion">动作</param>
        protected void NPCMotion(ActorPC pc, uint npcID, ushort motion)
        {
            NPCMotion(pc, npcID,  motion, false, 0,1);
        }
        protected void NPCMotion(ActorPC pc, uint npcID, ushort motion, bool loop, bool unknown)
        {
            if (unknown)
                NPCMotion(pc, npcID, motion, loop, 0, 1);
            else
                NPCMotion(pc, npcID, motion, loop, 0, 0);
        }
        /// <summary>
        /// 让NPC做动作
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPC ID</param>
        /// <param name="motion">动作</param>
        /// <param name="loop">是否重复</param>
        /// <param name="speed">動作速度，最低0, 基準為10, 沒上限</param>
        /// <param name="unknown">可能是bool</param>
        protected void NPCMotion(ActorPC pc, uint npcID, ushort motion, bool loop, uint speed, byte unknown)
        {
            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
            p.ActorID = npcID;
            p.Motion = (MotionType)motion;
            if (loop)
                p.Loop = 1;
            p.MotionSpeed = speed;
            p.Unknown = 1;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 打开商城ECOShop
        /// </summary>
        /// <param name="pc"></param>
        public void VShopOpen(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_VSHOP_CATEGORY p = new SagaMap.Packets.Server.SSMG_VSHOP_CATEGORY();
            p.CurrentPoint = pc.VShopPoints;
            p.Categories = ECOShopFactory.Instance.Items;
            client.netIO.SendPacket(p);
            client.vshopClosed = false;
        }

        /// <summary>
        /// 设置石像类型
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">石像类型</param>
        public void SetGolemType(ActorPC pc, GolemType type)
        {
            if (pc.Golem != null)
            {
                pc.Golem.GolemType = type;
            }
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_GOLEM_SET_TYPE p = new SagaMap.Packets.Server.SSMG_GOLEM_SET_TYPE();
            p.GolemType = type;
            client.netIO.SendPacket(p);
        }

        /// <summary>
        /// 召喚指定玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="name">指定玩家的名稱</param>
        public void Recall(ActorPC pc, string name)
        {
            try
            {
                var chr =
                    from c in MapClientManager.Instance.OnlinePlayer
                    where c.Character.Name == name
                    select c;
                MapClient tClient = chr.First();
                MapClient client = MapClient.FromActorPC(pc);
                uint n_Mapid;
                short n_X, n_Y;
                n_X = tClient.Character.X;
                n_Y = tClient.Character.Y;
                n_Mapid = tClient.Character.MapID;
                client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 召喚指定玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="charID">指定玩家的CharID</param>
        public void Recall(ActorPC pc, uint charID)
        {
            try
            {
                var chr =
                    from c in MapClientManager.Instance.OnlinePlayer
                    where c.Character.CharID == charID
                    select c;
                MapClient tClient = chr.First();
                MapClient client = MapClient.FromActorPC(pc);
                uint n_Mapid;
                short n_X, n_Y;
                n_X = tClient.Character.X;
                n_Y = tClient.Character.Y;
                n_Mapid = tClient.Character.MapID;
                client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 跳到指定玩家的位置
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="charID">玩家的CharID</param>
        public void Jump(ActorPC pc, uint charID)
        {
            try
            {
                var chr =
                    from c in MapClientManager.Instance.OnlinePlayer
                    where c.Character.CharID == charID
                    select c;
                MapClient tClient = chr.First();
                MapClient client = MapClient.FromActorPC(pc);
                uint n_Mapid;
                short n_X, n_Y;
                n_X = tClient.Character.X;
                n_Y = tClient.Character.Y;
                n_Mapid = tClient.Character.MapID;
                client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);

            }
            catch (Exception) { }
        }

        /// <summary>
        /// 跳到指定玩家的位置
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="name">指定玩家的名字</param>
        public void Jump(ActorPC pc, string name)
        {
            try
            {
                var chr =
                    from c in MapClientManager.Instance.OnlinePlayer
                    where c.Character.Name == name
                    select c;
                MapClient tClient = chr.First();
                MapClient client = MapClient.FromActorPC(pc);
                uint n_Mapid;
                short n_X, n_Y;
                n_X = tClient.Character.X;
                n_Y = tClient.Character.Y;
                n_Mapid = tClient.Character.MapID;
                client.Map.SendActorToMap(client.Character, n_Mapid, n_X, n_Y);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 顯示玩家列表
        /// </summary>
        /// <param name="pc">玩家</param>
        public void Who2(ActorPC pc)
        {
            try
            {
                MapClient client = MapClient.FromActorPC(pc);
                foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                {
                    byte x, y;

                    x = Global.PosX16to8(i.Character.X, i.map.Width);
                    y = Global.PosY16to8(i.Character.Y, i.map.Height);
                    client.SendSystemMessage(i.Character.Name + "(CharID:" + i.Character.CharID + ")[" + i.Map.Name + " " + x.ToString() + "," + y.ToString() + "]");
                }
                client.SendSystemMessage(LocalManager.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 隱藏玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        public void Hide(ActorPC pc)
        {
            pc.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);
        }

        /// <summary>
        /// 顯示玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        public void Show(ActorPC pc)
        {
            pc.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);
        }

        /// <summary>
        /// 取得某類道具的所有道具列表
        /// </summary>
        /// <param name="types">類型</param>
        /// <returns>道具列表</returns>
        public List<SagaDB.Item.Item.ItemData> GetItemTypeList(params ItemType[] types)
        {
            var lst = from KeyValuePair<uint, SagaDB.Item.Item.ItemData> i in ItemFactory.Instance.Items
                      where types.Contains(i.Value.itemType)
                      select i.Value;
            return lst.ToList();
        }

        /// <summary>
        /// 取得某類道具的所有道具列表
        /// </summary>
        /// <param name="types">類型</param>
        /// <returns>道具列表</returns>
        public List<SagaDB.Item.Item> GetItemTypeList(ActorPC pc, params ItemType[] types)
        {
            var lst = from SagaDB.Item.Item i in pc.Inventory.Items[ContainerType.BODY]
                      where types.Contains(i.BaseData.itemType)
                      select i;
            return lst.ToList();
        }

        /// <summary>
        /// 创建地下城
        /// </summary>
        /// <param name="pc">创建者</param>
        /// <param name="id">地下城数据ID</param>
        /// <param name="exitMap">退出MapID</param>
        /// <param name="exitX">退出X</param>
        /// <param name="exitY">退出Y</param>
        /// <returns>地下城的ID</returns>
        protected int CreateDungeon(ActorPC pc, uint id, uint exitMap, byte exitX, byte exitY)
        {
            int did = 0;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            did = Dungeon.DungeonFactory.Instance.CreateDungeon(id, pc, exitMap, exitX, exitY);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return did;
        }

        /// <summary>
        /// 传送玩家到遗迹
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void WarpToDungeon(ActorPC pc)
        {
            Dungeon.Dungeon dungeon = null;
            List<uint> dungeons = GetPossibleDungeons(pc);
            if (dungeons.Count > 0)
            {
                if (dungeons.Count == 1)
                    dungeon = Dungeon.DungeonFactory.Instance.GetDungeon(dungeons[0]);
                else
                {
                    string[] names = new string[dungeons.Count];
                    for (int i = 0; i < dungeons.Count; i++)
                    {
                        names[i] = Dungeon.DungeonFactory.Instance.GetDungeon(dungeons[i]).Creator.Name + LocalManager.Instance.Strings.ITD_DUNGEON_NAME;
                    }
                    dungeon = Dungeon.DungeonFactory.Instance.GetDungeon(dungeons[Select(pc, LocalManager.Instance.Strings.ITD_SELECT_DUUNGEON, "", names) - 1]);
                }
            }
            if (dungeon != null)
            {
                Warp(pc,
                    dungeon.Start.Map.ID,
                    dungeon.Start.Gates[SagaMap.Dungeon.GateType.Entrance].X,
                    dungeon.Start.Gates[SagaMap.Dungeon.GateType.Entrance].Y);

            }
        }

        /// <summary>
        /// 取得可能的地牢
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        protected List<uint> GetPossibleDungeons(ActorPC pc)
        {
            List<uint> list = new List<uint>();
            ////第一次创建时副本ID肯定写进去了..当然有些没有写
            //if (pc.DungeonID != 0)
            //    list.Add(pc.DungeonID);
            //第一次创建没有写入的情况下...或者掉线了清除了的情况下 遍历地下城列表获取自己创建的地下城
            if (Dungeon.DungeonFactory.Instance.GetDungeon(pc) != null)
                list.Add(Dungeon.DungeonFactory.Instance.GetDungeon(pc).DungeonID);
            ////自己不是创建人的情况下,获取队友创建的地下城
            //if (pc.Party != null)
            //{
            //    foreach (ActorPC i in pc.Party.Members.Values)
            //    {
            //        if (i == pc)
            //            continue;
            //        if (i.DungeonID != 0)
            //            list.Add(i.DungeonID);
            //    }
            //}
            ////移除重复的ID只保留一个
            //HashSet<uint> h = new HashSet<uint>(list);
            //list.Clear();
            //list.AddRange(h);
            return list;
        }

        /// <summary>
        /// 隐藏NPC
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        protected void NPCHide(ActorPC pc, uint npcID)
        {
            uint mapID;
            if (NPCFactory.Instance.Items.ContainsKey(npcID))
            {
                mapID = NPCFactory.Instance.Items[npcID].MapID;
            }
            else
                mapID = pc.MapID;
            if (!pc.NPCStates.ContainsKey(mapID))
                pc.NPCStates.Add(mapID, new Dictionary<uint, bool>());
            if (!pc.NPCStates[mapID].ContainsKey(npcID))
                pc.NPCStates[mapID].Add(npcID, false);
            else
                pc.NPCStates[mapID][npcID] = false;
            if (pc.MapID == mapID)
            {
                Packets.Server.SSMG_NPC_HIDE p = new SagaMap.Packets.Server.SSMG_NPC_HIDE();
                p.NPCID = npcID;
                MapClient.FromActorPC(pc).netIO.SendPacket(p);
            }
            MapServer.charDB.SaveChar(pc, false);
        }

        /// <summary>
        /// 显示NPC
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        protected void NPCShow(ActorPC pc, uint npcID)
        {
            uint mapID;
            if (NPCFactory.Instance.Items.ContainsKey(npcID))
            {
                mapID = NPCFactory.Instance.Items[npcID].MapID;
            }
            else
                mapID = pc.MapID;
            if (!pc.NPCStates.ContainsKey(mapID))
                pc.NPCStates.Add(mapID, new Dictionary<uint, bool>());
            if (!pc.NPCStates[mapID].ContainsKey(npcID))
                pc.NPCStates[mapID].Add(npcID, true);
            else
                pc.NPCStates[mapID][npcID] = true;
            if (pc.MapID == mapID)
            {
                Packets.Server.SSMG_NPC_SHOW p = new SagaMap.Packets.Server.SSMG_NPC_SHOW();
                p.NPCID = npcID;
                MapClient.FromActorPC(pc).netIO.SendPacket(p);
            }
            MapServer.charDB.SaveChar(pc, false);
        }

        /// <summary>
        /// 變身成怪物
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="MobID">怪物ID(設定為0時表示變回玩家的樣子)</param>
        protected void TranceMob(ActorPC pc, uint MobID)
        {
            SkillHandler.Instance.TranceMob(pc, MobID);
        }

        /// <summary>
        /// 显示影院电影播放表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="mapID">影院地图ID</param>
        protected void ShowTheaterSchedule(ActorPC pc, uint mapID)
        {
            if (pc.Online)
            {
                Packets.Server.SSMG_THEATER_SCHEDULE_HEADER p = new SagaMap.Packets.Server.SSMG_THEATER_SCHEDULE_HEADER();
                p.MapID = mapID;
                if (TheaterFactory.Instance.Items.ContainsKey(mapID))
                {
                    var query =
                        from movie in TheaterFactory.Instance.Items[mapID]
                        orderby movie.StartTime
                        select movie;
                    if (query.Count() > 0)
                    {
                        p.Count = query.Count();
                        MapClient.FromActorPC(pc).netIO.SendPacket(p);
                        int j = 0;
                        foreach (Movie i in query.ToList())
                        {
                            Packets.Server.SSMG_THEATER_SCHEDULE p2 = new SagaMap.Packets.Server.SSMG_THEATER_SCHEDULE();
                            p2.Index = j;
                            p2.TicketItem = i.Ticket;
                            p2.Time = string.Format("{0:00}:{1:00}", i.StartTime.Hour, i.StartTime.Minute);
                            p2.Title = i.Name;
                            MapClient.FromActorPC(pc).netIO.SendPacket(p2);
                            j++;
                        }
                    }
                    else
                        MapClient.FromActorPC(pc).netIO.SendPacket(p);
                }

                Packets.Server.SSMG_THEATER_SCHEDULE_FOOTER p3 = new SagaMap.Packets.Server.SSMG_THEATER_SCHEDULE_FOOTER();
                p3.MapID = mapID;
                MapClient.FromActorPC(pc).netIO.SendPacket(p3);
            }
        }

        /// <summary>
        /// 得到下一场即将放映的电影
        /// </summary>
        /// <param name="mapID">影院MapID</param>
        /// <returns>电影</returns>
        protected Movie GetNextMovie(uint mapID)
        {
            return TheaterFactory.Instance.GetNextMovie(mapID);
        }

        /// <summary>
        /// 得到正在放映的电影
        /// </summary>
        /// <param name="mapID">影院MapID</param>
        /// <returns>电影</returns>
        protected Movie GetCurrentMovie(uint mapID)
        {
            return TheaterFactory.Instance.GetCurrentMovie(mapID);
        }

        /// <summary>
        /// 盖印章
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="genre">印章系列</param>
        /// <param name="slot">印章槽</param>
        protected bool UseStamp(ActorPC pc, StampGenre genre, StampSlot slot)
        {
            if (!pc.Stamp[genre].Test(slot))
            {
                pc.Stamp[genre].SetValue(slot, true);
                Packets.Server.SSMG_STAMP_USE p = new SagaMap.Packets.Server.SSMG_STAMP_USE();
                p.Genre = genre;
                switch (slot)
                {
                    case StampSlot.One:
                        p.Slot = 0;
                        break;
                    case StampSlot.Two:
                        p.Slot = 1;
                        break;
                    case StampSlot.Three:
                        p.Slot = 2;
                        break;
                    case StampSlot.Four:
                        p.Slot = 3;
                        break;
                    case StampSlot.Five:
                        p.Slot = 4;
                        break;
                    case StampSlot.Six:
                        p.Slot = 5;
                        break;
                    case StampSlot.Seven:
                        p.Slot = 6;
                        break;
                    case StampSlot.Eight:
                        p.Slot = 7;
                        break;
                    case StampSlot.Nine:
                        p.Slot = 8;
                        break;
                    case StampSlot.Ten:
                        p.Slot = 9;
                        break;
                }
                MapClient.FromActorPC(pc).netIO.SendPacket(p);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 检查玩家印章某个系列是否收集齐全
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="genre">系列</param>
        /// <returns></returns>
        protected bool CheckStampGenre(ActorPC pc, StampGenre genre)
        {
            if (genre != StampGenre.Special)
            {
                return (pc.Stamp[genre].Test(StampSlot.One) && pc.Stamp[genre].Test(StampSlot.Two) && pc.Stamp[genre].Test(StampSlot.Three) &&
                    pc.Stamp[genre].Test(StampSlot.Four) && pc.Stamp[genre].Test(StampSlot.Five) && pc.Stamp[genre].Test(StampSlot.Six) &&
                    pc.Stamp[genre].Test(StampSlot.Seven) && pc.Stamp[genre].Test(StampSlot.Eight) && pc.Stamp[genre].Test(StampSlot.Nine) &&
                    pc.Stamp[genre].Test(StampSlot.Ten));
            }
            else
            {
                return (pc.Stamp[genre].Test(StampSlot.One) && pc.Stamp[genre].Test(StampSlot.Two) && pc.Stamp[genre].Test(StampSlot.Three) &&
                    pc.Stamp[genre].Test(StampSlot.Four) && pc.Stamp[genre].Test(StampSlot.Five) && pc.Stamp[genre].Test(StampSlot.Six) &&
                    pc.Stamp[genre].Test(StampSlot.Seven) && pc.Stamp[genre].Test(StampSlot.Eight));
            }
        }

        /// <summary>
        /// 清除某个系列的印章
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="genre">印章系列</param>
        protected void ClearStampGenre(ActorPC pc, StampGenre genre)
        {
            pc.Stamp[genre].Value = 0;
            MapClient.FromActorPC(pc).SendStamp();
        }

        /// <summary>
        /// 清除某个系列的印章
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="genre">印章系列</param>
        protected void CleardDailyStamp(ActorPC pc)
        {
            pc.DailyStamp.Stamps.Value = 0;
        }

        /// <summary>
        /// 取得指定等级所需经验
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="type">等级类别</param>
        /// <returns>所需经验</returns>
        protected ulong GetExpForLevel(uint level, LevelType type)
        {
            return ExperienceManager.Instance.GetExpForLevel(level, type);
        }

        /// <summary>
        /// 更改玩家高度
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="size">高度</param>
        protected void ChangePlayerSize(ActorPC pc, uint size)
        {
            pc.Size = size;
            MapClient.FromActorPC(pc).SendPlayerSizeUpdate();
        }


        /// <summary>
        /// 发型相关
        /// </summary>             
        public class HairStyleList
        {
            public string stylename;
            public ushort hairstyle;
            public ushort wig;
            public string saybefore;
            public string sayafter;
        }
        protected List<uint> gethairlist(ActorPC pc)//返回发型券
        {
            List<uint> hairtickets = new List<uint>();
            var res = from hair in HairFactory.Instance.Hairs where (CountItem(pc, hair.ItemID) > 0) select hair;
            List<Hair> hairs = res.ToList();
            foreach (Hair i in hairs)
            {
                uint couponid;
                couponid = i.ItemID;
                if (!hairtickets.Contains(couponid))
                {
                    hairtickets.Add(couponid);
                }
            }
            return hairtickets;
        }
        protected List<HairStyleList> gethairstyles(ActorPC pc, uint id)//返回发型详细信息
        {
            List<HairStyleList> hairstyles = new List<HairStyleList>();
            var res = from hair in HairFactory.Instance.Hairs where ((CountItem(pc, hair.ItemID) > 0) && (hair.ItemID == id)) select hair;
            List<Hair> hairs = res.ToList();
            foreach (Hair i in hairs)
            {
                HairStyleList hsl = new HairStyleList();
                hsl.stylename = i.HairName + "(" + i.WigName + ")";
                hsl.hairstyle = (ushort)i.HairStyle;
                hsl.wig = (ushort)i.HairWig;
                hsl.saybefore = i.NpcBeforeSay;
                hsl.sayafter = i.NpcAfterSay;
                if (i.Gender == 2)
                {
                    hairstyles.Add(hsl);
                }
                else
                {
                    if (i.Gender == 1 && pc.Gender == PC_GENDER.FEMALE)
                        hairstyles.Add(hsl);
                    if (i.Gender == 0 && pc.Gender == PC_GENDER.MALE)
                        hairstyles.Add(hsl);
                }
            }
            return hairstyles;
        }
        /// <summary>
        /// 强化装备
        /// </summary>
        /// <param name="pc">玩家</param>
        protected bool ItemEnhance(ActorPC pc)
        {
            var res = from item in pc.Inventory.GetContainer(ContainerType.BODY)
                      where ((item.IsArmor && ((CountItem(pc, 90000044) > 0 || CountItem(pc, 90000045) > 0 || CountItem(pc, 90000046) > 0 || CountItem(pc, 90000043) > 0)))
                      || (item.IsWeapon && (CountItem(pc, 90000044) > 0 || CountItem(pc, 90000045) > 0 || CountItem(pc, 90000046) > 0))
                      || (item.BaseData.itemType == ItemType.SHIELD && (CountItem(pc, 90000044) > 0 || CountItem(pc, 90000045) > 0))
                      || (item.BaseData.itemType == ItemType.ACCESORY_NECK && (CountItem(pc, 90000044) > 0))) && item.Refine < 25
                      select item;
            List<SagaDB.Item.Item> items = res.ToList();
            if (items.Count > 0)
            {
                Packets.Server.SSMG_ITEM_ENHANCE_LIST p = new SagaMap.Packets.Server.SSMG_ITEM_ENHANCE_LIST();
                p.Items = items;
                MapClient client = MapClient.FromActorPC(pc);
                client.netIO.SendPacket(p);
                client.itemEnhance = true;

                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (client.itemEnhance)
                {
                    System.Threading.Thread.Sleep(500);
                }
                if (blocked)
                    ClientManager.EnterCriticalArea();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 幻化
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="confirm"></param>
        protected void ItemFusion(ActorPC pc, string confirm)
        {

            int sel = 0;
            int rate = Configuration.Instance.ItemFusionRate;
            MapClient client = MapClient.FromActorPC(pc);
            Packets.Server.SSMG_ITEM_FUSION_RESULT p2;

            do
            {
                Packets.Server.SSMG_ITEM_FUSION p = new SagaMap.Packets.Server.SSMG_ITEM_FUSION();
                client.itemFusion = true;
                client.itemFusionView = 0;
                client.itemFusionEffect = 0;
                client.netIO.SendPacket(p);

                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (client.itemFusion)
                {
                    System.Threading.Thread.Sleep(500);
                }
                if (blocked)
                    ClientManager.EnterCriticalArea();
                if (client.itemFusionEffect != 0 && client.itemFusionView != 0)
                {
                    SagaDB.Item.Item effectItem = pc.Inventory.GetItem(client.itemFusionEffect);
                    SagaDB.Item.Item viewItem = pc.Inventory.GetItem(client.itemFusionView);
                    p2 = new SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT();
                    if (effectItem != null && viewItem != null)
                    {
                        int price = effectItem.BaseData.possibleLv * 1000;
                        sel = Select(pc, confirm, ""
                            , LocalManager.Instance.Strings.NPC_ITEM_FUSION_RECHOOSE
                            , LocalManager.Instance.Strings.NPC_ITEM_FUSION_CANCEL
                            , string.Format(LocalManager.Instance.Strings.NPC_ITEM_FUSION_CONFIRM, price, rate));
                        switch (sel)
                        {
                            case 1:
                            case 2:
                                p2.Result = SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.CANCELED;
                                break;
                            case 3:
                                if (pc.Gold >= price)
                                {
                                    SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult res = checkFusionItem(effectItem, viewItem);
                                    p2.Result = res;
                                    if (res == SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.OK)
                                    {
                                        pc.Gold -= price;
                                        if (Global.Random.Next(0, 99) < rate)
                                        {
                                            //修正为幻化传递
                                            if (viewItem.PictID != 0)
                                                effectItem.PictID = viewItem.PictID;
                                            else
                                                effectItem.PictID = viewItem.ItemID;
                                            TakeItemBySlot(pc, viewItem.Slot, 1);
                                            client.SendItemInfo(effectItem);
                                            ShowEffect(pc, 5191);
                                        }
                                        else
                                            p2.Result = SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.FAILED;
                                    }
                                }
                                else
                                    p2.Result = SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.NOT_ENOUGH_GOLD;
                                break;
                        }
                    }
                    else
                        p2.Result = SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.FAILED;
                    if (sel != 1)
                        client.netIO.SendPacket(p2);
                }
            } while (sel == 1 && (client.itemFusionEffect != 0 && client.itemFusionView != 0));
        }

        SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult checkFusionItem(SagaDB.Item.Item effect, SagaDB.Item.Item view)
        {
            if (effect.BaseData.possibleLv < view.BaseData.possibleLv)
                return SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.LV_TOO_LOW;
            foreach (PC_JOB i in effect.BaseData.possibleJob.Keys)
            {
                if (effect.BaseData.possibleJob[i] && !view.BaseData.possibleJob[i])
                    return SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.JOB_NOT_FIT;
            }
            foreach (PC_GENDER i in effect.BaseData.possibleGender.Keys)
            {
                if (effect.BaseData.possibleGender[i] && !view.BaseData.possibleGender[i])
                    return SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.GENDER_NOT_FIT;
            }
            foreach (PC_RACE i in effect.BaseData.possibleRace.Keys)
            {
                if (effect.BaseData.possibleRace[i] && !view.BaseData.possibleRace[i])
                    return SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.NOT_FIT;
            }
            return SagaMap.Packets.Server.SSMG_ITEM_FUSION_RESULT.FusionResult.OK;
        }

        /// <summary>
        /// CheckMapFlag
        /// </summary>
        /// <param name="pc">pc</param>
        /// <param name="flag">flag</param>
        protected bool CheckMapFlag(ActorPC pc, SagaDB.Map.MapFlags flag)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            return map.Info.Flag.Test(flag);
        }

        /// <summary>
        /// 改变NPC的外观(失效中)
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        /// <param name="mobID">怪物ID</param>
        protected void NPCChangeView(ActorPC pc, uint npcID, uint mobID)
        {
            Packets.Server.SSMG_NPC_CHANGE_VIEW p = new SagaMap.Packets.Server.SSMG_NPC_CHANGE_VIEW();
            p.NPCID = npcID;
            p.MobID = mobID;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 是否可参加防御战
        /// </summary>
        /// <param name="mapID">战场MapID</param>
        /// <returns></returns>
        protected bool ODWarCanApply(uint mapID)
        {
            return ODWarManager.Instance.CanApply(mapID);
        }

        /// <summary>
        /// 复活象征
        /// </summary>
        /// <param name="mapID">战场MapID</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        protected SymbolReviveResult ODWarReviveSymbol(uint mapID, int number)
        {
            return ODWarManager.Instance.ReviveSymbol(mapID, number);
        }

        /// <summary>
        /// 是否是防御战状态
        /// </summary>
        /// <param name="mapID">战场MapID</param>
        /// <returns></returns>
        protected bool ODWarIsDefence(uint mapID)
        {
            return ODWarManager.Instance.IsDefence(mapID);
        }

        /// <summary>
        /// 给武器打洞
        /// </summary>
        /// <param name="pc"></param>
        protected void ItemAddSlot(ActorPC pc)
        {
            List<uint> items = new List<uint>();
            foreach (SagaDB.Item.Item i in pc.Inventory.GetContainer(ContainerType.BODY))
            {
                if (i.IsEquipt)
                {
                    if (i.CurrentSlot >= 5)
                        continue;
                    if (i.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE ||
                        i.EquipSlot[0] == EnumEquipSlot.UPPER_BODY ||
                        i.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                    {
                        items.Add(i.Slot);
                    }
                }
            }
            foreach (SagaDB.Item.Item i in pc.Inventory.GetContainer(ContainerType.BACK_BAG))
            {
                if (i.IsEquipt)
                {
                    if (i.CurrentSlot >= 5)
                        continue;
                    if (i.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE ||
                        i.EquipSlot[0] == EnumEquipSlot.UPPER_BODY ||
                        i.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                    {
                        items.Add(i.Slot);
                    }
                }
            }
            foreach (SagaDB.Item.Item i in pc.Inventory.GetContainer(ContainerType.LEFT_BAG))
            {
                if (i.IsEquipt)
                {
                    if (i.CurrentSlot >= 5)
                        continue;
                    if (i.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE ||
                        i.EquipSlot[0] == EnumEquipSlot.UPPER_BODY ||
                        i.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                    {
                        items.Add(i.Slot);
                    }
                }
            }
            foreach (SagaDB.Item.Item i in pc.Inventory.GetContainer(ContainerType.RIGHT_BAG))
            {
                if (i.IsEquipt)
                {
                    if (i.CurrentSlot >= 5)
                        continue;
                    if (i.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE ||
                        i.EquipSlot[0] == EnumEquipSlot.UPPER_BODY ||
                        i.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                    {
                        items.Add(i.Slot);
                    }
                }
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].CurrentSlot < 5)
                    items.Add(pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot);
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                if (pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].CurrentSlot < 5)
                    items.Add(pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].Slot);
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                if (pc.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE].CurrentSlot < 5)
                    items.Add(pc.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE].Slot);

            if (items.Count > 0)
            {
                MapClient client = MapClient.FromActorPC(pc);
                client.irisAddSlot = true;
                client.SendSkillDummy(2090, 1);

                Packets.Server.SSMG_IRIS_ADD_SLOT_ITEM_LIST p = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_ITEM_LIST();
                p.Items = items;
                client.netIO.SendPacket(p);

                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (client.irisAddSlot)
                {
                    System.Threading.Thread.Sleep(500);
                }
                if (blocked)
                    ClientManager.EnterCriticalArea();

            }
            else
            {
                Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT p = new SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT();
                //p.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM;
                p.Result = -2;
                MapClient.FromActorPC(pc).netIO.SendPacket(p);
            }
        }

        /// <summary>
        /// 卡片组合
        /// </summary>
        /// <param name="pc"></param>
        protected void IrisCardAssemble(ActorPC pc)
        {
            Dictionary<SagaDB.Iris.IrisCard, int> cards = new Dictionary<SagaDB.Iris.IrisCard, int>();
            foreach (SagaDB.Item.Item i in pc.Inventory.Items[ContainerType.BODY])
            {
                if (i.BaseData.itemType == ItemType.IRIS_CARD)
                {
                    if (SagaDB.Iris.IrisCardFactory.Instance.Items.ContainsKey(i.BaseData.id))
                    {
                        SagaDB.Iris.IrisCard card = SagaDB.Iris.IrisCardFactory.Instance.Items[i.BaseData.id];
                        if (card.NextCard != 0)
                        {
                            cards.Add(card, 5000);
                        }
                    }
                }
            }
            MapClient client = MapClient.FromActorPC(pc);
            if (cards.Count > 0)
            {
                client.irisCardAssemble = true;
                client.SendSkillDummy(2091, 1);

                Packets.Server.SSMG_IRIS_CARD_ASSEMBLE p = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE();
                p.CardAndPrice = cards;
                client.netIO.SendPacket(p);

                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (client.irisCardAssemble)
                {
                    System.Threading.Thread.Sleep(500);
                }
                if (blocked)
                    ClientManager.EnterCriticalArea();
            }
            else
            {
                Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT p = new SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT();
                p.Result = SagaMap.Packets.Server.SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NO_ITEM;
                client.netIO.SendPacket(p);
            }
        }

        /// <summary>
        /// 检查地图标识
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="flag">要检查的标识</param>
        /// <returns>结果</returns>
        protected bool CheckMapFlag(uint mapID, MapFlags flag)
        {
            Map map = MapManager.Instance.GetMap(mapID);
            if (map == null)
                return false;
            return map.Info.Flag.Test(flag);
        }

        /// <summary>
        /// 添加计时器，启动延迟为0.5秒，设置需要脚本
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="period">再启动间隔</param>
        /// <param name="autoDispose">是否随着玩家下线自动销毁</param>
        /// <returns>添加的计时器</returns>
        protected Timer AddTimer(string name, int period, ActorPC pc, bool autoDispose = false)
        {
            return AddTimer(name, period, 500, pc, true, autoDispose);
        }
        /// <summary>
        /// 添加计时器
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="period">重复间隔，单位为毫秒</param>
        /// <param name="dueTime">启动延迟</param>
        /// <param name="pc">挂钩的玩家</param>
        /// <param name="needScript">是否需要用脚本</param>
        /// <returns>添加的计时器</returns>
        protected Timer AddTimer(string name, int period, int dueTime, ActorPC pc, bool needScript)
        {
            return AddTimer(name, period, dueTime, pc, needScript, true);
        }
        /// <summary>
        /// 添加计时器
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="period">重复间隔，单位为毫秒</param>
        /// <param name="dueTime">启动延迟</param>
        /// <param name="pc">挂钩的玩家</param>
        /// <param name="needScript">是否需要用脚本</param>
        /// <param name="autoDispose">是否随着玩家下线自动销毁</param>
        /// <returns>添加的计时器</returns>
        protected Timer AddTimer(string name, int period, int dueTime, ActorPC pc, bool needScript, bool autoDispose = false)
        {
            Timer timer = new Timer(name, period, dueTime);
            timer.AttachedPC = pc;
            timer.NeedScript = needScript;

            string timerName = string.Format("{0}:{1}({2})", name, pc.Name, pc.CharID);
            if (ScriptManager.Instance.Timers.ContainsKey(timerName))
            {
                ScriptManager.Instance.Timers[timerName].Deactivate();
                ScriptManager.Instance.Timers[timerName] = null;
                ScriptManager.Instance.Timers[timerName] = timer;
            }
            else
                ScriptManager.Instance.Timers.Add(timerName, timer);
            if (autoDispose)
            {
                if (pc.Tasks.ContainsKey(timerName))
                    pc.Tasks[timerName].Deactivate();
                pc.Tasks[timerName] = timer;
            }
            return timer;
        }


        /// <summary>
        /// 删除指定Timer
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="pc">绑定的玩家</param>
        protected void DeleteTimer(string name, ActorPC pc)
        {
            string timerName = string.Format("{0}:{1}({2})", name, pc.Name, pc.CharID);
            if (ScriptManager.Instance.Timers.ContainsKey(timerName))
            {
                ScriptManager.Instance.Timers[timerName].Deactivate();
                ScriptManager.Instance.Timers.Remove(timerName);
            }
        }

        /// <summary>
        /// 取得制定Timer
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="pc">绑定的玩家</param>
        protected Timer GetTimer(string name, ActorPC pc)
        {
            string timerName = string.Format("{0}:{1}({2})", name, pc.Name, pc.CharID);
            if (ScriptManager.Instance.Timers.ContainsKey(timerName))
                return (Timer)ScriptManager.Instance.Timers[timerName];
            else
                return null;
        }

        /// <summary>
        /// 在客户端显示图片
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="path">图片路径</param>
        /// <param name="title">标题</param>
        protected void ShowPicture(ActorPC pc, string path, string title)
        {
            Packets.Server.SSMG_NPC_SHOW_PIC p = new SagaMap.Packets.Server.SSMG_NPC_SHOW_PIC();
            p.Path = path;
            p.Title = title;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 改变飞空庭天气
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="weather">天气</param>
        protected void FGardenChangeWeather(ActorPC pc, byte weather)
        {
            Packets.Server.SSMG_FG_CHANGE_WEATHER p = new SagaMap.Packets.Server.SSMG_FG_CHANGE_WEATHER();
            p.Weather = weather;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 改变飞空庭天空
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="sky">天空</param>
        protected void FGardenChangeSky(ActorPC pc, byte sky)
        {
            Packets.Server.SSMG_FG_CHANGE_SKY p = new SagaMap.Packets.Server.SSMG_FG_CHANGE_SKY();
            p.Sky = sky;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 发送全服公告
        /// </summary>
        /// <param name="text">文字</param>
        protected void Announce(string text)
        {
            MapClientManager.Instance.Announce(text);
        }

        /// <summary>
        /// 给指定玩家发公告
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="text">文字</param>
        protected void Announce(ActorPC pc, string text)
        {
            MapClient.FromActorPC(pc).SendAnnounce(text);
        }

        /// <summary>
        /// 向某地图发公告
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="text">文字</param>
        protected void Announce(uint mapID, string text)
        {
            Map map = MapManager.Instance.GetMap(mapID);
            if (map != null)
            {
                Actor[] actors = map.Actors.Values.ToArray();
                foreach (Actor i in actors)
                {
                    if (i.type == ActorType.PC)
                    {
                        MapClient.FromActorPC((ActorPC)i).SendAnnounce(text);
                    }
                }
            }
        }
        /// <summary>
        /// 在指定地点刷怪物
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mobID">怪物ID</param>
        /// <param name="Event">事件处理器</param>
        /// <param name="Callbacktype">事件类型 1、死亡时 2、使用技能时 3、移动结束时 4、攻击时 5、血量变化时</param>
        /// <returns></returns>
        protected ActorMob SpawnMob(uint mapID, byte x, byte y, uint mobID, MobCallback Event, byte Callbacktype)
        {
            return SpawnMob(mapID, x, y, mobID, 1, Event, Callbacktype)[0];
        }
        /// <summary>
        /// 在指定地点刷怪物
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mobID">怪物ID</param>
        /// <param name="count">数量</param>
        protected List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count)
        {
            return SpawnMob(mapID, x, y, mobID, count, null, 0);
        }
        /// <summary>
        /// 刷怪物并且设置死亡事件
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mobID">怪物ID</param>
        /// <param name="count">数量</param>
        /// <param name="Event">事件处理器</param>
        protected List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count, MobCallback Event)
        {
            return SpawnMob(mapID, x, y, mobID, count, Event, 1);
        }
        /// <summary>
        /// 刷怪物并且设置死亡事件
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mobID">怪物ID</param>
        /// <param name="count">数量</param>
        /// <param name="Event">事件处理器</param>
        /// <param name="Callbacktype">事件类型 1、死亡时 2、使用技能时 3、移动结束时 4、攻击时 5、血量变化时</param>
        protected List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count, MobCallback Event, byte Callbacktype)
        {
            Map map = MapManager.Instance.GetMap(mapID);
            List<ActorMob> mobs = new List<ActorMob>();
            for (int i = 0; i < count; i++)
            {
                ActorMob mob = map.SpawnMob(mobID, Global.PosX8to16(x, map.Width), Global.PosY8to16(y, map.Height), 1000, null);
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)mob.e;
                if (Event != null)
                {
                    switch (Callbacktype)
                    {
                        case 1:
                            eh.Dying += Event;
                            break;
                        case 2:
                            eh.SkillUsing += Event;
                            break;
                        case 3:
                            eh.Moving += Event;
                            break;
                        case 4:
                            eh.Attacking += Event;
                            break;
                        case 5:
                            eh.Defending += Event;
                            break;
                    }
                }
                mobs.Add(mob);
            }
            return mobs;
        }

        /// <summary>
        /// 恢复玩家身上的宠物的亲密度
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="amount">恢复数量</param>
        protected void PetRecover(ActorPC pc, int amount)
        {
            var it =
                from c in pc.Inventory.GetContainer(ContainerType.BODY)
                where (c.BaseData.itemType == ItemType.PET || c.BaseData.itemType == ItemType.PET_NEKOMATA || c.BaseData.itemType == ItemType.RIDE_PET) &&
                    c.Durability < c.BaseData.durability
                select c;

            MapClient client = MapClient.FromActorPC(pc);
            client.selectedPet = 0;

            Packets.Server.SSMG_NPC_PET_SELECT p = new SagaMap.Packets.Server.SSMG_NPC_PET_SELECT();
            p.Type = SagaMap.Packets.Server.SSMG_NPC_PET_SELECT.SelType.Recover;
            p.Pets = it.ToList();
            MapClient.FromActorPC(pc).netIO.SendPacket(p);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.selectedPet == 0)
            {
                System.Threading.Thread.Sleep(100);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();

            if (client.selectedPet != uint.MaxValue)
            {
                SagaDB.Item.Item item = pc.Inventory.GetItem(client.selectedPet);
                if (item != null)
                {
                    if (item.BaseData.itemType == ItemType.PET || item.BaseData.itemType == ItemType.PET_NEKOMATA || item.BaseData.itemType == ItemType.RIDE_PET)
                    {
                        item.Durability += (ushort)amount;
                        if (item.Durability > item.BaseData.durability)
                            item.Durability = item.BaseData.durability;
                        client.SendItemInfo(item);
                    }
                }
            }
        }

        /// <summary>
        /// 打开DEM 的CL购买窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void DEMCL(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient client = MapClient.FromActorPC(pc);
            bool dominion = pc.InDominionWorld;
            Packets.Server.SSMG_DEM_COST_LIMIT p = new SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT();
            p.CurrentEP = pc.EPUsed;
            p.EPRequired = (short)(ExperienceManager.Instance.GetEPRequired(pc) - pc.EPUsed);
            client.demCLBuy = true;
            client.netIO.SendPacket(p);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.demCLBuy)
            {
                System.Threading.Thread.Sleep(100);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 打开DEM身体改造窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void DEMParts(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient client = MapClient.FromActorPC(pc);
            Packets.Server.SSMG_DEM_PARTS p = new SagaMap.Packets.Server.SSMG_DEM_PARTS();

            client.demParts = true;
            client.netIO.SendPacket(p);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.demParts)
            {
                System.Threading.Thread.Sleep(100);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 打开ＤＥＭＩＣ窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void DEMIC(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient client = MapClient.FromActorPC(pc);
            client.demic = true;
            Packets.Server.SSMG_DEM_DEMIC_HEADER p = new SagaMap.Packets.Server.SSMG_DEM_DEMIC_HEADER();

            if (client.map.Info.Flag.Test(MapFlags.Dominion))
            {
                p.CL = pc.DominionCL;
                client.netIO.SendPacket(p);
                foreach (byte i in pc.Inventory.DominionDemicChips.Keys)
                {
                    Packets.Server.SSMG_DEM_DEMIC_DATA p2 = new SagaMap.Packets.Server.SSMG_DEM_DEMIC_DATA();
                    p2.Page = i;
                    p2.Chips = pc.Inventory.GetChipList(i, true);
                    client.netIO.SendPacket(p2);
                }
            }
            else
            {
                p.CL = pc.CL;
                client.netIO.SendPacket(p);
                foreach (byte i in pc.Inventory.DemicChips.Keys)
                {
                    Packets.Server.SSMG_DEM_DEMIC_DATA p2 = new SagaMap.Packets.Server.SSMG_DEM_DEMIC_DATA();
                    p2.Page = i;
                    p2.Chips = pc.Inventory.GetChipList(i, false);
                    client.netIO.SendPacket(p2);
                }
            }

            Packets.Server.SSMG_DEM_DEMIC_FOOTER p3 = new SagaMap.Packets.Server.SSMG_DEM_DEMIC_FOOTER();
            client.netIO.SendPacket(p3);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.demic)
            {
                System.Threading.Thread.Sleep(100);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 移动NPC
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dir">方向</param>
        /// <param name="type">移动方式</param>
        protected void NPCMove(ActorPC pc, uint npcID, short x, short y, short dir, MoveType type)
        {
            Packets.Server.SSMG_ACTOR_MOVE p1 = new SagaMap.Packets.Server.SSMG_ACTOR_MOVE();
            p1.ActorID = npcID;
            p1.X = x;
            p1.Y = y;
            p1.Dir = (ushort)dir;
            p1.MoveType = type;
            MapClient.FromActorPC(pc).netIO.SendPacket(p1);
        }

        /// <summary>
        /// 移动NPC
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed">速度</param>
        /// <param name="dir">方向</param>
        /// <param name="type">类型，7为水平移动，0xb为跳跃</param>
        /// <param name="motion">表情</param>
        /// <param name="motionSpeed">表情速度</param>
        /// <param name="ShowType">设0为移动，设18为瞬移</param>
        protected void NPCMove(ActorPC pc, uint npcID, byte x, byte y, ushort speed, byte type, ushort motion, ushort motionSpeed)
        {
            NPCMove(pc, npcID, x, y, speed, 0, type, motion, motionSpeed,0);
        }
        protected void NPCMove(ActorPC pc, uint npcID, byte x, byte y, ushort speed, byte type, ushort motion, ushort motionSpeed, ushort showType)
        {
            NPCMove(pc, npcID, x, y, speed, 0, type, motion, motionSpeed, showType);
        }
        protected void NPCMove(ActorPC pc, uint npcID, byte x, byte y, ushort speed, byte dir, byte type, ushort motion, ushort motionSpeed, ushort showType)
        {
            Packets.Server.SSMG_NPC_MOVE p1 = new SagaMap.Packets.Server.SSMG_NPC_MOVE();
            p1.NPCID = npcID;
            p1.X = x;
            p1.Y = y;
            p1.Speed = speed;
            p1.Type = type;
            p1.Dir = dir;
            p1.ShowType = showType;
            p1.Motion = motion;
            p1.MotionSpeed = motionSpeed;
            MapClient.FromActorPC(pc).netIO.SendPacket(p1);
        }

        /// <summary>
        /// 打开芯片购买窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        protected void DEMChipShop(ActorPC pc)
        {
            MapClient client = MapClient.FromActorPC(pc);
            Packets.Server.SSMG_DEM_CHIP_SHOP_CATEGORY p = new SagaMap.Packets.Server.SSMG_DEM_CHIP_SHOP_CATEGORY();
            p.Categories = ChipShopFactory.Instance.GetCategoryFromLv(pc.Level);

            client.chipShop = true;

            client.netIO.SendPacket(p);

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.chipShop)
            {
                System.Threading.Thread.Sleep(100);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// 出现传送门
        /// </summary>
        /// <param name="pc">玩家，用于确定地图</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="eventID">传送门触发的EventID</param>
        protected void ShowPortal(ActorPC pc, byte x, byte y, uint eventID)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            Actor[] actors = map.Actors.Values.ToArray();
            foreach (Actor i in actors)
            {
                if (i.type == ActorType.PC)
                {
                    ActorPC j = (ActorPC)i;
                    if (j.Online)
                    {
                        MapClient client = MapClient.FromActorPC(j);
                        Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                        p1.StartX = x;
                        p1.StartY = y;
                        p1.EndX = x;
                        p1.EndY = y;
                        p1.EventID = eventID;
                        p1.EffectID = 9005;
                        client.netIO.SendPacket(p1);
                    }
                }
            }
        }

        /// <summary>
        /// 更换宠物外观
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="pictID">宠物外观ID,值为0的时候还原</param>
        protected void PetShowReplace(ActorPC pc, uint pictID)
        {
            MapClient client = MapClient.FromActorPC(pc);
            if (pc.Inventory.Equipments[EnumEquipSlot.PET] != null)
            {
                pc.Inventory.Equipments[EnumEquipSlot.PET].PictID = pictID;
                client.DeletePet();
                client.SendPet(pc.Inventory.Equipments[EnumEquipSlot.PET]);
                //pc.Pet.PictID = pictID;
                //Actor pet = pc.Pet;
                //client.Map.OnActorVisibilityChange(pet);
            }
        }

        /// <summary>
        /// 改变某个玩家的职业-3
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="job">职业</param>
        protected void ChangePlayerJobTo3(ActorPC pc, PC_JOB job)
        {
            if ((byte)job % 10 != 7)
                return;
            pc.Job = job;
            pc.Level1 = pc.Level;
            pc.Level = 1;
            pc.CEXP = 0;
            pc.JEXP = 0;

            ResetStatusPoint(pc);
            ResetSkill(pc, 2);
            ResetSkill(pc, 1);
            pc.StatsPoint = 2;

            PC.StatusFactory.Instance.CalcStatus(pc);
            pc.HP = pc.MaxHP;
            pc.MP = pc.MaxMP;
            pc.SP = pc.MaxSP;
            GetMapClient(pc).SendPlayerInfo();
            GetMapClient(pc).SendCharInfo();
        }

        /// <summary>
        /// 改变臉型
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="faceid">臉型ID</param>
        protected void ChangePlayerFace(ActorPC pc, ushort faceid)
        {
            MapClient client = MapClient.FromActorPC(pc);
            if (!FaceFactory.Instance.Faces.ContainsKey(faceid))
            {
                client.SendSystemMessage("尝试切换成不存在的脸ID");
                return;
            }
            client.Character.Face = faceid;
            client.SendCharInfoUpdate();
        }
        ///<summary>
        ///增加獲得任務點數
        ///</summary>
        ///<param name="pc">玩家</param>
        ///<param name="val">個數</param>
        protected void QuestPointBonus (ActorPC pc, byte val)
        {
            pc.QuestRemaining += val;
            GetMapClient(pc).SendQuestPoints();
        }
        /// <summary>
        /// 鎖定玩家視角
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="X">X軸</param>
        /// <param name="Y">Y軸</param>
        /// <param name="Z">Z軸</param>
        /// <param name="Xdir">X方向 0至360</param>
        /// <param name="Ydir">Y方向 0至360</param>
        protected void LockViewAngle(ActorPC pc, short X, short Y, short Z, short Xdir, short Ydir, ushort speed, short param)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_LOCK_VIEWANGLE p = new Packets.Server.SSMG_NPC_LOCK_VIEWANGLE();
            p.X = X;
            p.Y = Y;
            p.Z = Z;
            p.Xdir = Xdir;
            p.Ydir = Ydir;
            p.CameraMoveSpeed = speed;
            p.Param = param;
            client.netIO.SendPacket(p);
        }
        protected void LockViewAngle(ActorPC pc, byte X, byte Y, short Z, short Xdir, short Ydir, ushort speed, short param)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_LOCK_VIEWANGLE p = new Packets.Server.SSMG_NPC_LOCK_VIEWANGLE();
            short x = Global.PosX8to16(X, client.Map.Width);
            short y = Global.PosY8to16(Y, client.Map.Height);
            p.X = Global.PosX8to16(X, client.Map.Width);
            p.Y = Global.PosY8to16(Y, client.Map.Height);
            p.Z = Z;
            p.Xdir = Xdir;
            p.Ydir = Ydir;
            p.CameraMoveSpeed = speed;
            p.Param = param;
            client.netIO.SendPacket(p);
        }

        protected void FGardenFurnitureMotion(ActorPC pc,uint ActorID, ushort Motion, ushort EndMotion, short Z, ushort Dir)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_FG_FURNITURE_MOTION p = new Packets.Server.SSMG_FG_FURNITURE_MOTION();
            p.ActorID = ActorID;
            p.Motion = Motion;
            p.EndMotion = EndMotion;
            p.Z = Z;
            p.Dir = Dir;
            client.netIO.SendPacket(p);
        }
    }
}
