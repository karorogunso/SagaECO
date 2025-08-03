using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Mob;
using SagaDB.Actor;

namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// 當怪物為某系列時，命中率提升
    /// </summary>
    public class SomeTypeHitUp : DefaultPassiveSkill
    {
         /// <summary>
        /// 怪物類型
        /// </summary>
        public Dictionary<MobType, ushort> MobTypes = new Dictionary<MobType, ushort>();

        public SomeTypeHitUp(SagaDB.Skill.Skill skill, Actor actor, string name)
            : base(skill, actor, name, true)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        public SomeTypeHitUp(SagaDB.Skill.Skill skill, Actor actor, string name,int peroid,int lifetime)
            : base(skill, actor, name, true,peroid,lifetime)
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
