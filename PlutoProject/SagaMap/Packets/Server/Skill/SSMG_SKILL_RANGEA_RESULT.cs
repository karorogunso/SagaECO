using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_RANGEA_RESULT : Packet
    {
        public SSMG_SKILL_RANGEA_RESULT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x0FAB;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }              
        }

        public uint Speed
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

    }
}

