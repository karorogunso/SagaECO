using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.FGarden;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DAILY_STAMP_OPEN : Packet
    {
        public CSMG_DAILY_STAMP_OPEN()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return new CSMG_DAILY_STAMP_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerOpenDailyStamp(this);

        }

    }
}