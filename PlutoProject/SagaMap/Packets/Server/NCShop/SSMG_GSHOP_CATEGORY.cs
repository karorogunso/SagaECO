using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.ECOShop;

namespace SagaMap.Packets.Server
{
    public class SSMG_GSHOP_CATEGORY : Packet
    {
        public SSMG_GSHOP_CATEGORY()
        {
            this.data = new byte[27];
            this.offset = 2;
            this.ID = 0x062C;
        }

        public uint CurrentPoint
        {
            set
            {
                this.PutUInt(value, 11);
            }
        }
        public byte type
        {
            set
            {
                PutByte(value, 6);
            }
        }
        public Dictionary<uint, GShopCategory> Categories
        {
            set
            {
                int count = 0;
                int l = 0;
                List<byte[]> names = new List<byte[]>();
                PutByte((byte)value.Count, 23);
                byte[] buf = new byte[24 + 4 * value.Count+1 + 2];
                data.CopyTo(buf, 0);
                data = buf;
                foreach (var item in value.Values)
                {
                    PutInt(count, (ushort)(24 + 4 * count));
                    count++;
                    byte[] d = Global.Unicode.GetBytes(item.Name);
                    names.Add(d);
                    l += 1 + d.Length;
                }
                ushort s = (ushort)(25 + 4 * count - 1);
                PutByte((byte)value.Count, s);
                s++;
                buf = new byte[s + l];
                data.CopyTo(buf, 0);
                data = buf;
                foreach (var item in names)
                {
                    PutByte((byte)item.Length, s);
                    s++;
                    PutBytes(item);
                    s += (ushort)item.Length;
                }
            }
        }
    }
}

