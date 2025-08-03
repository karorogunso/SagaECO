using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_RECRUIT : Packet
    {
        public CSMG_COMMUNITY_RECRUIT()
        {
            this.offset = 2;
        }

        public Manager.RecruitmentType Type
        {
            get
            {
                return (SagaMap.Manager.RecruitmentType)this.GetByte(2);
            }
        }

        public int Page
        {
            get
            {
                return this.GetInt(3);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_RECRUIT();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRecruit(this);
        }

    }
}