using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ABYSSTEAM_LIST_CLOSE : Packet
    {
        public CSMG_ABYSSTEAM_LIST_CLOSE()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_ABYSSTEAM_LIST_CLOSE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAbyssTeamListClose(this);
        }

    }
}