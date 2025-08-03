using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DEM_STATS_PRE_CALC : Packet
    {
        public CSMG_DEM_STATS_PRE_CALC()
        {
            this.offset = 2;
        }

        public ushort Str
        {
            get
            {
                return this.GetUShort(5);
            }
        }

        public ushort Dex
        {
            get
            {
                return this.GetUShort(7);
            }
        }

        public ushort Int
        {
            get
            {
                return this.GetUShort(9);
            }
        }

        public ushort Vit
        {
            get
            {
                return this.GetUShort(11);
            }
        }

        public ushort Agi
        {
            get
            {
                return this.GetUShort(13);
            }
        }

        public ushort Mag
        {
            get
            {
                return this.GetUShort(15);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_STATS_PRE_CALC();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMStatsPreCalc(this);
        }

    }
}