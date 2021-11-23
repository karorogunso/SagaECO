using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_COUNT_UPDATE : Packet
    {
        public SSMG_PLAYER_SHOP_COUNT_UPDATE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x191C;
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public short ShopCount
        {
            set
            {
                this.PutShort(value, 6);
            }
        }
    }
}

