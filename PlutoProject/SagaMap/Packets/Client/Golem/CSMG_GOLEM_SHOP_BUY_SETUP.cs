using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_GOLEM_SHOP_BUY_SETUP : Packet
    {
        public CSMG_GOLEM_SHOP_BUY_SETUP()
        {
            this.offset = 2;
        }

        public uint[] InventoryIDs
        {
            get
            {
                uint[] ids = new uint[GetByte(2)];
                for (int i = 0; i < ids.Length; i++)
                {
                    ids[i] = GetUInt();
                }
                return ids;
            }
        }

        public ushort[] Counts
        {
            get
            {
                byte len = GetByte(2);
                ushort[] counts = new ushort[GetByte((ushort)(3 + len * 4))];
                for (int i = 0; i < counts.Length; i++)
                {
                    counts[i] = GetUShort();
                }
                return counts;
            }
        }

        public uint[] Prices
        {
            get
            {
                byte len = GetByte(2);
                uint[] prices = new uint[GetByte((ushort)(4 + len * 6))];
                for (int i = 0; i < prices.Length; i++)
                {
                    prices[i] = GetUInt();
                }
                return prices;
            }
        }

        public uint BuyLimit
        {
            get
            {
               byte len = GetByte(2);
               return this.GetUInt((ushort)(5 + len * 10));
            }
        }

        public string Comment
        {
            get
            {
                byte len = GetByte(2);
                len = GetByte((ushort)(9 + len * 10));
                return Global.Unicode.GetString(GetBytes(len, (ushort)(10 + GetByte(2) * 10))).Replace("\0", "");
            }
        }

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_GOLEM_SHOP_BUY_SETUP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGolemShopBuySetup(this);
        }

    }
}