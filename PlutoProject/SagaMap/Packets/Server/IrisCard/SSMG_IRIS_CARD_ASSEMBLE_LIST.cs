using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaMap.Manager;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_ASSEMBLE_LIST : Packet
    {
        public SSMG_IRIS_CARD_ASSEMBLE_LIST()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x140A;
        }

        public List<Item> Cards
        {
            set
            {
                this.data = new byte[10 + 4 * value.Count];
                this.offset = 2;
                this.ID = 0x140A;

                PutByte((byte)value.Count);

                foreach (var item in value)
                {
                    PutUInt(item.Slot);
                }
                PutUInt(0u);
                PutByte(01);
                PutShort(0x64);
                PutByte(0);
            }
        }
    }
}

