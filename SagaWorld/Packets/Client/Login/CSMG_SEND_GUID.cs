using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaWorld;
using SagaWorld.Network.Client;

namespace SagaWorld.Packets.Client
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
            return (SagaLib.Packet)new SagaWorld.Packets.Client.CSMG_SEND_GUID();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((WorldClient)(client)).OnSendGUID(this);
        }

    }
}
