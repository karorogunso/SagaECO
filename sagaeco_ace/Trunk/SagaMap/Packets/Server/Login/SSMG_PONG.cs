using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_PONG : Packet
    {
        public SSMG_PONG()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x33;   
        }

    }
}

