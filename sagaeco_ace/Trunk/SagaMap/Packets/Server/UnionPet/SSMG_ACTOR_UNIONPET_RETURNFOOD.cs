using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_UNIONPET_RETURNFOOD : Packet
    {
        public SSMG_ACTOR_UNIONPET_RETURNFOOD()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x219a;
            this.PutByte(1, 3);
        }

        public byte location
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }

    }
}

