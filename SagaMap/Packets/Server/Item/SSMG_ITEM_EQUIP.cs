using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_EQUIP : Packet
    {
        public SSMG_ITEM_EQUIP()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x09E8;   
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
                this.PutByte((byte)value, 6);
            }
        }

        
    }
}

