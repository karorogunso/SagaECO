using SagaLib;
using SagaMap.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_REQUIRE_REBIRTHREWARD : Packet
    {
        public CSMG_PLAYER_REQUIRE_REBIRTHREWARD()
        {
            this.offset = 2;
        }

        public override Packet New()
        {
            return new CSMG_PLAYER_REQUIRE_REBIRTHREWARD();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnRequireRebirthReward(this);
        }
    }
}
