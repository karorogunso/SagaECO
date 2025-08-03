using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_PAPER_USE_RESULT : Packet
    {
        public SSMG_ANO_PAPER_USE_RESULT()
        {
            this.data = new byte[13];
            this.offset = 2;
            this.ID = 0x23A7;
        }

        public byte paperID
        {
            set
            {
                this.PutByte(value, 4);
            }
        }
        public ulong value
        {
            set
            {
                this.PutULong(value, 5);
            }
        }
    }
}

