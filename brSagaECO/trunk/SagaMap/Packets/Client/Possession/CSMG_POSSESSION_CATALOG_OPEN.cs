using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_POSSESSION_CATALOG_OPEN : Packet
    {
        public CSMG_POSSESSION_CATALOG_OPEN()
        {
            this.offset = 2;
        }

        public ushort Page
        {
            get
            {
                return GetUShort(3);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_POSSESSION_CATALOG_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPossessionCatalogOpen(this);
        }

    }
}