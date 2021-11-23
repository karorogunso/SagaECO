using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SYNTHESE_RESULT : Packet
    {
        public SSMG_NPC_SYNTHESE_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13B8;
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

