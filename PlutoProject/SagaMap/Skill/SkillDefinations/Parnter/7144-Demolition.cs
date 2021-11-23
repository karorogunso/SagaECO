using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Scripting;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// デモリッション
    /// </summary>
    public class Demolition : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }



        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 18.0f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);


        }
        #endregion

    }
}
