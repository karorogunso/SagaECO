using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaMap.Skill.SkillDefinations;

namespace SagaMap.Skill.Additions.Global
{
    public class 自杀CD : ICDBuff
    {
        void ICDBuff.ApplyBuff(Actor actor, int lifetime)
        {
            string BuffName = "自杀CD";
            if (!actor.Status.Additions.ContainsKey(BuffName))
            {
                StableAddition cd = new StableAddition(null, actor, "自杀CD", lifetime);
                SkillHandler.ApplyAddition(actor, cd);
            }
            else
                (actor.Status.Additions[BuffName] as StableAddition).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, lifetime);
        }

    }
}
