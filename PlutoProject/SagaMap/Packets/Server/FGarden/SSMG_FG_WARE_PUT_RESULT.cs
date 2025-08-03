using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;

namespace SagaMap.Packets.Server
{
    public class SSMG_FG_WARE_PUT_RESULT : Packet
    {
        public SSMG_FG_WARE_PUT_RESULT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1c30;
        }
    }
}