using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_SSO_LOGOUT : Packet
    {
        public SSMG_SSO_LOGOUT()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x001D;
        }
    }
}

