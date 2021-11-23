using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_SIT : Packet
    {
        public CSMG_CHAT_SIT()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_SIT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnSit(this);
        }

    }
}