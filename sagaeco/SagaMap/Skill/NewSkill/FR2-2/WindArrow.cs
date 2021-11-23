using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.FR2_2
{
    /// <summary>
    /// 破風箭
    /// </summary>
    public class WindArrow: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            else
                return -5;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = 1.0f + 0.4f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Water, factor);
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Paralyse, 100))
            {
                Paralyse 麻痺 = new Paralyse(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, 麻痺);
            }
        }
        #endregion
    }
}
