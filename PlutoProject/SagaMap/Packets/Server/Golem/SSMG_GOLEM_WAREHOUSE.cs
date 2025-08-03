using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public class SSMG_GOLEM_WAREHOUSE : Packet
    {
        public SSMG_GOLEM_WAREHOUSE()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x17F3;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 3);
            }
        }

        public string Title
        {
            set
            {
                byte[] title = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[8 + title.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)title.Length, 7);
                this.PutBytes(title, 8);
            }
        }
    }
}

