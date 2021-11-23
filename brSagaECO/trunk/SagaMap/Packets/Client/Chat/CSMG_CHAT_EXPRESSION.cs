using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_EXPRESSION : Packet
    {
        public CSMG_CHAT_EXPRESSION()
        {
            this.offset = 2;
        }

        public byte Motion
        {
            get
            {
                return this.GetByte(2);
            }
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_EXPRESSION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnExpression(this);
        }

    }
}