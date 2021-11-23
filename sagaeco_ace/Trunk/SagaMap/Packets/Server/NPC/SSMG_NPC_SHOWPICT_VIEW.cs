using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOWPICT_VIEW : Packet
    {
        public SSMG_NPC_SHOWPICT_VIEW()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x05E4;
        }

        public uint NPCID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint PictID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}

