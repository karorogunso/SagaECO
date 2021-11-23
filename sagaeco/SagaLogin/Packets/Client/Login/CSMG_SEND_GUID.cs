using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_SEND_GUID : Packet
    {
        public CSMG_SEND_GUID()
        {
            this.size = 360;
            this.offset = 8;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_SEND_GUID();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSendGUID(this);
        }

    }
}
