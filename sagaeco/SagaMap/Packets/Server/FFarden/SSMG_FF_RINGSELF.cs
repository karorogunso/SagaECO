using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_RINGSELF : Packet
    {
        public SSMG_FF_RINGSELF()
        {
            this.data = new byte[2];
            this.offset = 2;
            this.ID = 0x201B;
        }

        public string name
        {
            set
            {
                byte[] ffname = Global.Unicode.GetBytes(value);//定于飞空城名称
                byte[] buf = new byte[(byte)ffname.Length + 3];//定义buf，长度为飞空城名称的长度+3
                this.data.CopyTo(buf, 0);//copy
                this.data = buf;
                this.PutByte((byte)ffname.Length, 2);//发送名称字节个数
                this.PutBytes(ffname, 3);//发送名称字节
            }
        }
    }
}