using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// エミッション
    /// </summary>
    class Emission : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 20.0f;
            if (!SkillHandler.Instance.isBossMob(dActor))
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, sActor.WeaponElement, factor);
            else
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
