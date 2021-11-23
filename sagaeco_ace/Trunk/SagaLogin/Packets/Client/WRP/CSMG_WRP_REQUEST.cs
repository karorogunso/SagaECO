using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_WRP_REQUEST : Packet
    {
        public CSMG_WRP_REQUEST()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_WRP_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnWRPRequest(this);
        }

    }
}