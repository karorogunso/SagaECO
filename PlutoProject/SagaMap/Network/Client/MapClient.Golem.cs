using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnGolemWarehouse(Packets.Client.CSMG_GOLEM_WAREHOUSE p)
        {
            Packets.Server.SSMG_GOLEM_WAREHOUSE p1 = new SagaMap.Packets.Server.SSMG_GOLEM_WAREHOUSE();
            p1.ActorID = this.Character.ActorID;
            p1.Title = this.Character.Golem.Title;
            this.netIO.SendPacket(p1);

            foreach (Item i in this.Character.Inventory.GetContainer(ContainerType.GOLEMWAREHOUSE))
            {
                Packets.Server.SSMG_GOLEM_WAREHOUSE_ITEM p2 = new SagaMap.Packets.Server.SSMG_GOLEM_WAREHOUSE_ITEM();
                p2.InventorySlot = i.Slot;
                p2.Container = ContainerType.GOLEMWAREHOUSE;
                p2.Item = i;
                this.netIO.SendPacket(p2);
            }

            Packets.Server.SSMG_GOLEM_WAREHOUSE_ITEM_FOOTER p3 = new SagaMap.Packets.Server.SSMG_GOLEM_WAREHOUSE_ITEM_FOOTER();
            this.netIO.SendPacket(p3);

        }

        public void OnGolemWarehouseSet(Packets.Client.CSMG_GOLEM_WAREHOUSE_SET p)
        {
            if (this.Character.Golem != null)
                this.Character.Golem.Title = p.Title;
        }

        public void OnGolemWarehouseGet(Packets.Client.CSMG_GOLEM_WAREHOUSE_GET p)
        {
            Item item = this.Character.Inventory.GetItem(p.InventoryID);
            if (item != null)
            {
                ushort count = p.Count;
                if (item.Stack >= count)
                {
                    Item newItem = item.Clone();
                    newItem.Stack = count;
                    if (newItem.Stack > 0)
                    {
                        Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + this.Character.CharID + ")", newItem.BaseData.name + "(" + newItem.ItemID + ")",
                            string.Format("GolemWarehouseGet Count:{0}", count), false);
                    }
                    
                    this.Character.Inventory.DeleteItem(p.InventoryID, count);
                    Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                    string.Format("GolemWarehouse Count:{0}", item.Stack), false);
                    AddItem(newItem, false);
                    Packets.Server.SSMG_GOLEM_WAREHOUSE_GET p1 = new SagaMap.Packets.Server.SSMG_GOLEM_WAREHOUSE_GET();
                    this.netIO.SendPacket(p1);
                }
            }
        }

        public void OnGolemShopBuySell(Packets.Client.CSMG_GOLEM_SHOP_BUY_SELL p)
        {
            Actor actor = this.map.GetActor(p.ActorID);
            Dictionary<uint, ushort> items = p.Items;
            if (actor.type == ActorType.GOLEM)
            {
                ActorGolem golem = (ActorGolem)actor;
                uint gold = 0;
                foreach (uint i in items.Keys)
                {
                    Item item = this.Character.Inventory.GetItem(i);
                    if (item == null)
                        continue;
                    if (items[i] == 0)
                        continue;
                    //if (item.BaseData.noTrade)
                        //continue;
                    Item newItem = item.Clone();
                    if (item.Stack >= items[i])
                    {
                        uint inventoryID = 0;
                        foreach (uint j in golem.BuyShop.Keys)
                        {
                            if (golem.BuyShop[j].ItemID == newItem.ItemID)
                            {
                                inventoryID = j;
                                break;
                            }
                        }
                        gold += (golem.BuyShop[inventoryID].Price * items[i]);
                        if (golem.BuyLimit < gold)
                        {
                            gold -= (golem.BuyShop[inventoryID].Price * items[i]);
                            break;
                        }
                        newItem.Stack = items[i];
                        Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                            string.Format("GolemSell Count:{0}", items[i]), false);
                        DeleteItem(i, items[i], true);
                        golem.BuyShop[inventoryID].Count -= items[i];

                        if (golem.BoughtItem.ContainsKey(item.ItemID))
                        {
                            golem.BoughtItem[item.ItemID].Count += items[i];
                        }
                        else
                        {
                            golem.BoughtItem.Add(item.ItemID, new GolemShopItem());
                            golem.BoughtItem[item.ItemID].Price = golem.BuyShop[inventoryID].Price;
                            golem.BoughtItem[item.ItemID].Count += items[i];
                        }
                        if (newItem.Stack > 0)
                        {
                            Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + this.Character.CharID + ")", newItem.BaseData.name + "(" + newItem.ItemID + ")",
                                string.Format("GolemBuy Count:{0}", newItem.Stack), false);
                        }
                        if (golem.BuyShop[inventoryID].Count == 0)//新加
                            golem.BuyShop.Remove(inventoryID);
                        //golem.Owner.Inventory.AddItem(ContainerType.BODY, newItem);
                    }
                }
                //golem.Owner.Gold -= (int)gold;
                golem.BuyLimit -= gold;
                this.Character.Gold += (int)gold;
            }
        }

        public void OnGolemShopSellBuy(Packets.Client.CSMG_GOLEM_SHOP_SELL_BUY p)
        {
            Actor actor = this.map.GetActor(p.ActorID);
            Dictionary<uint, ushort> items = p.Items;
            Packets.Server.SSMG_GOLEM_SHOP_SELL_ANSWER p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_SELL_ANSWER();
            if (actor.type == ActorType.GOLEM)
            {
                ActorGolem golem = (ActorGolem)actor;
                uint gold = 0;
                foreach (uint i in items.Keys)
                {
                    Item item = ItemFactory.Instance.GetItem(golem.SellShop[i].ItemID);
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
                    /*if (item.BaseData.noTrade)
                    {
                        p1.Result = -1;
                        this.netIO.SendPacket(p1);
                        return;
                    }*/
                    if (golem.SellShop[i].Count >= items[i])
                    {
                        gold += (golem.SellShop[i].Price * items[i]);
                        if (this.Character.Gold < gold)
                        {
                            p1.Result = -7;
                            this.netIO.SendPacket(p1);
                            return;
                        }
                        /*if (gold + golem.Owner.Gold >= 99999999)
                        {
                            p1.Result = -9;
                            this.netIO.SendPacket(p1);
                            return;
                        }*/
                        Item newItem = item.Clone();
                        newItem.Stack = items[i];
                        if (newItem.Stack > 0)
                        {
                            Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + this.Character.CharID + ")", newItem.BaseData.name + "(" + newItem.ItemID + ")",
                                string.Format("GolemSell Count:{0}", items[i]), false);
                        }
                        //golem.Owner.Inventory.DeleteItem(i, items[i]);
                        golem.SellShop[i].Count -= items[i];
                        if (golem.SoldItem.ContainsKey(item.ItemID))
                        {
                            golem.SoldItem[item.ItemID].Count += items[i];
                        }
                        else
                        {
                            golem.SoldItem.Add(item.ItemID, new GolemShopItem());
                            golem.SoldItem[item.ItemID].Price = golem.SellShop[i].Price;
                            golem.SoldItem[item.ItemID].Count += items[i];
                        }
                        if (golem.SellShop[i].Count == 0)
                        {
                            golem.SellShop.Remove(i);
                        }

                        if (golem.SellShop.Count == 0)
                        {
                            golem.invisble = true;
                            this.map.OnActorVisibilityChange(golem);
                        }
                        Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("GolemBuy Count:{0}", item.Stack), false);
                        AddItem(newItem, true);
                    }
                    else
                    {
                        p1.Result = -5;
                        this.netIO.SendPacket(p1);
                        return;
                    }
                }
                //golem.Owner.Gold += (int)gold;
                this.Character.Gold -= (int)gold;
                /*try
                {
                     SagaMap.MapServer. charDB.SaveChar(golem.Owner, true, false);
                }
                catch (Exception ex) { Logger.ShowError(ex); }*/
            }
        }

        public void OnGolemShopOpen(Packets.Client.CSMG_GOLEM_SHOP_OPEN p)
        {
            Actor actor = this.map.GetActor(p.ActorID);
            if (actor.type == ActorType.GOLEM)
            {
                ActorGolem golem = (ActorGolem)actor;

                if (golem.GolemType == GolemType.Sell)
                {
                    Packets.Server.SSMG_GOLEM_SHOP_OPEN_OK p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_OPEN_OK();
                    p1.ActorID = p.ActorID;
                    this.netIO.SendPacket(p1);
                    Packets.Server.SSMG_GOLEM_SHOP_HEADER p2 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_HEADER();
                    p2.ActorID = p.ActorID;
                    this.netIO.SendPacket(p2);
                    foreach (uint i in golem.SellShop.Keys)
                    {
                        Item item = golem.Owner.Inventory.GetItem(i);
                        if (item == null)
                            continue;
                        Packets.Server.SSMG_GOLEM_SHOP_ITEM p3 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_ITEM();
                        p3.InventorySlot = i;
                        p3.Container = ContainerType.BODY;
                        p3.Price = golem.SellShop[i].Price;
                        p3.ShopCount = golem.SellShop[i].Count;
                        p3.Item = item;
                        this.netIO.SendPacket(p3);
                    }
                    Packets.Server.SSMG_GOLEM_SHOP_FOOTER p4 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_FOOTER();
                    this.netIO.SendPacket(p4);
                }
                if (golem.GolemType == GolemType.Buy)
                {
                    Packets.Server.SSMG_GOLEM_SHOP_BUY_HEADER p2 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_HEADER();
                    p2.ActorID = p.ActorID;
                    this.netIO.SendPacket(p2);

                    Packets.Server.SSMG_GOLEM_SHOP_BUY_ITEM p3 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_ITEM();
                    p3.Items = golem.BuyShop;
                    p3.BuyLimit = golem.BuyLimit;
                    this.netIO.SendPacket(p3);
                }
            }
        }

        public void OnGolemShopSellClose(Packets.Client.CSMG_GOLEM_SHOP_SELL_CLOSE p)
        {
            Packets.Server.SSMG_GOLEM_SHOP_SELL_SET p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_SELL_SET();
            this.netIO.SendPacket(p1);
        }

        public void OnGolemShopSellSetup(Packets.Client.CSMG_GOLEM_SHOP_SELL_SETUP p)
        {
            uint[] ids = p.InventoryIDs;
            ushort[] counts = p.Counts;
            uint[] prices = p.Prices;
            if (ids.Length != 0)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (!this.Character.Golem.SellShop.ContainsKey(ids[i]))
                    {
                        GolemShopItem item = new GolemShopItem();
                        item.InventoryID = ids[i];
                        item.ItemID = this.Character.Inventory.GetItem(ids[i]).ItemID;
                        this.Character.Golem.SellShop.Add(ids[i], item);
                    }
                    if (counts[i] == 0)
                        this.Character.Golem.SellShop.Remove(ids[i]);
                    else
                    {
                        this.Character.Golem.SellShop[ids[i]].Count = counts[i];
                        this.Character.Golem.SellShop[ids[i]].Price = prices[i];
                    }
                }
            }
            this.Character.Golem.Title = p.Comment;
        }

        public void OnGolemShopSell(Packets.Client.CSMG_GOLEM_SHOP_SELL p)
        {
            Packets.Server.SSMG_GOLEM_SHOP_SELL_SETUP p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_SELL_SETUP();
            p1.Comment = this.Character.Golem.Title;
            this.netIO.SendPacket(p1);
        }

        public void OnGolemShopBuyClose(Packets.Client.CSMG_GOLEM_SHOP_BUY_CLOSE p)
        {
            Packets.Server.SSMG_GOLEM_SHOP_BUY_SET p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_SET();
            this.netIO.SendPacket(p1);
        }       

        public void OnGolemShopBuySetup(Packets.Client.CSMG_GOLEM_SHOP_BUY_SETUP p)
        {
            uint[] ids = p.InventoryIDs;
            ushort[] counts = p.Counts;
            uint[] prices = p.Prices;
            if (ids.Length != 0)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (!this.Character.Golem.BuyShop.ContainsKey(ids[i]))
                    {
                        GolemShopItem item = new GolemShopItem();
                        item.InventoryID = ids[i];
                        item.ItemID = this.Character.Inventory.GetItem(ids[i]).ItemID;
                        this.Character.Golem.BuyShop.Add(ids[i], item);
                    }
                    if (counts[i] == 0)
                        this.Character.Golem.BuyShop.Remove(ids[i]);
                    else
                    {
                        this.Character.Golem.BuyShop[ids[i]].Count = counts[i];
                        this.Character.Golem.BuyShop[ids[i]].Price = prices[i];
                    }
                }
            }
            this.Character.Golem.BuyLimit = p.BuyLimit;
            this.Character.Golem.Title = p.Comment;
        }

        public void OnGolemShopBuy(Packets.Client.CSMG_GOLEM_SHOP_BUY p)
        {
            Packets.Server.SSMG_GOLEM_SHOP_BUY_SETUP p1 = new SagaMap.Packets.Server.SSMG_GOLEM_SHOP_BUY_SETUP();
            p1.BuyLimit = this.Character.Golem.BuyLimit;
            p1.Comment = this.Character.Golem.Title;
            this.Character.Golem.BuyShop.Clear();
            this.netIO.SendPacket(p1);
        }
    }
}
