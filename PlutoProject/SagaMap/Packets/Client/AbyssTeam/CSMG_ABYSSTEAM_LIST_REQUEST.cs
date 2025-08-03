using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ABYSSTEAM_LIST_REQUEST : Packet
    {
        public CSMG_ABYSSTEAM_LIST_REQUEST()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_ABYSSTEAM_LIST_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAbyssTeamListRequest(this);
        }

    }
}