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
            if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                this.data = new byte[22 + 4 * combo + 6 * combo + 4 * combo];
            if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                //this.data = new byte[23 + 4 * combo + 12 * combo + 4 * combo + 12 * combo];
                this.data = new byte[22 + combo * 4 + combo * 28];
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
            if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
            {
                this.PutByte(combo, (ushort)(17 + combo * 4));
                for (int i = 0; i < combo; i++)
                {
                    this.PutShort((short)hp[i], (ushort)(18 + combo * 4 + i * 2));
                }
            }

            if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
            {
                this.PutByte(combo, (ushort)(17 + combo * 4));
                for (int i = 0; i < combo; i++)
                {
                    this.PutLong((long)hp[i], (ushort)(18 + combo * 4 + i * 8));
                    //this.PutInt(hp[i], (ushort)(18 + combo * 8 + i * 4));
                }
            }
        }

        public void SetMP(List<int> mp)
        {
            if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
            {
                this.PutByte(combo, (ushort)(18 + combo * 4 + combo * 2));
                for (int i = 0; i < combo; i++)
                {
                    this.PutShort((short)mp[i], (ushort)(19 + combo * 4 + combo * 2 + i * 2));
                }
            }

            if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
            {
                this.PutByte(combo, (ushort)(18 + combo * 4 + combo * 8));
                for (int i = 0; i < combo; i++)
                {
                    this.PutInt(mp[i], (ushort)(19 + combo * 4 + combo * 12 + i * 4));
                    this.PutInt(mp[i], (ushort)(19 + combo * 4 + combo * 16 + i * 4));
                }
            }
        }

        public void SetSP(List<int> sp)
        {
            if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
            {
                this.PutByte(combo, (ushort)(19 + combo * 4 + combo * 4));
                for (int i = 0; i < combo; i++)
                {
                    this.PutShort((short)sp[i], (ushort)(20 + combo * 4 + combo * 4 + i * 2));
                }
            }

            if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
            {
                this.PutByte(combo, (ushort)(19 + combo * 4 + combo * 16));
                for (int i = 0; i < combo; i++)
                {
                    this.PutInt(sp[i], (ushort)(20 + combo * 4 + combo * 20 + i * 4));
                    this.PutInt(sp[i], (ushort)(20 + combo * 4 + combo * 24 + i * 4));
                }
            }
        }

        public void AttackFlag(List<AttackFlag> flag)
        {
            if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
            {
                this.PutByte(combo, (ushort)(20 + combo * 4 + combo * 6));
                for (int i = 0; i < combo; i++)
                {
                    this.PutUInt((uint)flag[i], (ushort)(21 + combo * 4 + combo * 6 + i * 4));
                }
            }

            if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
            {
                this.PutByte(combo, (ushort)(20 + combo * 4 + combo * 24));
                for (int i = 0; i < combo; i++)
                {
                    this.PutUInt((uint)flag[i], (ushort)(21 + combo * 4 + combo * 24 + i * 4));
                }
            }
        }

        public byte SkillLv
        {
            set
            {
                if (Configuration.Instance.Version <= SagaLib.Version.Saga9)
                    this.PutByte(value, (ushort)(21 + combo * 4 + combo * 6 + combo * 4));

                if (Configuration.Instance.Version >= SagaLib.Version.Saga9_2)
                    this.PutByte(value, (ushort)(21 + combo * 4 + combo * 28));
            }
        }
    }
}

