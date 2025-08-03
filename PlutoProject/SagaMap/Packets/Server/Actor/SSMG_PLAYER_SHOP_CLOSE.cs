using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_CLOSE : Packet
    {
        public SSMG_PLAYER_SHOP_CLOSE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1916;
        }

        public int Reason
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}

