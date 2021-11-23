using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_ITEM_ABILITY : Packet
    {
        public enum Types
        {
            Deck,
            Total,
            Max,
        }

        public SSMG_IRIS_CARD_ITEM_ABILITY()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x1DC5;
        }

        public Types Type
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }

        public List<SagaDB.Iris.AbilityVector> AbilityVectors
        {
            set
            {
                byte[] buff = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count, 3);
                foreach (SagaDB.Iris.AbilityVector i in value)
                {
                    this.PutUInt(i.ID);
                }
            }
        }

        public List<int> VectorValues
        {
            set
            {
                byte[] buff = new byte[this.data.Length + 2 * value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count);
                foreach (int i in value)
                {
                    this.PutShort((short)i);
                }
            }
        }

        public List<int> VectorLevels
        {
            set
            {
                byte[] buff = new byte[this.data.Length + value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count);
                foreach (int i in value)
                {
                    this.PutByte((byte)i);
                }
            }
        }

        public List<SagaDB.Iris.ReleaseAbility> ReleaseAbilities
        {
            set
            {
                byte[] buff = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count);
                foreach (SagaDB.Iris.ReleaseAbility i in value)
                {
                    this.PutInt((int)i);
                }
            }
        }

        public List<int> AbilityValues
        {
            set
            {
                byte[] buff = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count);
                foreach (int i in value)
                {
                    this.PutInt(i);
                }
            }
        }

        public Dictionary<Elements, int> ElementsAttack
        {
            set
            {
                byte[] buff = new byte[this.data.Length + 4 * value.Count + 1];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte(0);
                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUShort((ushort)value[(Elements)i]);
                }
            }
        }

        public Dictionary<Elements, int> ElementsDefence
        {
            set
            {
                byte[] buff = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUShort((ushort)value[(Elements)i]);
                }
            }
        }
    }
}

