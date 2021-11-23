using SagaDB.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.Additions.Global
{
    public class MaxHPUP : DefaultBuff
    {
        public MaxHPUP(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "MaxHPUP", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
            int MaxHP_add = 150;
            actor.Status.hp_skill += (short)MaxHP_add;
        }
        void EndEvent(Actor actor, DefaultBuff skill)
        {
            actor.Status.hp_skill -= (short)150;
        }
    }
}
