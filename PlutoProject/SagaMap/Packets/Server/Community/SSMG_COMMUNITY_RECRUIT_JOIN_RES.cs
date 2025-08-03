using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{

    public class SSMG_COMMUNITY_RECRUIT_JOIN_RES : Packet
    {
       
        public SSMG_COMMUNITY_RECRUIT_JOIN_RES()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1BA9;
        }

        public int Result
        {
            set
            {
                PutInt(value, 2);
            }
        }

        public uint CharID
        {
            set
            {
                PutUInt(value, 6);
            }
        }
        
    }
}

