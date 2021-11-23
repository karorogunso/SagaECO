using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_PET_DELETE : Packet
    {
        public SSMG_ACTOR_PET_DELETE()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1234;
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

