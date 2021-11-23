using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_POSSESSION_CATALOG : HasItemDetail
    {
        public SSMG_POSSESSION_CATALOG()
        {
            this.data = new byte[220];
            this.offset = 2;
            this.ID = 0x178F;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public string comment
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
                byte[] commentbuf = Encoding.UTF8.GetBytes(value);
                byte[] buf = new byte[this.data.Length+commentbuf.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)commentbuf.Length, 6);
                this.PutBytes(commentbuf, 7);
                this.offset =(ushort)(commentbuf.Length + 7);

                this.PutByte(0xD4);
            }
        }
        public uint Index
        {
            set
            {
                this.PutUInt(value);
            }
        }
        public Item Item
        {
            set
            {
                this.ItemDetail = value;
            }
        }
    }
}