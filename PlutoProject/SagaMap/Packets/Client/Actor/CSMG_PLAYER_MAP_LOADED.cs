using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_MAP_LOADED : Packet
    {
        public CSMG_PLAYER_MAP_LOADED()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_MAP_LOADED();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMapLoaded(this);
        }

    }
}