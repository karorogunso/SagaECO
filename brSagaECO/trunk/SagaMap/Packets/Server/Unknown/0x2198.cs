using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Map;

namespace SagaMap.Packets.Server
{
    public class SSMG_0x2198 : Packet
    {
        public SSMG_0x2198()
        {
            this.data = new byte[2];
            this.ID = 0x2198;
        }

        public void PutData(List<uint> Data)
        {

            byte[] buf = new byte[(byte)(this.data.Length + Data.Count * 4 + 2)];
            this.data.CopyTo(buf, 0);
            this.data = buf;
            this.offset = 2;

            this.PutByte((byte)Data.Count, offset);
            offset++;
            foreach (uint value in Data)
            {
                this.PutUInt(value, offset);
                offset += 4;
            }
        }
    }
}
