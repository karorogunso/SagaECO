using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_OBTAIN_MODE : Packet//表示当前飞空城的取得状态
    {
        public SSMG_FF_OBTAIN_MODE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x2017;
        }
        /// <summary>
        /// 00 = 无入手   01 = 作出可能  03 = 已入手
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
