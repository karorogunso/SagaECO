using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_APPEAR2 : Packet
    {
        public SSMG_PLAYER_SHOP_APPEAR2()
        {
            this.data = new byte[6]; //TitleBytes.Length+2+4+5
            this.offset = 2;
            this.ID = 0x190E;
        }

    }
}

