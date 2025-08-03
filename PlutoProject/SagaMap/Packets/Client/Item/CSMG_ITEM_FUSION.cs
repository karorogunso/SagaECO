using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_FUSION : Packet
    {
        public CSMG_ITEM_FUSION()
        {
            this.offset = 2;
        }

        public uint EffectItem
        {
            get
            {
                return this.GetUInt(6);
            }
        }

        public uint ViewItem
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_FUSION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemFusion(this);
        }

    }
}