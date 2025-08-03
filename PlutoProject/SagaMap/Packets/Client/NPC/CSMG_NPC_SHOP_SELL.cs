using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_SHOP_SELL : Packet
    {
        public CSMG_NPC_SHOP_SELL()
        {
            this.offset = 2;
        }

        public uint[] Goods
        {
            get
            {
                byte num = this.GetByte(2);
                uint[] goods = new uint[num];
                for (int i = 0; i < num; i++)
                {
                    goods[i] = this.GetUInt((ushort)(3 + i * 4));
                }
                return goods;
            }
        }

        public uint[] Counts
        {
            get
            {
                byte num = this.GetByte(2);
                uint[] goods = new uint[num];
                for (int i = 0; i < num; i++)
                {
                    goods[i] = this.GetUInt((ushort)(4 + num * 4 + i * 4));
                }
                return goods;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_SHOP_SELL();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCShopSell(this);
        }

    }
}