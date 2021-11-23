using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.ECOShop;

namespace SagaMap.Packets.Server
{
    public class SSMG_NCSHOP_INFO : Packet
    {
        public SSMG_NCSHOP_INFO()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x0630;
        }

        public uint Point
        {
            set
            {
                PutUInt(value, 2);
            }
        }

        public uint ItemID
        {
            set
            {
                PutUInt(value, 6);
            }
        }

        public string Comment
        {
            set
            {
                byte[] comment = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[11 + comment.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)comment.Length, 10);
                this.PutBytes(comment, 11);
            }
        }
    }
}

