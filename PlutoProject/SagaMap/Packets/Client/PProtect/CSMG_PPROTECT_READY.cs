using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PPROTECT_READY : Packet
    {
        public CSMG_PPROTECT_READY()
        {
            this.offset = 2;
        }

        public byte State
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PPROTECT_READY();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPProtectReady(this);
        }

    }
}