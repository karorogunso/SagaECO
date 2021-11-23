using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_IRIS_GACHA_CANCEL : Packet
    {
        public CSMG_IRIS_GACHA_CANCEL()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_IRIS_GACHA_CANCEL();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnIrisGachaCancel(this);
        }

    }
}