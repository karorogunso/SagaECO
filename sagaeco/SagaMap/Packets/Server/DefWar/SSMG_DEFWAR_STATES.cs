using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;


namespace SagaMap.Packets.Server
{
    public class SSMG_DEFWAR_STATES : Packet
    {

        public SSMG_DEFWAR_STATES()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x1BCB;
            this.PutByte(1, 2);
        }

        public Dictionary<uint,byte> List
        {
            set
            {
                if (value.Count > 0)
                {
                    byte[] buf = new byte[6 + value.Count * 6];
                    this.data.CopyTo(buf, 0);
                    this.data = buf;
                    this.offset = 3;
                    this.PutByte((byte)value.Count);//地图ID
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//百分比
                    this.offset += (ushort)(value.Count);
                    this.PutByte((byte)value.Count);//不明
                    int index = 0;
                    foreach (var i in value)
                    {
                        this.PutUInt(i.Key, (ushort)(4 + index * 4));
                        this.PutByte(i.Value, (ushort)(5 + value.Count * 4 + index));
                        this.PutByte(0x1, (ushort)(6 + value.Count * 5 + index));
                        index++;
                    }
                }
            }
        }
    }
}
