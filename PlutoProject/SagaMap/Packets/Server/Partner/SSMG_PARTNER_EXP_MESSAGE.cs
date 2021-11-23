using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PARTNER_EXP_MESSAGE : Packet
    {
        public SSMG_PARTNER_EXP_MESSAGE()
        {
            this.data = new byte[15];
            this.offset = 2;
            this.ID = 0x2196;
        }

        public long EXP
        {
            set
            {
                this.PutLong(value, 3);
            }
        }

        public int Reliability
        {
            set
            {
                this.PutInt(value, 11);
            }
        }
    }
}
