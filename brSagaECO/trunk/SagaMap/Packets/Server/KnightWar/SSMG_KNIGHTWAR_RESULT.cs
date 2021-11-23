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
            this.data = new byte[40];
            this.offset = 2;
            this.ID = 0x1B6C;
            this.PutByte(4, 2);
            this.PutByte(4, 7);

        }

        public Country Rank1Country
        {
            set
            {
                this.PutByte((byte)value, 3);
            }
        }

        public Country Rank2Country
        {
            set
            {
                this.PutByte((byte)value, 4);
            }
        }

        public Country Rank3ountry
        {
            set
            {
                this.PutByte((byte)value, 5);
            }
        }

        public Country Rank4Country
        {
            set
            {
                this.PutByte((byte)value, 6);
            }
        }

        public int Rank1Point
        {
            set
            {
                this.PutInt(value, 8);
            }
        }

        public int Rank2Point
        {
            set
            {
                this.PutInt(value, 12);
            }
        }

        public int Rank3Point
        {
            set
            {
                this.PutInt(value, 16);
            }
        }

        public int Rank4Point
        {
            set
            {
                this.PutInt(value, 20);
            }
        }

        public int RankingBonus
        {
            set
            {
                this.PutInt(value, 24);
            }
        }

        public int DeathPenalty
        {
            set
            {
                this.PutInt(value, 28);
            }
        }

        public int ScoreBonus
        {
            set
            {
                this.PutInt(value, 32);
            }
        }

        public int RewardBonus
        {
            set
            {
                this.PutInt(value, 36);
            }
        }
    
    }
}

