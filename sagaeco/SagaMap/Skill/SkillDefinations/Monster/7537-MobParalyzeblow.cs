using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 注射麻醉針
    /// </summary>
    public class MobParalyzeblow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Actor act = dActor;
            int rate = 8;
            int lifetime = 3000;
            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Additions.Global.Stun skill1 = new SagaMap.Skill.Additions.Global.Stun(args.skill, act, lifetime);
                SkillHandler.ApplyAddition(act, skill1);
            }
        }
        #endregion
    }
}