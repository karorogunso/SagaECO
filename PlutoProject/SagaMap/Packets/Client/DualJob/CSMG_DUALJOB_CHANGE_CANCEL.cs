using SagaLib;
using SagaMap.Network.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Packets.Client
{
    public class CSMG_DUALJOB_CHANGE_CANCEL : Packet
    {
        public CSMG_DUALJOB_CHANGE_CANCEL()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_DUALJOB_CHANGE_CANCEL();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDualJobWindowClose();
        }
    }
}
