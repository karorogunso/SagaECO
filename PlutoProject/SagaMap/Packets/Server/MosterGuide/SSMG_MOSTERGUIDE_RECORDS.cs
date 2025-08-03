using SagaLib;
using System;
using System.Collections.Generic;

namespace SagaMap.Packets.Server
{
    public class SSMG_MOSTERGUIDE_RECORDS : Packet
    {
        public SSMG_MOSTERGUIDE_RECORDS()
        {
            this.data = new byte[3];
            this.ID = 0x2288;
            this.offset = 2;
        }
        public List<BitMask> Records
        {
            set
            {
                byte[] buf = new byte[this.data.Length + value.Count * 4];
                this.data.CopyTo(buf,0);
                this.data = buf;
                this.PutByte((byte)value.Count,2);
                foreach (BitMask mask in value)
                {
                    this.PutInt(mask.Value);
                }
            }
        }
    }
}
