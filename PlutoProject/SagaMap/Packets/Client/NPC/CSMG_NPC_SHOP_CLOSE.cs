using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_SHOP_CLOSE : Packet
    {
        public CSMG_NPC_SHOP_CLOSE()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_SHOP_CLOSE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCShopClose(this);
        }

    }
}