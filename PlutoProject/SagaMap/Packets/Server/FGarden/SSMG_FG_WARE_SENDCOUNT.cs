using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;

namespace SagaMap.Packets.Server
{
    public class SSMG_FG_WARE_SENDCOUNT : Packet
    {
        public SSMG_FG_WARE_SENDCOUNT()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1c20;
        }

        public int CurrentCount
        {
            set
            {
                this.PutInt(value);
            }
        }
    }
}
