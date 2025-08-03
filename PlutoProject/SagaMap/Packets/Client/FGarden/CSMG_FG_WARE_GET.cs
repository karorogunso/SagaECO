using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FG_WARE_GET : Packet
    {
        public CSMG_FG_WARE_GET()
        {
            this.offset = 2;
        }

        public uint InventoryID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public ushort Count
        {
            get
            {
                return this.GetUShort(6);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FG_WARE_GET();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFGardenWareGet(this);
        }

    }
}