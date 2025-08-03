using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_SHOW_INFOBOX : Packet
    {
        public SSMG_ANO_SHOW_INFOBOX()
        {
            this.data = new byte[436];
            this.offset = 2;
            this.ID = 0x23A5;
            this.PutByte(0, 2);//unknown
            this.PutByte(1, 4);//Page
            this.PutByte(1, 5);//MaxPage
            this.PutByte(7, 14);
            this.PutByte(7, 29);
            this.PutByte(7, 86);
            this.PutByte(7, 94);
            this.PutByte(7, 151);
            this.PutByte(7, 208);
            this.PutByte(7, 265);
            this.PutByte(7, 322);
            this.PutByte(7, 379);
        }
        public byte index
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
        public ulong cexp
        {
            set
            {
                this.PutULong(value, 6);
            }
        }
        public ushort usingPaperID
        {
            set
            {
                this.PutUShort(value, 15);
            }
        }
        public List<ushort> papersID
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUShort(value[i], 17 + i * 2);
                }
            }
        }
        public ulong usingPaperValue
        {
            set
            {
                PutULong(value, 30);
            }
        }
        public List<ulong> paperValues
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    PutULong(value[i], (ushort)(38 + i * 8));
                }
            }
        }
        public byte usingLv
        {
            set
            {
                PutByte(value, 87);
            }
        }
        public List<byte> papersLv
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    PutByte(value[i], 88 + i);
                }
            }
        }
        public ulong usingSkillEXP_1
        {
            set
            {
                PutULong(value, 95);
            }
        }
        public List<ulong> paperSkillsEXP_1
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    PutULong(value[i], (ushort)(103+i*8));
                }
            }
        }
        public ulong usingSkillEXP_2
        {
            set
            {
                PutULong(value, 152);
            }
        }
        public List<ulong> paperSkillsEXP_2
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    PutULong(value[i], (ushort)(160 + i * 8));
                }
            }
        }
        public ulong usingSkillEXP_3
        {
            set
            {
                PutULong(value, 209);
            }
        }
        public List<ulong> paperSkillsEXP_3
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    PutULong(value[i], (ushort)(217 + i * 8));
                }
            }
        }
        public ulong usingSkillEXP_4
        {
            set
            {
                PutULong(value, 266);
            }
        }
        public List<ulong> paperSkillsEXP_4
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    PutULong(value[i], (ushort)(274 + i * 8));
                }
            }
        }
    }
}

