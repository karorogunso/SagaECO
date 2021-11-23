using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_TRADE_PERFORM : Packet
    {
        public CSMG_TRADE_PERFORM()
        {
            this.offset = 2;
        }

        public byte State
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_TRADE_PERFORM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradePerform(this);
        }

    }
}