using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Iris;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_ASSEMBLE : Packet
    {
        public SSMG_IRIS_CARD_ASSEMBLE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x140A;
        }

        public Dictionary<IrisCard, int> CardAndPrice
        {
            set
            {
                byte[] buf = new byte[7 + 4 * value.Count];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)value.Count, 2);
                int price = 0;
                foreach (IrisCard i in value.Keys)
                {
                    price = value[i];
                    this.PutUInt(i.ID);
                }
                this.PutInt(price);
            }
        }
    }
}

