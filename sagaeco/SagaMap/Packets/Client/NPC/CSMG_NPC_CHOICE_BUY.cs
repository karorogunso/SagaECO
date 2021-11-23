using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_CHOICE_BUY : Packet
    {
        public CSMG_NPC_CHOICE_BUY()
        {
            this.offset = 2;
        }

        public uint[] Goods
        {
            get
            {
                byte num = GetByte(2);
                uint[] goods = new uint[num];
                for (int i = 0; i < num; i++)
                {
                    goods[i] = GetUInt((ushort)(3 + i * 4));
                }
                return goods;
            }
        }

        public ushort[] Counts
        {
            get
            {
                byte num = this.GetByte(2);
                ushort[] goods = new ushort[num];
                for (int i = 0; i < num; i++)
                {
                    goods[i] = this.GetUShort((ushort)(4 + num * 4 + i * 2));
                }
                return goods;
            }
        }

        public override Packet New()
        {
            return new CSMG_NPC_CHOICE_BUY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCChoiceBuy(this);
        }

    }
}