using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Npc
{
    public enum ShopType
    {
        None,
        CP,
        ECoin,
        unknown,
    }

    public class Shop
    {
        uint id;
        uint sellrate, buyrate, buylimit;
        List<uint> goods = new List<uint>();
        List<uint> npcs = new List<uint>();
        ShopType type;

        /// <summary>
        /// Shop的ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }

        public List<uint> RelatedNPC { get { return npcs; } }

        /// <summary>
        /// 贩卖倍率
        /// </summary>
        public uint SellRate { get { return this.sellrate; } set { this.sellrate = value; } }

        /// <summary>
        /// 购买倍率
        /// </summary>
        public uint BuyRate { get { return this.buyrate; } set { this.buyrate = value; } }

        /// <summary>
        /// 购买额度
        /// </summary>
        public uint BuyLimit { get { return this.buylimit; } set { this.buylimit = value; } }

        /// <summary>
        /// 商品
        /// </summary>
        public List<uint> Goods { get { return this.goods; } }

        /// <summary>
        /// 商店类型
        /// </summary>
        public ShopType ShopType { get { return this.type; } set { this.type = value; } }
    }
}
