using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_AAA_GROUP_START : Packet
    {
        public SSMG_AAA_GROUP_START()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x0;
        }

        public byte Result
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}

