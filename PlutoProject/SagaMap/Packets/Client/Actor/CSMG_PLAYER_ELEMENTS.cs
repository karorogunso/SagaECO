using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_ELEMENTS : Packet
    {
        public CSMG_PLAYER_ELEMENTS()
        {
            this.offset = 2;
        }        

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_ELEMENTS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerElements(this);
        }

    }
}