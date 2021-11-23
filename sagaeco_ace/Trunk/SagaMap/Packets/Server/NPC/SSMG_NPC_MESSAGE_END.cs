using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_MESSAGE_END : Packet
    {
        public SSMG_NPC_MESSAGE_END()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x03F9;
        }       
    }
}

