using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_STAMP_USE : Packet
    {
        public SSMG_STAMP_USE()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1BC1;
        }

        public StampGenre Genre
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }

        public byte Slot
        {
            set
            {
                this.PutByte(value, 3);
            }
        }
    }
}