using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12009 : ISkill
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
                if (pc.TInt["斥候远程模式"] != 1)
                    active = true;

                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "狂乱之舞", active);
                skill.OnAdditionStart += (s, e) =>
                {
                    pc.TInt["狂乱之舞攻击次数"] = 3;
                    pc.TInt["狂乱之舞攻击次数LV"] = level;
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    pc.TInt["狂乱之舞攻击次数"] = 0;
                    pc.TInt["狂乱之舞攻击次数LV"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
    }
}
