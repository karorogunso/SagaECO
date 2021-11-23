using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaValidation.Packets.Server
{
    public class SSMG_LOGIN_CONFIRMED : Packet
    {
        public SSMG_LOGIN_CONFIRMED()
        {
            this.data = new byte[6];
            this.ID = 0x0030;
        }
    }
}

