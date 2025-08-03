using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Mob;

namespace SagaMap.Skill.Additions.Global
{
    /// <summary>
    /// 知識用的Buff
    /// </summary>
    public class Knowledge : DefaultPassiveSkill
    {
        /// <summary>
        /// 怪物類型
        /// </summary>
        public List<MobType> MobTypes = new List<MobType>();

        public Knowledge(SagaDB.Skill.Skill skill, Actor actor, string name)
            : base(skill, actor, name, true)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        public Knowledge(SagaDB.Skill.Skill skill, Actor actor, string name,int peroid,int lifetime)
            : base(skill, actor, name, true,peroid,lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }


        public Knowledge(SagaDB.Skill.Skill skill, Actor actor, string name, params MobType[] mobTypes)
            : base(skill, actor, name, true)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;

            MobTypes.AddRange(mobTypes);
        }

        void StartEvent(Actor actor, DefaultPassiveSkill skill)
        {
        }

        void EndEvent(Actor actor, DefaultPassiveSkill skill)
        {
        }
    }
}
