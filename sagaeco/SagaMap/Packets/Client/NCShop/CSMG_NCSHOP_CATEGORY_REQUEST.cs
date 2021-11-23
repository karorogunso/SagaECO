using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NCSHOP_CATEGORY_REQUEST : Packet
    {
        public CSMG_NCSHOP_CATEGORY_REQUEST()
        {
            this.offset = 2;
        }

        public uint Page
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override Packet New()
        {
            return new CSMG_NCSHOP_CATEGORY_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNCShopCategoryRequest(this);
        }

    }
}