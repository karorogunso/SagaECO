using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.DEMIC
{
    public class ShopChip
    {
        uint itemID;
        ulong exp, jexp;
        string desc;

        public uint ItemID { get { return this.itemID; } set { this.itemID = value; } }
        public ulong EXP { get { return this.exp; } set { this.exp = value; } }
        public ulong JEXP { get { return this.jexp; } set { this.jexp = value; } }
        public string Description { get { return this.desc; } set { this.desc = value; } }
    }
    public class ChipShopCategory
    {
        uint id;
        string name;
        Dictionary<uint, ShopChip> items = new Dictionary<uint, ShopChip>();
        byte possibleLv;

        public uint ID { get { return this.id; } set { this.id = value; } }

        public string Name { get { return this.name; } set { this.name = value; } }

        public byte PossibleLv { get { return this.possibleLv; } set { this.possibleLv = value; } }

        public Dictionary<uint, ShopChip> Items { get { return this.items; } }
    }
}
