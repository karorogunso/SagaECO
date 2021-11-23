using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;

namespace SagaMap.Packets.Server
{
    public class SSMG_BOND_INVITE_PUPILIN_RESULT : Packet
    {
        public SSMG_BOND_INVITE_PUPILIN_RESULT()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1FE5;
        }
        public int Result
        {
            set
            {
                this.PutInt(value, 2);
            }
        }
        public string Name
        {
            set
            {
                if (value != "")
                {
                    if (value.Substring(value.Length - 1) != "\0")
                        value += "\0";
                }
                else
                    value = "\0";
                byte[] namebuf = Encoding.UTF8.GetBytes(value);
                byte[] buf = new byte[this.data.Length + namebuf.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)namebuf.Length, 6);
                this.PutBytes(namebuf, 7);
                this.offset = (ushort)(namebuf.Length + 7);
            }
        }
    }
}