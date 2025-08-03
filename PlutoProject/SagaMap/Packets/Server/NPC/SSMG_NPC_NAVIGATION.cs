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
            this.Type = 255;
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
        public byte Type
        {
            set
            {
                this.PutByte(value, 4);
            }
        }

    }
}

