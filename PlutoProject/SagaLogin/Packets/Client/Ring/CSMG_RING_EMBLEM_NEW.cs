using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_RING_EMBLEM_NEW : Packet
    {
        public CSMG_RING_EMBLEM_NEW()
        {
            this.size = 6;
            this.offset = 2;
        }

        public uint RingID
        {
            get
            {
                return GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_RING_EMBLEM_NEW();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnRingEmblemNew(this);
        }

    }
}