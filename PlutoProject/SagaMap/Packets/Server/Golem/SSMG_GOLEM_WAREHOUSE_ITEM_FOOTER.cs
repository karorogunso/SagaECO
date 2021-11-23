using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_WAREHOUSE_ITEM_FOOTER : Packet
    {
        public SSMG_GOLEM_WAREHOUSE_ITEM_FOOTER()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x17F7;
        }
    }
}

