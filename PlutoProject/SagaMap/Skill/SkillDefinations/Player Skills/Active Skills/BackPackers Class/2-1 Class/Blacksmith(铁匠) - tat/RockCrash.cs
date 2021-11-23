using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 岩鎖錠（ロッククラッシャー）
    /// </summary>
    public class RockCrash : ISkill 
    {
         #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
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
            float factor = 1.0f;
            if (dActor is ActorMob)
            {
                ActorMob dActorMob = (ActorMob)dActor;
                if (dActorMob.BaseData.mobType.ToString().ToLower().IndexOf("rock") > -1)
                {
                    //加成
                    factor = factor + 1.0f * level;
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            int rate=10+10*level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stiff, rate))
            {
                Stiff skill = new Stiff(args.skill, dActor, 5000);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}

