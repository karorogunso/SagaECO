using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_EMOTION : Packet
    {
        public CSMG_CHAT_EMOTION()
        {
            this.offset = 2;
        }

        public uint Emotion
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_EMOTION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnEmotion(this);
        }

    }
}