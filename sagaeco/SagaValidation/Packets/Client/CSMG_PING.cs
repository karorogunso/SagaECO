using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaValidation;
using SagaValidation.Network.Client;

namespace SagaValidation.Packets.Client
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
            return (SagaLib.Packet)new SagaValidation.Packets.Client.CSMG_PING();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ValidationClient)(client)).OnPing(this);
        }

    }
}