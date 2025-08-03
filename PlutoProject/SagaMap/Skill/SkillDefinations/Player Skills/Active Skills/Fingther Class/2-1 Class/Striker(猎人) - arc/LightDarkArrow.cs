using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 光之箭（シャイニングアロー）、 黑暗箭（ダークネスアロー）
    /// </summary>
    public class LightDarkArrow : ISkill
    {
        private SagaLib.Elements ArrowElement = SagaLib.Elements.Neutral;
        public LightDarkArrow(SagaLib.Elements e)
        {
            ArrowElement = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return SkillHandler.Instance.CheckPcBowAndArrow(sActor);
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PcArrowDown(sActor);
            float factor = 2.0f + 0.7f * level;
            args.argType = SkillArg.ArgType.Attack;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, this.ArrowElement, factor);
        }
        #endregion
    }
}
