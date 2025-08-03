using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_IRIS_CARD_REMOVE : Packet
    {
        public CSMG_IRIS_CARD_REMOVE()
        {
            this.offset = 2;
        }

        public short CardSlot
        {
            get
            {
                return this.GetShort(2);
            }
        }

        public byte Unknown
        {
            get
            {
                return this.GetByte(4);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_IRIS_CARD_REMOVE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnIrisCardRemove(this);
        }

    }
}