using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_MASTERENHANCE_RESULT : Packet
    {
        public SSMG_ITEM_MASTERENHANCE_RESULT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x1F58;
        }

        public int Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}

