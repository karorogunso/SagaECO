using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Treasure
{
    /// <summary>
    /// 宝物物品
    /// </summary>
    public class TreasureItem
    {
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
    /// 宝物列表
    /// </summary>
    public class TreasureList
    {
        string name;
        List<TreasureItem> items = new List<TreasureItem>();
        int totalRate;

        /// <summary>
        /// 列表组名
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// 宝物列表
        /// </summary>
        public List<TreasureItem> Items { get { return this.items; } }

        /// <summary>
        /// 宝物几率总和
        /// </summary>
        public int TotalRate { get { return this.totalRate; } set { this.totalRate = value; } }

        public override string ToString()
        {
            return string.Format("{0},Items:{1},TotalRate:{2}", this.name, this.items.Count, this.totalRate);
        }
    }
}
