using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_BUTTON_APPEAR : Packet
    {
        public SSMG_ANO_BUTTON_APPEAR()
        {
            this.data = new byte[5];
            this.offset = 2;
            this.ID = 0x23A0;
        }

        public byte Type
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}

