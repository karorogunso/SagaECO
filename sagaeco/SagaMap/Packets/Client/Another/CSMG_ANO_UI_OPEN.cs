using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_ANO_UI_OPEN : Packet
    {
        public CSMG_ANO_UI_OPEN()
        {
            this.offset = 2;
        }
        public byte index
        {
            get
            {
                return GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_ANO_UI_OPEN();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnAnoUIOpen(this);
        }

    }
}