using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_MIRROR : Packet
    {
        public CSMG_PLAYER_MIRROR()
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
            return new CSMG_PLAYER_MIRROR();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).EventActivate(654321);
        }

    }
}