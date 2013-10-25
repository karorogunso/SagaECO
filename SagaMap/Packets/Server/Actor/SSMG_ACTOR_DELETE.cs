using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_DELETE : Packet
    {
        public SSMG_ACTOR_DELETE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1211;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

    }
}

