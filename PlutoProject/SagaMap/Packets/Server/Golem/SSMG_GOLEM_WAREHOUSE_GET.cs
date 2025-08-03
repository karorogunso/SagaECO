using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_WAREHOUSE_GET : Packet
    {
        public SSMG_GOLEM_WAREHOUSE_GET()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x17F9;
        }
    }
}

