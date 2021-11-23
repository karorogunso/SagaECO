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
    public class CSMG_PARTNER_AI_MODE_SELECTION : Packet
    {
        public CSMG_PARTNER_AI_MODE_SELECTION()
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
        /// <summary>
        /// start at 0
        /// </summary>
        public byte AIMode
        {
            get
            {
                return this.GetByte(6);
            }
            set
            {
                this.PutByte(value, 6);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTNER_AI_MODE_SELECTION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerAIModeSelection(this);
        }

    }
}