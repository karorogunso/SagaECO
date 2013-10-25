using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_CHAR_STATUS : Packet
    {
       
        public SSMG_CHAR_STATUS()
        {
            this.data = new byte[5];
            this.offset = 14;
            this.ID = 0xDD;

            this.PutByte(1, 2);
            this.PutByte(1, 3);
        }

        public uint MapID
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }
    }
}

