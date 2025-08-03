using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_VSHOP_CLOSE : Packet
    {
        public CSMG_VSHOP_CLOSE()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_VSHOP_CLOSE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnVShopClose(this);
        }

    }
}