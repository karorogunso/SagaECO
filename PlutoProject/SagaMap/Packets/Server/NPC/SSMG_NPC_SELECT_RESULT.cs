using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SELECT_RESULT : Packet
    {
        public SSMG_NPC_SELECT_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x05F8;
            this.PutByte(0, 2);//unknown
        }

    }
}

