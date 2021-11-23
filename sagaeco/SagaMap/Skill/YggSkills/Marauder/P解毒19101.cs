using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S19101:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.Status.Additions.ContainsKey("Poison1"))
                SkillHandler.RemoveAddition(dActor, "Poison1");
            if (dActor.Status.Additions.ContainsKey("Poison2"))
                SkillHandler.RemoveAddition(dActor, "Poison2");
        }
        #endregion
    }
}
