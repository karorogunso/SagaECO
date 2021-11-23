using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    ///鷹眼
    /// </summary>
    public class DistanceArrow : ISkill
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
            float factor = 3.0f + 0.4f * level;
            args.type = ATTACK_TYPE.STAB;

            if (sActor.Status.Additions.ContainsKey("PluralityArrow"))
            {
                factor = factor * 1.5f;
                sActor.Status.Additions["PluralityArrow"].AdditionEnd();
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
