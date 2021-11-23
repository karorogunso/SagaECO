using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DEM_COST_LIMIT_BUY : Packet
    {
        public CSMG_DEM_COST_LIMIT_BUY()
        {
            this.offset = 2;
        }

        public short EP
        {
            get
            {
                return this.GetShort(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_COST_LIMIT_BUY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMCostLimitBuy(this);
        }

    }
}