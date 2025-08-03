using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_ANSWER : Packet
    {
        public SSMG_PLAYER_SHOP_ANSWER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x191B;
        }

        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}

