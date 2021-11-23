using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_KNIGHTWAR_SCORE : Packet
    {
        public SSMG_KNIGHTWAR_SCORE()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1B62;
        }

        public int Score
        {
            set
            {
                this.PutInt(value, 2);
            }
        }

        public int DeathCount
        {
            set
            {
                this.PutInt(value, 6);
            }
        }
    }
}

