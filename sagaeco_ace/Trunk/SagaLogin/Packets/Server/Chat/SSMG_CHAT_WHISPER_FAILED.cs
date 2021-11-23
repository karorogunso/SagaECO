using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_CHAT_WHISPER_FAILED : Packet
    {
        public SSMG_CHAT_WHISPER_FAILED()
        {
            this.data = new byte[7];
            this.ID = 0x00CA;
        }

        public uint Result
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public string Receiver
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[buf.Length + 7];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)buf.Length, (ushort)(6));
                this.PutBytes(buf, (ushort)(7));
            }
        }
    }
}

