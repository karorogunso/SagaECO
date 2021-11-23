using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_TAMAIRE_RENTAL_REQUEST : Packet
    {
        public CSMG_TAMAIRE_RENTAL_REQUEST()
        {
            this.offset = 2;
        }


        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_TAMAIRE_RENTAL_REQUEST();
        }

        public uint Lender
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTamaireRentalRequest(this);
        }

    }
}