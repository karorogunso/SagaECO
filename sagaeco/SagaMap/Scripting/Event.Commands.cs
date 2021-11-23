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
using SagaDB.Title;
using SagaMap.Skill;

namespace SagaMap.Scripting
{
    public abstract partial class Event
    {
        public void Royale(ActorPC pc,bool enter,bool randomPict = false)
        {
            MapClient client = MapClient.FromActorPC(pc);
            if (enter)
            {
                if (randomPict)
                {
                    pc.TInt["randomPict"] = 1;
                    Hair hair = HairFactory.Instance.Hairs[Random(0, HairFactory.Instance.Hairs.Count - 1)];
                    List<int> Colors = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12,50,51,52,60,61,62,70,71,72,18,19,20,21,22,80,81,82,23,24,25,26,27,28,29,30 };
                    uint faceID = FaceFactory.Instance.FaceIDs[Random(0, FaceFactory.Instance.FaceIDs.Count - 1)];
                    int color = Colors[Random(0, Colors.Count - 1)];
                    pc.hairStyletemp = (ushort)hair.HairStyle;
                    pc.hairColortemp = (byte)color;
                    pc.wigtemp = (ushort)hair.HairWig;
                    pc.facetemp = (ushort)faceID;
                    pc.nametemp = "咕咕" + Random(10000,99999).ToString();
                }
                pc.inventoryTemp = new Inventory(pc);
                pc.TInt["大逃杀模式"] = 1;
                foreach (var item in pc.InventoryOri.Items)
                {
                    foreach (var i in pc.InventoryOri.Items[item.Key])
                    {
                        Packets.Server.SSMG_ITEM_DELETE p = new Packets.Server.SSMG_ITEM_DELETE();
                        p.InventorySlot = i.Slot;
                        client.netIO.SendPacket(p);
                    }
                }
                foreach (var item in pc.InventoryOri.Equipments)
                {
                    Packets.Server.SSMG_ITEM_DELETE p = new Packets.Server.SSMG_ITEM_DELETE();
                    p.InventorySlot = item.Value.Slot;
                    client.netIO.SendPacket(p);
                }
                pc.sightRange = 1500;
                client.SendItems();
                client.CaculateItemSkills();
                PC.StatusFactory.Instance.CalcStatus(pc);
                client.SendStatusExtend();
                client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                client.SendPlayerInfo();
            }
            else
            {
                pc.TInt["randomPict"] = 0;
                pc.TInt["大逃杀模式"] = 0;
                foreach (var item in pc.InventoryOri.Items)
                {
                    foreach (var i in pc.InventoryOri.Items[item.Key])
                    {
                        Packets.Server.SSMG_ITEM_DELETE p = new Packets.Server.SSMG_ITEM_DELETE();
                        p.InventorySlot = i.Slot;
                        client.netIO.SendPacket(p);
                    }
                }
                foreach (var item in pc.InventoryOri.Equipments)
                {
                    Packets.Server.SSMG_ITEM_DELETE p = new Packets.Server.SSMG_ITEM_DELETE();
                    p.InventorySlot = item.Value.Slot;
                    client.netIO.SendPacket(p);
                }
                pc.sightRange = Global.MAX_SIGHT_RANGE;
                client.SendItems();
                client.CaculateItemSkills();
                PC.StatusFactory.Instance.CalcStatus(pc);
                client.SendStatusExtend();
                client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                client.SendPlayerInfo();
            }
        }
        /// <summary>
        /// 累积每周的任务点消耗
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="points"></param>
        public void WeeklyQPoints(ActorPC pc, int points)
        {
            if (points > 0)
            {
                pc.AInt["本周消耗任务点"] += points;
                SystemMessage(pc, "本周累积消耗了任务点：" + pc.AInt["本周消耗任务点"] + "点");

                //红豆活动部分
                /*ushort count = (ushort)(points / 2);
                if (count >= 1)
                    GiveItem(pc, 120005605, count);

                if (pc.AStr["每日红豆种子"] != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    pc.AStr["每日红豆种子"] = DateTime.Now.ToString("yyyy-MM-dd");
                    SystemMessage(pc, "今日第一次消耗任务点！额外获得了50个红豆种子");
                    GiveItem(pc, 120005605, 50);
                }

                if(Random(0,100) <= points)
                {
                    PlaySound(pc, 2814, false, 100, 50);
                    GiveItem(pc, 910000134, 1);
                }*/
            }
        }
        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="msg"></param>
        public void SystemMessage(ActorPC pc,string msg)
        {
            SkillHandler.SendSystemMessage(pc, msg);
        }
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="max">最大</param>
        /// <returns>随机数</returns>
        public int Random(int max)
        {
            return Global.Random.Next(max);
        }
        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="min">最小</param>
        /// <param name="max">最大</param>
        /// <returns>随机数</returns>
        public int Random(int min, int max)
        {
            return Global.Random.Next(min, max);
        }


        public void OpenChoiceShop(ActorPC pc)
        {
            MapClient client = MapClient.FromActorPC(pc);
            string args = "05 F9 02 31 00 00 00 04 00 01 00 00";
            byte[] buf = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            Packet p = new Packet();
            p.data = buf;
            client.netIO.SendPacket(p);
        }

        /// <summary>
        /// 显示周围玩家战斗信息（默认重置及复活）
        /// </summary>
        /// <param name="pc">请只传入一个玩家</param>
        public void 显示周围玩家战斗信息(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            List<Actor> actors = map.GetRoundAreaActors(pc.X, pc.Y, 5000, true);
            List<ActorPC> pcs = new List<ActorPC>();
            foreach (var item in actors)
            {
                if (item == null) continue;
                if (item.type == ActorType.PC && item.MapID == map.ID)
                    pcs.Add((ActorPC)item);
            }
            foreach (var item in pcs)
            {
                if (item.Buff.Dead)
                    MapClient.FromActorPC(item).RevivePC(item);
                string text = "玩家" + item.Name + " 在本次战斗总共造成伤害：" + item.TInt["伤害统计"].ToString() + " 受到伤害：" + item.TInt["受伤害统计"].ToString() +
       " 共治疗：" + item.TInt["治疗统计"].ToString() + " 共受到治疗：" + item.TInt["受治疗统计"].ToString() + " 总死亡次数：" + item.TInt["死亡统计"];
                item.TInt["伤害统计"] = 0;
                item.TInt["受伤害统计"] = 0;
                item.TInt["治疗统计"] = 0;
                item.TInt["受治疗统计"] = 0;
                item.TInt["死亡统计"] = 0;
                foreach (var item2 in pcs)
                {
                    if (item2.Online)
                        MapClient.FromActorPC(item2).SendSystemMessage(text);
                }
            }
        }
        /// <summary>
        /// 显示队伍的战斗信息（默认重置及复活）
        /// </summary>
        /// <param name="pc">请只传入一个玩家</param>
        public void 显示队伍战斗信息(ActorPC pc)
        {
            if (pc.Party != null)
            {
                foreach (var item in pc.Party.Members.Values)
                {
                    if (item.Buff.Dead)
                        MapClient.FromActorPC(item).RevivePC(item);
                    string text = "队友" + item.Name + " 在本次战斗总共造成伤害：" + item.TInt["伤害统计"].ToString() + " 受到伤害：" + item.TInt["受伤害统计"].ToString() +
                           " 共治疗：" + item.TInt["治疗统计"].ToString() + " 共受到治疗：" + item.TInt["受治疗统计"].ToString() + " 总死亡次数：" + item.TInt["死亡统计"];
                    item.TInt["伤害统计"] = 0;
                    item.TInt["受伤害统计"] = 0;
                    item.TInt["治疗统计"] = 0;
                    item.TInt["受治疗统计"] = 0;
                    item.TInt["死亡统计"] = 0;
                    foreach (var item2 in pc.Party.Members)
                    {
                        if (item2.Value.Online)
                            MapClient.FromActorPC(item2.Value).SendSystemMessage(text);
                    }
                }
            }
        }

