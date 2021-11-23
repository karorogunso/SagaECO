using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_SKILL_MOVE : Packet
    {
        public SSMG_ACTOR_SKILL_MOVE()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x13AB;
          
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public short X
        {
            set
            {
                this.PutShort(value, 6);
            }
        }

        public short Y
        {
            set
            {
                this.PutShort(value, 8);
            }
        }
    }
}

