using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaValidation.Packets.Server
{
    public class SSMG_UNKNOWN_RETURN : Packet
    {
        public SSMG_UNKNOWN_RETURN()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x0030;
        }
    }
}

