using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnChat(Packets.Client.CSMG_CHAT_PUBLIC p)
        {
            if (!AtCommand.Instance.ProcessCommand(this, p.Content))
            {
                if (p.Content.Substring(0, 1) == "!")
                {
                    if (Character.Account.GMLevel > 100)
                        SendSystemMessage("Command error。");
                    return;
                }
                ChatArg arg = new ChatArg();
                arg.content = p.Content;
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, this.Character, true);
            }
        }

        public void OnTakeGift(Packets.Client.CSMG_CHAT_GIFT_TAKE p)
        {
            MapServer.charDB.GetGifts(Character);
            uint GiftID = p.GiftID;
            byte Type = p.type;
            if (Type == 0)
            {
                var gift = from G in Character.Gifts
                           where GiftID == G.MailID
                           select G;
                if (gift == null)
                {
                    SendSystemMessage("unexpected command");
                    return;
                }
                else
                {
                    if (gift.Count() == 0)
                    {
                        SendSystemMessage("unexpected command");
                        return;
                    }
                    SagaDB.BBS.Gift Gift = gift.First();
                    if(Gift.AccountID != Character.Account.AccountID)
                    {
                        SendSystemMessage("unexpected command");
                        return;
                    }
                    if (!MapServer.charDB.DeleteGift(Gift))
                    {
                        SendSystemMessage("unexpected command");
                        return;
                    }
                    foreach (var i in Gift.Items.Keys)
                    {
                        uint ItemID = i;
                        ushort Count = Gift.Items[i];
                        Item item = ItemFactory.Instance.GetItem(ItemID);
                        item.Stack = Count;
                        AddItem(item, true);

                    }
                    Character.Gifts.Remove(Gift);
                }
            }
            else
            {
                var gift = from G in Character.Gifts
                           where GiftID == G.MailID
                           select G;
                if (gift == null)
                {
                    SendSystemMessage("unexpected command");
                    return;
                }
                if (gift.Count() == 0)
                {
                    SendSystemMessage("unexpected command");
                    return;
                }
                SagaDB.BBS.Gift Gift = gift.First();
                if (!MapServer.charDB.DeleteGift(Gift))
                {
                    SendSystemMessage("unexpected command");
                    return;
                }
                Character.Gifts.Remove(Gift);
            }

            Packets.Server.SSMG_GIFT_TAKERECIPT p3 = new Packets.Server.SSMG_GIFT_TAKERECIPT();
            p3.type = Type;
            p3.MailID = GiftID;
            netIO.SendPacket(p3);
            return;
        }

        public void OnChatParty(Packets.Client.CSMG_CHAT_PARTY p)
        {
            if (this.Character != null)
            {
                if (p.Content.Substring(0, 1) == "!")
                    return;
                PartyManager.Instance.PartyChat(this.Character.Party, this.Character, p.Content);
            }
        }
        public void OnExpression(Packets.Client.CSMG_CHAT_EXPRESSION p)
        {
            ChatArg arg = new ChatArg();
            arg.expression = p.Motion;
            if (p.Loop == 0)
                Character.EMotionLoop = false;
            else
                Character.EMotionLoop = true;
            if (p.Motion <= 4)
                Character.EMotion = p.Motion;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);
        }
        public void OnWaitType(Packets.Client.CSMG_CHAT_WAITTYPE p)
        {
            this.Character.WaitType = p.type;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.WAITTYPE, null, this.Character, true);
        }
        public void OnMotion(Packets.Client.CSMG_CHAT_MOTION p)
        {

            //Cancel Cloak
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
                SagaMap.Skill.SkillHandler.RemoveAddition(this.Character, "Cloaking");

            ChatArg arg = new ChatArg();
            arg.motion = p.Motion;
            arg.loop = p.Loop;
            Character.Motion = arg.motion;
            if (arg.loop == 1)
                Character.MotionLoop = true;
            else
                Character.MotionLoop = false;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);

            if ((int)p.Motion == 140 || (int)p.Motion == 141 || (int)p.Motion == 159 || (int)p.Motion == 113 || (int)p.Motion == 210
                || (int)p.Motion == 555 || (int)p.Motion == 556 || (int)p.Motion == 557 || (int)p.Motion == 558 || (int)p.Motion == 559
                || (int)p.Motion == 400)
            {
                if (Character.Partner != null)
                {
                    ActorPartner partner = Character.Partner;
                    ChatArg parg = new ChatArg();
                    parg.motion = p.Motion;
                    parg.loop = 1;
                    partner.Motion = parg.motion;
                    partner.MotionLoop = true;
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, parg, partner, true);
                }
            }
        }

        public void OnEmotion(Packets.Client.CSMG_CHAT_EMOTION p)
        {
            ChatArg arg = new ChatArg();
            arg.emotion = p.Emotion;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.EMOTION, arg, this.Character, true);
        }

        public void OnSit(Packets.Client.CSMG_CHAT_SIT p)
        {
            ChatArg arg = new ChatArg();

            if (this.Character.Motion != MotionType.SIT)
            {
                arg.motion = MotionType.SIT;
                arg.loop = 1;
                this.Character.Motion = MotionType.SIT;
                this.Character.MotionLoop = true;
                PartnerTalking(Character.Partner, TALK_EVENT.MASTERSIT, 50, 5000);

                if (Character.Partner != null)
                {
                    ActorPartner partner = Character.Partner;
                    ChatArg parg = new ChatArg();
                    parg.motion = (MotionType)135;
                    parg.loop = 1;
                    partner.Motion = parg.motion;
                    partner.MotionLoop = true;
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, parg, partner, true);
                }

                if (!this.Character.Tasks.ContainsKey("Regeneration"))
                {
                    Tasks.PC.Regeneration reg = new SagaMap.Tasks.PC.Regeneration(this);
                    this.Character.Tasks.Add("Regeneration", reg);
                    reg.Activate();
                }
            }
            else
            {
                if (this.Character.Tasks.ContainsKey("Regeneration"))
                {
                    this.Character.Tasks["Regeneration"].Deactivate();
                    this.Character.Tasks.Remove("Regeneration");
                }
                arg.motion = MotionType.STAND;
                arg.loop = 0;
                this.Character.Motion = MotionType.NONE;
                this.Character.MotionLoop = false;
            }
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);
        }

        public void OnSign(Packets.Client.CSMG_CHAT_SIGN p)
        {
            this.Character.Sign = p.Content;
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SIGN_UPDATE, null, this.Character, true);
        }

        public void SendMotion(MotionType motion, byte loop)
        {
            ChatArg arg = new ChatArg();
            arg.motion = motion;
            arg.loop = loop;
            this.Character.Motion = arg.motion;
            if (arg.loop == 1)
                this.Character.MotionLoop = true;
            else
                this.Character.MotionLoop = false;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);

            if (arg.loop == 0)
                Character.Motion =  (MotionType)111;

            if ((int)arg.motion == 140 || (int)arg.motion == 141 || (int)arg.motion == 159 || (int)arg.motion == 113 || (int)arg.motion == 210
    || (int)arg.motion == 555 || (int)arg.motion == 556 || (int)arg.motion == 557 || (int)arg.motion == 558 || (int)arg.motion == 559
    || (int)arg.motion == 400)
            {
                if (Character.Partner != null)
                {
                    ActorPartner partner = Character.Partner;
                    ChatArg parg = new ChatArg();
                    parg.motion = arg.motion;
                    parg.loop = loop;
                    partner.Motion = parg.motion;
                    partner.MotionLoop = true;
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, parg, partner, true);
                }
            }
        }

        public void SendSystemMessage(string content)
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_CHAT_PUBLIC p = new SagaMap.Packets.Server.SSMG_CHAT_PUBLIC();
                p.ActorID = 0xFFFFFFFF;
                p.Message = content;
                this.netIO.SendPacket(p);
            }
        }

        public void SendSystemMessage(Packets.Server.SSMG_SYSTEM_MESSAGE.Messages message)
        {
            Packets.Server.SSMG_SYSTEM_MESSAGE p = new SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE();
            p.Message = message;
            this.netIO.SendPacket(p);
        }

        public void SendChatParty(string sender, string content)
        {
            Packets.Server.SSMG_CHAT_PARTY p = new SagaMap.Packets.Server.SSMG_CHAT_PARTY();
            p.Sender = sender;
            p.Content = content;
            this.netIO.SendPacket(p);
        }
        #region 商人商店
        //an添加 (MarkChat)
        public void OnPlayerShopOpen(Packets.Client.CSMG_PLAYER_SHOP_OPEN p)
        {
            Actor actor = this.map.GetActor(p.ActorID);//mark3
            ActorPC pc = (ActorPC)actor;
            if (pc.Fictitious)
            {
                int EventID = pc.TInt["虚拟玩家EventID"];
                EventActivate((uint)EventID);
                Character.TInt["触发的虚拟玩家ID"] = (int)pc.ActorID;
            }
            else
            {
                MapClient client = MapClient.FromActorPC(pc);
                client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.SHOP_OPEN, this.Character.Name));
                Packets.Server.SSMG_PLAYER_SHOP_HEADER2 p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_HEADER2();
                p1.ActorID = p.ActorID;
                this.netIO.SendPacket(p1);
                Packets.Server.SSMG_PLAYER_SHOP_HEADER p2 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_HEADER();
                p2.ActorID = p.ActorID;
                this.netIO.SendPacket(p2);
                foreach (uint i in pc.Playershoplist.Keys)
                {
                    Item item = pc.Inventory.GetItem(i);
                    if (item == null)
                        continue;
                    Packets.Server.SSMG_PLAYER_SHOP_ITEM p3 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_ITEM();
                    p3.InventorySlot = i;
                    p3.Container = ContainerType.BODY;
                    p3.Price = pc.Playershoplist[i].Price;
                    p3.ShopCount = pc.Playershoplist[i].Count;
                    p3.Item = item;
                    this.netIO.SendPacket(p3);
                }
                Packets.Server.SSMG_PLAYER_SHOP_FOOTER p4 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_FOOTER();
                this.netIO.SendPacket(p4);
            }
        }
        public void OnPlayerSetShopSetup(Packets.Client.CSMG_PLAYER_SETSHOP_SETUP p)//mark11
        {
            if(p.Comment.Length < 1)
            {
                SendSystemMessage("输入商店名称");
                return;
            }
            uint[] ids = p.InventoryIDs;
            ushort[] counts = p.Counts;
            ulong[] prices = p.Prices;
            this.Shoptitle = p.Comment;
            try
            {
                if (ids.Length != 0)
                {
                    if (this.Character.Playershoplist != null)
                        this.Character.Playershoplist.Clear();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (!this.Character.Playershoplist.ContainsKey(ids[i]))
                        {
                            PlayerShopItem item = new PlayerShopItem();
                            item.InventoryID = ids[i];
                            item.ItemID = this.Character.Inventory.GetItem(ids[i]).ItemID;
                            //if(ItemFactory.Instance.GetItem(item.ItemID).BaseData.itemType != ItemType.FACECHANGE
                            //&& item.ItemID != 950000005 && item.ItemID != 100000000 && item.ItemID != 110128500 && item.ItemID != 110132000 && item.ItemID != 110165300)

                            Item item2 = Character.Inventory.GetItem(ids[i]);
                            if (item2.BaseData.itemType == ItemType.IRIS_CARD)
                            {
                                SendSystemMessage("卡片物品：【" + item2.BaseData.name + "】目前无法上架交易。");
                                continue;
                            }
                            if ((item2.EquipSlot.Count < 1 || item2.BaseData.itemType == ItemType.PET || item2.BaseData.itemType == ItemType.PARTNER
                                || item2.BaseData.itemType == ItemType.RIDE_PET||item2.BaseData.itemType == ItemType.RIDE_PARTNER) && item2.BaseData.itemType != ItemType.FURNITURE 
                                && item2.BaseData.itemType != ItemType.FG_GARDEN_MODELHOUSE && item2.BaseData.itemType != ItemType.FG_GARDEN_FLOOR && item2.BaseData.itemType != ItemType.FG_ROOM_FLOOR
                                && item2.BaseData.itemType != ItemType.FG_FLYING_SAIL && item2.BaseData.itemType != ItemType.FG_ROOM_WALL && Character.Account.GMLevel < 200)
                            {
                                SendSystemMessage("无法上架的物品：【" + item2.BaseData.name + "】目前无法上架交易。");
                                continue;
                            }
                            if(item2.Refine > 30)
                            {
                                SendSystemMessage("无法强化大于等于31的物品：【" + item2.BaseData.name + "】");
                                continue;
                            }
                            else
                                Character.Playershoplist.Add(ids[i], item);
                        }
                        if (counts[i] == 0)
                        {
                            this.Character.Playershoplist.Remove(ids[i]);
                        }
                        else
                        {
                            this.Character.Playershoplist[ids[i]].Count = counts[i];
                            this.Character.Playershoplist[ids[i]].Price = prices[i];
                            SendShopGoodInfo(ids[i], counts[i], prices[i]);
                        }
                    }
                }
                else
                {
                    this.Character.Playershoplist.Clear();
                }
            }
            catch (Exception ex1)
            {
                Logger.ShowError(ex1);
            }
            Packets.Server.SSMG_PLAYER_SHOP_APPEAR p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_APPEAR();
            p1.ActorID = this.Character.ActorID;
            p1.Title = this.Shoptitle;
            if (ids.Length != 0 && this.Shoptitle != "" && Character.Playershoplist.Count > 0)
            {
                this.Shopswitch = 1;
                p1.button = 1;
            }
            else
            {
                this.Shopswitch = 0;
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE_CLOSE, null, this.Character, true);
                p1.button = 0;
            }
            this.netIO.SendPacket(p1);
        }
        public void SendShopGoodInfo(uint slotid, ushort count,ulong gold)
        {
            Packets.Server.SSMG_PLAYER_SHOP_GOLD_UPDATA p = new Packets.Server.SSMG_PLAYER_SHOP_GOLD_UPDATA();
            p.SlotID = slotid;
            p.Count = count;
            p.gold = gold;
            netIO.SendPacket(p);
        }
        public void OnPlayerShopSellBuy(Packets.Client.CSMG_PLAYER_SHOP_SELL_BUY p)
        {
            Actor actor = this.map.GetActor(p.ActorID);
            Dictionary<uint, ushort> items = p.Items;
            Packets.Server.SSMG_PLAYER_SHOP_ANSWER p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_ANSWER();
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                MapClient client = FromActorPC(pc);
                long gold = 0;
                //if (client.Character.Account.MacAddress == Character.Account.MacAddress && client.account.GMLevel < 200)
                //{
                //    MapClient clients = FromActorPC(Character);
                //    clients.SendSystemMessage("发生未知的错误，交易失败。");
                //    return;
                //}
                foreach (uint i in items.Keys)
                {
                    Item item = pc.Inventory.GetItem(i);
                    /*GAME_SMSG_STALL_VEN_DEAL_ERR1,"露店を見ていないのに取引しようとしました"
                        GAME_SMSG_STALL_VEN_DEAL_ERR2,"露店を見ている最中に相手が変わりました"
                        GAME_SMSG_STALL_VEN_DEAL_ERR3,"指定した人は商人ではありません"
                        GAME_SMSG_STALL_VEN_DEAL_ERR4,"露店を開いていません"
                        GAME_SMSG_STALL_VEN_DEAL_ERR5,"露店の客として認識されていませんでした"
                        GAME_SMSG_STALL_VEN_DEAL_ERR6,"指定した露店は自分の露店です"
                        GAME_SMSG_STALL_VEN_DEAL_ERR7,"取引可能な商品がありませんでした"
                        GAME_SMSG_STALL_VEN_DEAL_ERR8,"現在の所持金が取引金額に達していませんでした"
                        GAME_SMSG_STALL_VEN_DEAL_ERR9,"これ以上アイテムを所持することはできません"
                        GAME_SMSG_STALL_VEN_DEAL_ERR10,"相手の所持金が上限に達したためキャンセルされました"*/
                    if (item == null)
                    {
                        p1.Result = -4;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                    if (item.ItemID == 950000006 || item.ItemID == 950000007)
                    {
                        p1.Result = -1;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                    if (items[i] == 0)
                    {
                        p1.Result = -2;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                    /*if (item.IsEquipt)
                    {
                        p1.Result = -4;
                        this.netIO.SendPacket(p1);
                        return;
                    }*/
                    if (item.Stack >= items[i])
                    {
                        gold += (long)(pc.Playershoplist[i].Price * items[i]);
                        long singleprice = (long)(pc.Playershoplist[i].Price * items[i]);
                        if (this.Character.Gold < gold)
                        {
                            p1.Result = -7;
                            this.netIO.SendPacket(p1);
                            return;
                        }
                        if (gold + pc.Gold >= 999999999999)
                        {
                            p1.Result = -9;
                            this.netIO.SendPacket(p1);
                            return;
                        }
                        uint cpfee = 0;//(uint)(100 + singleprice * 0.01f);
                        /*if(client.Character.CP < cpfee)
                        {
                            client.SendSystemMessage("玩家: " + this.Character.Name + " 试图向您购买物品，可是您的CP不足，无法贩卖。");
                            this.SendSystemMessage("无法向玩家: " + client.Character.Name + " 购买物品，因为他的CP不足了。");
                            return;
                        }
                        //client.Character.CP -= cpfee;*/

                        Item newItem = item.Clone();
                        newItem.Stack = items[i];
                        if (newItem.Stack > 0)
                        {
                            Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + this.Character.CharID + ")", newItem.BaseData.name + "(" + newItem.ItemID + ")",
                                string.Format("GolemSell Count:{0}", items[i]), false);
                        }
                        InventoryDeleteResult result = pc.Inventory.DeleteItem(i, items[i]);
                        pc.Playershoplist[i].Count -= items[i];

                        SendShopGoodInfo(i, pc.Playershoplist[i].Count, pc.Playershoplist[i].Price);

                        if (pc.Playershoplist[i].Count == 0)
                            pc.Playershoplist.Remove(i);
                        //返回卖家info
                        switch (result)
                        {
                            case InventoryDeleteResult.STACK_UPDATED:
                                Packets.Server.SSMG_ITEM_COUNT_UPDATE p2 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                                p2.InventorySlot = item.Slot;
                                p2.Stack = item.Stack;
                                client.netIO.SendPacket(p2);
                                break;
                            case InventoryDeleteResult.ALL_DELETED:
                                Packets.Server.SSMG_ITEM_DELETE p3 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                                p3.InventorySlot = item.Slot;
                                client.netIO.SendPacket(p3);
                                break;
                        }


                        client.Character.Inventory.CalcPayloadVolume();
                        client.SendCapacity();
                        client.SendSystemMessage("玩家: " + this.Character.Name + " 向您购买了 " + newItem.Stack + " 个 [" + newItem.BaseData.name + "]，售价：" + singleprice.ToString() + "G");
                        client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_DELETED, item.BaseData.name, items[i]));
                        Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("GolemBuy Count:{0}", item.Stack), false);
                        this.SendSystemMessage("向玩家: " + client.Character.Name + " 购买了 " + newItem.Stack + " 个 [" + newItem.BaseData.name + "]，花费：" + singleprice.ToString() + "G");
                        if (newItem.BaseData.itemType == ItemType.PARTNER)
                            newItem.ActorPartnerID = 0;
                        AddItem(newItem, true);


                        Logger log = new Logger("玩家交易记录.txt");
                        string text = "\r\n玩家: " + Character.Name + " 向玩家：" + client.Character.Name + " 购买了 " + newItem.Stack + " 个 [" + newItem.BaseData.name + "]，花费：" + singleprice.ToString() + "G";
                        text += "\r\n买家IP/MAC：" + Character.Account.LastIP + "/" + Character.Account.MacAddress + "   卖家IP/MAC：" + client.Character.Account.LastIP + "/" + client.Character.Account.MacAddress;
                        if (newItem.Refine > 10)
                            text += "\r\n装备道具：" + newItem.BaseData.name + " 强化次数" + newItem.Refine;
                        log.WriteLog(text);


                        if (Character.Account.MacAddress == client.Character.Account.MacAddress || Character.Account.LastIP == client.Character.Account.LastIP)
                        {
                            Logger log2 = new Logger("同IP或MAC的玩家交易记录.txt");
                            string text2 = "\r\n玩家: " + Character.Name + " 向玩家：" + client.Character.Name + " 购买了 " + newItem.Stack + " 个 [" + newItem.BaseData.name + "]，花费：" + singleprice.ToString() + "G";
                            text2 += "\r\n买家IP/MAC：" + Character.Account.LastIP + "/" + Character.Account.MacAddress + "   卖家IP/MAC：" + client.Character.Account.LastIP + "/" + client.Character.Account.MacAddress;
                            if (newItem.Refine > 10)
                                text2 += "\r\n装备道具：" + newItem.BaseData.name + " 强化次数" + newItem.Refine;
                            log2.WriteLog(text2);
                        }
                        
                    }
                    else
                    {
                        p1.Result = -5;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                }
                Character.Gold -= gold;
                pc.Gold += gold;

                if (pc.Playershoplist.Count == 0)
                {
                    pc.Fictitious = false;
                    client.Shopswitch = 0;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE_CLOSE, null, this.Character, true);
                }
            }
        }
        public void OnPlayerShopBuyClose(Packets.Client.CSMG_PLAYER_SETSHOP_CLOSE p)
        {
            //this.Shopswitch = 0;
            //this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE_CLOSE, null, this.Character, true);
            //Packets.Server.SSMG_PLAYER_SETSHOP_SET p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SETSHOP_SET();
            //this.netIO.SendPacket(p1);

            Packets.Server.SSMG_PLAYER_SHOP_CLOSE p1 = new Packets.Server.SSMG_PLAYER_SHOP_CLOSE();
            // Reason is positive int
            /*
             GAME_SMSG_STALL_VEN_CLOSE_ERR1,"露店商の商品は売り切れました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR2,"露店商が商品の再設定をしています"
            GAME_SMSG_STALL_VEN_CLOSE_ERR3,"露店商が遠く離れました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR4,"露店商がマップ移動しました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR5,"露店商がトレードを始めました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR6,"露店商が憑依しました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR8,"露店商が行動不能状態となりました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR9,"露店商がいなくなりました"
            GAME_SMSG_STALL_VEN_CLOSE_ERR10,"露店商がイベントを始めました"
            */
            this.netIO.SendPacket(p1);
        }
        public void OnPlayerShopChangeClose(Packets.Client.CSMG_PLAYER_SETSHOP_OPEN p)
        {
            Character.Playershoplist.Clear();
            Character.Fictitious = false;
            this.Shopswitch = 0;
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE_CLOSE, null, this.Character, true);
        }
        public void OnPlayerShopChange(Packets.Client.CSMG_PLAYER_SETSHOP_SETUP p)
        {
            this.Shoptitle = p.Comment;
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE, null, this.Character, true);
            if(this.Shopswitch == 0 && this.Shoptitle == "")
            {
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE_CLOSE, null, this.Character, true);
            }
        }
        public void OnPlayerSetShop(Packets.Client.CSMG_PLAYER_SETSHOP_OPEN p)
        {
            Packets.Server.SSMG_PLAYER_SETSHOP_OPEN_SETUP p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SETSHOP_OPEN_SETUP();
            Packets.Server.SSMG_PLAYER_SHOP_APPEAR p2 = new Packets.Server.SSMG_PLAYER_SHOP_APPEAR();
            p2.ActorID = this.Character.ActorID;
            p2.Title = this.Shoptitle;
            this.Shopswitch = 0;
            p2.button = 0;
            p1.Comment = this.Shoptitle;
            this.netIO.SendPacket(p1);
            this.netIO.SendPacket(p2);
        }
        #endregion

        /// <summary>
        /// 查看目标装备
        /// </summary>
        public void OnPlayerEquipOpen(uint charID)
        {
            ActorPC pc = this.map.GetPC(charID);
            if(pc.Fictitious)
            {
                SendSystemMessage("無法查看已融合的裝備列表。");
                return;
            }
            if (!pc.showEquipment)
            {
                SendSystemMessage("對方已設置不允許查看裝備列表。");
                return;
            }
            MapClient.FromActorPC(pc).SendSystemMessage(this.Character.Name + "正在查看你的裝備列表");
            Packets.Server.SSMG_PLAYER_EQUIP_START p1 = new SagaMap.Packets.Server.SSMG_PLAYER_EQUIP_START();
            this.netIO.SendPacket(p1);
            Packets.Server.SSMG_PLAYER_EQUIP_NAME p2 = new SagaMap.Packets.Server.SSMG_PLAYER_EQUIP_NAME();
            p2.ActorName = pc.Name;
            this.netIO.SendPacket(p2);
            foreach (KeyValuePair<EnumEquipSlot, Item> i in pc.Inventory.Equipments)
            {
                Item item = i.Value;
                if (item == null)
                    continue;
                Packets.Server.SSMG_PLAYER_EQUIP_INFO p3 = new SagaMap.Packets.Server.SSMG_PLAYER_EQUIP_INFO();
                p3.InventorySlot = item.Slot;
                p3.Container = pc.Inventory.GetContainerType(item.Slot);
                p3.Item = item;
                this.netIO.SendPacket(p3);
            }
            Packets.Server.SSMG_PLAYER_EQUIP_END p4 = new SagaMap.Packets.Server.SSMG_PLAYER_EQUIP_END();
            this.netIO.SendPacket(p4);
        }

        public void OnPlayerFurnitureSit(Packets.Client.CSMG_PLAYER_FURNITURE_SIT p)
        {
            if (p.unknown != -1)
            {
                this.chara.FurnitureID = p.FurnitureID;
                this.chara.FurnitureID_old = (uint)p.unknown;
            }
            else
            {
                this.chara.FurnitureID_old = 255;
                this.chara.FurnitureID = 255;
            }

            Packets.Server.SSMG_PLAYER_FURNITURE_SIT p1 = new SagaMap.Packets.Server.SSMG_PLAYER_FURNITURE_SIT();
            p1.FurnitureID = p.FurnitureID;
            p1.unknown = p.unknown;
            this.netIO.SendPacket(p1);

            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.FURNITURE_SIT, null, this.Character, true);
        }

    }
}
