using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_ADD_SLOT_ITEM_LIST : Packet
    {
        public SSMG_IRIS_ADD_SLOT_ITEM_LIST()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x13E2;
        }

        public List<uint> Items
        {
            set
            {
                this.data = new byte[10 + 4 * value.Count];
                this.offset = 2;
                this.ID = 0x13E2;

                PutByte((byte)value.Count);

                foreach (var item in value)
                {
                    PutUInt(item);
                }
                PutUInt(0u);
                PutByte(01);
                PutShort(100);
                PutByte(0);
            }
        }
    }
}

