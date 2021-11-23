using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.FR2_2
{
    /// <summary>
    /// 火燄箭
    /// </summary>
    public class FireArrow: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcBowAndArrow(pc);
        }


        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowDown(sActor);
            SkillArg args2 = args.Clone();
            float factor = 0;
            factor = 1.0f + 0.5f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Fire, factor);
            //SkillHandler.Instance.MagicAttack(sActor, dActor, args2, SagaLib.Elements.Fire, factor);
            args.AddSameActor(args2);
        }
        #endregion
    }
}
