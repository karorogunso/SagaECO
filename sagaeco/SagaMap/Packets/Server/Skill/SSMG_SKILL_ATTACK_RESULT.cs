using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_ATTACK_RESULT : Packet
    {
        public SSMG_SKILL_ATTACK_RESULT()
        {
            this.data = new byte[47];
            this.offset = 2;
            this.ID = 0x0FA1;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint TargetID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public SagaDB.Actor.ATTACK_TYPE AttackType
        {
            set
            {
                this.PutByte((byte)value, 10);
            }
        }

        public int HP
        {
            set
            {
                PutLong(value, 11);
            }
        }

        public int MP
        {
            set
            {
                PutLong(value, 19);
            }
        }

        public int SP
        {
            set
            {
                PutLong(value, 27);
            }
        }

        public AttackFlag AttackFlag
        {
            set
            {
                PutUInt((uint)value, 35);
            }
        }

        public uint Delay
        {
            set
            {
                PutUInt(value, 39);
            }
        }

        public uint Unknown
        {
            set
            {
                PutUInt(value, 43);
            }
        }
    }
}

