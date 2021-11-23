using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_ADD :HasItemDetail
    {
        public SSMG_ITEM_ADD()
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                this.data = new byte[170];
            else if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                this.data = new byte[217];
            else if (Configuration.Instance.Version >=SagaLib.Version.Saga18)
                this.data = new byte[215];
            this.offset = 2;
            this.ID = 0x09D4;   
        }

        public Item Item
        {
            set
            {
                if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                    this.PutByte(0xa6, 2);
                else if (Configuration.Instance.Version < SagaLib.Version.Saga9_Iris)
                    this.PutByte(0xd6, 2);
                else if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutByte(0xd4, 2);
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

        public ContainerType Container
        {
            set
            {
                this.PutByte((byte)value, 15);
            }
        }

        
    }
}

