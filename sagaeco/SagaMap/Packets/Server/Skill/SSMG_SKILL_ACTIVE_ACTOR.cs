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
                PutLong(hp[i], (ushort)(8 + combo * 4 + i * 8));
            }
        }

        public void SetMP(List<int> mp)
        {
            this.PutByte(combo, (ushort)(8 + combo * 4 + combo * 8));//20
            for (int i = 0; i < combo; i++)
            {
                PutLong(mp[i], (ushort)(9 + combo * 4 + combo * 8 + i * 8));
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)(9 + combo * 4 + combo * 16));//29
            for (int i = 0; i < combo; i++)
            {
                PutLong(sp[i], (ushort)(10 + combo * 4 + combo * 16 + i * 8));
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(10 + combo * 4 + combo * 24));//38
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)(11 + combo * 4 + combo * 24 + i * 8));//39
            }
        }
    }
}

