using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_HIDE : Packet
    {
        public SSMG_NPC_HIDE()
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

