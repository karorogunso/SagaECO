using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDB.DualJob
{
    /// <summary>
    /// 副职业所学会的技能信息
    /// </summary>
    [Serializable]
    public class DualJobSkill
    {
        /// <summary>
        /// 副职ID
        /// </summary>
        public byte DualJobID = 0;
        /// <summary>
        /// 技能ID
        /// </summary>
        public ushort SkillID = 0;
        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName = "";
        /// <summary>
        /// 技能对应职业ID
        /// </summary>
        public byte SkillJobID = 0;

        /// <summary>
        /// 学习该技能所需的职业等级
        /// </summary>
        public List<byte> LearnSkillLevel = new List<byte>();
    }
}
