using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_FURNITURE_SIT : Packet
    {
        public CSMG_PLAYER_FURNITURE_SIT()
        {
            this.data = new byte[10];
            this.offset = 2;
        }

        public uint FurnitureID
        {
            get
            {
                return GetUInt(2);
            }
        }

        public int unknown
        {
            get
            {
                return GetInt(6);
            }
        }



        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_FURNITURE_SIT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerFurnitureSit(this);
        }

    }
}