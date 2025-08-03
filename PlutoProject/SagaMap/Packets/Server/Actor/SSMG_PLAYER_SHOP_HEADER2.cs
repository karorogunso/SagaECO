using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SHOP_HEADER2 : Packet
    {
        public SSMG_PLAYER_SHOP_HEADER2()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1917;
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

