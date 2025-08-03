using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_LIST : Packet
    {
        public SSMG_FF_LIST()
        {
            this.data = new byte[14];
            this.offset = 2;
            this.ID = 0x200F;
        }

        public uint ActorID
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
        public uint Page
        {
            set
            {
                this.PutUInt(0, 6);
            }
        }
        public uint MaxPaga
        {
            set
            {
                this.PutUInt(value, 10);
            }
        }
        public List<SagaDB.FFarden.FFarden> Entries
        {
            set
            {
                byte[] buf = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, 14);//个数
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i].ID, (ushort)(15 + 4 * i));//RingID(int) * 个数
                }

                buf = new byte[this.data.Length + value.Count * 4 + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, (ushort)(15 + value.Count * 4));//个数
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutUInt(value[i].Level, (ushort)(16 + value.Count * 4 + i * 4));//飞空城等级(int) * 个数
                }

                buf = new byte[this.data.Length + value.Count + 1];
                this.data.CopyTo(buf, 0);
                this.data = buf;
                this.PutByte((byte)value.Count, (ushort)(16 + value.Count * 8));//个数
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].IsLock)
                        this.PutByte(1, (ushort)(17 + value.Count * 8 + i));//是否上锁
                    else
                        this.PutByte(0, (ushort)(17 + value.Count * 8 + i));
                }

                byte[][] strings = new byte[value.Count][];//定义一定数的strings数组
                int size = 0;//定义一个size
                for (int i = 0; i < value.Count; i++)
                {
                    strings[i] = Global.Unicode.GetBytes(value[i].Name);//给第i个组赋予名字
                    size += (strings[i].Length + 1);//令size数字增加 上面的名字长度+1
                }
                buf = new byte[this.data.Length + size + 1];//令buf重新创建长度为 原长度+size+1 的组
                this.data.CopyTo(buf, 0);//将data复制进buf中，从0开始复制
                this.data = buf;//令data等于新的buf
                this.PutByte((byte)value.Count, (ushort)(17 + value.Count * 9));//发送个数
                size = 0;//size清0
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)strings[i].Length, (ushort)(18 + value.Count * 9 + size));//发送第i个名字的字节数
                    this.PutBytes(strings[i], (ushort)(19 + value.Count * 9 + size));//发送名字字节
                    size += (strings[i].Length + 1);//size增加名字长度+1
                }

                strings = new byte[value.Count][];//初始化strings数组
                size = 0;//初始化size
                for (int i = 0; i < value.Count; i++)
                {
                    strings[i] = Global.Unicode.GetBytes(value[i].Name);//遍历飞空城名字进入数组
                    size += (strings[i].Length + 1);//记录数组的长度
                }
                buf = new byte[this.data.Length + size + 1];//初始化buf为 原长度+size+1 的组
                this.data.CopyTo(buf, 0);//将data复制进buf中，从0开始复制
                this.data = buf;//令data等于新的buf
                this.PutByte((byte)value.Count);//发送个数
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)strings[i].Length);//发送飞空城名字字节长度
                    this.PutBytes(strings[i]);//发送飞空城名字字节
                }

                strings = new byte[value.Count][];//初始化strings
                size = 0;//初始化size
                for (int i = 0; i < value.Count; i++)
                {
                    strings[i] = Global.Unicode.GetBytes(value[i].Content);//遍历飞空城简介进入数组
                    size += (strings[i].Length + 1);////记录数组的长度
                }
                buf = new byte[this.data.Length + size + 1];//初始化buf为 原长度+size+1 的组
                this.data.CopyTo(buf, 0);//将data复制进buf中，从0开始复制
                this.data = buf;//令data等于新的buf
                this.PutByte((byte)value.Count);//发送个数
                for (int i = 0; i < value.Count; i++)
                {
                    this.PutByte((byte)strings[i].Length);//发送飞空城简介字节长度
                    this.PutBytes(strings[i]);//发送飞空城简介字节
                }
            }
        }
        
    }
}
