using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_TITLE_CHECK_PROGRESS : Packet
    {
        public CSMG_PLAYER_TITLE_CHECK_PROGRESS()
        {
            this.offset = 2;
        }
        
        public uint Title
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_TITLE_CHECK_PROGRESS();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTitleCheckProgress(this);
        }
    }
}