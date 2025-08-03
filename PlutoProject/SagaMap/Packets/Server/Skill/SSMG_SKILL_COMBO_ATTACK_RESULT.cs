using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_COMBO_ATTACK_RESULT : Packet
    {
        byte combo;
        public SSMG_SKILL_COMBO_ATTACK_RESULT(byte combo)
        {
            this.data = new byte[26 + 33 * combo];
            this.offset = 2;
            this.ID = 0x0FA2;
            this.combo = combo;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public List<Actor> TargetID
        {
            set
            {
                this.PutByte(combo, 6);
                for (int i = 0; i < combo; i++)
                {
                    this.PutUInt(value[i].ActorID, (ushort)(7 + i * 4));
                }
            }
        }

        public SagaDB.Actor.ATTACK_TYPE AttackType
        {
            set
            {
                this.PutByte((byte)value, (ushort)(7 + combo * 4));
            }
        }

        public void SetHP(List<int> hp)
        {
            this.PutByte(combo, (ushort)(8 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                this.PutInt(hp[i], (ushort)(9 + combo * 4 + i * 4));
                this.PutInt(hp[i], (ushort)(9 + combo * 8 + i * 4));
            }
        }

        public void SetMP(List<int> mp)
        {
            this.PutByte(combo, (ushort)(9 + combo * 12));
            for (int i = 0; i < combo; i++)
            {
                this.PutInt(mp[i], (ushort)(10 + combo * 12 + i * 4));
                this.PutInt(mp[i], (ushort)(10 + combo * 16 + i * 4));
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)(10 + combo * 20));
            for (int i = 0; i < combo; i++)
            {
                this.PutInt(sp[i], (ushort)(11 + combo * 20 + i * 4));
                this.PutInt(sp[i], (ushort)(11 + combo * 24 + i * 4));
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(11 + combo * 28));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)(12 + combo * 28 + i * 4));
            }
        }

        public byte Unknown2
        {
            set
            {
                this.PutByte(combo, (ushort)(12 + combo * 32));
                for (int i = 0; i < combo; i++)
                {
                    this.PutByte(value, (ushort)(13 + combo * 32 + i));
                }
            }
        }

        public uint Delay
        {
            set
            {
                this.PutUInt(value, (short)(13 + combo * 33));
            }
        }

        public uint Unknown
        {
            set
            {
                this.PutUInt(value, (short)(17 + combo * 33));
            }
        }
        public uint SkillID
        {
            set
            {
                this.PutUInt(value, (short)(21 + combo * 33));
            }
        }
        public byte SkillLevel
        {
            set
            {
                this.PutByte(value, (short)(25 + combo * 33));
            }
        }
    }
}

