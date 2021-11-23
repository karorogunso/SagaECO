using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_FACEVIEW_CLOSE : Packet
    {
        public CSMG_ITEM_FACEVIEW_CLOSE()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_FACEVIEW_CLOSE();
        }

        public override void Parse(SagaLib.Client client)
        {

        }

    }
}