using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_THEATER_SCHEDULE_HEADER : Packet
    {
        public SSMG_THEATER_SCHEDULE_HEADER()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1A9A;
        }

        public uint MapID
        {
            set
            {
                PutUInt(value, 2);
            }
        }

        public int Count
        {
            set
            {
                PutInt(value, 6);
            }
        }
    }
}

