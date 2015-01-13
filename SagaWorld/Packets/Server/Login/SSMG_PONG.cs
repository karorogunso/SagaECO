using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_PONG : Packet
    {
        public SSMG_PONG()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0B;   
        }

    }
}

