using SagaLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Packets.Server
{
    public class SSMG_DUALJOB_SKILL_SEND : Packet
    {
        public SSMG_DUALJOB_SKILL_SEND()
        {
            this.data = new byte[36];
            this.offset = 2;
            this.ID = 0x22D2;
        }

        public List<SagaDB.Skill.Skill> Skills
        {
            set
            {
                this.PutByte(byte.Parse(value.Count.ToString()), offset);

                foreach (var item in value)
                {
                    this.PutUShort(ushort.Parse(item.ID.ToString()));
                }
            }
        }

        public List<SagaDB.Skill.Skill> SkillLevels
        {
            set
            {
                this.PutByte(byte.Parse(value.Count.ToString()), offset);

                foreach (var item in value)
                {
                    this.PutByte(item.Level);
                }
            }
        }
    }
}
