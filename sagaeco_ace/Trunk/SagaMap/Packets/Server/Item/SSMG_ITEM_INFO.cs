using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_INFO :HasItemDetail
    {
        public SSMG_ITEM_INFO()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                this.data = new byte[171];
            else
                this.data = new byte[218];
            this.offset = 2;
            this.ID = 0x0203;   
        }

        public Item Item
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                    this.PutByte(0xa6, 3);
                else
                    this.PutByte(0xd6, 3);                
                this.offset = 8;
                this.ItemDetail = value;
            }
        }

        public byte Size
        {
            set
            {
                this.PutByte(value, 3);
            }
        }

        public uint InventorySlot
        {
            set
            {
                this.PutUInt(value, 4);
            }
        }

        public uint ItemID
        {
            set
            {
                this.PutUInt(value, 8);
            }
        }

        public ContainerType Container
        {
            set
            {
                if (value >= ContainerType.HEAD2)
                    this.PutByte((byte)(value - 200), 16);
                else
                    this.PutByte((byte)value, 16);
            }
        }
    }
}

