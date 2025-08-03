using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_LEARN : Packet
    {
        public enum LearnResult
        {
            OK = 0,
            SKILL_NOT_EXIST = -1,
            NOT_ENOUGH_SKILL_POINT = -2,
            NOT_ENOUGH_JOB_LEVEL = -3,
            SKILL_NOT_LEARNED = -4,
            SKILL_MAX_LEVEL_EXEED = -5,
        }

        public SSMG_SKILL_LEARN()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x0228;            
        }

        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }

        public ushort SkillPoints
        {
            set
            {
                this.PutUShort(value, 4);
            }
        }

        public ushort SkillPoints2
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        public LearnResult Result
        {
            set
            {
                this.PutByte((byte)value, 8);
            }        
        }
    }
}

