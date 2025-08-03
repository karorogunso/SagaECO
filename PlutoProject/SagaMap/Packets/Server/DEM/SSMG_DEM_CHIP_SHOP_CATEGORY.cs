using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaDB.DEMIC;

namespace SagaMap.Packets.Server
{
    public class SSMG_DEM_CHIP_SHOP_CATEGORY : Packet
    {
        public SSMG_DEM_CHIP_SHOP_CATEGORY()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x0636;            
        }

        public List<ChipShopCategory> Categories
        {
            set
            {
                byte[][] names = new byte[value.Count][];
                int j = 0;
                int count = 0;
                foreach (ChipShopCategory i in value)
                {
                    names[j] = Global.Unicode.GetBytes(i.Name);
                    count += (names[j].Length + 1);
                    j++;
                }

                byte[] buf = new byte[4 + value.Count * 4 + count];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                offset = 2;
                this.PutByte((byte)value.Count);
                foreach (ChipShopCategory i in value)
                {
                    this.PutUInt(i.ID);
                }
                this.PutByte((byte)value.Count);
                foreach (byte[] i in names)
                {
                    this.PutByte((byte)i.Length);
                    this.PutBytes(i);
                }
            }
        }
    }
}

