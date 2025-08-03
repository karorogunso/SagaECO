using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.FGarden;
using SagaMap.Manager;

namespace SagaMap.Packets.Server
{
    public class SSMG_FF_MATERIAL_POINT : Packet
    {
        /// <summary>
        /// 飞空城的所持材料数(所持マテリアルポイント)
        /// </summary>
        public SSMG_FF_MATERIAL_POINT()
        {
            this.data = new byte[6];
            this.offset = 2;
            this.ID = 0x201C;
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
