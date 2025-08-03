using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_BBS_OPEN : Packet
    {
        public SSMG_COMMUNITY_BBS_OPEN()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1AF4;
        }

        public uint Gold
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

