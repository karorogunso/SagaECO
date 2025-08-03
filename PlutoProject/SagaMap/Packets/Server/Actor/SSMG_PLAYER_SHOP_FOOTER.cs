using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_FOOTER : Packet
    {
        public SSMG_PLAYER_SHOP_FOOTER()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x1919;
        }
    }
}

