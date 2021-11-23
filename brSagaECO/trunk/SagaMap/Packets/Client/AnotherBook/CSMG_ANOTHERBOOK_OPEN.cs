using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ANOTHERBOOK_OPEN :Packet
    {
        public CSMG_ANOTHERBOOK_OPEN()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ANOTHERBOOK_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAnotherBookOpen(this);
        }
    }
}
