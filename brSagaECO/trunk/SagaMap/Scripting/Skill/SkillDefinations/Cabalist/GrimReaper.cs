using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    public class GrimReaper : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            args.type = ATTACK_TYPE.BLOW;

            factor = 1.0f + 0.3f * level;

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Dark, factor);
        }

        #endregion
    }
}
