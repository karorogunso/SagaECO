using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE_GET : Packet
    {
        public SSMG_PLAYER_TITLE_GET()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x241B;   
        }
    }
}

