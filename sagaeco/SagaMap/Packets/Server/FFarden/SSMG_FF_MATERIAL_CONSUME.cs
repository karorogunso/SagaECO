using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_MATERIAL_CONSUME : Packet
    {
        /// <summary>
        /// 飞空城的材料点数消耗(マテリアルコスト)
        /// </summary>
        public SSMG_FF_MATERIAL_CONSUME()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x201D;
        }
        public uint value
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }
    }
}
