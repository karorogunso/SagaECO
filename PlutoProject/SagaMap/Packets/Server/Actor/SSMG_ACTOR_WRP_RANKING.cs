using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_WRP_RANKING : Packet
    {
        public SSMG_ACTOR_WRP_RANKING()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0236;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint Ranking
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

    }
}

