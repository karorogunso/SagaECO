using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_CHANGE_VIEW : Packet
    {
        public SSMG_NPC_CHANGE_VIEW()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0xFFFE;
        }

        public uint NPCID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint MobID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

    }
}

