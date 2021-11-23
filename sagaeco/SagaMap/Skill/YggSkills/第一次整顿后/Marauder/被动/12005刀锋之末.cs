using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12005 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "刀锋之末", true);
            skill.OnAdditionStart += (s, e) =>
            {
                sActor.TInt["刀锋之末破防"] = level * 25;
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                sActor.TInt["刀锋之末破防"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }
    }
}
