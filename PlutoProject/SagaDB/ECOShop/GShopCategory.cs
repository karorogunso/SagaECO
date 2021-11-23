using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.ECOShop
{
    public class GShopItem
    {
        public uint points;
        public int rental;
        public string comment;
    }
    public class GShopCategory
    {
        uint id;
        string name;
        Dictionary<uint, ShopItem> items = new Dictionary<uint, ShopItem>();

        public uint ID { get { return this.id; } set { this.id = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public Dictionary<uint, ShopItem> Items { get { return this.items; } }
    }
}
