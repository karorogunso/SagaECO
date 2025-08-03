using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTY_INVITE : Packet
    {
        public CSMG_PARTY_INVITE()
        {
            this.offset = 2;
        }

        public uint CharID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTY_INVITE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyInvite(this);
        }

    }
}