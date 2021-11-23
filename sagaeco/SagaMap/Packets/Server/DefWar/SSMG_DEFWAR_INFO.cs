using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.DefWar;

namespace SagaMap.Packets.Server
{
    public class SSMG_DEFWAR_INFO : Packet
    {
        public SSMG_DEFWAR_INFO()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x1BD0;
        }


        public List<DefWar> List
        {
            set
            {
                if (value.Count > 0)
                {
                    byte[] buf = new byte[11 + value.Count * 20];
                    this.data.CopyTo(buf, 0);
                    this.data = buf;
                    this.offset = 2;
                    this.PutByte((byte)value.Count);//指令顺序
                    this.offset += (ushort)(value.Count);
                    this.PutByte((byte)value.Count);//指令列表
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//结果1
                    this.offset += (ushort)(value.Count);
                    this.PutByte((byte)value.Count);//结果2
                    this.offset += (ushort)(value.Count);
                    this.PutByte((byte)value.Count);//Unknown
                    this.offset += (ushort)(value.Count);
                    this.PutByte((byte)value.Count);//Unknown
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//Unknown
                    this.offset += (ushort)(value.Count * 4);
                    this.PutByte((byte)value.Count);//Unknown
                    for (int i = 0; i < value.Count; i++)
                    {
                        DefWar dw = value[i];
                        this.PutByte(dw.Number, (ushort)(3 + i));
                        this.PutUInt(dw.ID, (ushort)(4 + value.Count + i * 4));
                        this.PutByte(dw.Result1, (ushort)(5 + value.Count * 5 + i));
                        this.PutByte(dw.Result2, (ushort)(6 + value.Count * 6 + i));
                        this.PutByte(dw.unknown1, (ushort)(7 + value.Count * 7 + i));
                        this.PutInt(dw.unknown2, (ushort)(8 + value.Count * 8 + i * 4));
                        this.PutInt(dw.unknown3, (ushort)(9 + value.Count * 12 + i * 4));
                        this.PutInt(dw.unknown4, (ushort)(10 + value.Count * 16 + i * 4));
                    }
                }
            }
        }
    }
}
