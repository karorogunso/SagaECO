using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ACTIVE_FLOOR : Packet
    {
        byte combo;
        public SSMG_ITEM_ACTIVE_FLOOR(byte combo)
        {
            this.data = new byte[19 + 4 * combo + 6 * combo + 4 * combo];
            this.offset = 2;
            this.ID = 0x09C6;
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

        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)(13 + combo * 4));
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)(14 + combo * 4));
            }
        }

        public void SetHP(List<int> hp)
        {
            this.PutByte(combo, (ushort)(15 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                this.PutShort((short)hp[i], (ushort)(16 + combo * 4 + i * 2));
            }
        }

        public void SetMP(List<int> mp)
        {
            this.PutByte(combo, (ushort)(16 + combo * 4 + combo * 2));
            for (int i = 0; i < combo; i++)
            {
                this.PutShort((short)mp[i], (ushort)(17 + combo * 4 + combo * 2 + i * 2));
            }
        }

        public void SetSP(List<int> sp)
        {
            this.PutByte(combo, (ushort)(17 + combo * 4 + combo * 4));
            for (int i = 0; i < combo; i++)
            {
                this.PutShort((short)sp[i], (ushort)(18 + combo * 4 + combo * 4 + i * 2));
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            this.PutByte(combo, (ushort)(18 + combo * 4 + combo * 6));
            for (int i = 0; i < combo; i++)
            {
                this.PutUInt((uint)flag[i], (ushort)(19 + combo * 4 + combo * 6 + i * 4));
            }
        }
    }
}

