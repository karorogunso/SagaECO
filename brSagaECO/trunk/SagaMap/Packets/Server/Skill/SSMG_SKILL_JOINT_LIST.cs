using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Skill;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_JOINT_LIST : Packet
    {
        public SSMG_SKILL_JOINT_LIST()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x022F;   
        }

        public List<SagaDB.Skill.Skill> Skills
        {
            set
            {
                this.data = new byte[3 + 2 * value.Count];
                this.ID = 0x022F;
                this.PutByte((byte)value.Count, 2);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUShort((ushort)value[i].ID, (ushort)(3 + 2 * i));
                }
            }
        }
    }
}