        /// <summary>
        /// 打开石像收购页面
        /// </summary>
        /// <param name="pc"></param>
        public void OpenGolemBuy(ActorPC pc, ActorGolem golem)
        {
            uint ActorID = golem.ActorID;
            MapClient client = MapClient.FromActorPC(pc);

            Packets.Server.SSMG_GOLEM_SHOP_BUY_HEADER p2 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_HEADER();
            p2.ActorID = ActorID;
            client.netIO.SendPacket(p2);

            Packets.Server.SSMG_GOLEM_SHOP_BUY_ITEM p3 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_ITEM();
            p3.Items = golem.BuyShop;
            p3.BuyLimit = golem.BuyLimit;
            client.netIO.SendPacket(p3);
        }
        public void OpenGolemSell(ActorPC pc, ActorGolem golem)
        {
            uint ActorID = golem.ActorID;
            MapClient client = MapClient.FromActorPC(pc);

            Packets.Server.SSMG_GOLEM_SHOP_OPEN_OK p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_OPEN_OK();
            p1.ActorID = ActorID;
            client.netIO.SendPacket(p1);
            Packets.Server.SSMG_GOLEM_SHOP_HEADER p2 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_HEADER();
            p2.ActorID = ActorID;
            client.netIO.SendPacket(p2);
            foreach (uint i in golem.SellShop.Keys)
            {
                SagaDB.Item.Item item = ItemFactory.Instance.GetItem(golem.SellShop[i].ItemID);
                if (item == null)
                    continue;
                Packets.Server.SSMG_GOLEM_SHOP_ITEM p3 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_ITEM();
                p3.InventorySlot = i;
                p3.Container = ContainerType.BODY;
                p3.Price = golem.SellShop[i].Price;
                p3.ShopCount = golem.SellShop[i].Count;
                p3.Item = item;
                client.netIO.SendPacket(p3);
            }
            Packets.Server.SSMG_GOLEM_SHOP_FOOTER p4 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_FOOTER();
            client.netIO.SendPacket(p4);
        }
        /// <summary>
        /// 更新某个称号的条件
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ID">称号ID</param>
        /// <param name="value">进度提升数量</param>
        public void TitleProccess(ActorPC pc, uint ID, uint value)
        {
            MapClient client = MapClient.FromActorPC(pc);
            client.TitleProccess(pc, ID, value);
        }
        /// <summary>
        /// 无视条件，强制解锁某个称号
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ID">称号ID</param>
        public void UnlockTitle(ActorPC pc, uint ID)
        {
            MapClient client = MapClient.FromActorPC(pc);
            client.UnlockTitle(pc, ID);
        }
        /// <summary>
        /// 无视条件，强制锁定某个称号，并清空进度
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ID">称号ID</param>
        public void LockTitle(ActorPC pc, uint ID)
        {
            MapClient client = MapClient.FromActorPC(pc);
            if (TitleFactory.Instance.TitleList.ContainsKey(ID) && client.CheckTitle((int)ID))
            {
                Title t = TitleFactory.Instance.TitleList[ID];
                client.SetTitle((int)ID, false);
            }
        }
        /// <summary>
        /// 显示AAA对话框
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ID">对话框ID</param>
        public void ShowDialog(ActorPC pc, ushort ID)
        {
            Packets.Server.SSMG_ANO_DIALOG_BOX p = new Packets.Server.SSMG_ANO_DIALOG_BOX();
            p.DID = ID;
            MapClient client = GetMapClient(pc);
            client.netIO.SendPacket(p);
        }
        public void ChangeMessageBox(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_MESSAGE_GALMODE p = new Packets.Server.SSMG_NPC_MESSAGE_GALMODE();
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 打开卡片扭蛋界面
        /// </summary>
        /// <param name="pc"></param>
        public void OpenIrisGacha(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_IRIS_GACHA_UI_OPEN p = new Packets.Server.SSMG_IRIS_GACHA_UI_OPEN();
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 每日盖章
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="StampCountm">章数，最多10</param>
        /// <param name="type">1为显示，2为盖</param>
        public void DailyStamp(ActorPC pc, uint StampCountm, byte type)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_DAILY_STAMP p = new Packets.Server.SSMG_NPC_DAILY_STAMP();
            p.StampCount = (byte)StampCountm;
            p.Type = 2;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 显示发型预览UI
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">2为显示全部介绍，其他参数未知</param>
        public void ShowHairPreview(ActorPC pc, int type)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SHOW_HAIR_PREVIEW p = new Packets.Server.SSMG_NPC_SHOW_HAIR_PREVIEW();
            p.type = 2;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 面對玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        public void FaceToPC(ActorPC pc)
        {

        }
        /// <summary>
        /// 立刻保存系統變量
        /// </summary>
        public void SaveServerSvar()
        {
            MapServer.charDB.SaveServerVar(ScriptManager.Instance.VariableHolder);
        }
        /// <summary>
        /// 隱藏周圍玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        public void HidePlayer(ActorPC pc)
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
        public void ShowPict(ActorPC pc, uint NpcID, byte x, byte y, byte dir, uint PictID)
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
        public void ShowPictCancel(ActorPC pc, uint NpcID)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_SHOWPICT_CANCEL p = new Packets.Server.SSMG_NPC_SHOWPICT_CANCEL();
            p.NPCID = NpcID;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 锁定玩家镜头
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="X">左右位置</param>
        /// <param name="Y">上下位置</param>
        /// <param name="Z">前后位置</param>
        /// <param name="Xdir">左右方向</param>
        /// <param name="Ydir">上下方向</param>
        public void LockCamera(ActorPC pc, short X, short Y, short Z, short Xdir, short Ydir)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NPC_LOCK_CAMERA p = new Packets.Server.SSMG_NPC_LOCK_CAMERA();
            p.X = X;
            p.Y = Y;
            p.Z = Z;
            p.Xdir = Xdir;
            p.Ydir = Ydir;
            client.netIO.SendPacket(p);
        }
        /// <summary>
        /// 移动玩家自指定位置
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="speed">移动速度</param>
        /// <param name="dir">方向</param>
        public void MovePC(ActorPC pc, byte x, byte y, ushort speed, ushort dir, MoveType movetype)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            short[] pos = new short[2];
            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
            map.MoveActor(Map.MOVE_TYPE.START, pc, pos, dir, speed, true, movetype);
        }
        /// <summary>
        /// 让玩家做指定动作
        /// </summary>
        /// <param name="pc">pc</param>
        /// <param name="MotionID">动作ID</param>
        /// <param name="loop">是否循环</param>
        public void MotionPC(ActorPC pc, uint MotionID, byte Loop)
        {
            MapClient client = GetMapClient(pc);
            client.SendMotion((MotionType)MotionID, Loop);
        }
        /// <summary>
        /// 为对象设置定时说话
        /// </summary>
        /// <param name="actor">对象</param>
        /// <param name="Millisecond">毫秒</param>
        /// <param name="message">说话内容</param>
        public void SetTimingSpeak(Actor actor, int Millisecond, string message)
        {
            Tasks.Mob.TimingSpeak ts = new Tasks.Mob.TimingSpeak(actor, Millisecond, message);
            actor.Tasks.Add(actor.ActorID.ToString() + message + SagaLib.Global.Random.Next(0, 90001000).ToString(), ts);
            ts.Activate();
        }
        /// <summary>
        /// 设置玩家死亡时触发的ID
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="EventID">事件ID</param>
        /// <param name="MapID">触发的地图ID </param>
        public void SetOnDieEvent(ActorPC pc, int EventID, int MapID)
        {
            pc.TInt["死亡事件ID"] = EventID;
            pc.TInt["死亡事件地图ID"] = MapID;
        }
        /// <summary>
        /// 设置玩家下一次移动触发的脚本
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="EventID">事件ID</param>
        public void SetNextMoveEvent(ActorPC pc, uint EventID)
        {
            pc.CInt["NextMoveEventID"] = (int)EventID;
        }
        /// <summary>
        /// 根据NaviID获得步骤列表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">导航ID</param>
        /// <returns>SagaDB.Navi.Navi</returns>
        public SagaDB.Navi.Event GetNavi(ActorPC pc, uint NaviID)
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
        public SagaDB.Navi.Event GetNavi(ActorPC pc, byte ListID, byte QuestID)
        {
            return pc.Navi.Categories[ListID].Events[QuestID];
        }

