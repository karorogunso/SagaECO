using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Analysis : DefaultBuff
    {
        public Analysis(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Analysis", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }
        public Analysis(SagaDB.Skill.Skill skill, Actor actor)
            : base(skill, actor, "Analysis", int.MaxValue )
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
        }
    }
}

