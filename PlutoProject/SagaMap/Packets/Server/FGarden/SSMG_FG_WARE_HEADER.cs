using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;

namespace SagaMap.Packets.Server
{
    public class SSMG_FG_WARE_HEADER : Packet
    {
        public SSMG_FG_WARE_HEADER()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1c25;
        }
    }
}
