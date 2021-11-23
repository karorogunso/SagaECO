using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.MasterEnchance
{
    public class MasterEnhanceMaterial
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public MasterEnhanceType Ability { get; set; }
        public short MinValue { get; set; }
        public short MaxValue { get; set; }
    }

    public enum MasterEnhanceType
    {
        STR = 0,
        DEX = 1,
        INT = 2,
        VIT = 3,
        AGI = 4,
        MAG = 5,
    }
}
