using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SHOP_ITEM : HasItemDetail
    {
        public SSMG_GOLEM_SHOP_ITEM()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                this.data = new byte[170];
            else
                this.data = new byte[217];
            this.offset = 2;
            this.ID = 0x1801;
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

