using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTY_ROLL : Packet
    {
        public CSMG_PARTY_ROLL()
        {
            this.offset = 2;
        }

        public uint status
        {
            get
            {
                return GetByte(2);
            }
        }

        public override Packet New()
        {
            return new CSMG_PARTY_ROLL();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyRoll(this);
        }

    }
}