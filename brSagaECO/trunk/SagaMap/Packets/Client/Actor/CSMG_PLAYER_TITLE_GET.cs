using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_TITLE_GET : Packet
    {
        public CSMG_PLAYER_TITLE_GET()
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
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_TITLE_GET();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTitleGet(this);
        }
    }
}