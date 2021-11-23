using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Scripting;

namespace SagaMap.Packets.Server
{
    public class SSMG_NPC_SHOW_PIC : Packet
    {
        public SSMG_NPC_SHOW_PIC()
        {
            this.data = new byte[13];
            this.offset = 2;
            this.ID = 0x067C;
        }

        public string Path
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[this.data.Length + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)buf.Length, 2);
                this.PutBytes(buf, 3);
            }
        }

        public int Unknown
        {
            set
            {
                byte len = GetByte(2);
                this.PutInt(value, (ushort)(3 + len));
            }
        }

        public int Unknown2
        {
            set
            {
                byte len = GetByte(2);
                this.PutInt(value, (ushort)(7 + len));
            }
        }

        public string Title
        {
            set
            {
                byte len = GetByte(2);
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[this.data.Length + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)buf.Length, (ushort)(11 + len));
                this.PutBytes(buf, (ushort)(12 + len));
            }
        }
    }
}

