using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_TRADE_GOLD : Packet
    {        
        public SSMG_TRADE_GOLD()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0A1F;
        }

        public long Gold
        {
            set
            {
                this.PutLong(value, 2);
            }
        }
    }
}

