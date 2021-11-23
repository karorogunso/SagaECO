using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.FR2_2
{
    /// <summary>
    /// 大地箭
    /// </summary>
    public class EarthArrow : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcBowAndArrow(pc);
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowDown(sActor);
            float factor = 0;
            factor = 1.3f + 0.2f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (level == 6)
            {
                factor = 3.5f;
                Stone stone = new Stone(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, stone);
            }
        }
        #endregion
    }
}
