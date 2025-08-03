using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Client
{
    public class CSMG_NYASHIELD_VERSION : Packet
    {
        public CSMG_NYASHIELD_VERSION()
        {
            this.size = 6;
            this.offset = 2;
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Client.CSMG_NYASHIELD_VERSION();
        }

        public ushort ver
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override void Parse(SagaLib.Client client)
        {
            //((LoginClient)(client)).OnNya(this);
        }

    }
}