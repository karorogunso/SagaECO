using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 連続射撃いっきますよ～♪
    /// </summary>
    public class ContinuousShooting : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            
            float factor = 1.5f;
            args.argType = SkillArg.ArgType.Attack;
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < 5; i++)
            {
                target.Add(dActor);
            }
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
