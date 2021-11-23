using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_SHOP_SELL_SETUP : Packet
    {
        public SSMG_GOLEM_SHOP_SELL_SETUP()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x17E9;
            MaxItemCount = 100;
        }

        public uint Unknown
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public byte MaxItemCount
        {
            set
            {
                this.PutByte(value, 6);
            }
        }

        public string Comment
        {
            set
            {
                byte[] comment = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[10 + comment.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)comment.Length, 7);
                this.PutBytes(comment, 8);
            }
        }
    }
}

