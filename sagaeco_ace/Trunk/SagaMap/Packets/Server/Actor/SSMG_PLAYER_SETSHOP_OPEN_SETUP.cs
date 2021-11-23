using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_SETSHOP_OPEN_SETUP : Packet
    {
        public SSMG_PLAYER_SETSHOP_OPEN_SETUP()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x190B;
        }

        public uint Unknown
        {
            set
            {
                this.PutUInt(0, 2);
            }
        }


        public string Comment
        {
            set
            {
                byte[] comment = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[7 + comment.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)comment.Length, 6);
                this.PutBytes(comment, 7);
            }
        }
    }
}