        /// <summary>
        /// 调整某个导航的状态
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">导航ID</param>
        /// <param name="state">状态（00不显示，01显示，03完成，07未领奖）</param>
        public void OpenNavi(ActorPC pc, uint NaviID, byte state)
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
        public void OpenNavi(ActorPC pc, byte ListID, byte QuestID, byte state)
        {
            pc.Navi.Categories[ListID].Events[QuestID].State = state;
        }
        /// <summary>
        /// 完成导航的某个步骤
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="NaviID">步骤ID</param>
        /*public void NaviQuestCompletedUP(ActorPC pc, uint NaviID)
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
        public void NaviQuestMarkUP(ActorPC pc, uint NaviID)
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
        public void OpenRecycle(ActorPC pc)
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
        public void OpenChangePCForm(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_TEST_EVOLVE_OPEN p1 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN();
            client.netIO.SendPacket(p1);
            Packets.Server.SSMG_TEST_EVOLVE_OPEN2 p2 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN2();
            client.netIO.SendPacket(p2);
            Packets.Server.SSMG_TEST_EVOLVE_OPEN3 p3 = new Packets.Server.SSMG_TEST_EVOLVE_OPEN3();
            client.netIO.SendPacket(p3);
        }
        /// <summary>
        /// 进入飞空城
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="FFMaster">飞空城主人</param>
        public void EnterFFarden(ActorPC pc, ActorPC FFMaster)
        {
            /*if (pc.FFarden == null)
            {
                return;
            }

            if (pc.FFarden.MapID == 0)
            {
                Map map = MapManager.Instance.GetMap(pc.MapID);
                pc.FFarden.MapID = MapManager.Instance.CreateMapInstance(pc, 90001000, pc.MapID, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height));
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
        /*public void OpenFFList(ActorPC pc)
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
        public void Warp(ActorPC pc, uint mapID, byte x, byte y)
        {
            MapClient client = GetMapClient(pc);
            if (Configuration.Instance.HostedMaps.Contains(mapID))
            {
                Map newMap = MapManager.Instance.GetMap(mapID);
                if (client.Character.Marionette != null)
                    client.MarionetteDeactivate();
                client.Map.SendActorToMap(pc, mapID, Global.PosX8to16(x, newMap.Width), Global.PosY8to16(y, newMap.Height));
            }
        }
        /// <summary>
        /// 传送玩家到指定地图
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="mapID">地图ID</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <example>Warp(pc, 10024000, 150,130);</example>
        public void Warp(ActorPC pc, uint mapID, short x, short y)
        {
            MapClient client = GetMapClient(pc);
            if (Configuration.Instance.HostedMaps.Contains(mapID))
            {
                if (client.Character.Marionette != null)
                    client.MarionetteDeactivate();
                client.Map.SendActorToMap(pc, mapID, x, y);
            }
        }
        /// <summary>
        /// NPC对话框
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="motion">NPC动作</param>
        /// <param name="message">消息内容</param>
        public void Say(ActorPC pc, ushort motion, string message)
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
        public void Say(ActorPC pc, ushort motion, string message, string title)
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
        public void Say(ActorPC pc, uint npcID, ushort motion, string message)
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
        public void Say(ActorPC pc, uint npcID, ushort motion, string message, string title)
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
        /// 播放指定BGM
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="SoundID">音乐ID</param>
        /// <param name="Loop">循环</param>
        /// <param name="Volume">音量</param>
        public void ChangeBGM(ActorPC pc, uint SoundID, byte Loop, uint Volume)
        {
            MapClient client = GetMapClient(pc);

            Packets.Server.SSMG_NPC_CHANGE_BGM p3 = new Packets.Server.SSMG_NPC_CHANGE_BGM();
            p3.SoundID = SoundID;
            p3.Loop = Loop;
            p3.Volume = Volume;
            client.netIO.SendPacket(p3);
        }


        /// <summary>
        /// 让客户端延迟一定事件
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="wait">延迟事件，单位为毫秒</param>
        public void Wait(ActorPC pc, uint wait)
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
        public int Select(ActorPC pc, string title, string confirm, params string[] options)
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
        public int Select(ActorPC pc, string title, string confirm, bool canCancel, params string[] options)
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
            Packets.Server.SSMG_NPC_SELECT_RESULT p2 = new Packets.Server.SSMG_NPC_SELECT_RESULT();
            client.netIO.SendPacket(p2);
            return client.npcSelectResult;
        }

        /// <summary>
        /// 添加道具到商店
        /// </summary>
        /// <param name="goods">道具ID</param>
        public void AddGoods(params uint[] goods)
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
        public void OpenShopByList(ActorPC pc, params uint[] Goods)
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
        public void OpenShopByList(ActorPC pc, uint rate, ShopType type, params uint[] Goods)
        {
            MapClient client = GetMapClient(pc);
            List<uint> items = Goods.ToList();
            foreach (var item in items)
            {
                if (!KujiListFactory.Instance.ZeroPriceList.Contains(item))
                    KujiListFactory.Instance.ZeroPriceList.Add(item);
            }
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
                    client.Character.TInt["ShopType"] = 1;
                    break;
                case ShopType.ECoin:
                    p.Gold = pc.ECoin;
                    client.Character.TInt["ShopType"] = 2;
                    break;
            }
            p.Type = type;
            client.netIO.SendPacket(p);

            client.npcShopClosed = false;
            client.currentShop = null;
            client.currentEvent.Goods.AddRange(items);
            client.Character.Status.buy_rate = (short)rate;

            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!client.npcShopClosed)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();

            client.currentEvent.Goods.Clear();
            client.Character.Status.buy_rate = 100;
            client.currentShop = null;
            client.Character.TInt["ShopType"] = 0;
        }
        /// <summary>
        /// 根据Shop ID打开商店
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="shopID">商店ID</param>
        public void OpenShopBuy(ActorPC pc, uint shopID)
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
        public void OpenShopBuy(ActorPC pc)
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
        public void OpenShopSell(ActorPC pc, uint shopID)
        {
            Packets.Server.SSMG_NPC_SHOP_SELL p = new SagaMap.Packets.Server.SSMG_NPC_SHOP_SELL();
            Shop shop = ShopFactory.Instance.Items[shopID];
            MapClient client = GetMapClient(pc);
            //p.Rate = shop.BuyRate *10;
            p.Rate = 0;
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
        public void OpenShopSell(ActorPC pc)
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
        public void PlaySound(ActorPC pc, uint soundID, bool loop, uint volume, byte balance)
        {
            MapClient client = GetMapClient(pc);
            byte ifloop;
            if (loop)
                ifloop = 1;
            else
                ifloop = 0;
            client.SendNPCPlaySound(soundID, ifloop, volume, balance);
        }

        public void ChangeBGM(ActorPC pc, uint soundID, bool loop, uint volume, byte balance)
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
        public void ShowEffect(ActorPC pc, uint effectID)
        {
            ShowEffect(pc, pc, effectID);
        }

        /// <summary>
        /// 在指定NPC处显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="target">NPCID</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffect(ActorPC pc, uint target, uint effectID)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = target;
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
        public void ShowEffect(ActorPC pc, byte x, byte y, uint effectID)
        {
            EffectArg arg = new EffectArg();
            arg.effectID = effectID;
            arg.actorID = 0xFFFFFFFF;
            arg.x = x;
            arg.y = y;
            MapClient client = GetMapClient(pc);
            client.SendNPCShowEffect(arg.actorID, arg.x, arg.y, arg.height, arg.effectID, arg.oneTime);
            //client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, pc, true);
        }

        /// <summary>
        /// 在指定对象处显示特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="target">对象</param>
        /// <param name="effectID">特效ID</param>
        public void ShowEffect(ActorPC pc, Actor target, uint effectID)
        {
            ShowEffect(pc, target.ActorID, effectID);
        }

        /// <summary>
        /// 开始Event
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="eventID"></param>
        public void StartEvent(ActorPC pc, uint eventID)
        {
            MapClient client = GetMapClient(pc);
            client.SendCurrentEvent(eventID);
        }

        /// <summary>
        /// 取得地图名称
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <returns></returns>
        public string GetMapName(uint mapID)
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
        public void SetHomePoint(ActorPC pc, uint mapID, byte x, byte y)
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
        public int CountItem(ActorPC pc, uint itemID)
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
        public List<SagaDB.Item.Item> GetItem(ActorPC pc, uint ID)
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
        public void GiveItem(ActorPC pc, SagaDB.Item.Item item)
        {
            MapClient client = GetMapClient(pc);
            Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + pc.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                    string.Format("ScriptGive Count:{0}", item.Stack), true);
            client.AddItem(item, true, true, true);

        }

        /// <summary>
        /// 给予玩家指定个数的道具
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="itemID">道具ID</param>
        /// <param name="count">个数</param>
        public void GiveItem(ActorPC pc, uint itemID, ushort count)
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
        public void GiveItem(ActorPC pc, uint itemID, ushort count, int rentalMinutes)
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
        public void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified)
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
        public void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified, uint pictID)
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
        public void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified, uint pictID, int rentalMinutes)
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
                client.AddItem(item, true, true, true);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    item.Stack = 1;
                    item.Identified = true;//免鉴定
                    client.AddItem(item, true, true, true);
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
        public int ItemFreeSlotCount(ActorPC pc)
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
        public int GetItemCount(ActorPC pc, ContainerType container)
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
        public SagaDB.Item.Item TakeItem(ActorPC pc, uint itemID, ushort count)
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
        public void TakeItemBySlot(ActorPC pc, uint slotID, ushort count)
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
        public bool TakeEquipment(ActorPC pc, EnumEquipSlot eqSlot)
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
        public void RemoveEquipment(ActorPC pc, EnumEquipSlot eqSlot)
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
        public void ActivateMarionette(ActorPC pc, uint id)
        {
            MapClient client = GetMapClient(pc);
            client.MarionetteActivate(id, false, true);
        }

        /// <summary>
        /// 让某特定对象（玩家，怪物）的HP/MP/SP全满
        /// </summary>
        /// <param name="actor">对象</param>
        public void Heal(Actor actor)
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
        public bool CheckMapInstance(ActorPC creator, uint mapID)
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
        public int CreateMapInstance(int template, uint exitMap, byte exitX, byte exitY)
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
        public int CreateMapInstance(int template, uint exitMap, byte exitX, byte exitY, bool autoDispose)
        {
            return (int)MapManager.Instance.CreateMapInstance(this.currentPC, (uint)template, exitMap, exitX, exitY, autoDispose);
        }
        /// <summary>
        /// 创建一个地图副本
        /// </summary>
        /// <param name="template">模板地图</param>
        /// <param name="exitMap">玩家退出时返回的地图</param>
        /// <param name="exitX">玩家退出时返回的X坐标</param>
        /// <param name="exitY">玩家退出时返回的Y坐标</param>
        /// <param name="autoDispose">是否在玩家登出的时候自动删除</param>
        /// <param name="ResurrectionLimit">复活次数</param>
        /// <param name="returnori">是否返回原地图ID而不是副本地图的ID（影响小地图和NPC可见）</param>
        /// <returns></returns>
        public int CreateMapInstance(int template, uint exitMap, byte exitX, byte exitY, bool autoDispose, uint ResurrectionLimit, bool returnori)
        {
            return (int)MapManager.Instance.CreateMapInstance(this.currentPC, (uint)template, exitMap, exitX, exitY, autoDispose, ResurrectionLimit, returnori);
        }
        /// <summary>
        /// 删除一个地图副本
        /// </summary>
        /// <param name="id">地图ID</param>
        /// <returns></returns>
        public bool DeleteMapInstance(int id)
        {
            return MapManager.Instance.DeleteMapInstance((uint)id);
        }

        /// <summary>
        /// 为某个地图添加刷怪点
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="spawnFile">刷怪文件</param>
        public void LoadSpawnFile(int mapID, string spawnFile)
        {
            Mob.MobSpawnManager.Instance.LoadOne(spawnFile, (uint)mapID);
        }

        /// <summary>
        /// 改变某个玩家的职业
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="job">职业</param>
        public void ChangePlayerJob(ActorPC pc, PC_JOB job)
        {
            if (pc.Status.Additions.ContainsKey("自由射击"))
            {
                SkillHandler.RemoveAddition(pc, "自由射击");
            }
            pc.TInt["斥候远程模式"] = 0;
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                SagaDB.Item.Item rightWeapon = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                GetMapClient(pc).ItemMoveSub(rightWeapon, ContainerType.BODY, rightWeapon.Stack);
                GetMapClient(pc).SendItemInfo(rightWeapon);
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item leftWeapon = pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                GetMapClient(pc).ItemMoveSub(leftWeapon, ContainerType.BODY, leftWeapon.Stack);
                GetMapClient(pc).SendItemInfo(leftWeapon);
            }
            MapServer.charDB.SaveSkill(pc);
            pc.Skills.Clear();
            pc.Skills2.Clear();
            pc.Skills2_1.Clear();
            pc.Skills2_2.Clear();
            pc.SkillsReserve.Clear();
            pc.SkillsEquip.Clear();
            //pc.Skills3.Clear();
            pc.JobLevel3 = 1;
            pc.SkillPoint = 0;
            pc.SkillPoint2T = 0;
            pc.SkillPoint2X = 0;
            pc.SkillPoint3 = 0;
            pc.SkillsReserve.Clear();
            pc.Job = job;
            pc.JEXP = 1;
            
            MapServer.charDB.GetSkill(pc);
            MapClient.FromActorPC(pc).CaculateItemSkills();
            SkillHandler.Instance.CastPassiveSkills(pc);
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
        public void LearnSkill(ActorPC pc, uint skillID)
        {
            if (!SkillFactory.Instance.CheckSkillList(pc, SkillFactory.SkillPaga.p1).ContainsKey(skillID))
                return;
            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
            byte jobLV = SkillFactory.Instance.CheckSkillList(pc, SkillFactory.SkillPaga.p1)[skillID];
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
        public SagaDB.Item.Item EnhanceItemList(ActorPC pc, List<SagaDB.Item.Item> items)
        {
            List<uint> slotID = new List<uint>();
            foreach (SagaDB.Item.Item i in items)
            {
                slotID.Add(i.Slot);
            }
            return EnhanceItemList(pc, slotID);
        }
        /// <summary>
        /// 向玩家打开潜在强化选择窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>玩家选择的道具</returns>
        public SagaDB.Item.Item EnhanceItemList(ActorPC pc, List<uint> slots)
        {
            MapClient client = GetMapClient(pc);

            client.InventorySlot = uint.MaxValue;
            client.SendMasterEnhanceList(slots);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (client.InventorySlot == uint.MaxValue)
            {
                System.Threading.Thread.Sleep(500);
            }
            if (blocked)
                ClientManager.EnterCriticalArea();
            if (client.InventorySlot == 0) return null;

            SagaDB.Item.Item item = client.Character.Inventory.GetItem(client.InventorySlot);
            client.InventorySlot = 0;
            return item;
        }


        /// <summary>
        /// 向玩家打开NPC交易窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>玩家交易的道具</returns>
        public List<SagaDB.Item.Item> NPCTrade(ActorPC pc, string name = "")
        {
            MapClient client = GetMapClient(pc);
            client.npcTrade = true;
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
        public bool CheckInventory(ActorPC pc, uint itemID, int amount)
        {
            SagaDB.Item.Item.ItemData item = ItemFactory.Instance.Items[itemID];
            int volume = (int)item.volume * amount;
            int weight = (int)item.weight * amount;
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
        public void SkillPointBonus(ActorPC pc, byte val)
        {
            pc.SkillPoint += val;
            GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// 2-1スキルポイントボーナス
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="val">值</param>
        public void SkillPointBonus2T(ActorPC pc, byte val)
        {
            pc.SkillPoint2T += val;
            GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// 2-2スキルポイントボーナス
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="val">值</param>
        public void SkillPointBonus2X(ActorPC pc, byte val)
        {
            pc.SkillPoint2X += val;
            GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// 复活某个玩家
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="level">复活术等级</param>
        public void Revive(ActorPC pc, byte level)
        {
            if (level > 0)
                GetMapClient(pc).SendRevive(level);
        }

        /// <summary>
        /// 取得当前服务器的在线玩家
        /// </summary>
        public List<ActorPC> OnlinePlayers
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
        public SagaDB.Item.Item CreateItem(uint itemID, ushort count, bool identified)
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
        public List<SagaDB.Item.Item> CreateItemList(params uint[] itemID)
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
        public void Navigate(ActorPC pc, byte x, byte y)
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
        public void NavigateCancel(ActorPC pc)
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
                int fexp = 0;
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
                                {
                                    byte c = 0;
                                    SagaDB.Item.Item pItem = ItemFactory.Instance.GetItem(j.ID);
                                    if (pItem.BaseData.itemType == ItemType.FOOD)//料理
                                    {
                                        byte lv = info.SkillLv;
                                        byte level = 1;
                                        if (pc.CInt["料理EXP"] > 5000)
                                            level = 2;
                                        if (pc.CInt["料理EXP"] > 30000)
                                            level = 3;
                                        if (pc.CInt["料理EXP"] > 150000)
                                            level = 4;
                                        if (pc.CInt["料理EXP"] > 500000)
                                            level = 5;
                                        int rate = Global.Random.Next(0, 100);
                                        c = GetfoodlevelBouns((byte)(lv - level));
                                        if (j.Exp > 0)
                                        {
                                            pc.CInt["料理EXP"] += j.Exp;
                                            fexp += j.Exp;
                                        }
                                        int doubleRate = 1;
                                        switch(level)
                                        {
                                            case 2:
                                                doubleRate = 5;
                                                break;
                                            case 3:
                                                doubleRate = 8;
                                                break;
                                            case 4:
                                                doubleRate = 12;
                                                break;
                                            case 5:
                                                doubleRate = 15;
                                                break;
                                        }
                                        
                                        if (pItem.ItemID >= 111000000 && pItem.ItemID <= 111111000)
                                            CalcFoodBounds(level, pItem);
                                        pItem.Name += pItem.BaseData.name;
                                        pItem.Stack = 1;
                                        MapClient.FromActorPC(pc).SendSystemMessage("烹饪出了「" + pItem.Name + "」！");
                                        pItem.Name += " By." + pc.Name;
                                        MapClient.FromActorPC(pc).AddItem(pItem, false, true, true);
                                        if (Global.Random.Next(0, 100) < doubleRate)
                                        {
                                            pItem = ItemFactory.Instance.GetItem(j.ID);
                                            if (pItem.ItemID >= 111000000 && pItem.ItemID <= 111111000)
                                                CalcFoodBounds(level, pItem);
                                            pItem.Name += pItem.BaseData.name;
                                            pItem.Stack = 1;
                                            MapClient.FromActorPC(pc).SendSystemMessage("哇哦！你额外烹饪出了一份「" + pItem.Name + "」！");
                                            pItem.Name += " By." + pc.Name;
                                            MapClient.FromActorPC(pc).AddItem(pItem, false, true, true);
                                        }
                                    }
                                    else
                                        GiveItem(pc, j.ID, (j.Count));
                                }
                                //大厨：满肉全席制作50次。
                                if (j.ID == 131140200) TitleProccess(pc, 89, 1);
                                //黑暗料理界：做出1000份黑暗料理。
                                if (j.ID == 110123200) TitleProccess(pc, 90, 1);
                            }
                            baseValue = maxVlaue;
                        }
                    }
                    if (fexp > 0)
                    {
                        MapClient.FromActorPC(pc).SendSystemMessage("获得了" + fexp + "点料理经验。");
                        SkillHandler.Instance.ShowEffectByActor(pc, 7136);
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
        void CalcFoodBounds(byte Level, SagaDB.Item.Item item)
        {
            int minBoundRate = 0;
            int maxBoundRate = 0;
            int TerribleRate = 100;//难吃时属性会在30%~80%波动
            int NoramlRate = 0;//普通时属性会在70%~120%波动
            int NiceRate = 0;//好次的时属性会在100%~150%波动
            int DeliciousRate = 0;//超好次时属性会在150%~200%波动
            string TerribleName = "难吃的";
            string NoramlName = "普通的";
            string NiceName = "好次的";
            string DeliciousName = "超好次";
            int rate = Random(0, 100);
            switch (Level)
            {
                case 1:
                    TerribleRate = 50;
                    NoramlRate = 100;
                    NiceRate = 0;
                    DeliciousRate = 0;
                    break;
                case 2:
                    TerribleRate = 30;
                    NoramlRate = 80;
                    NiceRate = 100;
                    DeliciousRate = 0;
                    break;
                case 3:
                    TerribleRate = 20;
                    NoramlRate = 60;
                    NiceRate = 95;
                    DeliciousRate = 100;
                    break;
                case 4:
                    TerribleRate = 5;
                    NoramlRate = 40;
                    NiceRate = 80;
                    DeliciousRate = 100;
                    break;
                case 5:
                    TerribleRate = 0;
                    NoramlRate = 30;
                    NiceRate = 70;
                    DeliciousRate = 100;
                    break;
            }

            //计算波动限界
            if (rate < TerribleRate)
            {
                item.Name += TerribleName;
                minBoundRate = 30;
                maxBoundRate = 80;
            }
            else if (rate < NoramlRate)
            {
                item.Name += NoramlName;
                minBoundRate = 70;
                maxBoundRate = 120;
            }
            else if (rate < NiceRate)
            {
                item.Name += NiceName;
                minBoundRate = 100;
                maxBoundRate = 150;
            }
            else if (rate <= DeliciousRate)
            {
                item.Name += DeliciousName;
                minBoundRate = 150;
                maxBoundRate = 200;
            }
            item.Str = (short)((item.BaseData.str * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.str);
            item.Mag = (short)((item.BaseData.mag * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.mag);
            item.Agi = (short)((item.BaseData.agi * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.agi);
            item.Dex = (short)((item.BaseData.dex * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.dex);
            item.Vit = (short)((item.BaseData.vit * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.vit);
            item.Int = (short)((item.BaseData.intel * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.intel);
            item.HP = (short)((item.BaseData.hp * Random(minBoundRate, maxBoundRate) / 100f) - item.BaseData.hp);
            //TODO:继续完成其他属性
        }
        byte GetfoodlevelBouns(byte level)
        {
            byte c = 0;
            int rate = Global.Random.Next(0, 100);
            switch (level)
            {
                case 1:
                    if (rate < 40)
                        c = 0;
                    else if (rate < 70)
                        c = 1;
                    else if (rate < 90)
                        c = 2;
                    else c = 3;
                    break;
                case 2:
                    if (rate < 25)
                        c = 0;
                    else if (rate < 60)
                        c = 1;
                    else if (rate < 80)
                        c = 2;
                    else if (rate < 95)
                        c = 3;
                    else c = 4;
                    break;
                case 3:
                    if (rate < 10)
                        c = 0;
                    else if (rate < 40)
                        c = 1;
                    else if (rate < 70)
                        c = 2;
                    else if (rate < 90)
                        c = 3;
                    else c = 4;
                    break;
                case 4:
                    if (rate < 0)
                        c = 0;
                    else if (rate < 30)
                        c = 1;
                    else if (rate < 60)
                        c = 2;
                    else if (rate < 80)
                        c = 3;
                    else c = 4;
                    break;
                default:
                    if (rate < 60)
                        c = 0;
                    else if (rate < 90)
                        c = 1;
                    else
                        c = 2;
                    break;
            }
            return c;
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
        public void BankDeposit(ActorPC pc)
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
        public void BankWithdraw(ActorPC pc)
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
        public string InputBox(ActorPC pc, string title, InputType type)
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
        public void OpenWareHouse(ActorPC pc, WarehousePlace place)
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
        public void GiveRandomTreasure(ActorPC pc, string treasureGroup)
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
        public SagaDB.Item.Item GetRandomTreasure(string treasureGroup)
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
        public void OpenTreasureBox(ActorPC pc)
        {
            OpenTreasureBox(pc, true, true, true);
        }

        /// <summary>
        /// 打开玩家身上的宝物箱
        /// </summary>
        /// <param name="pc">玩家</param>
        public void OpenTreasureBox(ActorPC pc, bool timber, bool treasure, bool container)
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
                client.SendSystemMessage(LocalManager.Instance.Strings.ITEM_TREASURE_NO_NEED);
            }
        }

        /// <summary>
        /// 显示屏幕特效
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="type">特效类型，Out=渐出，In=渐进</param>
        /// <param name="effect">特效效果，黑或者白</param>
        public void Fade(ActorPC pc, FadeType type, FadeEffect effect)
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
        public bool JobSwitch(ActorPC pc)
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
        public void ResetSkill(ActorPC pc, byte job)
        {
            GetMapClient(pc).ResetSkill(job);
            GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// 重置属性点
        /// </summary>
        /// <param name="pc">玩家</param>
        public void ResetStatusPoint(ActorPC pc)
        {
            GetMapClient(pc).ResetStatusPoint();
        }

        /// <summary>
        /// 打开指定ID的揭示版
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="bbsID">揭示版ID</param>
        /// <param name="cost">发贴费用</param>
        public void OpenBBS(ActorPC pc, uint bbsID, uint cost)
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
        public bool CreateRing(ActorPC pc, string name)
        {
            MapClient client = MapClient.FromActorPC(pc);
            Ring ring = RingManager.Instance.CreateRing(pc, name);
            return (ring != null);
        }

        /// <summary>
        /// 发送飞空庭制作材料
        /// </summary>
        /// <param name="parts"></param>
        public void SendFGardenCreateMaterial(ActorPC pc, BitMask<FGardenParts> parts)
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
        public void EnterFGarden(ActorPC pc, ActorEvent rope)
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
        public void ExitFGarden(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            pc.Speed = 350;
            if (map.OriID == 70000000)
            {
                Warp(pc, map.ClientExitMap, map.ClientExitX, map.ClientExitY);
            }
        }

        /// <summary>
        /// 进入飞空庭房间
        /// </summary>
        /// <param name="pc">玩家</param>
        public void EnterFGRoom(ActorPC pc)
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
        public void ExitFGRoom(ActorPC pc)
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
        public void ExitFFRoom(ActorPC pc)
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
        public ActorPC GetFGardenOwner(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            if (map.OriID == 70000000 || map.OriID == 75000000)
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
        public ActorEvent GetRopeActor(ActorPC pc)
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
        public void ReturnRope(ActorPC pc)
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
        public void ShowUI(ActorPC pc, UIType type)
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
        public void NPCMotion(ActorPC pc, uint npcID, ushort motion)
        {
            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
            p.ActorID = npcID;
            p.Motion = (MotionType)motion;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 让NPC做动作
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPC ID</param>
        /// <param name="motion">动作</param>
        /// <param name="loop">是否重复</param>
        /// <param name="motionspeed">动作速度，09以下为慢速，0为默认，10为1倍速，50为5倍速</param>
        public void NPCMotion(ActorPC pc, uint npcID, ushort motion, bool loop, uint motionspeed)
        {
            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
            p.ActorID = npcID;
            p.Motion = (MotionType)motion;
            if (loop)
                p.Loop = 1;
            p.motionspeed = motionspeed;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 打开商城NCShop
        /// </summary>
        /// <param name="pc"></param>
        public void NCShopOpen(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_NCSHOP_CATEGORY p = new SagaMap.Packets.Server.SSMG_NCSHOP_CATEGORY();
            p.CurrentPoint = pc.CP;
            p.type = 6;
            p.Categories = NCShopFactory.Instance.Items;
            client.netIO.SendPacket(p);
            client.vshopClosed = false;
        }

        /// <summary>
        /// 打开商城ECOShop
        /// </summary>
        /// <param name="pc"></param>
        public void VShopOpen(ActorPC pc)
        {
            MapClient client = GetMapClient(pc);
            Packets.Server.SSMG_GSHOP_CATEGORY p = new SagaMap.Packets.Server.SSMG_GSHOP_CATEGORY();
            p.CurrentPoint = (uint)pc.Gold;
            p.type = 0;
            //p.Categories = GShopFactory.Instance.Items;
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
        /// 创建遗迹
        /// </summary>
        /// <param name="pc">创建者</param>
        /// <param name="id">遗迹ID</param>
        /// <param name="exitMap">退出MapID</param>
        /// <param name="exitX">退出X</param>
        /// <param name="exitY">退出Y</param>
        /// <returns>遗迹的ID</returns>
        public uint CreateDungeon(ActorPC pc, uint id, uint exitMap, byte exitX, byte exitY)
        {
            Dungeon.Dungeon dungeon;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            dungeon = Dungeon.DungeonFactory.Instance.CreateDungeon(id, pc, exitMap, exitX, exitY);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return dungeon.DungeonID;
        }

        /// <summary>
        /// 传送玩家到遗迹
        /// </summary>
        /// <param name="pc">玩家</param>
        public void WarpToDungeon(ActorPC pc)
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
        public List<uint> GetPossibleDungeons(ActorPC pc)
        {
            List<uint> list = new List<uint>();
            if (pc.DungeonID != 0)
                list.Add(pc.DungeonID);
            if (pc.Party != null)
            {
                foreach (ActorPC i in pc.Party.Members.Values)
                {
                    if (i == pc)
                        continue;
                    if (i.DungeonID != 0)
                        list.Add(i.DungeonID);
                }
            }
            return list;
        }

        /// <summary>
        /// 隐藏NPC
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        public void NPCHide(ActorPC pc, uint npcID)
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
        public void NPCShow(ActorPC pc, uint npcID)
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
        public void TranceMob(ActorPC pc, uint MobID)
        {
            SkillHandler.Instance.TranceMob(pc, MobID);
        }

        /// <summary>
        /// 显示影院电影播放表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="mapID">影院地图ID</param>
        public void ShowTheaterSchedule(ActorPC pc, uint mapID)
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
        public Movie GetNextMovie(uint mapID)
        {
            return TheaterFactory.Instance.GetNextMovie(mapID);
        }

        /// <summary>
        /// 得到正在放映的电影
        /// </summary>
        /// <param name="mapID">影院MapID</param>
        /// <returns>电影</returns>
        public Movie GetCurrentMovie(uint mapID)
        {
            return TheaterFactory.Instance.GetCurrentMovie(mapID);
        }

        /// <summary>
        /// 盖印章
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="genre">印章系列</param>
        /// <param name="slot">印章槽</param>
        public bool UseStamp(ActorPC pc, StampGenre genre, StampSlot slot)
        {
            return false;
            if (!pc.Stamp[genre].Test(slot))
            {
                pc.Stamp[genre].SetValue(slot, true);
                Packets.Server.SSMG_STAMP_USE p = new SagaMap.Packets.Server.SSMG_STAMP_USE();
                p.Page = 0;
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
        public bool CheckStampGenre(ActorPC pc, StampGenre genre)
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
        public void ClearStampGenre(ActorPC pc, StampGenre genre)
        {
            pc.Stamp[genre].Value = 0;
            MapClient.FromActorPC(pc).SendStamp();
        }

        /// <summary>
        /// 取得指定等级所需经验
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="type">等级类别</param>
        /// <returns>所需经验</returns>
        public ulong GetExpForLevel(uint level, LevelType type)
        {
            return ExperienceManager.Instance.GetExpForLevel(level, type);
        }

        /// <summary>
        /// 更改玩家高度
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="size">高度</param>
        public void ChangePlayerSize(ActorPC pc, uint size)
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
        public List<uint> gethairlist(ActorPC pc)//返回发型券
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
        public List<HairStyleList> gethairstyles(ActorPC pc, uint id)//返回发型详细信息
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
        /// 强化效果对装备基础值的提升效果
        /// </summary>
        /// <param name="equip">装备</param>
        /// <param name="refinenum">本次强化若成功则提升到的目标强化次数</param>
        /// <returns></returns>
        public uint EquipEnhanceBasicPreview(SagaDB.Item.Item equip, ushort refinenum)
        {
            uint[] ups = new uint[25] { 1, 1, 1, 1, 2, 1, 1, 1, 1, 3, 1, 1, 1, 1, 4, 1, 1, 1, 1, 5, 1, 1, 1, 1, 6 };
            if (refinenum > 25)
                refinenum = 25;
            if (equip.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)//weapon
                return ups[refinenum - 1];
            else
                return 1;
        }
        /// <summary>
        /// 强化成功率（%）即100=100%=1=必定成功
        /// </summary>
        /// <param name="equip"></param>
        /// <returns></returns>
        public uint EquipEnhanceRate(SagaDB.Item.Item equip)
        {
            uint[] rates = new uint[25] { 100, 90, 80, 70, 60, 50, 50, 50, 50, 35, 35, 35, 35, 35, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return rates[equip.Refine];
        }
        /// <summary>
        /// 强化判定成功后修改装备数值(不包括强化材料删除过程)
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="equip">装备</param>
        /// <param name="btype">物理魔法强化类型</param>
        /// <param name="stype">素质点强化类型</param> 
        public void ApplyEquipEnhanceSuccess(ActorPC pc, SagaDB.Item.Item equip, BasicEnhanceType btype, StatusEnhanceType stype)
        {
            equip.Refine++;
            switch (stype)
            {
                case StatusEnhanceType.STR:
                    equip.Str++;
                    break;
                case StatusEnhanceType.DEX:
                    equip.Dex++;
                    break;
                case StatusEnhanceType.INT:
                    equip.Int++;
                    break;
                case StatusEnhanceType.VIT:
                    equip.Vit++;
                    break;
                case StatusEnhanceType.AGI:
                    equip.Agi++;
                    break;
                case StatusEnhanceType.MAG:
                    equip.Mag++;
                    break;
            }
            short enhance_up = 1;
            switch (equip.EquipSlot[0])
            {
                case EnumEquipSlot.RIGHT_HAND: //weapon
                    if (equip.Refine == 5)
                    {
                        enhance_up = 2;
                    }
                    else if (equip.Refine == 10)
                    {
                        enhance_up = 3;
                    }
                    else if (equip.Refine == 15)
                    {
                        enhance_up = 4;
                    }
                    else if (equip.Refine == 20)
                    {
                        enhance_up = 5;
                    }
                    else if (equip.Refine == 25)
                    {
                        enhance_up = 6;
                    }
                    if (btype == BasicEnhanceType.Physics)
                    {
                        equip.Atk1 = (short)(equip.Atk1 + enhance_up);
                        equip.Atk2 = (short)(equip.Atk2 + enhance_up);
                        equip.Atk3 = (short)(equip.Atk3 + enhance_up);
                    }
                    else
                    {
                        equip.MAtk = (short)(equip.MAtk + enhance_up);
                    }
                    break;
                case EnumEquipSlot.UPPER_BODY: //cloth
                case EnumEquipSlot.CHEST_ACCE: //necklace
                    if (btype == BasicEnhanceType.Physics)
                    {
                        equip.Def = (short)(equip.Def + enhance_up);
                    }
                    else
                    {
                        equip.MDef = (short)(equip.MDef + enhance_up);
                    }
                    break;
            }
            MapClient.FromActorPC(pc).SendItemInfo(equip);
            PC.StatusFactory.Instance.CalcStatus(pc);
            MapClient.FromActorPC(pc).SendPlayerInfo();
        }
        /// <summary>
        /// 强化判定失败后修改装备数值(不包括强化材料删除过程)
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="equip">装备</param>
        /// <param name="btype">物理魔法强化类型</param>
        /// <param name="stype">素质点强化类型</param> 
        public void ApplyEquipEnhanceFail(ActorPC pc, SagaDB.Item.Item equip, BasicEnhanceType btype, StatusEnhanceType stype)
        {
            int refine_down = 0;
            if (equip.Refine < 4)
            {
                return;
            }
            else if (equip.Refine >= 4 && equip.Refine < 8)
            {
                refine_down = 1;
            }
            else if (equip.Refine >= 8 && equip.Refine < 12)
            {
                refine_down = 2;
            }
            else if (equip.Refine >= 12 && equip.Refine < 16)
            {
                refine_down = 3;
            }
            else if (equip.Refine >= 16 && equip.Refine < 20)
            {
                refine_down = 4;
            }
            else if (equip.Refine >= 20 && equip.Refine < 24)
            {
                refine_down = 5;
            }
            else if (equip.Refine >= 24)
            {
                refine_down = 6;
            }
            for (int i = 0; i < refine_down; i++)
            {
                List<StatusEnhanceType> sbonuslist = new List<StatusEnhanceType>();
                List<BasicEnhanceType> bbonuslist = new List<BasicEnhanceType>();
                uint basicenhanceup = EquipEnhanceBasicPreview(equip, equip.Refine);
                if (equip.Str > 0)
                    sbonuslist.Add(StatusEnhanceType.STR);
                if (equip.Dex > 0)
                    sbonuslist.Add(StatusEnhanceType.DEX);
                if (equip.Int > 0)
                    sbonuslist.Add(StatusEnhanceType.INT);
                if (equip.Vit > 0)
                    sbonuslist.Add(StatusEnhanceType.VIT);
                if (equip.Agi > 0)
                    sbonuslist.Add(StatusEnhanceType.AGI);
                if (equip.Mag > 0)
                    sbonuslist.Add(StatusEnhanceType.MAG);
                if (equip.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND) //weapon
                {
                    if (equip.Atk1 >= basicenhanceup && equip.Atk2 >= basicenhanceup && equip.Atk3 >= basicenhanceup)
                        bbonuslist.Add(BasicEnhanceType.Physics);
                    if (equip.MAtk >= basicenhanceup)
                        bbonuslist.Add(BasicEnhanceType.Magic);
                }
                else //armor
                {
                    if (equip.Def >= basicenhanceup)
                        bbonuslist.Add(BasicEnhanceType.Physics);
                    if (equip.MDef >= basicenhanceup)
                        bbonuslist.Add(BasicEnhanceType.Magic);
                }
                if (sbonuslist.Count >= 1 && bbonuslist.Count >= 1)
                {
                    StatusEnhanceType status_down = sbonuslist[Global.Random.Next(sbonuslist.Count - 1)];
                    BasicEnhanceType basic_down = bbonuslist[Global.Random.Next(bbonuslist.Count - 1)];
                    switch (status_down)
                    {
                        case StatusEnhanceType.STR:
                            equip.Str--;
                            break;
                        case StatusEnhanceType.DEX:
                            equip.Dex--;
                            break;
                        case StatusEnhanceType.INT:
                            equip.Int--;
                            break;
                        case StatusEnhanceType.VIT:
                            equip.Vit--;
                            break;
                        case StatusEnhanceType.AGI:
                            equip.Agi--;
                            break;
                        case StatusEnhanceType.MAG:
                            equip.Mag--;
                            break;
                    }
                    switch (basic_down)
                    {
                        case BasicEnhanceType.Physics:
                            if (equip.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND) //weapon
                            {
                                equip.Atk1 = (short)(equip.Atk1 - basicenhanceup);
                                equip.Atk2 = (short)(equip.Atk2 - basicenhanceup);
                                equip.Atk3 = (short)(equip.Atk3 - basicenhanceup);
                            }
                            else
                            {
                                equip.Def = (short)(equip.Def - basicenhanceup);
                            }
                            break;
                        case BasicEnhanceType.Magic:
                            if (equip.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND) //weapon
                            {
                                equip.MAtk = (short)(equip.MAtk - basicenhanceup);
                            }
                            else
                            {
                                equip.MDef = (short)(equip.MDef - basicenhanceup);
                            }
                            break;
                    }
                    equip.Refine--;
                }
            }
            Skill.SkillHandler.Instance.EquipWorn(pc, equip);
            MapClient.FromActorPC(pc).SendItemInfo(equip);
            PC.StatusFactory.Instance.CalcStatus(pc);
            MapClient.FromActorPC(pc).SendPlayerInfo();
        }
        /// <summary>
        /// 强化装备
        /// </summary>
        /// <param name="pc">玩家</param>
        public bool ItemEnhance(ActorPC pc)
        {
            return ItemEnhance(pc, 200000);
        }
        /// <summary>
        /// 强化装备
        /// </summary>
        /// <param name="pc">玩家</param>
        public bool ItemEnhance(ActorPC pc, uint price)
        {

            /*var res = from item in pc.Inventory.GetContainer(ContainerType.BODY)
                      where ((item.IsArmor && ((CountItem(pc, 90000044) > 0 || CountItem(pc, 90000045) > 0 || CountItem(pc, 90000046) > 0 || CountItem(pc, 90000043) > 0)))
                      || (item.IsWeapon && (CountItem(pc, 90000044) > 0 || CountItem(pc, 90000045) > 0 || CountItem(pc, 90000046) > 0))
                      || (item.BaseData.itemType == ItemType.SHIELD && (CountItem(pc, 90000044) > 0 || CountItem(pc, 90000045) > 0))
                      || (item.BaseData.itemType == ItemType.ACCESORY_NECK && (CountItem(pc, 90000044) > 0))) && item.Refine < 10
                      select item;*/
            var res = from item in pc.Inventory.GetContainer(ContainerType.BODY)
                      where ((item.IsArmor || item.IsWeapon || item.BaseData.itemType == ItemType.SHIELD || item.BaseData.itemType == ItemType.ACCESORY_NECK
                      || item.ItemID == 90000044 || item.ItemID == 90000045 || item.ItemID == 90000046) && item.Refine < 10)
                      select item;
            List<SagaDB.Item.Item> items = res.ToList();
            if (items.Count > 0)
            {
                Packets.Server.SSMG_ITEM_ENHANCE_LIST p = new SagaMap.Packets.Server.SSMG_ITEM_ENHANCE_LIST();
                p.Items = items;
                p.price = price;
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
        /// 
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="confirm"></param>
        public void ItemFusion(ActorPC pc, string confirm)
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
        public bool CheckMapFlag(ActorPC pc, SagaDB.Map.MapFlags flag)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            return map.Info.Flag.Test(flag);
        }

