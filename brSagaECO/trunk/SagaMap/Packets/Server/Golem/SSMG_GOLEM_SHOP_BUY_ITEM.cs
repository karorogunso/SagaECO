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
            this.data = new byte[9 + 10 * 32];
            this.offset = 2;
            this.ID = 0x1826;
            this.PutByte(32, 2);
            this.PutByte(32, (ushort)(3 + 4 * 32));
            this.PutByte(32, (ushort)(4 + 6 * 32));
        }

        public Dictionary<uint, GolemShopItem> Items
        {
            set
            {
                int j = 0;
                foreach (GolemShopItem i in value.Values)
                {
                    this.PutUInt(i.ItemID, (ushort)(3 + j * 4));
                    this.PutUShort(i.Count, (ushort)(4 + 32 * 4 + j * 2));
                    this.PutUInt(i.Price, (ushort)(5 + 32 * 6 + j * 4));
                    j++;
                }
            }
        }

        public uint BuyLimit
        {
            set
            {
                this.PutUInt(value, (ushort)(5 + 32 * 10));
            }
        }
    }
}

