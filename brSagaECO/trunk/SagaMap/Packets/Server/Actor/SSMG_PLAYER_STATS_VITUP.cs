using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_STATS_VITUP : Packet
    {
        public SSMG_PLAYER_STATS_VITUP()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x025D;
            //this.PutByte(0x20, 2);
        }
        public byte STATS
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        /* str110 = 1
         * dex110 = 2
         * int110 = 4
         * dex110 = 8
         * agi110 = 16
         * mag110 = 32*/
    }
}

