using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_DELETE : Packet
    {
        public SSMG_ITEM_DELETE()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x09CE;   
        }
       
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }        
    }
}

