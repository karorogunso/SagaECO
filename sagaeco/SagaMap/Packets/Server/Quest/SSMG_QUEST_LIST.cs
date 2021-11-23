using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Quests;

namespace SagaMap.Packets.Server
{
    public class SSMG_QUEST_LIST : Packet
    {        
        public SSMG_QUEST_LIST()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x1964;            
        }

        public List<QuestInfo> Quests
        {
            set
            {
                byte[] buf = new byte[3 + value.Count * 4];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)value.Count);
                foreach (QuestInfo i in value)
                {
                    this.PutUInt(i.ID);
                }

                /*
                byte[][] names=new byte[value.Count][];
                int j = 0;
                int len = 0;
                foreach (QuestInfo i in value)
                {
                    byte[] buff = Global.Unicode.GetBytes(i.Name);
                    names[j++] = buff;
                    len += (buff.Length + 1);
                }
                byte[] buf = new byte[7 + value.Count * 10 + len];
                this.data.CopyTo(buf, 0);
                this.data = buf;

                this.PutByte((byte)value.Count, (ushort)2);
                this.PutByte((byte)value.Count, (ushort)(3 + value.Count * 4));
                this.PutByte((byte)value.Count, (ushort)(4 + value.Count * 5));
                this.PutByte((byte)value.Count, (ushort)(5 + len + value.Count * 5));
                this.PutByte((byte)value.Count, (ushort)(6 + len + value.Count * 9));

                j = 0;
                int len2 = 0;
                foreach (QuestInfo i in value)
                {
                    this.PutUInt(i.ID, (ushort)(3 + j * 4));
                    this.PutByte((byte)i.QuestType, (ushort)(4 + value.Count * 4 + j));
                    this.PutByte((byte)(names[j].Length), (ushort)(5 + len2++ + value.Count * 5));
                    this.PutBytes(names[j], (ushort)(5 + len2 + value.Count * 5));
                    len2 += names[j].Length;
                    this.PutInt(i.TimeLimit, (ushort)(6 + len + value.Count * 5 + j * 4));
                    this.PutByte(i.MinLevel, (ushort)(7 + len + value.Count * 9 + j));
                    j++;
                }//*/
            }
        }
    }
}

