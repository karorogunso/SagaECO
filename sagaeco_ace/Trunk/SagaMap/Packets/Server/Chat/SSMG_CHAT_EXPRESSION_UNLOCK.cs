using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_EXPRESSION_UNLOCK : Packet
    {
        public SSMG_CHAT_EXPRESSION_UNLOCK()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1D06;
        }

        public uint unlock
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

