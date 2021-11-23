using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_SETOPTION : Packet
    {
        public CSMG_PLAYER_SETOPTION()
        {
            this.offset = 2;
        }        

        public uint GetOption
        {
            get
            {
                return GetUInt(2);
            }
        }
        public override Packet New()
        {
            return new CSMG_PLAYER_SETOPTION();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPlayerSetOption(this);
        }

    }
}