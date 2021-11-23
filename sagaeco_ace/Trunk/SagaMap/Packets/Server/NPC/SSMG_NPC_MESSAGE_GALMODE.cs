using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_MESSAGE_GALMODE : Packet
    {
        public SSMG_NPC_MESSAGE_GALMODE()
        {
            this.data = new byte[19];
            this.offset = 2;
            this.ID = 0x0606;
            this.PutUInt(1, 2);
            this.PutUInt(1, 15);
        }

    }
}

