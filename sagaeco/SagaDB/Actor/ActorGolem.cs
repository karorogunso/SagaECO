using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public enum GolemType
    {
        Sell,
        Buy,
        Plant = 3,
        Mineral,
        Food,
        Magic,
        TreasureBox,
        Excavation,
        Any,
        Strange,
        None = 0xff,
    }

    public class GolemShopItem
    {
        uint inventoryID;
        uint itemID;
        ushort count;
        uint price;

        public uint InventoryID { get { return this.inventoryID; } set { this.inventoryID = value; } }
        public uint ItemID { get { return this.itemID; } set { this.itemID = value; } }
        public ushort Count { get { return this.count; } set { this.count = value; } }
        public uint Price { get { return this.price; } set { this.price = value; } }
    }

    /// <summary>
    /// 石像Actor
    /// </summary>
    public class ActorGolem : ActorMob
    {
        public byte AIMode;
        public uint EventID;
        Item.Item item;
        string title;
        ActorPC owner;
        GolemType golemType;
        uint buyLimit;
        ushort motion;
        public bool motion_loop;
        Dictionary<uint, GolemShopItem> sellShop = new Dictionary<uint, GolemShopItem>();
        Dictionary<uint, GolemShopItem> soldItem = new Dictionary<uint, GolemShopItem>();

        Dictionary<uint, GolemShopItem> buyShop = new Dictionary<uint, GolemShopItem>();
        Dictionary<uint, GolemShopItem> boughtItem = new Dictionary<uint, GolemShopItem>();
        
        public ActorGolem()
        {
            this.type = ActorType.GOLEM;
            this.Speed = 410;
            this.sightRange = 1500;
            this.golemType = GolemType.None;
        }

        /// <summary>
        /// 石像道具
        /// </summary>
        public Item.Item Item { get { return this.item; } set { this.item = value; } }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get { return this.title; } set { this.title = value; } }

        /// <summary>
        /// 石像拥有者
        /// </summary>
        public ActorPC Owner { get { return this.owner; } set { this.owner = value; } }

        /// <summary>
        /// 石像类型
        /// </summary>
        public GolemType GolemType { get { return this.golemType; } set { this.golemType = value; } }

        /// <summary>
        /// 石像收购金额上限
        /// </summary>
        public uint BuyLimit { get { return this.buyLimit; } set { this.buyLimit = value; } }

        /// <summary>
        /// 石像贩卖的道具
        /// </summary>
        public Dictionary<uint, GolemShopItem> SellShop { get { return this.sellShop; } set { sellShop = value; } }

        /// <summary>
        /// 石像收购的道具
        /// </summary>
        public Dictionary<uint, GolemShopItem> BuyShop { get { return this.buyShop; } set { BuyShop = value; } }

        /// <summary>
        /// 石像已收购道具
        /// </summary>
        public Dictionary<uint, GolemShopItem> BoughtItem { get { return this.boughtItem; } }

        /// <summary>
        /// 石像已贩卖道具
        /// </summary>
        public Dictionary<uint, GolemShopItem> SoldItem { get { return this.soldItem; } }


        /// <summary>
        /// 动作
        /// </summary>
        public ushort Motion { get { return this.motion; } set { this.motion = value; } }
        public bool MotionLoop { get { return this.motion_loop; } set { this.motion_loop = value; } }


    }
}
