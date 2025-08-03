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
    public class CSMG_PLAYER_SHOP_SELL_BUY : Packet
    {
        public CSMG_PLAYER_SHOP_SELL_BUY()
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
                    uint id = GetUInt((ushort)(7 + i * 4));
                    ushort count = GetUShort((ushort)(8 + num * 4 + i * 2));
                    items.Add(id, count);
                }
                return items;
            }
        }

        public ContainerType Container
        {
            get
            {
                byte num = GetByte(6);
                return (ContainerType)GetByte((ushort)(8 + num * 6));
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_SHOP_SELL_BUY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerShopSellBuy(this);
        }

    }
}