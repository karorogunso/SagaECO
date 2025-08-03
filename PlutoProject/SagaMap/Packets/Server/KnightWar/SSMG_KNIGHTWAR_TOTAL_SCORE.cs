using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_KNIGHTWAR_TOTAL_SCORE : Packet
    {
        public SSMG_KNIGHTWAR_TOTAL_SCORE()
        {
            this.data = new byte[42];
            this.offset = 2;
            this.ID = 0x1B5C;
            this.PutUInt(0xffffffff, 22);
            this.PutUInt(0xffffffff, 26);
            this.PutUInt(0xffffffff, 30);
            this.PutUInt(0xffffffff, 34);
            this.PutUInt(2, 38);
        }

        public int Second
        {
            set
            {
                this.PutInt(value, 2);
            }
        }

        public int EastPoint
        {
            set
            {
                this.PutInt(value, 6);
            }
        }

        public int WestPoint
        {
            set
            {
                this.PutInt(value, 10);
            }
        }

        public int SouthPoint
        {
            set
            {
                this.PutInt(value, 14);
            }
        }

        public int NorthPoint
        {
            set
            {
                this.PutInt(value, 18);
            }
        }
    }
}

