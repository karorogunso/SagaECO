using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S43201 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 4.0f;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Neutral, factor);
            SkillHandler.Instance.Seals(sActor, dActor, 1);
        }
        #endregion
    }
}