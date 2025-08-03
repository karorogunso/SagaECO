using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_REQUEST_NYA : Packet
    {
        public SSMG_REQUEST_NYA()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x0150;   
        }

    }
}

