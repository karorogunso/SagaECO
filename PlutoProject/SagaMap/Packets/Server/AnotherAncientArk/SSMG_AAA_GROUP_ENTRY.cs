using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_AAA_GROUP_ENTRY : Packet
    {
        public SSMG_AAA_GROUP_ENTRY()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x23E3;
        }

        public int GroupID
        {
            set
            {
                this.PutInt(value, 3);
            }
        }
    }
}

