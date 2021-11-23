using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_NYA_WRONG_VERSION : Packet
    {
        public SSMG_NYA_WRONG_VERSION()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x0152;   
        }

    }
}

