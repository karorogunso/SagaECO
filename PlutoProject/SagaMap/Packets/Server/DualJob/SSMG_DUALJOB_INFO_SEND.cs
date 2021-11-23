using SagaLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Packets.Server
{
    public class SSMG_DUALJOB_INFO_SEND : Packet
    {
        public SSMG_DUALJOB_INFO_SEND()
        {
            this.data = new byte[41];
            this.offset = 2;
            this.ID = 0x22D4;
        }

        public byte[] JobList
        {
            set
            {
                this.PutBytes(value, 2);
            }
        }

        public byte[] JobLevel
        {
            set
            {
                this.PutBytes(value);
            }
        }
    }
}
