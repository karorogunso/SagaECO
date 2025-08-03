using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_CHAT_SIGN : Packet
    {
        public SSMG_CHAT_SIGN()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x041B;   
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public String Message
        {
            set
            {
                if (value != "")
                {
                    if (value.Substring(value.Length - 1, 1) != "\0") value += "\0";
                    byte[] buf = Global.Unicode.GetBytes(value);
                    byte[] buff = new byte[buf.Length + 7];
                    this.data.CopyTo(buff, 0);
                    this.data = buff;
                    this.PutByte((byte)buf.Length, 6);
                    this.PutBytes(buf, 7);
                }
                else
                {
                    this.PutByte(1, 6);
                }
            }
        }
    }
}

