using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PARTY_NAME : Packet
    {
        public CSMG_PARTY_NAME()
        {
            this.offset = 2;
        }

        public string Name
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes((ushort)(this.GetByte(2) - 1), 3));
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PARTY_NAME();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPartyName(this);
        }

    }
}