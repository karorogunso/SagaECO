using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19100:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition oa = new OtherAddition(args.skill, sActor, "ApplyPoison",30000);
            SkillHandler.ApplyAddition(sActor, oa);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5119);
        }
        #endregion
    }
}
