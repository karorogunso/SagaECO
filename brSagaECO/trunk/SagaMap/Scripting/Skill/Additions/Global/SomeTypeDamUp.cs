using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Mob;

namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// 當怪物為某系列時，迴避率提升
    /// </summary>
    public class SomeTypeDamUp : DefaultPassiveSkill
    {
        /// <summary>
        /// 怪物類型
        /// </summary>
        public Dictionary<MobType, ushort> MobTypes = new Dictionary<MobType, ushort>();

        public SomeTypeDamUp(SagaDB.Skill.Skill skill, Actor actor, string name)
            : base(skill, actor, name, true)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        public SomeTypeDamUp(SagaDB.Skill.Skill skill, Actor actor, string name, int peroid, int lifetime)
            : base(skill, actor, name, true, peroid, lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        public void AddMobType(MobType type, ushort addValue)
        {
            MobTypes.Add(type, addValue);
        }

        void StartEvent(Actor actor, DefaultPassiveSkill skill)
        {
        }

        void EndEvent(Actor actor, DefaultPassiveSkill skill)
        {
        }
    }
}