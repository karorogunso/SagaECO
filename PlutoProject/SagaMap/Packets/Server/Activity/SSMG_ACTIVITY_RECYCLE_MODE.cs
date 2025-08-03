using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTIVITY_RECYCLE_MODE : Packet
    {
        public SSMG_ACTIVITY_RECYCLE_MODE()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x2260;
        }

        public DateTime EndTime
        {
            set
            {
                uint date = (uint)(value - new DateTime(1970, 1, 1)).TotalSeconds;
                this.PutUInt(date, 2);
            }
        }

        public uint Result
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }
    }
}

