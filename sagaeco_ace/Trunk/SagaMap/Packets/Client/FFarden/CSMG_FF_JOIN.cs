using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FFGARDEN_JOIN : Packet
    {
        public CSMG_FFGARDEN_JOIN()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FFGARDEN_JOIN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFFardenJoin(this);
        }

    }
}