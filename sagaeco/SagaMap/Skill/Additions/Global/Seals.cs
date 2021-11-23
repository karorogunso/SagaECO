using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Seals : DefaultBuff
    {
        public Seals(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Seals", lifetime)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.Seals < 5)
                actor.Seals += 1;
            actor.IsSeals = 0;
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.IsSeals == 0)
                actor.Seals = 0;
        }
    }
}
