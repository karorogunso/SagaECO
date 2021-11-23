using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOWPICT_CANCEL : Packet
    {
        public SSMG_NPC_SHOWPICT_CANCEL()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x05E1;
        }

        public uint NPCID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}

