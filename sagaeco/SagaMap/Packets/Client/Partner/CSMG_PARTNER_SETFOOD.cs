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
    public class CSMG_PARTNER_SETFOOD : Packet
    {
        public CSMG_PARTNER_SETFOOD()
        {
            this.offset = 2;
        }
        /// <summary>
        /// 1 to get into the food list, 0 to get out of the food list
        /// </summary>
        public byte MoveType
        {
            get
            {
                return this.GetByte(2);
            }
            set
            {
                this.PutByte(value, 2);
            }
        }

        public uint ItemID
        {
            get
            {
                return this.GetUInt(3);
            }
            set
            {
                this.PutUInt(value, 3);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTNER_SETFOOD();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartnerFoodListSet(this);
        }

    }
}