using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.KnightWar
{
    /// <summary>
    /// 骑士团演习
    /// </summary>
    public class KnightWar
    {
        uint id;
        uint mapID;
        byte maxLV;
        DateTime startTime;
        int duration;

        public uint ID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// 演习地图ID
        /// </summary>
        public uint MapID { get { return this.mapID; } set { this.mapID = value; } }

        /// <summary>
        /// 最高等级限制
        /// </summary>
        public byte MaxLV { get { return this.maxLV; } set { this.maxLV = value; } }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get { return this.startTime; } set { this.startTime = value; } }

        /// <summary>
        /// 持续时间（分钟）
        /// </summary>
        public int Duration { get { return this.duration; } set { this.duration = value; } }

    }
}
