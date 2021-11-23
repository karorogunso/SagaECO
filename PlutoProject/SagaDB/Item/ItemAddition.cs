using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.Item
{
    [Serializable]
    public class ItemAddition
    {
        public uint ID { get; set; }
        public string Desc { get; set; }
        public uint ItemID { get; set; }
        public List<ItemBonus> BonusList { get; set; }
        public List<string> BonusString { get; set; }

    }
}
