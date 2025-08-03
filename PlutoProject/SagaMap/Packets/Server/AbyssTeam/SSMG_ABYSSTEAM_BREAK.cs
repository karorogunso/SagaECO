using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_ABYSSTEAM_BREAK : Packet
    {
        public SSMG_ABYSSTEAM_BREAK()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x22F5;
        }
        public byte Result
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        public string TeamName
        {
            set
            {
                byte[] name = Encoding.UTF8.GetBytes(value + "\0");
                byte[] buf = new byte[this.data.Length + name.Length + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)name.Length, 3);
                this.PutBytes(name, 4);
            }
        }
    }
}