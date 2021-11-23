using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_SPEED : Packet
    {
        public SSMG_ACTOR_SPEED()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x1239;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

    }
}

