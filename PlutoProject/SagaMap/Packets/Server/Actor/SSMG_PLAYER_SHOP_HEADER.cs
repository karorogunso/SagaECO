using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_HEADER : Packet
    {
        public SSMG_PLAYER_SHOP_HEADER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1914;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

