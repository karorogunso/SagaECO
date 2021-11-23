using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_GOLD_UPDATE : Packet
    {
        /// <summary>
        /// [00][06][09][EC]
        /// [00][00][27][10] //10000Gold
        /// </summary>
        public SSMG_PLAYER_GOLD_UPDATE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x09EC;
        }

        public uint Gold
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

