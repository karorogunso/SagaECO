using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Astralist
{
    public class DelayOut : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("DelayOut"))
                return -30;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "DelayOut", 10000);
            skill.OnAdditionEnd += skill_OnAdditionEnd;
            skill.OnAdditionStart += skill_OnAdditionStart;
            SkillHandler.ApplyAddition(sActor, skill);
        }

        void skill_OnAdditionStart(Actor actor, DefaultBuff skill)
        {
            
        }

        void skill_OnAdditionEnd(Actor actor, DefaultBuff skill)
        {

        }
    }
}
