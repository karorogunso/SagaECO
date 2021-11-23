using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.PProtect;


namespace SagaMap.Packets.Server
{
    public class SSMG_PPROTECT_LIST : Packet
    {
        public SSMG_PPROTECT_LIST()
        {
            this.data = new byte[16];
            this.offset = 2;
            this.ID = 0x235C;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public ushort PageMax { set { this.PutUShort(value, 3); } }

        /// <summary>
        /// 当前页
        /// </summary>
        public ushort Page { set { this.PutUShort(value, 5); } }

        public List<PProtect> List
        {
            set
            {
                int count = value.Count;
                if (count == 0)
                    return;

                byte[] buff = new byte[this.data.Length + count * 15];
                this.data.CopyTo(buff, 0);
                this.data = buff;

                this.PutByte((byte)count, 7);
                this.offset = 8;
                for (int i = 0; i < count; i++)
                {
                    this.offset += setString(value[i].Name, offset);
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.PutUInt(value[i].ID, offset);
                    //this.offset += 4;
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.offset += setString(value[i].Leader.Name, offset);
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.PutByte((byte)value[i].MemberCount, offset);
                    //this.offset += 1;
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.PutByte((byte)value[i].MaxMember, offset);
                    //this.offset += 1;
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    if (value[i].IsPassword)
                        this.PutByte(0x01, offset);
                    else
                        this.PutByte(0x00, offset);
                    //this.offset += 1;
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.PutUInt(value[i].TaskID, offset);
                    //this.offset += 4;
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.offset += setString(value[i].Message, offset);
                }
                this.PutByte((byte)count, offset);
                //this.offset += 1;
                for (int i = 0; i < count; i++)
                {
                    this.PutByte(value[i].IsRun, offset);
                    //this.offset++;
                }

            }
        }

        ushort setString(string str,int i)
        {
            byte[] buf = Global.Unicode.GetBytes(str);
            byte[] buff = new byte[this.data.Length + buf.Length];
            byte size = (byte)buf.Length;
            this.data.CopyTo(buff, 0);
            this.data = buff;
            this.PutByte(size, i);
            this.PutBytes(buf, i + 1);

            return (ushort)(size + 1);
        }
    }
}

