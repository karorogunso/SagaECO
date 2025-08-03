using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_WAIT : Packet
    {
        public SSMG_NPC_WAIT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x05EB;
        }

        public uint Wait
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

