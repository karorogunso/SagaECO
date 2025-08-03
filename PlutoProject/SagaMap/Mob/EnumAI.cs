using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaMap.Mob
{
    public enum AIFlag
    {
        Normal,
        Active = 0x1,
        NoAttack = 0x2,
        NoMove = 0x4,
        RunAway = 0x8,
        HelpSameType = 0x10,
        HateHeal = 0x20,
        HateMagic = 0x40,
        Symbol = 0x80,
        SymbolTrash = 0x100,
    }

    /// <summary>
    /// AI的模式
    /// </summary>
    public class AIMode
    {
        //新增AI部分 by:An
        public class SkillsInfo
        {
            public uint SkillID { set; get; }
            public int Delay { set; get; }
        }
        public bool isAnAI { set; get; }

        public class SkillList
        {
            public int MaxHP { set; get; }
            public int MinHP { set; get; }
            public int Rate { set; get; }

            Dictionary<uint, SkillsInfo> anAI_SkillList = new Dictionary<uint, SkillsInfo>();
            public Dictionary<uint, SkillsInfo> AnAI_SkillList { get { return anAI_SkillList; } }
        }

        Dictionary<uint, SkillList> anAI_SkillAssemblage = new Dictionary<uint, SkillList>();
        public Dictionary<uint, SkillList> AnAI_SkillAssemblage { get { return anAI_SkillAssemblage; } }
        //新增结束
        //新增AI部分 by:TT
        public class SkilInfo
        {
            public int Rate { set; get; }
            public int CD { set; get; }
            public int MaxHP { set; get; }
            public int MinHP { set; get; }
            public int HP { set; get; }
            public int OverTime { set; get; }
        }

        int distance = 20;

        int shortCD = 0;
        int longCD = 0;

        Dictionary<uint, SkilInfo> skillOfShort = new Dictionary<uint, SkilInfo>();
        Dictionary<uint, SkilInfo> skillOfLong = new Dictionary<uint, SkilInfo>();
        Dictionary<int, uint> skillOfHP = new Dictionary<int, uint>();


        public bool isNewAI { set; get; }
        public int Distance { set { distance = value; } get { return distance; } }
        public int ShortCD { set { shortCD = value; } get { return shortCD; } }
        public int LongCD { set { longCD = value; } get { return longCD; } }
        public Dictionary<uint, SkilInfo> SkillOfShort { get { return skillOfShort; } }
        public Dictionary<uint, SkilInfo> SkillOfLong { get { return skillOfLong; } }
        public Dictionary<int, uint> SkillOfHP { get { return skillOfHP; } }
        //新增结束


        public BitMask mask;
        uint mobID;

        Dictionary<uint, int> eventAttacking = new Dictionary<uint, int>();
        Dictionary<uint, int> eventMasterCombat = new Dictionary<uint, int>();
        int eventAttackingSkillRate;
        int eventMasterCombatRate;

        public AIMode(int value)
        {
            mask = new BitMask(value);
        }

        public AIMode()
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
        public bool Active { get { return mask.Test(AIFlag.Active); }set { } }

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
        /// 怪物在攻击时，需要使用技能时的技能列表，Key＝技能，Value＝几率
        /// </summary>
        public Dictionary<uint, int> EventAttacking { get { return this.eventAttacking; } set { this.eventAttacking = value; } }

        /// <summary>
        /// 怪物在攻击时，使用技能的几率
        /// </summary>
        public int EventAttackingSkillRate { get { return this.eventAttackingSkillRate; } set { this.eventAttackingSkillRate = value; } }

        /// <summary>
        /// 怪物的主人在战斗中时，需要使用技能时的技能列表，Key＝技能，Value＝几率
        /// </summary>
        public Dictionary<uint, int> EventMasterCombat { get { return this.eventMasterCombat; } set { this.eventMasterCombat = value; } }

        /// <summary>
        /// 怪物的主人在战斗中时，使用技能的几率
        /// </summary>
        public int EventMasterCombatSkillRate { get { return this.eventMasterCombatRate; } set { this.eventMasterCombatRate = value; } }
    }
}
