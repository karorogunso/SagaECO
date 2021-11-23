using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 各種屬性箭
    /// </summary>
    public class ElementArrow :ISkill 
    {
        private SagaLib.Elements ArrowElement = SagaLib.Elements.Neutral;
        public ElementArrow( SagaLib.Elements e)
        {
            ArrowElement = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifeTime = 7500 * 2500 * level;
            args.argType = SkillArg.ArgType.Attack;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, this.ArrowElement, 1.0f);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ElementArrow", lifeTime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.Status.elements_skill.ContainsKey(ArrowElement))
            {
                actor.Status.elements_skill[ArrowElement] += 60;
            }
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.Status.elements_skill.ContainsKey(ArrowElement))
            {
                actor.Status.elements_skill[ArrowElement] -= 60;
            }
        }
        #endregion
    }
}
