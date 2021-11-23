using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_DAILYSTAMP_OPEN : Packet
    {
        public CSMG_DAILYSTAMP_OPEN()
        {
            this.offset = 2;
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_DAILYSTAMP_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnDailyStampOpen(this);
        }

    }
}