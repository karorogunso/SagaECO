using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_GOLEM_SHOP_OPEN : Packet
    {
        public CSMG_GOLEM_SHOP_OPEN()
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

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_GOLEM_SHOP_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnGolemShopOpen(this);
        }

    }
}