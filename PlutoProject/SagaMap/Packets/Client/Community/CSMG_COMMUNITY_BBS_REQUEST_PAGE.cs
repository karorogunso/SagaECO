using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_BBS_REQUEST_PAGE : Packet
    {
        public CSMG_COMMUNITY_BBS_REQUEST_PAGE()
        {
            this.offset = 2;
        }

        public int Page
        {
            get
            {
                return this.GetInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_BBS_REQUEST_PAGE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnBBSRequestPage(this);
        }

    }
}