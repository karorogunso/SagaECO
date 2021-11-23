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
        Dictionary<byte, Dictionary<ReleaseAbility, int>> releaseabilities = new Dictionary<byte, Dictionary<ReleaseAbility, int>>();
        string name;
        uint id;

        /// <summary>
        /// 该能力向量拥有的具体RA能力，Key为向量等级
        /// </summary>
        public Dictionary<byte, Dictionary<ReleaseAbility, int>> ReleaseAbilities
        {
            get
            {
                return releaseabilities;
            }
        }
        /// <summary>
        /// Iris Ability ID (0-1000 原版能力 1000-2000 组队条件触发长时间面板显示能力 2000-3000 actorpc自身条件影响攻防结果计算时判定能力 3000-4000 对象条件影响攻防结果计算时判定能力 4000+待定)
        /// </summary>
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
