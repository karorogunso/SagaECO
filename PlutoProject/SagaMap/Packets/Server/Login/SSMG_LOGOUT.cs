using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_LOGOUT : Packet
    {

        public enum Results
        {
            START,
            CANCEL = 0xf9,
        }
        public SSMG_LOGOUT()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x0020;

        }

        public Results Result
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}

