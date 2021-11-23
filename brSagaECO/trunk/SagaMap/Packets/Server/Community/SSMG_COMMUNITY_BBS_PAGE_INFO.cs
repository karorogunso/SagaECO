using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.BBS;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_COMMUNITY_BBS_PAGE_INFO : Packet
    {
        public SSMG_COMMUNITY_BBS_PAGE_INFO()
        {
            this.data = new byte[10];
            this.offset = 2;
            this.ID = 0x1B09;
        }

        public List<Post> Posts
        {
            set
            {
                byte[][] names = new byte[value.Count][];
                byte[][] titles = new byte[value.Count][];
                byte[][] contents = new byte[value.Count][];

                int j = 0;
                int count = 0;
                foreach (Post i in value)
                {
                    names[j] = Global.Unicode.GetBytes(i.Name + "\0");
                    titles[j] = Global.Unicode.GetBytes(i.Title + "\0");
                    contents[j] = Global.Unicode.GetBytes(i.Content + "\0");
                    if (contents[j].Length >= 0xfd)
                        count += 4;
                    count += (names[j].Length + titles[j].Length + contents[j].Length + 3);
                    j++;
                }
                byte[] buf = new byte[10 + count + 4 * value.Count];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, 6);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt((uint)((value[i].Date - new DateTime(1970, 1, 1)).TotalSeconds));
                }

                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)names[i].Length);
                    this.PutBytes(names[i]);
                }

                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)titles[i].Length);
                    this.PutBytes(titles[i]);
                }

                this.PutByte((byte)value.Count);
                for (int i = 0; i < value.Count; i++)
                {
                    if (contents[i].Length < 0xfd)
                    {
                        this.PutByte((byte)contents[i].Length);
                        this.PutBytes(contents[i]);
                    }
                    else
                    {
                        this.PutByte(0xfd);
                        this.PutInt(contents[i].Length);
                        this.PutBytes(contents[i]);
                    }
                }
            }
        }
    }
}

