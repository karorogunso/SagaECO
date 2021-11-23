using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_ACTIVE_ACTOR : Packet
    {
        byte combo;
        public SSMG_SKILL_ACTIVE_ACTOR(byte combo)
        {
            this.data = new byte[11 + 4 * combo + 12 * combo + 4 * combo];
            this.offset = 2;
            this.ID = 0x13B0;
            this.combo = combo;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public List<SagaDB.Actor.Actor> AffectedID
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

        public void SetHP(List<int> hp)
        {
            this.PutByte(combo, (ushort)(7 + combo * 4));//11
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)hp[i], (ushort)(8 + combo * 4 + i * 2));//12
                this.PutUInt((uint)hp[i], (ushort)(8 + combo * 8 + i * 2));//16
            }
        }

        public void SetMP(List<int> mp)
        {
            this.PutByte(combo, (ushort)(8 + combo * 4 + combo * 8));//20
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)mp[i], (ushort)(9 + combo * 4 + combo * 8 + i * 4));//21
                this.PutUInt((uint)mp[i], (ushort)(9 + combo * 4 + combo * 8 + combo * 4 + i * 4));//25
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)(9 + combo * 4 + combo * 16));//29
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)sp[i], (ushort)(10 + combo * 4 + combo * 16 + i * 4));//30
                this.PutUInt((uint)sp[i], (ushort)(10 + combo * 4 + combo * 16 + combo * 4 + i * 4));//34
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(10 + combo * 4 + combo * 24));//38
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)(11 + combo * 4 + combo * 24 + i * 4));//39
            }
        }
    }
}

