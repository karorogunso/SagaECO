using SagaLib;
using SagaValidation.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaValidation.Packets.Client
{
    public class CSMG_UNKNOWN_LIST : Packet
    {
        public CSMG_UNKNOWN_LIST()
        {
            this.size = 6;
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaValidation.Packets.Client.CSMG_UNKNOWN_LIST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((ValidationClient)(client)).OnUnknownList(this);
        }
    }
}
