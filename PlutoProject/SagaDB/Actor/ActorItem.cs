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
        uint lootedBy = 0xFFFFFFFF;
        string comment;
        Actor owner;
        bool party;
        DateTime createTime = DateTime.Now;

        public ActorItem(Item.Item item)
        {
            this.item = item;
            this.Name = item.BaseData.name;
            this.type = ActorType.ITEM;            
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
                return this.item.PossessionedActor != null;
            }
        }

        public string Comment { get { return this.comment; } set { this.comment = value; } }

        public uint LootedBy { get { return this.lootedBy; } set { this.lootedBy = value; } }

        public Actor Owner { get { return this.owner; } set { this.owner = value; } }

        public DateTime CreateTime { get { return this.createTime; } set { this.createTime = value; } }

        public bool Party { get { return this.party; } set { this.party = value; } }

        public bool Roll;
    }
}
