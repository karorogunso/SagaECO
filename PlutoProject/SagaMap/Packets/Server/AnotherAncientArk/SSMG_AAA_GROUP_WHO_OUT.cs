using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_AAA_GROUP_WHO_OUT : Packet
    {
        public SSMG_AAA_GROUP_WHO_OUT()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x23E7;
        }

        public byte Position
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public byte Reason
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
    }
}

