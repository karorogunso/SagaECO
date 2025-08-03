using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_RING_EMBLEM : Packet
    {
        public CSMG_RING_EMBLEM()
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

        public DateTime UpdateTime
        {
            get
            {
                DateTime date = new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(GetUInt(6));
                return date;
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_RING_EMBLEM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnRingEmblem(this);
        }

    }
}