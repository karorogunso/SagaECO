using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ENHANCE_LIST : Packet
    {
        public SSMG_ITEM_ENHANCE_LIST()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x13C4;
        }
        int count = 0;
        public List<Item> Items
        {
            set
            {
                this.data = new byte[10 + 4 * value.Count];
                this.offset = 2;
                this.ID = 0x13C4;

                PutByte((byte)value.Count);

                foreach (var item in value)
                {
                    PutUInt(item.Slot);
                }
                PutUInt(0u);
                PutByte(01);
                PutShort(100);
                PutByte(0);
            }
        }
    }
}

