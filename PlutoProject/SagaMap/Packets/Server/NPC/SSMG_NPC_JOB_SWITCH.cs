using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_JOB_SWITCH : Packet
    {
        public SSMG_NPC_JOB_SWITCH()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x02BC;
        }

        public PC_JOB Job
        {
            set
            {
                this.PutUShort((ushort)value, 2);
            }
        }

        public byte LevelReduced
        {
            set
            {
                this.PutByte(value, 4);
            }
        }

        public byte Level
        {
            set
            {
                this.PutByte(value, 5);
            }
        }

        public uint LevelItem
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint ItemCount
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public ushort PossibleReserveSkills
        {
            set
            {
                this.PutUShort(value, 14);
            }
        }

        public List<SagaDB.Skill.Skill> PossibleSkills
        {
            set
            {
                byte[] buff = new byte[18 + 3 * value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count, (ushort)16);
                this.PutByte((byte)value.Count, (ushort)(17 + 2 * value.Count));

                int j = 0;
                foreach (SagaDB.Skill.Skill i in value)
                {
                    this.PutUShort((ushort)i.ID, (ushort)(17 + 2 * j));
                    this.PutByte(i.Level, (ushort)(18 + 2 * value.Count + j));
                    j++;
                }
            }
        }
    }
}

