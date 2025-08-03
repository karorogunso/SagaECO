using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_ANO_EQUIP_RESULT : Packet
    {
        public SSMG_ANO_EQUIP_RESULT()
        {
            this.data = new byte[5];
            this.offset = 2;
            this.ID = 0x23AB;
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

