using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_IDENTIFY : Packet
    {
        public SSMG_ITEM_IDENTIFY()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x09D1;
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public bool Identify
        {
            set
            {
                if (value)
                    this.PutInt(this.GetInt(6) | 1, 6);
            }
        }

        public bool Lock
        {
            set
            {
                if (value)
                    this.PutInt(this.GetInt(6) | 0x20, 6);
            }
        }
    }
}

