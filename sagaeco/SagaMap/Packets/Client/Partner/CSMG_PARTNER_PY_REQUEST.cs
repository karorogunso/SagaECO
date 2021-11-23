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
    public class CSMG_PARTNER_PY_REQUEST : Packet
    {
        public CSMG_PARTNER_PY_REQUEST()
        {
            this.offset = 2;
        }

        public uint Slot
        {
            get
            {
                return this.GetUInt(2);
            }
        }
        public byte Type
        {
            get
            {
                return this.GetByte(6);
            }
        }

        public override SagaLib.Packet New()
        {
            return new CSMG_PARTNER_PY_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerItemPY(this);
        }

    }
}