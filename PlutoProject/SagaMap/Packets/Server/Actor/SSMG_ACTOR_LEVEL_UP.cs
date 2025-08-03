using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_LEVEL_UP : Packet
    {
        public SSMG_ACTOR_LEVEL_UP()
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[43];
            else if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                this.data = new byte[41];
            else
                this.data = new byte[25];
            this.offset = 2;
            this.ID = 0x023F;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte Level
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public byte JobLevel
        {
            set
            {
                this.PutByte(value, 7);
            }
        }

        public int ExpPerc
        {
            set
            {
                this.PutInt(value, 8);
            }
        }

        public int JExpPerc
        {
            set
            {
                this.PutInt(value, 12);
            }
        }

        public long Exp
        {
            set
            {
                this.PutLong(value, 16);
            }
        }

        public long JExp
        {
            set
            {
                this.PutLong(value, 24);
            }
        }

        public short StatusPoints
        {
            set
            {
                this.PutShort(value, 32);
            }
        }

        public short SkillPoints
        {
            set
            {
                this.PutShort(value, 34);
            }
        }

        public short SkillPoints2X
        {
            set
            {
                this.PutShort(value, 36);
            }
        }

        public short SkillPoints2T
        {
            set
            {
                this.PutShort(value, 38);
            }
        }

        public short UnknownSkillPoints
        {
            set
            {
                this.PutShort(value, 40);
            }
        }

        public byte LvType
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                    this.PutByte(value, 42);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                    this.PutByte(value, 40);
                else
                    this.PutByte(value, 24);
            }
        }
    }
}

