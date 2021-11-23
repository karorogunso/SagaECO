using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ACTIVITY_RECYCLE_WINDOW : Packet
    {
        public SSMG_ACTIVITY_RECYCLE_WINDOW()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x226B;
        }

        public ushort Percent
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }

        public uint PCount
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }
    }
}

