using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_PAPER_COMPOUND_RESULT : Packet
    {
        public SSMG_ANO_PAPER_COMPOUND_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x23A9;
        }
        public byte paperID
        {
            set
            {
                this.PutByte(value, 4);
            }
        }
        public byte lv
        {
            set
            {
                this.PutByte(value, 5);
            }
        }
    }
}

