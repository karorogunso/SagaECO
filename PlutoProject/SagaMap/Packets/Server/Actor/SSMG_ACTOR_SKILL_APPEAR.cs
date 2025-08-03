using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTOR_SKILL_APPEAR : Packet
    {
        public SSMG_ACTOR_SKILL_APPEAR()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x13A1;
          
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 8);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 9);
            }
        }

        public ushort Speed
        {
            set
            {
                this.PutUShort(value, 10);
            }
        }

        public byte SkillLv
        {
            set
            {
                this.PutByte(value, 12);
            }
        }

        public byte Dir
        {
            set
            {
                this.PutByte(value, 13);
            }
        }

    }
}

