using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_WHOLE : Packet
    {
        public SSMG_CHAT_WHOLE()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x041F;   
        }

        public string Sender
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + 4];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 2);
                this.PutBytes(buf, 3);
            }
        }

        public string Content
        {
            set
            {
                byte size = this.GetByte(2);
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + this.data.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, (ushort)(3 + size));
                this.PutBytes(buf, (ushort)(4 + size));
            }
        }
    }
}

