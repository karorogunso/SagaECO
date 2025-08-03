using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOW_HAIR_PREVIEW : Packet
    {
        public SSMG_NPC_SHOW_HAIR_PREVIEW()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0616;
        }

        public byte type
        {
            set
            {
                this.PutByte(value, 5);
            }
        }

    }
}

