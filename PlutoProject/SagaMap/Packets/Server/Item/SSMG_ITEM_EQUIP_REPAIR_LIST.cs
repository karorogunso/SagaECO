using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_EQUIP_REPAIR_LIST : Packet
    {
        public SSMG_ITEM_EQUIP_REPAIR_LIST()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x13BF;
        }


        public List<Item> Items
        {
            set
            {
                byte[] buf = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, 2);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i].Slot, (ushort)(3 + 4 * i));
                }

            }
        }
    }
}

