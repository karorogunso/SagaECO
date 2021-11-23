using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_KNIGHTWAR_BUFF : Packet
    {
        public SSMG_KNIGHTWAR_BUFF()
        {
            this.data = new byte[25];
            this.offset = 2;
            this.ID = 0x1B5E;
        }

        public byte exp
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        public byte buffID
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
        public int time
        {
            set
            {
                this.PutInt(value, 5);
                this.PutInt(value, 10);
                this.PutInt(value, 15);
                this.PutInt(value, 20);
            }
        }
    }
}

