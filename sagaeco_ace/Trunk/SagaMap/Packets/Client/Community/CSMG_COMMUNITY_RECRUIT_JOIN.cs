using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_RECRUIT_JOIN : Packet
    {
        public CSMG_COMMUNITY_RECRUIT_JOIN()
        {
            this.offset = 2;
        }

        public uint CharID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_RECRUIT_JOIN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRecruitJoin(this);
        }

    }
}