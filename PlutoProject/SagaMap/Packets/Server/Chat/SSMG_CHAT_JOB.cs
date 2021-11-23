using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_JOB : Packet
    {
        public SSMG_CHAT_JOB()
        {
            this.data = new byte[5];
            this.offset = 2;
            this.ID = 0x0425;   
        }

        public byte Type
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
        public string Sender
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + 5];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 3);
                this.PutBytes(buf, 4);
            }
        }

        public string Content
        {
            set
            {
                byte size = this.GetByte(3);
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + this.data.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, (ushort)(4 + size));
                this.PutBytes(buf, (ushort)(5 + size));
            }
        }
    }
}

