using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_COUNT_UPDATE : Packet
    {
        public SSMG_ITEM_COUNT_UPDATE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x09CF;   
        }
       
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ushort Stack
        {
            set
            {
                this.PutUShort(value, 6);
            }
        }

        
    }
}

