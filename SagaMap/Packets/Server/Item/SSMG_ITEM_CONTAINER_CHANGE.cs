using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_CONTAINER_CHANGE : Packet
    {
        public SSMG_ITEM_CONTAINER_CHANGE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x09E3;   
        }
       
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public ContainerType Target
        {
            set
            {
                this.PutByte((byte)value, 7);
            }
        }

        
    }
}

