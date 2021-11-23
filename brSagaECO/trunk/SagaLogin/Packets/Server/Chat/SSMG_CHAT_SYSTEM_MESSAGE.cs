using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;

using SagaLib;

namespace SagaLogin.Packets.Server
{
    public class SSMG_CHAT_SYSTEM_MESSAGE : Packet
    {
        public SSMG_CHAT_SYSTEM_MESSAGE()
        {
            this.data = new byte[8];
            this.ID = 0x00BF;
        }

        public enum MessageType
        {
            YellowSystemMessage,
            PurpleAdminMessage,
        }

        public MessageType Type
        {
            set
            {
                this.PutInt((int)value, 2);
            }
        }
        public string Content
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value);
                byte[] buff = new byte[buf.Length + this.data.Length+1];
                this.data.CopyTo(buff, 0);
                this.data = buff;
                this.PutByte((byte)(buf.Length + 1), 6);
                this.PutBytes(buf, 7);
            }
        }
    }
}

