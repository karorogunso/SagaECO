using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ADD :HasItemDetail
    {
        public SSMG_ITEM_ADD()
        {
            this.data = new byte[0xD7];
            this.offset = 2;
            this.ID = 0x09D4;
        }

        public Item Item
        {
            set
            {
                this.offset = 7;
                this.ItemDetail = value;
                this.PutByte((byte)(this.data.Length - 3), 2);
            }
        }
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public ContainerType Container
        {
            set
            {
                this.PutByte((byte)value, 15);
            }
        }

        
    }
}

