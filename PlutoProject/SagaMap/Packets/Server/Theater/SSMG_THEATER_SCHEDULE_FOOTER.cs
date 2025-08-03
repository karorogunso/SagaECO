using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_THEATER_SCHEDULE_FOOTER : Packet
    {
        public SSMG_THEATER_SCHEDULE_FOOTER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1A9C;
        }

        public uint MapID
        {
            set
            {
                PutUInt(value, 2);
            }
        }
    }
}

