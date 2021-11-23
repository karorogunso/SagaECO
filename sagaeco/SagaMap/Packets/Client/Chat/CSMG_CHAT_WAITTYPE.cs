using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_WAITTYPE : Packet
    {
        public CSMG_CHAT_WAITTYPE()
        {
            this.offset = 2;
        }

        public byte type
        {
            get
            {
                return this.GetByte(3);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_WAITTYPE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnWaitType(this);
        }

    }
}