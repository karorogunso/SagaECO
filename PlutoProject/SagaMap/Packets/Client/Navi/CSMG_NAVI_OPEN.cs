using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB;
using SagaLib;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NAVI_OPEN : Packet
    {
        public CSMG_NAVI_OPEN()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NAVI_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNaviOpen(this);
        }
    }
}
