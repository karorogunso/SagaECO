using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S43203 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("ReincarnateCD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                DefaultBuff skill = new DefaultBuff(args.skill, sActor, "Reincarnate", 30000);
                SkillHandler.ApplyAddition(sActor, skill);
                DefaultBuff skillCD = new DefaultBuff(args.skill, sActor, "ReincarnateCD", 1200000);
                SkillHandler.ApplyAddition(sActor, skillCD);
                SkillHandler.Instance.ShowEffectOnActor(sActor, 5020);
            }

        }
    }
}