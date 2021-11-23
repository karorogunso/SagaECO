using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_NAVIGATION : Packet
    {
        public SSMG_NPC_NAVIGATION()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1A2C;
        }

        public byte X
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public byte Y
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

    }
}

