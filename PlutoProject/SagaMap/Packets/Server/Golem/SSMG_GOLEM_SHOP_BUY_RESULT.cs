using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SHOP_BUY_RESULT : Packet
    {
        public SSMG_GOLEM_SHOP_BUY_RESULT()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x182E;
        }

        public Dictionary<uint, GolemShopItem> BoughtItems
        {
            set
            {
                this.data = new byte[12 + value.Count * 6];
                this.offset = 2;
                this.ID = 0x182E;
                uint gold = 0;
                this.PutByte((byte)value.Count, 10);
                this.PutByte((byte)value.Count, (ushort)(11 + value.Count * 4));
                int j = 0;
                foreach (uint i in value.Keys)
                {
                    this.PutUInt(i, (ushort)(11 + j * 4));
                    this.PutUShort(value[i].Count, (ushort)(12 + value.Count * 4 + j * 2));
                    gold += (uint)(value[i].Count * value[i].Price);
                    j++;
                }
                this.PutUInt(gold, 6);
            }
        }
    }
}

