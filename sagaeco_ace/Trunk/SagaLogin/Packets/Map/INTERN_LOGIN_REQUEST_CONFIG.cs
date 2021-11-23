using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaLogin;
using SagaLogin.Network.Client;

namespace SagaLogin.Packets.Map
{
    public class INTERN_LOGIN_REQUEST_CONFIG : Packet
    {
        public INTERN_LOGIN_REQUEST_CONFIG()
        {
            this.offset = 2;
        }

        public SagaLib.Version Version
        {
            get
            {
                return (SagaLib.Version)this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaLogin.Packets.Map.INTERN_LOGIN_REQUEST_CONFIG();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)(client)).OnInternMapRequestConfig(this);
        }

    }
}