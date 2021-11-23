using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 砍擊裝備系列 (スラッシュ)
    /// </summary>
    public abstract class Slash 
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
        public void SkillProc(Actor sActor, Actor dActor, SkillArg args, byte level,SagaLib.PossessionPosition Position)
        {
            if (dActor.type == ActorType.PC)
            {
                int dePossessionRate = 10 + 20 * level;
                if (SagaLib.Global.Random.Next(0, 99) < dePossessionRate)
                {
                    ActorPC actor = (ActorPC)dActor;
                    SkillHandler.Instance.PossessionCancel(actor, Position);
                }
            }
            float factor = 0.8f + 0.2f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
