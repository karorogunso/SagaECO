using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTNER_PARTNER_MOTION : Packet
    {
        public CSMG_PARTNER_PARTNER_MOTION()
        {
            this.offset = 2;
        }

        public uint id
        {
            get
            {
                return this.GetUInt(2);
            }
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public override Packet New()
        {
            return new CSMG_PARTNER_PARTNER_MOTION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerMotion(this);
        }

    }
}