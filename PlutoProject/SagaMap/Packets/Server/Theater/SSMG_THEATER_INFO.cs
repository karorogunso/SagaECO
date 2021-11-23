using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_THEATER_INFO : Packet
    {
        public enum Type
        {
            MESSAGE = 0x0A,
            MOVIE_ADDRESS = 0x14,
            STOP_BGM = 0x1F,
            PLAY = 0x28,
        }

        public SSMG_THEATER_INFO()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x1A90;
        }

        public Type MessageType
        {
            set
            {
                this.PutUInt((uint)value, 2);
            }
        }

        public string Message
        {
            set
            {
                byte[] buf = Global.Unicode.GetBytes(value + "\0");
                byte[] buff = new byte[7 + buf.Length];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)buf.Length, 6);
                this.PutBytes(buf, 7);
            }
        }
    }
}

