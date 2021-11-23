using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class IceArrow:ISkill
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
            float factor = 0;
            factor = 1f + 0.3f * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Water, factor);
            if (level == 6)
            {
                factor = 5f;
                Additions.Global.Freeze skill = new SagaMap.Skill.Additions.Global.Freeze(args.skill, dActor, 2500);
                SkillHandler.ApplyAddition(dActor, skill);
            }

        }

        #endregion
    }
}
