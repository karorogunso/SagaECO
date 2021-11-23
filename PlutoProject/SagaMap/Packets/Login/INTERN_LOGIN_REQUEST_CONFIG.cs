using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Login
{
    public class INTERN_LOGIN_REQUEST_CONFIG : Packet
    {
        public INTERN_LOGIN_REQUEST_CONFIG()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0xFFF1;            
        }

        public SagaLib.Version Version
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}

