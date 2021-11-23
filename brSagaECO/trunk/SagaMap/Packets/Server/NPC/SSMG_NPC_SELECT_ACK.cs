using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SELECT_ACK : Packet
    {
        public SSMG_NPC_SELECT_ACK()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x05F8;
        }
        public byte Result
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}

