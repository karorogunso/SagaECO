using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_MOTION : Packet
    {
        public CSMG_CHAT_MOTION()
        {
            this.offset = 2;
        }

        public MotionType Motion
        {
            get
            {
                return (MotionType)this.GetUShort(2);
            }
        }

        public byte Loop
        {
            get
            {
                return this.GetByte(4);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_CHAT_MOTION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnMotion(this);
        }

    }
}