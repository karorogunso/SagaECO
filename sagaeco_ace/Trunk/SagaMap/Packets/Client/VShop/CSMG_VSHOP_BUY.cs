using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_VSHOP_BUY : Packet
    {
        public CSMG_VSHOP_BUY()
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

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_VSHOP_BUY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnVShopBuy(this);
        }

    }
}