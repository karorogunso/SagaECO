using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_MASTERENHANCE_LIST : Packet
    {
        public SSMG_ITEM_MASTERENHANCE_LIST()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1F54;
        }

        public List<Item> Items
        {
            set
            {
                byte[] buf = new byte[4 + 4 * value.Count];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                PutByte((byte)value.Count, 2);
                int j = 0;
                foreach (Item i in value)
                {
                    PutUInt(i.Slot, (ushort)(3 + 4 * j));
                    j++;
                }
                this.PutByte(0x0, 4 + 4 * value.Count);
            }
        }
    }
}

