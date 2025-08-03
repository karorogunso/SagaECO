using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_RECRUIT_CREATE : Packet
    {
        public SSMG_COMMUNITY_RECRUIT_CREATE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1B8B;
        }

        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}

