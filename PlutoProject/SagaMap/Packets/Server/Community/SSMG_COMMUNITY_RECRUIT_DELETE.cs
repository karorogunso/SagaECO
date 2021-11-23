using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_RECRUIT_DELETE : Packet
    {
        public SSMG_COMMUNITY_RECRUIT_DELETE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1B95;
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

