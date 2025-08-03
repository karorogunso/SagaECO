using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_FF_FURNITURE_ROOM_ENTER : Packet
    {
        public CSMG_FF_FURNITURE_ROOM_ENTER()
        {
            this.offset = 2;
        }

        public int data
        {
            get
            {
                return GetInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_FF_FURNITURE_ROOM_ENTER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnFFFurnitureRoomEnter(this);
        }

    }
}