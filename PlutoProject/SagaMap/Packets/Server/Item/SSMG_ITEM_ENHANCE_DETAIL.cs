using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaMap.Packets.Server
{
    public enum EnhanceType
    {
        HP,
        MP,
        SP,
        Atk,
        MAtk,
        Def,
        MDef,
        Cri = 13,
        AvoidCri,
    }

    public class EnhanceDetail
    {
        public uint material;
        public EnhanceType type;
        public short value;
        public byte exp1;
        public byte exp2;
    }

    public class SSMG_ITEM_ENHANCE_DETAIL : Packet
    {
        public SSMG_ITEM_ENHANCE_DETAIL()
        {
            this.data = new byte[5];
            this.offset = 2;
            this.ID = 0x13C6;
        }

        public List<EnhanceDetail> Items
        {
            set
            {
                byte[] buf = new byte[6 + 12 * value.Count];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                PutByte((byte)value.Count, 2);
                PutByte((byte)value.Count, (ushort)(3 + 4 * value.Count));
                PutByte((byte)value.Count, (ushort)(4 + 6 * value.Count));
                PutByte((byte)value.Count, (ushort)(5 + 8 * value.Count));
                int j = 0;
                foreach (EnhanceDetail i in value)
                {
                    PutUInt(i.material, (ushort)(3 + 4 * j));
                    PutShort((short)i.type, (ushort)(4 + 4 * value.Count + 2 * j));
                    PutShort(i.value, (ushort)(5 + 6 * value.Count + 2 * j));
                    PutByte(0, 6 + 8 * value.Count + j);
                    PutByte(1, 6 + 9 * value.Count + j);
                    PutByte(i.exp1, 6 + 10 * value.Count + j);
                    PutByte(i.exp2, 6 + 11 * value.Count + j);
                    j++;
                }
            }
        }
    }
}

