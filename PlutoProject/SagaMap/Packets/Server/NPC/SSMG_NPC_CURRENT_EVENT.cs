using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_CURRENT_EVENT : Packet
    {
        public SSMG_NPC_CURRENT_EVENT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x05E4;
        }

        public uint EventID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

