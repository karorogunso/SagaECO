using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Item;

namespace SagaDB.Actor
{
    public class ActorItem : Actor
    {
        Item.Item item;
        bool possession;
        uint lootedBy = 0xFFFFFFFF;

        public ActorItem(Item.Item item)
        {
            this.item = item;
            this.Name = item.BaseData.name;
            this.type = ActorType.ITEM;
            this.possession = false;
        }

        public Item.Item Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        public bool PossessionItem
        {
            get
            {
                return this.possession;
            }
            set
            {
                this.possession = value;
            }
        }

        public uint LootedBy { get { return this.lootedBy; } set { this.lootedBy = value; } }
    }
}
