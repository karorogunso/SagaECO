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
    public class CSMG_INFINITECORRIDOR_WARP : Packet
    {
        public CSMG_INFINITECORRIDOR_WARP()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_INFINITECORRIDOR_WARP();
        }

        public override void Parse(SagaLib.Client client)
        {
        }

    }
}