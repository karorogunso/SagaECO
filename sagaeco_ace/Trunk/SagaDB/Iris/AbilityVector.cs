using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Iris
{
    /// <summary>
    /// Iris卡片能力向量
    /// </summary>
    public class AbilityVector
    {
        Dictionary<byte, Dictionary<ReleaseAbility, int>> abilities = new Dictionary<byte, Dictionary<ReleaseAbility, int>>();
        string name;
        uint id;

        /// <summary>
        /// 该能力向量拥有的具体能力，Key为向量等级
        /// </summary>
        public Dictionary<byte, Dictionary<ReleaseAbility, int>> Abilities
        {
            get
            {
                return abilities;
            }
        }

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
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
