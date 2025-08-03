using SagaDB.Item;
using SagaLib;
using SagaMap.Packets.Client;
using SagaMap.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public bool itemexchange = false;
        public void OnItemExchangeConfirm(CSMG_ITEM_EXCHANGE_CONFIRM p)
        {
            var exchangetype = p.ExchangeType;
            var inventoryid = p.InventorySlot;
            var exchangetargetid = p.ExchangeTargetID;

            Item item = this.Character.Inventory.GetItem(inventoryid);
            if (item == null || item.ItemID == 10000000)
            {
                SSMG_ITEM_EXCHANGE_WINDOW_RESET p2 = new SSMG_ITEM_EXCHANGE_WINDOW_RESET();
                this.netIO.SendPacket(p2);
                SendCapacity();
                return;
            }

            if (!ItemExchangeListFactory.Instance.ExchangeList.ContainsKey(item.ItemID))
            {
                SSMG_ITEM_EXCHANGE_WINDOW_RESET p3 = new SSMG_ITEM_EXCHANGE_WINDOW_RESET();
                this.netIO.SendPacket(p3);
                SendCapacity();
                return;
            }

            var oriitem = ItemExchangeListFactory.Instance.ExchangeList[item.ItemID].OriItemID;

            var canexchangelist = ExchangeFactory.Instance.ExchangeItems[oriitem];

            if (!canexchangelist.ItemsID.Contains(exchangetargetid) && canexchangelist.OriItemID != exchangetargetid)
            {
                SSMG_ITEM_EXCHANGE_WINDOW_RESET p4 = new SSMG_ITEM_EXCHANGE_WINDOW_RESET();
                this.netIO.SendPacket(p4);
                SendCapacity();
                return;
            }

            Item targetitem = ItemFactory.Instance.GetItem(exchangetargetid, true);

            DeleteItem(inventoryid, 1, true);
            AddItem(targetitem, true);
            //Logger.ShowInfo("Receive Item Exchange Request. Type:" + exchangetype + ", itemslot:" + inventoryid + ", targetid:" + exchangetargetid);

            SSMG_ITEM_EXCHANGE_WINDOW_RESET p1 = new SSMG_ITEM_EXCHANGE_WINDOW_RESET();
            this.netIO.SendPacket(p1);
            SendCapacity();
        }

        public void OnItemExchangeWindowClose(CSMG_ITEM_EXCHANGE_CLOSE p)
        {
            itemexchange = false;
        }
    }
}
