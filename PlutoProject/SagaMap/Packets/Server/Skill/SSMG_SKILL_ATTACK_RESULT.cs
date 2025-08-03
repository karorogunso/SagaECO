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
            if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
            {
                this.data = new byte[29];
                this.offset = 2;
                this.ID = 0x0FA1;
            }
            if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
            {
                this.data = new byte[47];
                this.offset = 2;
                this.ID = 0x0FA1;
            }
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
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                    this.PutShort((short)value, 11);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                {
                    this.PutInt(value, 11);
                    this.PutInt(value, 15);
                }
            }
        }

        public int MP
        {
            set
            {
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                    this.PutShort((short)value, 13);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                {
                    this.PutInt(value, 19);
                    this.PutInt(value, 23);
                }
            }
        }

        public int SP
        {
            set
            {
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                    this.PutShort((short)value, 15);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                {
                    this.PutInt(value, 27);
                    this.PutInt(value, 31);
                }
            }
        }

        public AttackFlag AttackFlag
        {
            set
            {
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9) 
                    this.PutUInt((uint)value, 17);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                    this.PutUInt((uint)value, 35);
            }
        }

        public uint Delay
        {
            set
            {
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                    this.PutUInt(value, 21);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                    this.PutUInt(value, 39);
            }
        }

        public uint Unknown
        {
            set
            {
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                    this.PutUInt(value, 25);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                    this.PutUInt(value, 43);
            }
        }
    }
}

