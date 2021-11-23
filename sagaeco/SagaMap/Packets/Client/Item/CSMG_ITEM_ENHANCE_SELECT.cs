using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_ENHANCE_SELECT : Packet
    {
        public CSMG_ITEM_ENHANCE_SELECT()
        {
            this.offset = 2;
            this.data = new byte[6];
        }

        public uint InventorySlot
        {
            get
            {
                return this.GetUInt(2);
            }
            set
            {
                PutUInt(value, 2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_ENHANCE_SELECT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemEnhanceSelect(this);
        }

    }
}