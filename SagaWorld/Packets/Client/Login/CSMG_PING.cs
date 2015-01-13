using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaWorld;
using SagaWorld.Network.Client;

namespace SagaWorld.Packets.Client
{
    public class CSMG_PING : Packet
    {
        public CSMG_PING()
        {
            this.size = 6;
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaWorld.Packets.Client.CSMG_PING();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((WorldClient)(client)).OnPing(this);
        }

    }
}