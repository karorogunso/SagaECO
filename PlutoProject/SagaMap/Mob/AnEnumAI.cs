using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaMap.Mob
{

    /// <summary>
    /// AI的模式
    /// </summary>
    public class AnAIMode
    {
        public BitMask mask;
        uint mobID;

        Dictionary<uint, int> eventAttackingOnShortRange = new Dictionary<uint, int>();
        Dictionary<uint, int> eventAttackingOnLongRange = new Dictionary<uint, int>();
        int maximumRange, minimumRange;

        int eventAttackingSkillRateOnLongRange, eventAttackingSkillRateOnShortRange;
        Dictionary<uint, int> eventAttackingSkillCDOnLongRange, eventAttackingSkillCDOnShortRange;

        public AnAIMode(int value)
        {
            mask = new BitMask(value);
        }

        public AnAIMode()
        {
            mask = new BitMask(0);
        }

        /// <summary>
        /// 怪物ID
        /// </summary>
        public uint MobID { get { return this.mobID; } set { this.mobID = value; } }

        /// <summary>
        /// 怪物的AI模式
        /// </summary>
        public int AI { get { return this.mask.Value; } set { this.mask.Value = value; } }

        /// <summary>
        /// 是否主动
        /// </summary>
        public bool Active { get { return mask.Test(AIFlag.Active); } }

        /// <summary>
        /// 是否不会攻击
        /// </summary>
        public bool NoAttack { get { return mask.Test(AIFlag.NoAttack); } }

        /// <summary>
        /// 是否无法移动
        /// </summary>
        public bool NoMove { get { return mask.Test(AIFlag.NoMove); } }

        /// <summary>
        /// 是否看见玩家会逃跑
        /// </summary>
        public bool RunAway { get { return mask.Test(AIFlag.RunAway); } }

        /// <summary>
        /// 是否帮助同类型怪物
        /// </summary>
        public bool HelpSameType { get { return mask.Test(AIFlag.HelpSameType); } }

        /// <summary>
        /// 是否仇恨治愈魔法
        /// </summary>
        public bool HateHeal { get { return mask.Test(AIFlag.HateHeal); } }

        /// <summary>
        /// 是否仇恨吟唱魔法
        /// </summary>
        public bool HateMagic { get { return mask.Test(AIFlag.HateMagic); } }

        /// <summary>
        /// 是否是象征
        /// </summary>
        public bool Symbol { get { return mask.Test(AIFlag.Symbol); } }
        
        /// <summary>
        /// 是否是象征残骸
        /// </summary>
        public bool SymbolTrash { get { return mask.Test(AIFlag.SymbolTrash); } }


        /// <summary>
        /// 怪物在攻击时(近程)，需要使用技能时的技能列表，Key＝技能，Value＝几率
        /// </summary>
        public Dictionary<uint, int> EventAttackingOnShortRange { get { return this.eventAttackingOnShortRange; } set { this.eventAttackingOnShortRange = value; } }

        /// <summary>
        /// 怪物使用技能的CD(近程)，Key＝技能，Value＝CD
        /// </summary>
        public Dictionary<uint, int> EventAttackingSkillCDOnShortRange { get { return this.eventAttackingSkillCDOnShortRange; } set { this.eventAttackingSkillCDOnShortRange = value; } }

        /// <summary>
        /// 怪物在攻击时(远程)，需要使用技能时的技能列表，Key＝技能，Value＝几率
        /// </summary>
        public Dictionary<uint, int> EventAttackingOnLongRange { get { return this.eventAttackingOnLongRange; } set { this.eventAttackingOnLongRange = value; } }

        /// <summary>
        /// 怪物使用技能的CD(远程)，Key＝技能，Value＝CD
        /// </summary>
        public Dictionary<uint, int> EventAttackingSkillCDOnLongRange { get { return this.eventAttackingOnLongRange; } set { this.eventAttackingOnLongRange = value; } }

        /// <summary>
        /// 仇恨在多少范围内为近战（格）
        /// </summary>
        public int MaximumRange { get { return this.maximumRange; } set { this.maximumRange = value; } }

        /// <summary>
        /// 仇恨在多少范围内为远战（格）
        /// </summary>
        public int MinimumRange { get { return this.minimumRange; } set { this.minimumRange = value; } }

        /// <summary>
        /// 怪物在攻击时(近程)，使用技能的几率
        /// </summary>
        public int EventAttackingSkillRateOnShortRange { get { return this.eventAttackingSkillRateOnShortRange; } set { this.eventAttackingSkillRateOnShortRange = value; } }

        /// <summary>
        /// 怪物在攻击时(远程)，使用技能的几率
        /// </summary>
        public int EventAttackingSkillRateOnLongRange { get { return this.eventAttackingSkillRateOnLongRange; } set { this.eventAttackingSkillRateOnLongRange = value; } }

    }
}
