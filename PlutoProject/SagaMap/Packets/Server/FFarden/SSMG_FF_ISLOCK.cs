using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_ISLOCK: Packet
    {
        //当前飞空城的入场条件
        public SSMG_FF_ISLOCK()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x201A;
        }
        /// <summary>
        /// 00 = 无限制 01 = 需要密码
        /// </summary>
        public byte value
        {
            set
            {
                this.PutByte(value, 2);
            }
        }
    }
}
