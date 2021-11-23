using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11105 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition 无刀取 = new OtherAddition(null, sActor, "无刀取", 500 + 500 * level);
            SkillHandler.ApplyAddition(sActor, 无刀取);
            硬直 僵直 = new 硬直(null, sActor, 1000);
            SkillHandler.ApplyAddition(sActor, 僵直);
        }
        #endregion
    }
}