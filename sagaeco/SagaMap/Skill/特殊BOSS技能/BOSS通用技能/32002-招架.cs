using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations
{
    public class S32002 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition skill = new OtherAddition(args.skill, sActor, "怪物格挡", 2000, 0);
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}
