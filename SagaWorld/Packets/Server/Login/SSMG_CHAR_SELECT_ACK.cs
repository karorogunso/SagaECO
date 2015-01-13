using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_CHAR_SELECT_ACK : Packet
    {
       
        public SSMG_CHAR_SELECT_ACK()
        {
            this.data = new byte[18];
            this.offset = 14;
            this.ID = 0xA8;            
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

