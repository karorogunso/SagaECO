using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12014 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "残忍", true);

            skill.OnAdditionStart += (s, e) =>
            {
                int stunDmgUp = level * 15 + 5;
                sActor.TInt["残忍提升%"] = stunDmgUp;
            };
            skill.OnAdditionEnd += (s, e) =>
            {
                sActor.TInt["残忍提升%"] = 0;
            };
            SkillHandler.ApplyAddition(sActor, skill);
        }

        #endregion
    }
}
