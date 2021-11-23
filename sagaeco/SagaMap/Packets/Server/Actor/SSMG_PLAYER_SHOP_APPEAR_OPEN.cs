using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_GOLD_UPDATA: Packet
    {
        public SSMG_PLAYER_SHOP_GOLD_UPDATA()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x190F;
        }
        public uint SlotID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public ushort Count
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }
        public ulong gold
        {
            set
            {
                PutULong(value, 8);
            }
        }
    }
}

