using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_TRADE_ITEM_FOOT : Packet
    {        
        public SSMG_TRADE_ITEM_FOOT()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x0A21;
        }        
    }
}

