using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_RETURN_HOME : Packet
    {
        public CSMG_PLAYER_RETURN_HOME()
        {
            this.offset = 2;
        }        

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_RETURN_HOME();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerReturnHome(this);
        }

    }
}