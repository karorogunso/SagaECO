using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_CAST_RESULT : Packet
    {
        public SSMG_SKILL_CAST_RESULT()
        {
            this.data = new byte[21];
            this.offset = 2;
            this.ID = 0x1389;   
        }

        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 2);
            }              
        }

        public byte Result
        {
            set
            {
                this.PutByte(value, 4);
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 5);
            }
        }

        public uint CastTime
        {
            set
            {
                this.PutUInt(value, 9);
            }
        }

        public uint TargetID
        {
            set
            {
                this.PutUInt(value, 13);
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 17);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 18);
            }
        }

        public byte SkillLv
        {
            set
            {
                this.PutByte(value, 19);
            }
        }
    }
}

