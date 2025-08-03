using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_RECRUIT : Packet
    {
        public SSMG_COMMUNITY_RECRUIT()
        {
            this.data = new byte[11];
            this.offset = 2;
            this.ID = 0x1B9F;
        }

        public RecruitmentType Type
        {
            set
            {
                this.PutByte((byte)value, 2);
            }
        }

        public int Page
        {
            set
            {
                this.PutInt(value, 3);
            }
        }

        public int MaxPage
        {
            set
            {
                this.PutInt(value, 7);
            }
        }

        public List<Recruitment> Entries
        {
            set
            {
                byte[] buf = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, 11);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i].Creator.CharID, (ushort)(12 + i * 4));
                }

                buf = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, (ushort)(12 + value.Count * 4));
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i].Creator.MapID, (ushort)(13 + value.Count * 4 + i * 4));
                }

                buf = new byte[this.data.Length + value.Count + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, (ushort)(13 + value.Count * 8));
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].Creator.Party != null)
                    {
                        this.PutByte((byte)value[i].Creator.Party.MemberCount, (ushort)(14 + value.Count * 8 + i));
                    }
                    else
                    {
                        this.PutByte(0, (ushort)(14 + value.Count * 8 + i));
                    }
                }

                byte[][] strings = new byte[value.Count][];
                int size = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    strings[i] = Global.Unicode.GetBytes(value[i].Creator.Name);
                    size += (strings[i].Length + 1);
                }
                buf = new byte[this.data.Length + size + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, (ushort)(14 + value.Count * 9));
                size = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)strings[i].Length, (ushort)(15 + value.Count * 9 + size));
                    this.PutBytes(strings[i], (ushort)(16 + value.Count * 9 + size));
                    size += (strings[i].Length + 1);
                }

                strings = new byte[value.Count][];
                size = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    strings[i] = Global.Unicode.GetBytes(value[i].Title);
                    size += (strings[i].Length + 1);
                }
                buf = new byte[this.data.Length + size + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)strings[i].Length);
                    this.PutBytes(strings[i]);
                }

                strings = new byte[value.Count][];
                size = 0;
                for (int i = 0; i < value.Count; i++)
                {
                    strings[i] = Global.Unicode.GetBytes(value[i].Content);
                    size += (strings[i].Length + 1);
                }
                buf = new byte[this.data.Length + size + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)strings[i].Length);
                    this.PutBytes(strings[i]);
                }
            }
        }
    }
}

