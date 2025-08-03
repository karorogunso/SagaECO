using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_COMMUNITY_BBS_CLOSE : Packet
    {
        public CSMG_COMMUNITY_BBS_CLOSE()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_COMMUNITY_BBS_CLOSE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnBBSClose(this);
        }

    }
}