using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PPROTECT_CREATED_INITI : Packet
    {
        public CSMG_PPROTECT_CREATED_INITI()
        {
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PPROTECT_CREATED_INITI();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPProtectCreatedIniti(this);
        }

    }
}