using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.ECOShop;

namespace SagaMap.Packets.Server
{
    public class SSMG_NCSHOP_INFO_HEADER : Packet
    {
        public SSMG_NCSHOP_INFO_HEADER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x062F;
        }

        public uint Page
        {
            set
            {
                PutUInt(value, 2);
            }
        }
    }
}

