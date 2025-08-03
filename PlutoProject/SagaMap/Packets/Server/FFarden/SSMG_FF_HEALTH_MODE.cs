using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    //表示当前飞空城的健康状态
    public class SSMG_FF_HEALTH_MODE : Packet
    {
        public SSMG_FF_HEALTH_MODE()
        {
            this.data = new byte[3];
            this.offset = 2;
            this.ID = 0x2018;
        }
        /// <summary>
        /// 00 = 正常 01 = 停滞状态 02 = 扣押状态 03 = 维持不能
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
