using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ACTIVE_SELF : Packet
    {
        byte combo;
        public SSMG_ITEM_ACTIVE_SELF(byte combo)
        {
            this.data = new byte[17 + 4 * combo + 24 * combo + 4 * combo];
            this.offset = 2;
            this.ID = 0x09C8;
            this.combo = combo;
            this.PutByte(1, 6);
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }

        public List<SagaDB.Actor.Actor> AffectedID
        {
            set
            {
                this.PutByte(combo, 12);
                for (int i = 0; i < combo; i++)
                {
                    this.PutUInt(value[i].ActorID, (ushort)(13 + i * 4));
                }
            }
        }

        public void SetHP(List<int> hp)
        {
            this.PutByte(combo, (ushort)(13 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)hp[i], (ushort)(14 + combo * 4 + i * 4));
                this.PutUInt((uint)hp[i], (ushort)(14 + combo * 8 + i * 4));
            }
        }

        public void SetMP(List<int> mp)
        {
            this.PutByte(combo, (ushort)(14 + combo * 4 + combo * 8));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)mp[i], (ushort)(15 + combo * 4 + combo * 8 + i * 4));
                this.PutUInt((uint)mp[i], (ushort)(15 + combo * 4 + combo * 12 + i * 4));
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)(15 + combo * 4 + combo * 16));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)sp[i], (ushort)(16 + combo * 4 + combo * 16 + i * 4));
                this.PutUInt((uint)sp[i], (ushort)(16 + combo * 4 + combo * 20 + i * 4));
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(16 + combo * 4 + combo * 24));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)(17 + combo * 4 + combo * 24 + i * 4));
            }
        }
    }
}