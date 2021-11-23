using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;

namespace SagaMap.Packets.Server
{
    public class SSMG_0x240E : Packet
    {
        public SSMG_0x240E()
        {
            this.data = new byte[3];
            this.ID = 0x240E;
        }

        public void PutData (Dictionary<uint,uint> Data)
        {
            List<uint> keys = new List<uint>(Data.Keys);
            List<uint> values = new List<uint>(Data.Values);

            byte[] buf = new byte[(byte)(this.data.Length + Data.Count * 8 + 2)];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.offset = 2;

            this.PutByte((byte)Data.Count, offset);
            offset++;
            foreach (uint key in keys)
            {
                this.PutUInt(key, offset);
                offset += 4;
            }
            this.PutByte((byte)values.Count, offset);
            offset++;
            foreach (uint value in values)
            {
                this.PutUInt(value, offset);
                offset += 4;
            }
        }

        public byte Type
        {
            set
            {
                this.PutByte(value, offset);
            }
        }
    }
}


