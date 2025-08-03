using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_CHANGE_ADD : Packet
    {
        public SSMG_ITEM_CHANGE_ADD()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x022D;
            this.PutByte(3, 2);
            this.PutUShort(10100, 3);
            this.PutUShort(10101, 5);
            this.PutUShort(10102, 7);
        }
    }
}

