using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ACTOR_DISAPPEAR : Packet
    {
        public SSMG_ITEM_ACTOR_DISAPPEAR()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x07DF;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Count
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public uint Looter
        {
            set
            {
                this.PutUInt(value, 7);
            }
        }
    }
}

