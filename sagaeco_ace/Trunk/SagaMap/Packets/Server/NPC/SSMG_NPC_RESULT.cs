using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_EVENT_START_RESULT : Packet
    {
        public SSMG_NPC_EVENT_START_RESULT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x05E3;
            this.PutUInt(0, 6);//unknown
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

