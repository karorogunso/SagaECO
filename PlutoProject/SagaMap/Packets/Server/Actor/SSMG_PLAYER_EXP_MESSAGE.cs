using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_EXP_MESSAGE : Packet
    {
        public SSMG_PLAYER_EXP_MESSAGE()
        {
            this.data = new byte[27];
            this.offset = 2;
            this.ID = 0x0238;
        }

        public enum EXP_MESSAGE_TYPE
        {
            NormalGain,
            PossessionGain,
            Loss,
            TamaireGain
        }

        /// <summary>
        /// Base EXP
        /// </summary>
        public long EXP
        {
            set
            {
                this.PutLong(value, 2);
            }
        }
        /// <summary>
        /// Job EXP
        /// </summary>
        public long JEXP
        {
            set
            {
                this.PutLong(value, 10);
            }
        }

        /// <summary>
        /// Another Page EXP
        /// </summary>
        public long PEXP
        {
            set
            {
                this.PutLong(value, 18);
            }
        }

        public EXP_MESSAGE_TYPE Type
        {
            set
            {
                this.PutByte((byte)value,26);
            }
        }
    }
}

