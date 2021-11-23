using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{ 
    public class S冰霜之焰 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "冰霜之焰", true);
                skill.OnAdditionStart += (s, e) =>
                {
                    
                };
                skill.OnAdditionEnd += (s, e) =>
                {

                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }

        #endregion
    }
}
