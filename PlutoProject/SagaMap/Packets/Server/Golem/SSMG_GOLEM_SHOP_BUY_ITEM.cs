using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SHOP_BUY_ITEM :Packet
    {
        public SSMG_GOLEM_SHOP_BUY_ITEM()
        {
            this.data = new byte[1413];
            this.offset = 2;
            this.ID = 0x1826;
            this.PutByte(100, 2);
            this.PutByte(100, (ushort)(3 + 4 * 100));
            this.PutByte(100, (ushort)(4 + 6 * 100));
        }

        public Dictionary<uint, GolemShopItem> Items
        {
            set
            {
                int j = 0;
                foreach (GolemShopItem i in value.Values)
                {
                    this.PutUInt(i.ItemID, (ushort)(3 + j * 4));
                    this.PutUShort(i.Count, (ushort)(4 + 100 * 4 + j * 2));
                    this.PutULong(i.Price, (ushort)(5 + 100 * 6 + j * 8));
                    j++;
                }
            }
        }

        public uint BuyLimit
        {
            set
            {
                this.PutULong(value, 1405);
            }
        }
    }
}

