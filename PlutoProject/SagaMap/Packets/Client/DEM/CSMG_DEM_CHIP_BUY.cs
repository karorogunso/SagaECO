using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DEM_CHIP_BUY : Packet
    {
        public CSMG_DEM_CHIP_BUY()
        {
            this.offset = 2;
        }

        public uint[] ItemIDs
        {
            get
            {
                uint[] items = new uint[this.GetByte(2)];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = this.GetUInt((ushort)(3 + i * 4));
                }
                return items;
            }
        }

        public int[] Counts
        {
            get
            {
                byte count = GetByte(2);
                int[] items = new int[count];
                for (int i = 0; i < count; i++)
                {
                    items[i] = GetInt((ushort)(4 + count * 4 + i * 4));
                }
                return items;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_CHIP_BUY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMChipBuy(this);
        }

    }
}