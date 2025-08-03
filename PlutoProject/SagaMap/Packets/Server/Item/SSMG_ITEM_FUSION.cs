using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_FUSION : Packet
    {
        public SSMG_ITEM_FUSION()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x13D8;
        } 
    }
}

