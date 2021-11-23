using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    ///  刀背擊（みね打ち）
    /// </summary>
    public class MiNeUChi :ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] StunRate = {0,30,40,50,55,60};
            int[] lifetime={0,8000,7500,7000,6500,6000};
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, StunRate[level]))
            {
                Stun skill = new Stun(args.skill, dActor, lifetime[level]);
                SkillHandler.ApplyAddition(dActor, skill);
            }
            float factor = 1f + 0.1f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);

        }
        #endregion
    }
}
