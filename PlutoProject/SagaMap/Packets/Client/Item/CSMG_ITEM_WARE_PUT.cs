using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ITEM_WARE_PUT : Packet
    {
        public CSMG_ITEM_WARE_PUT()
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ITEM_WARE_PUT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnItemWarePut(this);
        }

    }
}