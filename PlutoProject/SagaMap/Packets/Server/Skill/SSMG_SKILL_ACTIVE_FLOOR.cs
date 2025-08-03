using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_ACTIVE_FLOOR : Packet
    {
        byte combo;
        byte set = 1;
        public SSMG_SKILL_ACTIVE_FLOOR(byte combo)
        {
            this.data = new byte[17 + 33 * combo];
            this.combo = combo;
           
            this.offset = 2;
            this.ID = 0x138D;
        }

        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 2);
                this.PutByte(combo, 4);
                for (int i = 0; i < combo; i++)
                {
                    this.PutByte(0, 5 + i);
                }
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 5 + combo);
            }
        }
        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(combo, this.set + 8 + combo);
                for (int i = 0; i < combo; i++)
                {
                    if (value[i] != null)
                        this.PutUInt(value[i].ActorID, (ushort)((this.set + 9) + combo + i * 4));
                    else
                        this.PutUInt(0xFFFFFFFF, (ushort)((this.set + 9) + combo + i * 4));

                }
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)((this.set + 9) + combo * 4 + combo));
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)((this.set + 10) + combo * 4 + combo));
            }
        }

        public void SetHP(List<int> hp)
        {
            this.PutByte(combo, (ushort)((this.set + 11) + combo * 4 + combo));
            for (int i = 0; i < combo; i++)
            {
                this.PutInt(hp[i], (ushort)((this.set + 12) + combo * 4 + combo + i * 4));
                this.PutInt(hp[i], (ushort)((this.set + 12) + combo * 8 + combo + i * 4));
            }
        }

        public void SetMP(List<int> mp)
        {

            this.PutByte(combo, (ushort)((this.set + 12) + combo * 4 + combo + combo * 8));
            for (int i = 0; i < combo; i++)
            {
                this.PutInt(mp[i], (ushort)((this.set + 13) + combo * 4 + combo + combo * 8 + i * 4));
                this.PutInt(mp[i], (ushort)((this.set + 13) + combo * 8 + combo + combo * 8 + i * 4));
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)((this.set + 13) + combo * 4 + combo + combo * 16));
            for (int i = 0; i < combo; i++)
            {
                this.PutInt(sp[i], (ushort)((this.set + 14) + combo * 4 + combo + combo * 16 + i * 4));
                this.PutInt(sp[i], (ushort)((this.set + 14) + combo * 8 + combo + combo * 16 + i * 4));
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)((this.set + 14) + combo * 4 + combo + combo * 24));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)((this.set + 15) + combo * 4 + combo + combo * 24 + i * 4));
            }
        }

        public byte SkillLv
        {
            set
            {
                this.PutByte(value, (ushort)((this.set + 15) + combo * 4 + combo + combo * 24 + combo * 4));

            }
        }
    }
}

