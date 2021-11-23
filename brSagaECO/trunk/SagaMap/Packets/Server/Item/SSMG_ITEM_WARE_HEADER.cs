using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_WARE_HEADER : Packet
    {
        public SSMG_ITEM_WARE_HEADER()
        {
            this.data = new byte[18];
            this.offset = 2;
            this.ID = 0x09F6;
        }

        public WarehousePlace Place
        {
            set
            {
                this.PutInt((int)value, 2);
            }        
        }

        public int CountCurrent
        {
            set
            {
                this.PutInt(value, 6);
            }
        }

        public int CountAll
        {
            set
            {
                this.PutInt(value, 10);
            }
        }

        public int CountMax
        {
            set
            {
                this.PutInt(value, 14);
            }
        }
    }
}

