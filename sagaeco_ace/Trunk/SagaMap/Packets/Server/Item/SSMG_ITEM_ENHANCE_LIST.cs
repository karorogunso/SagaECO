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
                count = value.Count;
            }
        }
        public uint price
        {
            set
            {
                uint p = value;
                if (p == 0) p = 5000;
                PutUInt(value, (ushort)(3 + 4 * count));
                PutByte(100, (ushort)(9 + 4 * count));
            }
        }
    }
}

