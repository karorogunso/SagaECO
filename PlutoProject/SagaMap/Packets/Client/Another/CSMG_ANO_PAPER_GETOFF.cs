using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ANO_PAPER_TAKEOFF : Packet
    {
        public CSMG_ANO_PAPER_TAKEOFF()
        {
            this.offset = 2;
        }
        public override SagaLib.Packet New()
        {
            return new CSMG_ANO_PAPER_TAKEOFF();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAnoPaperTakeOff(this);
        }

    }
}