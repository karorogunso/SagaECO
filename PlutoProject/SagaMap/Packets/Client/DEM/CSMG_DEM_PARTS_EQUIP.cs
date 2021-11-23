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
    public class CSMG_DEM_PARTS_EQUIP : Packet
    {
        public CSMG_DEM_PARTS_EQUIP()
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DEM_PARTS_EQUIP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDEMPartsEquip(this);
        }

    }
}