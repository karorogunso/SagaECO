using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_WARE_ITEM :HasItemDetail
    {
        public SSMG_ITEM_WARE_ITEM()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                this.data = new byte[170];
            else
                this.data = new byte[217];
            this.offset = 2;
            this.ID = 0x09F9;   
        }

        public Item Item
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                    this.PutByte(0xa6, 2);
                else
                    this.PutByte(0xd6, 2);
                this.offset = 7;
                this.ItemDetail = value;
            }
        }
        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public WarehousePlace Place
        {
            set
            {
                this.PutByte((byte)value, 15);
            }
        }

        
    }
}

