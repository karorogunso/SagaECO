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
    public class CSMG_PARTNER_AI_DETAIL_OPEN : Packet
    {
        public CSMG_PARTNER_AI_DETAIL_OPEN()
        {
            this.offset = 2;
        }

        public uint PartnerInventorySlot
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
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTNER_AI_DETAIL_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerAIOpen();
        }

    }
}