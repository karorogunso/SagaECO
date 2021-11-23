using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_RING_INVITE_ANSWER : Packet
    {
        bool accepted = false;
        public CSMG_RING_INVITE_ANSWER(bool accepted)
        {
            this.offset = 2;
            this.accepted = accepted;
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_RING_INVITE_ANSWER(this.accepted);
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRingInviteAnswer(this, accepted);
        }

    }
}