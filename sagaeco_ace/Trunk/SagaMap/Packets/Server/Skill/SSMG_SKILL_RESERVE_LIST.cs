using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Skill;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_RESERVE_LIST : Packet
    {
        public SSMG_SKILL_RESERVE_LIST()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x022E;   
        }

        public List<SagaDB.Skill.Skill> Skills
        {
            set
            {
                this.data = new byte[4 + 3 * value.Count];
                this.ID = 0x022E;
                this.PutByte((byte)value.Count, 2);
                this.PutByte((byte)value.Count, (ushort)(3 + 2 * value.Count));
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUShort((ushort)value[i].ID, (ushort)(3 + 2 * i));
                    this.PutByte(value[i].Level, (ushort)(4 + 2 * value.Count + i));
                }
            }
        }
    }
}

