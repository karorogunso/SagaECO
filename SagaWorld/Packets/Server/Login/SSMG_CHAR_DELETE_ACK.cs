using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaWorld.Packets.Server
{
    public class SSMG_CHAR_DELETE_ACK : Packet
    {
        public enum Result
        {
            OK = 0,
            WRONG_DELETE_PASSWORD = 0x9C,            
        }

        public SSMG_CHAR_DELETE_ACK()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0xA6;
        }

        public Result DeleteResult
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }
    }
}

