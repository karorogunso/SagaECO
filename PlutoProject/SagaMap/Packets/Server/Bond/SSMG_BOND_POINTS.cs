using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_POINTS : Packet
    {
        public SSMG_BOND_POINTS()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x1FEC;
        }
        public int TeachingPoint
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
        public int AchievementPoint
        {
            set
            {
                this.PutInt(value, 6);
            }
        }
        public byte StudentLimit
        {
            set
            {
                this.PutByte(value, 10);
            }
        }
    }
}