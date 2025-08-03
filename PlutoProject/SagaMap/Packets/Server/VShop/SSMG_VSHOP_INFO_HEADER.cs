using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.ECOShop;

namespace SagaMap.Packets.Server
{
    public class SSMG_VSHOP_INFO_HEADER : Packet
    {
        public SSMG_VSHOP_INFO_HEADER()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x064F;
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

