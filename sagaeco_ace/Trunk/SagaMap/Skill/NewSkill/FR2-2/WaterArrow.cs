using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.FR2_2
{
    /// <summary>
    /// 寒冰箭
    /// </summary>
    public class WaterArrow : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            factor = 1.3f + 0.2f * level;

            if (level == 6)
            {
                factor = 3.5f;
                Freeze f = new Freeze(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, f);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Water, factor);
        }
        #endregion
    }
}
