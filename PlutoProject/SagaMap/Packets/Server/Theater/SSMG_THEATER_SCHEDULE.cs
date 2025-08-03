using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_THEATER_SCHEDULE : Packet
    {
        public SSMG_THEATER_SCHEDULE()
        {
            this.data = new byte[12];
            this.offset = 2;
            this.ID = 0x1A9B;
        }

        public int Index
        {
            set
            {
                PutInt(value, 2);
            }
        }

        public uint TicketItem
        {
            set
            {
                PutUInt(value, 6);
            }
        }

        public string Time
        {
            set
            {
                byte[] time = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[12 + time.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                PutByte((byte)time.Length, 10);
                PutBytes(time, 11);
            }
        }

        public string Title
        {
            set
            {
                byte offset = GetByte(10);
                byte[] title = Global.Unicode.GetBytes(value + "\0");
                byte[] buf = new byte[12 + offset + title.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                PutByte((byte)title.Length, (ushort)(11 + offset));
                PutBytes(title, (ushort)(12 + offset));
            }
        }
    }
}

