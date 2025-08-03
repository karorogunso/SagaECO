using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_CHAT_GIFT_TAKE : Packet
    {
        public CSMG_CHAT_GIFT_TAKE()
        {
            this.offset = 2;
        }

        public uint GiftID
        {
            get
            {
                return GetUInt(2);
            }
        }

        public byte type
        {
            get
            {
                return GetByte(6);
            }
        }

        public override Packet New()
        {
            return new CSMG_CHAT_GIFT_TAKE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTakeGift(this);
        }

    }
}