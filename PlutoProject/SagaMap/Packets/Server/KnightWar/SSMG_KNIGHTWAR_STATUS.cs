using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_KNIGHTWAR_STATUS : Packet
    {
        public SSMG_KNIGHTWAR_STATUS()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x1B5D;
        }

        public int EastPoint
        {
            set
            {
                this.PutInt(value, 2);
            }
        }

        public int WestPoint
        {
            set
            {
                this.PutInt(value, 6);
            }
        }

        public int SouthPoint
        {
            set
            {
                this.PutInt(value, 10);
            }
        }

        public int NorthPoint
        {
            set
            {
                this.PutInt(value, 14);
            }
        }
    }
}

