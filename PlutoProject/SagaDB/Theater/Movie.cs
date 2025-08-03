using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Theater
{
    /// <summary>
    /// 电影类
    /// </summary>
    public class Movie
    {
        string name;
        uint mapID;
        uint ticket;
        string url;
        DateTime startTime;
        int duration;

        /// <summary>
        /// 电影的名字
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// 影院地图ID
        /// </summary>
        public uint MapID { get { return this.mapID; } set { this.mapID = value; } }

        /// <summary>
        /// 电影票ID
        /// </summary>
        public uint Ticket { get { return this.ticket; } set { this.ticket = value; } }

        /// <summary>
        /// 电影网址，mms流地址
        /// </summary>
        public string URL { get { return this.url; } set { this.url = value; } }

        /// <summary>
        /// 上映时间
        /// </summary>
        public DateTime StartTime { get { return this.startTime; } set { this.startTime = value; } }

        /// <summary>
        /// 电影长度
        /// </summary>
        public int Duration { get { return this.duration; } set { this.duration = value; } }

    }
}
