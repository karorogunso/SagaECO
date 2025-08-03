using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_IRIS_CARD_UNLOCK_RESULT : Packet
    {
        public SSMG_IRIS_CARD_UNLOCK_RESULT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1DCC;
        }

        public byte Result
        {
            set
            {
                this.PutShort(value);
            }
        }
    }
}