        /// <summary>
        /// 改变NPC的外观///失效中
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID</param>
        /// <param name="mobID">怪物ID</param>
        public void NPCChangeView(ActorPC pc, uint npcID, uint mobID)
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
        public bool ODWarCanApply(uint mapID)
        {
            return ODWarManager.Instance.CanApply(mapID);
        }

        /// <summary>
        /// 复活象征
        /// </summary>
        /// <param name="mapID">战场MapID</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public SymbolReviveResult ODWarReviveSymbol(uint mapID, int number)
        {
            return ODWarManager.Instance.ReviveSymbol(mapID, number);
        }

        /// <summary>
        /// 是否是防御战状态
        /// </summary>
        /// <param name="mapID">战场MapID</param>
        /// <returns></returns>
        public bool ODWarIsDefence(uint mapID)
        {
            return ODWarManager.Instance.IsDefence(mapID);
        }

        /// <summary>
        /// 给武器打洞
        /// </summary>
        /// <param name="pc"></param>
        public void ItemAddSlot(ActorPC pc)
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
                p.Result = SagaMap.Packets.Server.SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM;
                MapClient.FromActorPC(pc).netIO.SendPacket(p);
            }
        }

        /// <summary>
        /// 卡片组合
        /// </summary>
        /// <param name="pc"></param>
        public void IrisCardAssemble(ActorPC pc)
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
        public bool CheckMapFlag(uint mapID, MapFlags flag)
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
        public Timer AddTimer(string name, int period, ActorPC pc, bool autoDispose = false)
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
        public Timer AddTimer(string name, int period, int dueTime, ActorPC pc, bool needScript)
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
        public Timer AddTimer(string name, int period, int dueTime, ActorPC pc, bool needScript, bool autoDispose = false)
        {
            Timer timer = new Timer(name, period, dueTime);
            timer.AttachedPC = pc;
            timer.NeedScript = needScript;
            string timerName = "";
            if (pc != null)
            {
                timerName = string.Format("{0}:{1}({2})", name, pc.Name, pc.CharID);
            }
            else
                timerName = name + DateTime.Today.ToString();
            if (ScriptManager.Instance.Timers.ContainsKey(timerName))
            {
                ScriptManager.Instance.Timers[timerName].Deactivate();
                ScriptManager.Instance.Timers[timerName] = null;
                ScriptManager.Instance.Timers[timerName] = timer;
            }
            else
                ScriptManager.Instance.Timers.Add(timerName, timer);
            if (autoDispose && pc != null)
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
        public void DeleteTimer(string name, ActorPC pc)
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
        public Timer GetTimer(string name, ActorPC pc)
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
        public void ShowPicture(ActorPC pc, string path, string title)
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
        public void FGardenChangeWeather(ActorPC pc, byte weather)
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
        public void FGardenChangeSky(ActorPC pc, byte sky)
        {
            Packets.Server.SSMG_FG_CHANGE_SKY p = new SagaMap.Packets.Server.SSMG_FG_CHANGE_SKY();
            p.Sky = sky;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
        }

        /// <summary>
        /// 发送全服公告
        /// </summary>
        /// <param name="text">文字</param>
        public void Announce(string text)
        {
            MapClientManager.Instance.Announce(text);
        }

        /// <summary>
        /// 给指定玩家发公告
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="text">文字</param>
        public void Announce(ActorPC pc, string text)
        {
            MapClient.FromActorPC(pc).SendAnnounce(text);
        }

        /// <summary>
        /// 向某地图发公告
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <param name="text">文字</param>
        public void Announce(uint mapID, string text)
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
        public ActorMob SpawnMob(uint mapID, byte x, byte y, uint mobID, MobCallback Event, byte Callbacktype)
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
        public List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count)
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
        public List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count, MobCallback Event)
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
        public List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count, MobCallback Event, byte Callbacktype)
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
        public void PetRecover(ActorPC pc, int amount)
        {
            var it =
                from c in pc.Inventory.GetContainer(ContainerType.BODY)
                where (c.BaseData.itemType == ItemType.PET || c.BaseData.itemType == ItemType.PET_NEKOMATA || c.BaseData.itemType == ItemType.RIDE_PET || c.BaseData.itemType == ItemType.PARTNER || c.BaseData.itemType == ItemType.RIDE_PARTNER) &&
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
                    if (item.BaseData.itemType == ItemType.PET || item.BaseData.itemType == ItemType.PET_NEKOMATA || item.BaseData.itemType == ItemType.RIDE_PET || item.BaseData.itemType == ItemType.PARTNER || item.BaseData.itemType == ItemType.RIDE_PARTNER)
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
        public void DEMCL(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient client = MapClient.FromActorPC(pc);
            bool dominion = pc.InDominionWorld;
            Packets.Server.SSMG_DEM_COST_LIMIT p = new SagaMap.Packets.Server.SSMG_DEM_COST_LIMIT();
            if (dominion)
            {
                p.CurrentEP = pc.DominionEPUsed;
                p.EPRequired = (short)(ExperienceManager.Instance.GetEPRequired(pc) - pc.DominionEPUsed);
            }
            else
            {
                p.CurrentEP = pc.EPUsed;
                p.EPRequired = (short)(ExperienceManager.Instance.GetEPRequired(pc) - pc.EPUsed);
            }
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
        public void DEMParts(ActorPC pc)
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
        public void DEMIC(ActorPC pc)
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
        public void NPCMove(ActorPC pc, uint npcID, short x, short y, short dir, MoveType type)
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
        /// 在指定位置显示一个没有名字的，不可以选中的临时NPC
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="npcID">NPCID（不知道有什么用）</param>
        /// <param name="pictID">外形ID</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="dir">方向</param>
        /// <param name="speed">移动速度</param>
        /// <param name="type">设1就好</param>
        /// <param name="motion">初始动作</param>
        /// <param name="motionspeed">动作的运行速度（10就是100%的正常速度）</param>
        public void CreateNpcPict(ActorPC pc, uint npcID, uint pictID, byte x, byte y, byte dir, ushort speed, byte type, ushort motion, ushort motionspeed)
        {
            Packets.Server.SSMG_NPC_SHOWPICT_LOCATION p = new Packets.Server.SSMG_NPC_SHOWPICT_LOCATION();
            p.NPCID = npcID;
            p.X = x;
            p.Y = y;
            p.Dir = dir;
            MapClient.FromActorPC(pc).netIO.SendPacket(p);

            Packets.Server.SSMG_NPC_MOVE p2 = new Packets.Server.SSMG_NPC_MOVE();
            p2.NPCID = npcID;
            p2.X = x;
            p2.Y = y;
            p2.Speed = speed;
            p2.Dir = dir;
            p2.ShowType = 18;
            p2.Motion = motion;
            p2.MotionSpeed = motionspeed;
            p2.Type = type;
            MapClient.FromActorPC(pc).netIO.SendPacket(p2);

            Packets.Server.SSMG_NPC_SHOWPICT_VIEW p3 = new Packets.Server.SSMG_NPC_SHOWPICT_VIEW();
            p3.NPCID = npcID;
            p3.PictID = pictID;
            MapClient.FromActorPC(pc).netIO.SendPacket(p3);
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
        /// <param name="ShowType">表情速度</param>
        public void NPCMove(ActorPC pc, uint npcID, byte x, byte y, ushort speed, byte type, ushort motion, ushort motionSpeed)
        {
            NPCMove(pc, npcID, x, y, speed, 0, type, motion, motionSpeed, 0);
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
        public void NPCMove(ActorPC pc, uint npcID, byte x, byte y, ushort speed, byte dir, byte type, ushort motion, ushort motionSpeed, ushort ShowType)
        {
            Packets.Server.SSMG_NPC_MOVE p1 = new SagaMap.Packets.Server.SSMG_NPC_MOVE();
            p1.NPCID = npcID;
            p1.X = x;
            p1.Y = y;
            p1.Speed = speed;
            p1.Type = type;
            p1.Dir = dir;
            p1.ShowType = ShowType;
            p1.Motion = motion;
            p1.MotionSpeed = motionSpeed;
            MapClient.FromActorPC(pc).netIO.SendPacket(p1);
        }

        /// <summary>
        /// 打开芯片购买窗口
        /// </summary>
        /// <param name="pc">玩家</param>
        public void DEMChipShop(ActorPC pc)
        {
            MapClient client = MapClient.FromActorPC(pc);
            Packets.Server.SSMG_DEM_CHIP_SHOP_CATEGORY p = new SagaMap.Packets.Server.SSMG_DEM_CHIP_SHOP_CATEGORY();
            if (pc.InDominionWorld)
                p.Categories = ChipShopFactory.Instance.GetCategoryFromLv(pc.DominionLevel);
            else
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
        public void ShowPortal(ActorPC pc, byte x, byte y, uint eventID)
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
        /// 出现传送门
        /// </summary>
        /// <param name="pc">玩家，用于确定地图</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="eventID">传送门触发的EventID</param>
        ///  <param name="effectID">传送门的特效</param>
        public void ShowPortal(ActorPC pc, byte x, byte y, uint eventID, uint effectID)
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
                        p1.EffectID = effectID;
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
        public void PetShowReplace(ActorPC pc, uint pictID)
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
        public void ChangePlayerJobTo3(ActorPC pc, PC_JOB job)
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
            GetMapClient(pc).SendCharInfo();
        }


        /// <summary>
        /// 向列表添加一个任务
        /// </summary>
        /// <param name="mapid">地图ID</param>
        /// <param name="dwID">任务ID</param>
        public void DefWarAdd(uint mapid, uint dwID)
        {
            SagaDB.DefWar.DefWar dw = new SagaDB.DefWar.DefWar(dwID);
            DefWarManager.Instance.AddDefWar(mapid, dw);
        }

        /// <summary>
        /// 设置任务状态
        /// </summary>
        /// <param name="mapid">地图ID</param>
        /// <param name="dwid">任务ID</param>
        /// <param name="dwn">任务序列号</param>
        /// <param name="r1">任务结果1</param>
        /// <param name="r2">任务结果2</param>
        public void DefWarSet(uint mapid, uint dwid, byte dwn, byte r1, byte r2)
        {
            DefWarManager.Instance.SetDefWar(mapid, dwid, dwn, r1, r2);
        }

        /// <summary>
        /// 任务结果(不知道啥用)
        /// </summary>
        /// <param name="mapid">地图ID</param>
        /// <param name="r1">攻略结果1:2为夺还,3为攻略</param>
        /// <param name="r2">攻略结果2:0为失败,1为成功,2为大成功,3为完全成功</param>
        /// <param name="exp">经验</param>
        /// <param name="jobexp">职业经验</param>
        /// <param name="cp">CP</param>
        /// <param name="u">不明</param>
        public void DefWarResult(uint mapid, byte r1, byte r2, int exp, int jobexp, int cp, byte u = 0)
        {
            DefWarManager.Instance.DefWarResult(mapid, r1, r2, exp, jobexp, cp, u);
        }

        /// <summary>
        /// 广播单个地图进度
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="rate"></param>
        public void DefWarState(uint mapid, byte rate)
        {
            DefWarManager.Instance.DefWarState(mapid, rate);
        }

        /// <summary>
        /// 广播多个地图结果
        /// </summary>
        /// <param name="list">Dictionary列表,key为地图ID,内容为%进度</param>
        public void DefWarStates(Dictionary<uint, byte> list)
        {
            DefWarManager.Instance.DefWarStates(list);
        }

        /// <summary>
        /// 清除一个地图任务
        /// </summary>
        public void DefWarMapClear(uint mapid)
        {
            DefWarManager.Instance.MapClear(mapid);
        }

        /// <summary>
        /// 清除全部地图任务
        /// </summary>
        public void DefWarAllClear()
        {
            DefWarManager.Instance.AllClear();
        }
    }
}
