using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SagaLib;

namespace SagaProxy.Packets.Server
{
    class RedirectServer : Packet
    {
        public RedirectServer()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x33;
        }

        public byte ServerID
        {
            set
            {
                this.PutByte(value, 2);
            }
        }

        public string IP
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value);
                byte size = (byte)(buf.Length + 1);
                byte[] buff = new byte[size + 8];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte(size, 3);
                this.PutBytes(buf, 4);
            }
        }

        private byte GetDataOffset()
        {
            byte size = this.GetByte(3);
            return (byte)(4 + size);
        }

        public int Port
        {
            set
            {
                this.PutInt(value, GetDataOffset());
            }
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
