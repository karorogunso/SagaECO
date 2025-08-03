using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_EQUIP_OPEN : Packet
    {
        public CSMG_PLAYER_EQUIP_OPEN()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return GetUInt(2);
            }
        }

       public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_EQUIP_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerEquipOpen(ActorID);
        }

    }
}