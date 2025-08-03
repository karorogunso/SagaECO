using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_EVENT_START : Packet
    {
        public SSMG_NPC_EVENT_START()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x05DC;
        }       
    }
}

