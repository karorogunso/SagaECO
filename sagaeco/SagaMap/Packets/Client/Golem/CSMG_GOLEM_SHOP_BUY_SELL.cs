using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaDB.Item;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_GOLEM_SHOP_BUY_SELL : Packet
    {
        public CSMG_GOLEM_SHOP_BUY_SELL()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return GetUInt(2);
            }
        }

        public Dictionary<uint, ushort> Items
        {
            get
            {
                byte num = GetByte(6);
                Dictionary<uint, ushort> items = new Dictionary<uint, ushort>();
                for (int i = 0; i < num; i++)
                {
                    uint id = GetUInt((ushort)(8 + num*4 + i * 4));
                    ushort count = GetUShort((ushort)(9 + num * 8 + i * 2));
                    items.Add(id, count);
                }
                return items;
            }
        }

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_GOLEM_SHOP_BUY_SELL();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGolemShopBuySell(this);
        }

    }
}