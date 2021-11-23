using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Partner;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTNER_PY_CANCEL : Packet
    {
        public CSMG_PARTNER_PY_CANCEL()
        {
            this.offset = 2;
        }
        public byte Type
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return new CSMG_PARTNER_PY_CANCEL();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerItemPYCancel(this);
        }

    }
}