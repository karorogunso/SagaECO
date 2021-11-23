using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_TAKEOFF_RESULT : Packet
    {
        public SSMG_ANO_TAKEOFF_RESULT()
        {
            this.data = new byte[5];
            this.offset = 2;
            this.ID = 0x23AD;
        }

        public ushort PaperID
        {
            set
            {
                this.PutUShort(value, 3);
            }
        }
    }
}

