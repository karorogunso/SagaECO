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
using SagaMap.Skill;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnFGardenWareClose(Packets.Client.CSMG_FG_WARE_CLOSE p)
        {
            currentWarehouse = WarehousePlace.Current;
        }

        public void OnFGardenWareGet(Packets.Client.CSMG_FG_WARE_GET p)
        {
            int result = 0;
            if (currentWarehouse == WarehousePlace.Current)
                result = 1;
            else
            {
                Item item = this.Character.Inventory.GetItem(currentWarehouse, p.InventoryID);
                if (item == null)
                    result = -2;
                else if (item.Stack < p.Count)
                    result = -3;
                else
                {
                    Item newItem;
                    switch (this.Character.Inventory.DeleteWareItem(currentWarehouse, item.Slot, p.Count))
                    {
                        case InventoryDeleteResult.ALL_DELETED:
                            Packets.Server.SSMG_ITEM_DELETE p1 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                            p1.InventorySlot = item.Slot;
                            this.netIO.SendPacket(p1);
                            newItem = item.Clone();
                            newItem.Stack = p.Count;
                            Logger.LogItemGet(Logger.EventType.ItemWareGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                                string.Format("WareGet Count:{0}", item.Stack), false);
                            AddItem(newItem, false);
                            this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_WARE_GET, item.BaseData.name, p.Count));
                            break;
                        case InventoryDeleteResult.STACK_UPDATED:
                            Packets.Server.SSMG_ITEM_COUNT_UPDATE p2 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                            p2.InventorySlot = item.Slot;
                            p2.Stack = item.Stack;
                            this.netIO.SendPacket(p2);
                            newItem = item.Clone();
                            newItem.Stack = p.Count;
                            Logger.LogItemGet(Logger.EventType.ItemWareGet, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                                string.Format("WareGet Count:{0}", item.Stack), false);
                            AddItem(newItem, false);
                            this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_WARE_GET, item.BaseData.name, p.Count));
                            break;
                        case InventoryDeleteResult.ERROR:
                            result = -99;
                            break;
                    }
                }
            }
            Packets.Server.SSMG_ITEM_WARE_GET_RESULT p5 = new SagaMap.Packets.Server.SSMG_ITEM_WARE_GET_RESULT();
            p5.Result = result;
            this.netIO.SendPacket(p5);
        }

        public void OnFGardenWarePut(Packets.Client.CSMG_FG_WARE_PUT p)
        {
            int result = 0;
            if (currentWarehouse == WarehousePlace.Current)
                result = -1;
            else
            {
                Item item = this.Character.Inventory.GetItem(p.InventoryID);
                if (item == null)
                    result = -2;
                else if (item.Stack < p.Count)
                    result = -3;
                else if (this.Character.Inventory.WareTotalCount >= Configuration.Instance.WarehouseLimit)
                    result = -4;
                else
                {
                    Logger.LogItemLost(Logger.EventType.ItemWareLost, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")", string.Format("WarePut Count:{0}", p.Count), false);
                    DeleteItem(p.InventoryID, p.Count, false);
                    Item newItem = item.Clone();
                    newItem.Stack = p.Count;
                    switch (this.Character.Inventory.AddWareItem(currentWarehouse, newItem))
                    {
                        case InventoryAddResult.NEW_INDEX:
                            Packets.Server.SSMG_FG_WARE_ITEM p1 = new SagaMap.Packets.Server.SSMG_FG_WARE_ITEM();
                            p1.InventorySlot = newItem.Slot;
                            p1.Item = newItem;
                            this.netIO.SendPacket(p1);
                            break;
                        case InventoryAddResult.STACKED:
                            Packets.Server.SSMG_ITEM_COUNT_UPDATE p2 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                            p2.InventorySlot = item.Slot;
                            p2.Stack = item.Stack;
                            this.netIO.SendPacket(p2);
                            break;
                        case InventoryAddResult.MIXED:
                            Packets.Server.SSMG_ITEM_COUNT_UPDATE p3 = new SagaMap.Packets.Server.SSMG_ITEM_COUNT_UPDATE();
                            p3.InventorySlot = item.Slot;
                            p3.Stack = item.Stack;
                            this.netIO.SendPacket(p3);
                            Packets.Server.SSMG_FG_WARE_ITEM p4 = new SagaMap.Packets.Server.SSMG_FG_WARE_ITEM();
                            p4.InventorySlot = this.Character.Inventory.LastItem.Slot;
                            p4.Item = this.Character.Inventory.LastItem;
                            this.netIO.SendPacket(p4);
                            break;
                    }
                    this.SendSystemMessage(string.Format(LocalManager.Instance.Strings.ITEM_WARE_PUT, item.BaseData.name, p.Count));
                }
            }
            Packets.Server.SSMG_ITEM_WARE_PUT_RESULT p5 = new SagaMap.Packets.Server.SSMG_ITEM_WARE_PUT_RESULT();
            p5.Result = result;
            this.netIO.SendPacket(p5);
        }

        public void SendFGardenWareItems()
        {
            currentWarehouse = WarehousePlace.FGarden;
            Packets.Server.SSMG_FG_WARE_SENDCOUNT p0 = new SagaMap.Packets.Server.SSMG_FG_WARE_SENDCOUNT();
            this.Character.Inventory.WareHouse[currentWarehouse] = new List<Item>(300);
            p0.CurrentCount = this.Character.Inventory.WareHouse[currentWarehouse].Capacity;
            this.netIO.SendPacket(p0);

            Packets.Server.SSMG_FG_WARE_HEADER p1 = new SagaMap.Packets.Server.SSMG_FG_WARE_HEADER();
            this.netIO.SendPacket(p1);

            foreach (Item j in this.Character.Inventory.WareHouse[currentWarehouse])
            {
                //if (j.Refine == 0)
                //    j.Clear();

                Packets.Server.SSMG_FG_WARE_ITEM p2 = new SagaMap.Packets.Server.SSMG_FG_WARE_ITEM();
                p2.Item = j;
                p2.InventorySlot = j.Slot;
                //if (i == place)
                //    p2.Place = WarehousePlace.Current;
                //else
                //    p2.Place = i;
                this.netIO.SendPacket(p2);
            }

            Packets.Server.SSMG_FG_WARE_FOOTER p3 = new SagaMap.Packets.Server.SSMG_FG_WARE_FOOTER();
            this.netIO.SendPacket(p3);
        }
    }
}
