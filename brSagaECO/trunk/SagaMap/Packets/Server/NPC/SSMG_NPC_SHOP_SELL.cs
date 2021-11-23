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
            if (Configuration.Instance.Version>=SagaLib.Version.Saga18)
            {
                this.data = new byte[27];
                this.ID = 0x0603;
            }
            else
            {
                this.data = new byte[15];
                this.ID = 0x05FF;
            }
            this.offset = 2;     
        }

        public ulong Rate
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutULong(value, 2);
                else
                    this.PutUInt((uint)value, 2);
            }
        }

        public ulong ShopLimit
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutULong(value, 10);
                else
                    this.PutUInt((uint)value, 6);
            }
        }

        public ulong Bank
        {
            set
            {
                if (Configuration.Instance.Version >= SagaLib.Version.Saga18)
                    this.PutULong(value, 18);
                else
                    this.PutUInt((uint)value, 10);
            }
        }
    }
}

