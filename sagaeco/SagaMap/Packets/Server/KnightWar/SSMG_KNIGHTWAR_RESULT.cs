using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_KNIGHTWAR_RESULT : Packet
    {
        public SSMG_KNIGHTWAR_RESULT()
        {
            this.data = new byte[73];
            this.offset = 2;
            this.ID = 0x1B6C;
            this.PutByte(2, 5);
            this.PutByte(4, 6);
            this.PutByte(4, 11);
            this.PutByte(4, 28);
            this.PutInt(-1, 29);
            this.PutInt(-1, 33);
            this.PutInt(-1, 37);
            this.PutInt(-1, 41);
        }

        public Country Rank1Country
        {
            set
            {
                this.PutByte((byte)value, 7);
            }
        }

        public Country Rank2Country
        {
            set
            {
                this.PutByte((byte)value, 8);
            }
        }

        public Country Rank3ountry
        {
            set
            {
                this.PutByte((byte)value, 9);
            }
        }

        public Country Rank4Country
        {
            set
            {
                this.PutByte((byte)value, 10);
            }
        }

        public int Rank1Point
        {
            set
            {
                this.PutInt(value, 12);
            }
        }

        public int Rank2Point
        {
            set
            {
                this.PutInt(value, 16);
            }
        }

        public int Rank3Point
        {
            set
            {
                this.PutInt(value, 20);
            }
        }

        public int Rank4Point
        {
            set
            {
                this.PutInt(value, 24);
            }
        }

        public int ExpBonus
        {
            set
            {
                this.PutInt(value, 45);
            }
        }

        public int ExpPenalty
        {
            set
            {
                this.PutInt(value, 49);
            }
        }

        public int ExpScoreBonus
        {
            set
            {
                this.PutInt(value, 53);
            }
        }

        public int JexpBonus
        {
            set
            {
                this.PutInt(value, 57);
            }
        }
        public int JexpPenalty
        {
            set
            {
                this.PutInt(value, 61);
            }
        }

        public int JexpScoreBonus
        {
            set
            {
                this.PutInt(value, 65);
            }
        }
        public int CPBouns
        {
            set
            {
                this.PutInt(value, 69);
            }
        }
    }
}

