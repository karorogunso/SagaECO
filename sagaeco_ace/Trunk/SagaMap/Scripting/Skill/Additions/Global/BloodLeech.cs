using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class BloodLeech : DefaultBuff
    {
        public float rate;
        public BloodLeech(SagaDB.Skill.Skill skill, Actor actor, int lifetime, float rate)
            : base(skill, actor, "BloodLeech", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.rate = rate;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {

        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {

        }
    }
}
