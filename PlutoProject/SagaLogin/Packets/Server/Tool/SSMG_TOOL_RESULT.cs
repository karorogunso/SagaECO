using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_TOOL_RESULT : Packet
    {
        public SSMG_TOOL_RESULT()
        {
            this.data = new byte[5];
            this.ID = 0xDDDE;
        }

        public byte type
        {
            set
            {
                PutByte(value, 2);
            }
        }

        public string Text
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + 4];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, 3);
                this.PutBytes(buf, 4);
            }
        }
    }
}

