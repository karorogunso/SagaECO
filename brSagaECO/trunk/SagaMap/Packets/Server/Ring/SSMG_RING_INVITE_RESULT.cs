using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_INVITE_RESULT : Packet
    {
        public SSMG_RING_INVITE_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1AB3;
        }

        public int Result
        {
            set
            {
                this.PutInt((int)value, 2);
            }
        }
    }
}

