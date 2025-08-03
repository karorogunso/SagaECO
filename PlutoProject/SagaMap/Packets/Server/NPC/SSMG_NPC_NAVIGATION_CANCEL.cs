using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_NAVIGATION_CANCEL : Packet
    {
        public SSMG_NPC_NAVIGATION_CANCEL()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x1A2D;
            this.PutByte(0, 2);
        }
    }
}

