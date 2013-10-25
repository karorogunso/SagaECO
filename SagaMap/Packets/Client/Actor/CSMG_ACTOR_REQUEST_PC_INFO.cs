using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ACTOR_REQUEST_PC_INFO : Packet
    {
        public CSMG_ACTOR_REQUEST_PC_INFO()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ACTOR_REQUEST_PC_INFO();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRequestPCInfo(this);
        }

    }
}