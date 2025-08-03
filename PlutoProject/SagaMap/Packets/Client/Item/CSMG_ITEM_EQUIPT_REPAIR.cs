using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_EQUIPT_REPAIR : Packet
    {
        public CSMG_ITEM_EQUIPT_REPAIR()
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

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_EQUIPT_REPAIR();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemEquiptRepair(this);
        }

    }
}