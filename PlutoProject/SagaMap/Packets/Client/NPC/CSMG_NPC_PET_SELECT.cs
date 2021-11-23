using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_NPC_PET_SELECT : Packet
    {
        public CSMG_NPC_PET_SELECT()
        {
            this.offset = 2;
        }

        public uint Result
        {
            get
            {
                return GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_NPC_PET_SELECT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnNPCPetSelect(this);
        }

    }
}