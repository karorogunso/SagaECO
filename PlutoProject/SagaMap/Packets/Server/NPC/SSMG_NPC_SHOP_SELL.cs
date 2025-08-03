using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOP_SELL : Packet
    {
        public SSMG_NPC_SHOP_SELL()
        {
            this.data = new byte[27];
            this.offset = 2;
            this.ID = 0x0603;            
        }

        public uint Rate
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint ShopLimit
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

        public uint Bank
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
    }
}

