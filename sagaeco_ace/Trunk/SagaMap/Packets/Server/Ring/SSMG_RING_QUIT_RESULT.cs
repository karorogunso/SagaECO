using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Ring;


namespace SagaMap.Packets.Server
{
    public class SSMG_RING_QUIT_RESULT : Packet
    {
        public SSMG_RING_QUIT_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1ABE;
        }

        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
    }
}

