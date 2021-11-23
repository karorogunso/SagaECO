using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 神圣之光（Ragnarok）
    /// </summary>
    public class HolyLight : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.5f;
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
            }
                
        }
        #endregion
    }
}
