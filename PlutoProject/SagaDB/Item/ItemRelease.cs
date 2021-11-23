using SagaLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.Item
{
    public class ItemReleaseAbility
    {
        public short MinValue { get; set; }
        public short MaxValue { get; set; }
    }
    public class ItemRelease
    {
        public uint ID { get;set; }
        public string Name { get; set; }
        public ItemReleaseAbility HP = new ItemReleaseAbility();
        public ItemReleaseAbility MP = new ItemReleaseAbility();
        public ItemReleaseAbility SP = new ItemReleaseAbility();
        public ItemReleaseAbility Weight = new ItemReleaseAbility();
        public ItemReleaseAbility Volume = new ItemReleaseAbility();
        public ItemReleaseAbility STR = new ItemReleaseAbility();
        public ItemReleaseAbility DEX = new ItemReleaseAbility();
        public ItemReleaseAbility INT = new ItemReleaseAbility();
        public ItemReleaseAbility VIT = new ItemReleaseAbility();
        public ItemReleaseAbility AGI = new ItemReleaseAbility();
        public ItemReleaseAbility MAG = new ItemReleaseAbility();
        public ItemReleaseAbility ATK1 = new ItemReleaseAbility();
        public ItemReleaseAbility ATK2 = new ItemReleaseAbility();
        public ItemReleaseAbility ATK3 = new ItemReleaseAbility();
        public ItemReleaseAbility MATK = new ItemReleaseAbility();
        public ItemReleaseAbility DEF_ADD = new ItemReleaseAbility();
        public ItemReleaseAbility MDEF_ADD = new ItemReleaseAbility();
        public ItemReleaseAbility SHIT = new ItemReleaseAbility();
        public ItemReleaseAbility LHIT = new ItemReleaseAbility();
        public ItemReleaseAbility MHIT = new ItemReleaseAbility();
        public ItemReleaseAbility SAVOID = new ItemReleaseAbility();
        public ItemReleaseAbility LAVOID = new ItemReleaseAbility();
        public ItemReleaseAbility MAVOID = new ItemReleaseAbility();
        public ItemReleaseAbility CRIT = new ItemReleaseAbility();
        public ItemReleaseAbility CAVOID = new ItemReleaseAbility();
        public Dictionary<Elements, ItemReleaseAbility> Elements = new Dictionary<Elements, ItemReleaseAbility>();
    }
}
