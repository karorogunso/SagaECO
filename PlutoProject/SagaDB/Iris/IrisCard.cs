using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Iris
{
    /// <summary>
    /// 伊利斯卡片
    /// </summary>
    [Serializable]
    public class IrisCard
    {
        uint id;
        string name;
        string serial;
        uint page;
        uint slot;
        int rank;
        uint beforeCard;
        uint nextCard;
        Rarity rarity;
        [NonSerialized]
        BitMask<CardSlot> slots = new BitMask<CardSlot>();
        [NonSerialized]
        Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
        [NonSerialized]
        Dictionary<AbilityVector, int> abilities = new Dictionary<AbilityVector, int>();

        public uint ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Serial { get { return this.serial; } set { this.serial = value; } }

        /// <summary>
        /// 卡册页面
        /// </summary>
        public uint Page { get { return this.page; } set { this.page = value; } }

        /// <summary>
        /// 卡册顺序
        /// </summary>
        public uint Slot { get { return this.slot; } set { this.slot = value; } }

        /// <summary>
        /// 等级
        /// </summary>
        public int Rank { get { return this.rank; } set { this.rank = value; } }
        
        /// <summary>
        /// 上一等级的卡片
        /// </summary>
        public uint BeforeCard { get { return this.beforeCard; } set { this.beforeCard = value; } }

        /// <summary>
        /// 下一等级的卡片
        /// </summary>
        public uint NextCard { get { return this.nextCard; } set { this.nextCard = value; } }

        /// <summary>
        /// 稀有度
        /// </summary>
        public Rarity Rarity { get { return this.rarity; } set { this.rarity = value; } }

        /// <summary>
        /// 是否可以插入项链
        /// </summary>
        public bool CanNeck { get { return slots.Test(CardSlot.胸); } set { slots.SetValue(CardSlot.胸, value); } }

        /// <summary>
        /// 是否可以插入武器
        /// </summary>
        public bool CanWeapon { get { return slots.Test(CardSlot.武器); } set { slots.SetValue(CardSlot.武器, value); } }

        /// <summary>
        /// 是否可以插入衣服
        /// </summary>
        public bool CanArmor { get { return slots.Test(CardSlot.服); } set { slots.SetValue(CardSlot.服, value); } }

        /// <summary>
        /// 属性补正
        /// </summary>
        public Dictionary<Elements, int> Elements { get { return this.elements; } }

        /// <summary>
        /// 能力向量值
        /// </summary>
        public Dictionary<AbilityVector, int> Abilities { get { return this.abilities; } }

        public override string ToString()
        {
            return this.name;
        }
    }
}
