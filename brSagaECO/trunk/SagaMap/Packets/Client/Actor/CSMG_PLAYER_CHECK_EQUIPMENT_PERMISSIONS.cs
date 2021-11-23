using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_CHECK_EQUIPMENT_PERMISSIONS : Packet
    {
        public CSMG_PLAYER_CHECK_EQUIPMENT_PERMISSIONS()
        {
            this.offset = 2;
        }

        public uint Option
        {
            get { return GetUInt(2); }
        }

        public override Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_CHECK_EQUIPMENT_PERMISSIONS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerCheckEquipmentPermissions(Option);
        }
    }
}
