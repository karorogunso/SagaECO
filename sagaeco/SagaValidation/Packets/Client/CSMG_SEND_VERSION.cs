using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaValidation;
using SagaValidation.Network.Client;

namespace SagaValidation.Packets.Client
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
            return (SagaLib.Packet)new SagaValidation.Packets.Client.CSMG_SEND_VERSION();
        }

        public override void Parse(SagaLib.Client client)
        {
            //((LoginClient)(client)).NewOnSendVersion(this);
            ((ValidationClient)(client)).OnSendVersion(this);
        }

    }
}
