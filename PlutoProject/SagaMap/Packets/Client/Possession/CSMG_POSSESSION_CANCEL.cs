using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_POSSESSION_PARTNER_CANCEL : Packet
    {
        public CSMG_POSSESSION_PARTNER_CANCEL()
        {
            this.data = new byte[3];
            this.offset = 2;
        }

        public PossessionPosition PossessionPosition
        {
            get
            {
                return (PossessionPosition)this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_POSSESSION_PARTNER_CANCEL();
        }

        public override void Parse(SagaLib.Client client)
        {
            //((MapClient)(client)).OnPossessionCancel(this);
        }

    }
}