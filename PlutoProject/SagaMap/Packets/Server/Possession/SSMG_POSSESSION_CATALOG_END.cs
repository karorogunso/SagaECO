using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_CATALOG_END : Packet
    {
        public SSMG_POSSESSION_CATALOG_END()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x1790;
        }

        public ushort Page
        {
            set
            {
                this.PutUShort(value, 2);
            }
        }
    }
}