using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_TITLE_REQUIRE : Packet
    {
        public CSMG_PLAYER_TITLE_REQUIRE()
        {
            this.offset = 2;
        }        

        public uint tID
        {
            get
            {
                return GetUInt(2);
            }
        }

        public override Packet New()
        {
            return new CSMG_PLAYER_TITLE_REQUIRE();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerTitleRequire(this);
        }

    }
}