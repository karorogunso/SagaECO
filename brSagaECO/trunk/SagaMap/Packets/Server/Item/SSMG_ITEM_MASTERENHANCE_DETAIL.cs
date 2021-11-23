using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public enum MasterEnhanceType
    {
        STR,
        DEX,
        INT,
        VIT,
        AGI,
        MAG,
    }

    public class MasterEnhanceDetail
    {
        public uint material;
        public MasterEnhanceType type;
        public short minvalue,maxvalue;
    }

    public class SSMG_ITEM_MASTERENHANCE_DETAIL : Packet
    {
        public SSMG_ITEM_MASTERENHANCE_DETAIL()
        {
            this.data = new byte[5];
            this.offset = 2;
            this.ID = 0x1F56;
        }

        public List<MasterEnhanceDetail> Items
        {
            set
            {
                byte[] buf = new byte[5 + 8 * value.Count];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                PutByte((byte)value.Count, 2);
                PutByte((byte)value.Count, (ushort)(3 + 4 * value.Count));
                PutByte((byte)value.Count, (ushort)(4 + 5 * value.Count));
                PutByte((byte)value.Count, (ushort)(5 + 6 * value.Count));

                int j = 0;
                foreach (MasterEnhanceDetail i in value)
                {
                    PutUInt(i.material, (ushort)(3 + 4 * j));
                    PutShort((short)i.type, (ushort)(4 + 4 * value.Count + j));
                    PutShort(i.minvalue, (ushort)(5 + 5 * value.Count + j));
                    PutShort(i.maxvalue, (ushort)(6 + 6 * value.Count + j));
                    j++;
                }
            }
        }
    }
}

