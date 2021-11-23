using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;
using SagaDB.Iris;

namespace SagaMap.Packets.Server
{
    public class SSMG_ITEM_IRIS_CARD_INFO : Packet
    {
        public SSMG_ITEM_IRIS_CARD_INFO()
        {
            this.data = new byte[9];
            this.offset = 2;
            this.ID = 0x09D5;   
        }

        public Item Item
        {
            set
            {
                this.PutUInt(value.Slot, 2);
                byte[] buf = new byte[9 + 4 * value.CurrentSlot];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)value.CurrentSlot, 6);
                for (int i = 0; i < value.CurrentSlot; i++)
                {
                    if (i < value.Cards.Count)
                        this.PutUInt(value.Cards[i].ID);
                    else
                        this.PutUInt(0);
                }

                this.PutShort(value.CurrentSlot);
            }
        }
    }
}

