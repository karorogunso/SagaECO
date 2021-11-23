using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    public class PassiveStr : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, dActor, "PassiveStr", true);
            skill.OnAdditionStart += skill_OnAdditionStart;
            skill.OnAdditionEnd += skill_OnAdditionEnd;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        private void skill_OnAdditionStart(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.str_skill += 20;
        }

        private void skill_OnAdditionEnd(Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.str_skill -= 20;
        }
    }
}
