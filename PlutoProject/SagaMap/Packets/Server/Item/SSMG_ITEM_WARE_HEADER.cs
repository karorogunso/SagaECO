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
            this.data = new byte[30];
            this.offset = 2;
            this.ID = 0x09F6;
            this.PutInt(0x0F, 18);
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
        public int Unknown
        {
            set
            {
                this.PutInt(value, 18);

            }
        }
        public ulong Gold
        {
            set
            {
                this.PutULong(value, 22);
            }
        }
    }
}

