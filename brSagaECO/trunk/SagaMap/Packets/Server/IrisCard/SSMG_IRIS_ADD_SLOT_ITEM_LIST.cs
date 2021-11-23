using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

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
                this.offset = 2;
                byte[] buf = new byte[3 + 4 * value.Count];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)value.Count);
                foreach (uint i in value)
                {
                    this.PutUInt(i);
                }
            }
        }
    }
}

