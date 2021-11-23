using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12011 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Job == PC_JOB.HAWKEYE)
                    active = true;

                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "毒素研究", active);
                skill.OnAdditionStart += (s, e) =>
                {
                    sActor.TInt["毒素研究提升"] = level;
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    sActor.TInt["毒素研究提升"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
    }
}
