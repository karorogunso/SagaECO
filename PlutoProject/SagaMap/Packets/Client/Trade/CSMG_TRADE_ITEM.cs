using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_TRADE_ITEM : Packet
    {
        public CSMG_TRADE_ITEM()
        {
            this.offset = 2;
        }

        public List<uint> InventoryID
        {
            get
            {
                List<uint> list = new List<uint>();
                byte count = this.GetByte(2);
                for (int i = 0; i < count; i++)
                {
                    list.Add(this.GetUInt((ushort)(3 + i * 4)));
                }
                return list;
            }
        }

        public List<ushort> Count
        {
            get
            {
                List<ushort> list = new List<ushort>();
                byte count = this.GetByte(2);
                for (int i = 0; i < count; i++)
                {
                    list.Add(this.GetUShort((ushort)(4 + count * 4 + i * 2)));
                }
                return list;
            }
        }

        public uint Gold
        {
            get
            {
                byte count = this.GetByte(2);
                return this.GetUInt((ushort)(8 + count * 6));
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_TRADE_ITEM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradeItem(this);
        }

    }
}