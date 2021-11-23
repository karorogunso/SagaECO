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
                ChatArg arg = new ChatArg();
                arg.content = p.Content;
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, this.Character, true);
            }
        }

        public void OnChatParty(Packets.Client.CSMG_CHAT_PARTY p)
        {
            if (this.Character != null)
            {
                PartyManager.Instance.PartyChat(this.Character.Party, this.Character, p.Content);
            }
        }
        public void OnExpression(Packets.Client.CSMG_CHAT_EXPRESSION p)
        {
            ChatArg arg = new ChatArg();
            arg.expression = p.Motion;
            this.Character.MotionLoop = false;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);
        }
        public void OnWaitType(Packets.Client.CSMG_CHAT_WAITTYPE p)
        {
            this.Character.WaitType = p.type;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.WAITTYPE, null, this.Character, true);
        }

        public void OnMotion(Packets.Client.CSMG_CHAT_MOTION p)
        {
            ChatArg arg = new ChatArg();
            arg.motion = p.Motion;
            arg.loop = p.Loop;
            this.Character.Motion = arg.motion;
            if (arg.loop == 1)
                this.Character.MotionLoop = true;
            else
                this.Character.MotionLoop = false;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);
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
        public void OnPlayerSetShopSetup(Packets.Client.CSMG_PLAYER_SETSHOP_SETUP p)//mark11
        {
            uint[] ids = p.InventoryIDs;
            ushort[] counts = p.Counts;
            uint[] prices = p.Prices;
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
                            this.Character.Playershoplist.Add(ids[i], item);
                        }
                        if (counts[i] == 0)
                            this.Character.Playershoplist.Remove(ids[i]);
                        else
                        {
                            this.Character.Playershoplist[ids[i]].Count = counts[i];
                            this.Character.Playershoplist[ids[i]].Price = prices[i];
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
            this.Shoptitle = p.Comment;
            Packets.Server.SSMG_PLAYER_SHOP_APPEAR p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_APPEAR();
            p1.ActorID = this.Character.ActorID;
            p1.Title = this.Shoptitle;
            if (ids.Length != 0 && this.Shoptitle != "")
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
        public void OnPlayerShopSellBuy(Packets.Client.CSMG_PLAYER_SHOP_SELL_BUY p)
        {
            Actor actor = this.map.GetActor(p.ActorID);
            Dictionary<uint, ushort> items = p.Items;
            Packets.Server.SSMG_PLAYER_SHOP_ANSWER p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SHOP_ANSWER();
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                MapClient client = MapClient.FromActorPC(pc);
                uint gold = 0;
                foreach (uint i in items.Keys)
                {
                    Item item = pc.Inventory.GetItem(i);
                    if (item == null)
                    {
                        p1.Result = -4;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                    if (items[i] == 0)
                    {
                        p1.Result = -2;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                    if (item.Stack >= items[i])
                    {
                        gold += (pc.Playershoplist[i].Price * items[i]);
                        if (this.Character.Gold < gold)
                        {
                            p1.Result = -7;
                            this.netIO.SendPacket(p1);
                            return;
                        }
                        if (gold + pc.Gold >= pc.GoldLimit)
                        {
                            p1.Result = -9;
                            this.netIO.SendPacket(p1);
                            return;
                        }
                        Item newItem = item.Clone();
                        newItem.Stack = items[i];
                        if (newItem.Stack > 0)
                        {
                            Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + this.Character.CharID + ")", newItem.BaseData.name + "(" + newItem.ItemID + ")",
                                string.Format("GolemSell Count:{0}", items[i]), false);
                        }
                        pc.Inventory.DeleteItem(i, items[i]);
                        pc.Playershoplist[i].Count -= items[i];
                        //返回卖家info
                        Packets.Server.SSMG_ITEM_DELETE p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = item.Slot;
                        client.netIO.SendPacket(p2);
                        if (item.IsEquipt)
                        {
                            SendAttackType();
                            Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                            p4.InventorySlot = 0xffffffff;
                            p4.Target = ContainerType.NONE;
                            p4.Result = 1;
                            p4.Range = client.Character.Range;
                            client.netIO.SendPacket(p4);
                        }
                        client.Character.Inventory.CalcPayloadVolume();
                        client.SendCapacity();
                        client.SendSystemMessage("玩家: " + this.Character.Name + " 向您购买了 " + newItem.Stack + " 个 [" + newItem.BaseData.name + "]");
                        //client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_DELETED, item.BaseData.name, items[i]));
                        //client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.SHOP_SELL, items[i], item.BaseData.name, this.Character.Name));

                        /*if (pc.SoldItem.ContainsKey(item.ItemID))
                        {
                            golem.SoldItem[item.ItemID].Count += items[i];
                        }
                        else
                        {
                            golem.SoldItem.Add(item.ItemID, new GolemShopItem());
                            golem.SoldItem[item.ItemID].Price = pc.Playershoplist[i].Price;
                            golem.SoldItem[item.ItemID].Count += items[i];
                        }
                        if (pc.SellShop[i].Count == 0)
                        {
                            pc.SoldItem.Remove(i);
                        }

                        if (pc.SoldItem.Count == 0)
                        {
                            pc.invisble = true;
                            this.map.OnActorVisibilityChange(pc);
                        }*/
                        Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("GolemBuy Count:{0}", item.Stack), false);
                        AddItem(newItem, false);
                    }
                    else
                    {
                        p1.Result = -5;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                }
                pc.Gold += (long)gold;
                this.Character.Gold -= (long)gold;
            }
        }
        public void OnPlayerShopBuyClose(Packets.Client.CSMG_PLAYER_SETSHOP_CLOSE p)
        {
            //this.Shopswitch = 0;
            //this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYERSHOP_CHANGE_CLOSE, null, this.Character, true);
            //Packets.Server.SSMG_PLAYER_SETSHOP_SET p1 = new SagaMap.Packets.Server.SSMG_PLAYER_SETSHOP_SET();
            //this.netIO.SendPacket(p1);
        }       
        public void OnPlayerShopChangeClose(Packets.Client.CSMG_PLAYER_SETSHOP_OPEN p)
        {
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
        //markend
        #endregion

        public void OnPlayerEquipOpen(uint charID)
        {
            ActorPC pc = this.map.GetPC(charID);
            if(!pc.CheckEquipmentPermissions)
            {
                this.SendSystemMessage("查看装备的请求被拒绝");
                return;
            }
            //MapClient mc = MapClientManager.Instance.FindClient(charID);//mark3
            //ActorPC pc = mc.Character;
            //ActorPC pc = (ActorPC)actor;
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
                string ss = p3.DumpData();
                this.netIO.SendPacket(p3);
            }
            Packets.Server.SSMG_PLAYER_EQUIP_END p4 = new SagaMap.Packets.Server.SSMG_PLAYER_EQUIP_END();
            this.netIO.SendPacket(p4);
        }

        public void OnPlayerCheckEquipmentPermissions(uint option)
        {
            if (option == 0)
            {
                this.Character.CheckEquipmentPermissions = true;
                this.SendSystemMessage("现在允许装备被查看");
            }
            else
            {
                this.Character.CheckEquipmentPermissions = false;
                this.SendSystemMessage("现在拒绝装备被查看");
            }
        }
    }
}
