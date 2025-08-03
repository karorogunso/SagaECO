using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.ECOShop;

namespace SagaMap.Packets.Server
{
    public class SSMG_VSHOP_CATEGORY : Packet
    {
        public SSMG_VSHOP_CATEGORY()
        {
            this.data = new byte[7];
            this.offset = 2;
            this.ID = 0x0640;
        }

        public uint CurrentPoint
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        
        public Dictionary<uint,ShopCategory> Categories
        {
            set
            {
                int count = 0;
                int j = 0;
                byte[][] names = new byte[value.Count][];
                foreach (ShopCategory i in value.Values)
                {
                    names[j] = Global.Unicode.GetBytes(i.Name);
                    count += (names[j].Length + 1);                    
                    j++;
                }
                byte[] buf = new byte[7 + count + 1 + 4 * names.Length];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.offset = 6;
                this.PutByte((byte)names.Length);
                foreach(byte[] i in names)
                {
                    this.PutByte((byte)i.Length);
                    this.PutBytes(i);
                }
                this.PutByte((byte)names.Length);
                for (int i = 1; i <= names.Length; i++)
                {
                    this.PutInt(i);
                }
            }
        }
    }
}

