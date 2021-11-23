using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
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
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_PING();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnPing(this);
        }

    }
}