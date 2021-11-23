using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 魔法劍
    /// </summary>
    public class MagicHit : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
            {
            //    if (!SkillHandler.Instance.CheckWeapon(pc, its))
                return -5;
            }
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1f + 0.4f * level;
            List<Actor> dators = new List<Actor>();
            dators.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, dators, args, SkillHandler.DefType.MDef, SagaLib.Elements.Neutral, 0, factor, false);
        }

        #endregion
    }
}
