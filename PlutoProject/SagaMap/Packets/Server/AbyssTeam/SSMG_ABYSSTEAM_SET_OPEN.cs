using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ABYSSTEAM_SET_OPEN : Packet
    {
        public SSMG_ABYSSTEAM_SET_OPEN()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x22E7;
        }

        public byte Result
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public int Floor
        {
            set
            {
                byte[] buf = new byte[this.data.Length + 4];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutInt(value, 3);
            }
        }
    }
}