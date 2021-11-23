using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Fish
{
    public class Fish
    {
        string name;
        uint id;
        int rate;
        int count;
        /// <summary>
        /// 物品ID
        /// </summary>
        public uint ID { get { return id; } set { this.id = value; } }

        /// <summary>
        ///取得几率 
        /// </summary>
        public int Rate { get { return rate; } set { this.rate = value; } }

        /// <summary>
        /// 取得数量
        /// </summary>
        public int Count { get { return count; } set { this.count = value; } }

        public override string ToString()
        {
            return string.Format("ItemID:{0}, Rate:{1},Count:{2}", this.id, this.rate, this.count);
        }
    }
    /// <summary>
    /// 钓鱼列表
    /// </summary>
    public class FishList
    {
        List<Fish> items = new List<Fish>();
        int totalRate;

        /// <summary>
        /// 钓鱼列表
        /// </summary>
        public List<Fish> Items { get { return this.items; } }

        /// <summary>
        /// 钓鱼列表几率总和
        /// </summary>
        public int TotalRate { get { return this.totalRate; } set { this.totalRate = value; } }

    }
}