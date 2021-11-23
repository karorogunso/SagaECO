using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_IRIS_ADD_SLOT_CONFIRM : Packet
    {
        public CSMG_IRIS_ADD_SLOT_CONFIRM()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_IRIS_ADD_SLOT_CONFIRM();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnIrisAddSlotConfirm(this);
        }

    }
}