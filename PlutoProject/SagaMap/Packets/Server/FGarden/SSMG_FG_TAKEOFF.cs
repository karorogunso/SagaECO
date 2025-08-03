using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;


namespace SagaMap.Packets.Server
{
    public class SSMG_FG_TAKEOFF : Packet
    {
        public SSMG_FG_TAKEOFF()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x18E3;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint MapID
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}

