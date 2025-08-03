using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_TRADE_REQUEST : Packet
    {
        public CSMG_TRADE_REQUEST()
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_TRADE_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradeRequest(this);
        }

    }
}