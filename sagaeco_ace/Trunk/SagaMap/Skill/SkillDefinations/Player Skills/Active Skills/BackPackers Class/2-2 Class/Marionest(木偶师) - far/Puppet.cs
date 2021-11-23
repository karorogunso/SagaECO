
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Marionest
{
    /// <summary>
    /// 身體調整（パペット）
    /// </summary>
    public class Puppet : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 10 * level;
            int lifetime = 3200 + 800 * level;
            if(SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.硬直 ,rate))
            {
                Stiff skill = new Stiff(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}