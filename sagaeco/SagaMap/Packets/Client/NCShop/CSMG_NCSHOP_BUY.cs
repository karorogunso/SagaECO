using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NCSHOP_BUY : Packet
    {
        public CSMG_NCSHOP_BUY()
        {
            this.offset = 2;
        }

        public uint[] Items
        {
            get
            {
                byte count = GetByte(2);
                uint[] items = new uint[count];
                for (int i = 0; i < count; i++)
                {
                    items[i] = GetUInt((ushort)(3 + i * 4));
                }
                return items;
            }
        }

        public uint[] Counts
        {
            get
            {
                byte count = GetByte(2);
                uint[] items = new uint[count];
                for (int i = 0; i < count; i++)
                {
                    items[i] = GetUInt((ushort)(4 + count * 4 + i * 4));
                }
                return items;
            }
        }

        public override Packet New()
        {
            return new CSMG_NCSHOP_BUY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNCShopBuy(this);
        }

    }
}