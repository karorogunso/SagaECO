using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;

namespace SagaMap.Packets.Server
{
    public class SSMG_0x2549 : Packet
    {
        public SSMG_0x2549()
        {
            this.data = new byte[2];
            this.ID = 0x2549;
        }

        public void PutData1(Dictionary<ulong,ulong> Data)
        {
            List <ulong> keys = new List<ulong>(Data.Keys);
            List<ulong> values = new List<ulong>(Data.Values);

            byte[] buf = new byte[(byte)(this.data.Length + Data.Count * 16 + 2)];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.offset = 2;

            this.PutByte((byte)Data.Count, offset);
            offset++;
            foreach (ulong key in keys)
            {
                this.PutULong(key, offset);
                offset += 8;
            }
            this.PutByte((byte)values.Count, offset);
            offset++;
            foreach (ulong value in values)
            {
                this.PutULong(value, offset);
                offset += 8;
            }
        }

        public void PutData2(Dictionary<ulong, ulong> Data)
        {
            List<ulong> keys = new List<ulong>(Data.Keys);
            List<ulong> values = new List<ulong> (Data.Values);

            byte[] buf = new byte[(byte)(this.data.Length + Data.Count * 16 + 2)];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.offset = 2;

            this.PutByte((byte)Data.Count, offset);
            offset++;
            foreach (ulong key in keys)
            {
                this.PutULong(key, offset);
                offset += 8;
            }
            this.PutByte((byte)values.Count, offset);
            offset++;
            foreach (ulong value in values)
            {
                this.PutULong(value, offset);
                offset += 8;
            }
        }
    }
}
