using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_EVENT_ACK :Packet
    {
        public SSMG_NPC_EVENT_ACK()
        {
            this.data = new byte[3];
            this.ID = 0x05F4;
            this.PutByte(0,2);
        }

    }
}
