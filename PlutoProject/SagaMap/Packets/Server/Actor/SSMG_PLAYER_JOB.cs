using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_JOB : Packet
    {
        public SSMG_PLAYER_JOB()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x0244;
        }

        public PC_JOB Job
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }

        public PC_JOB JointJob
        {
            set
            {
                this.PutUInt((uint)value - 1000, 6);
            }
        }

        public ushort DualJob
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }
    }
}

