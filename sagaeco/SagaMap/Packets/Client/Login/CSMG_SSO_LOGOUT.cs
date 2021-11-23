using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_SSO_LOGOUT : Packet
    {

        public CSMG_SSO_LOGOUT()
        {
            this.offset = 8;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_SSO_LOGOUT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSSOLogout(this);
        }

    }
}