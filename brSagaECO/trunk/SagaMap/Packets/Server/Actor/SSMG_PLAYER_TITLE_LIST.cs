using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_PLAYER_TITLE_LIST: Packet
    {
        int length;

        public SSMG_PLAYER_TITLE_LIST()
        {
            this.data = new byte[2]; //3,3,10,10 add up to 214
            this.offset = 2;
            this.ID = 0x2549;
        }

        public void PutUnknown1(List<long> unknown1)
        {
            offset = (ushort)this.data.Length;
            byte[] buf = new byte[this.data.Length + unknown1.Count*8 + 1];
            data.CopyTo(buf, 0);
            this.data = buf;

            this.PutByte((byte)unknown1.Count,offset);
            offset++;
            foreach (ulong unknown in unknown1)
            {
                this.PutULong(unknown, offset);
                offset += 8;
            }
        }
        public void PutUnknown2(List<long> unknown2)
        {
            offset = (ushort)this.data.Length;
            byte[] buf = new byte[this.data.Length + unknown2.Count * 8 + 1];
            data.CopyTo(buf, 0);
            this.data = buf;

            this.PutByte((byte)unknown2.Count, offset);
            offset++;
            foreach (ulong unknown in unknown2)
            {
                this.PutULong(unknown, offset);
                offset += 8;
            }
        }
        public void PutTitles(List<long> titles)
        {
            offset = (ushort)this.data.Length;
            byte[] buf = new byte[this.data.Length + titles.Count * 8 + 1];
            data.CopyTo(buf, 0);
            this.data = buf;

            this.PutByte((byte)titles.Count, offset);
            offset++;
            foreach (ulong unknown in titles)
            {
                this.PutULong(unknown, offset);
                offset += 8;
            }
        }
        public void PutNewTitles(List<long> newtitles)
        {
            offset = (ushort)this.data.Length;
            byte[] buf = new byte[this.data.Length + newtitles.Count * 8 + 1];
            data.CopyTo(buf, 0);
            this.data = buf;

            this.PutByte((byte)newtitles.Count, offset);
            offset++;
            foreach (ulong unknown in newtitles)
            {
                this.PutULong(unknown, offset);
                offset += 8;
            }
        }
    }
}

