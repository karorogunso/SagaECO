using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_ACTIVE : Packet
    {
        byte combo;
        public SSMG_SKILL_ACTIVE(byte combo)
        {
            this.data = new byte[22 + 4 * combo + 12 * combo + 4 * combo+12*combo];
            this.offset = 2;
            this.ID = 0x1392;
            this.combo = combo;
            this.PutByte(1, 4);
        }

        public ushort SkillID
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint TargetID
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }

        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(combo, 14);
                for (int i = 0; i < combo; i++)
                {
                    this.PutUInt(value[i].ActorID, (ushort)(15 + i * 4));
                }
            }
        }

        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)(15 + combo * 4));                
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)(16 + combo * 4));
            }
        }

        public void SetHP(List<int> hp)
        {
            this.PutByte(combo, (ushort)(17 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                PutULong((ulong)hp[i], (ushort)(18 + combo * 4 + i * 8));
            }
        }

        public void SetMP(List<int> mp)
        {
            this.PutByte(combo, (ushort)(18 + combo * 4 + combo * 8));
            for (int i = 0; i < combo; i++)
            {
                PutULong((ulong)mp[i], (ushort)(19 + combo * 12 + i * 8));
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)(19 + combo * 4 + combo * 16));
            for (int i = 0; i < combo; i++)
            {
                PutULong((ulong)sp[i], (ushort)(20 + combo * 20 + i * 8));
            }

        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(20 + combo * 4 + combo * 24));
            for (int i = 0; i < combo; i++)
            {
                PutUInt((uint)flag[i], (ushort)(21 + combo * 4 + combo * 24 + i * 4));
            }
        }

        public byte SkillLv
        {
            set
            {
                PutByte(value, (ushort)(21 + combo * 4 + combo * 24 + combo * 4));
            }
        }
    }
}

