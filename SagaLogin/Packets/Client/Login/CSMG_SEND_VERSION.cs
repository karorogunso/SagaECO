using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_SEND_VERSION : Packet
    {
        public CSMG_SEND_VERSION()
        {
            this.size = 10;
            this.offset = 2;
        }

        public string GetVersion()
        {
            byte[] buf = this.GetBytes(6, 4);
            return Conversions.bytes2HexString(buf);
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_SEND_VERSION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnSendVersion(this);
        }

    }
}
