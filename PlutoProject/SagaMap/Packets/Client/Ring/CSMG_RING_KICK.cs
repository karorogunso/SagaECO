using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_RING_KICK : Packet
    {
        public CSMG_RING_KICK()
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_RING_KICK();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRingKick(this);
        }

    }
}