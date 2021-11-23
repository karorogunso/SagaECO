using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SagaLib;

using SagaDB.Actor;

namespace SagaValidation.Packets.Server
{
    public class SSMG_SERVER_LST_SEND : Packet
    {
        public SSMG_SERVER_LST_SEND()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x33;
            
        }
        public string SevName
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + 4];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 2);
                this.PutBytes(buf, 3);
                size = (byte)(buf.Length + 3);
            }
        }
        public string SevIP
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + this.data.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, (ushort)(size));
                this.PutBytes(buf, (ushort)(1 + size));
            }
        }
    }
}