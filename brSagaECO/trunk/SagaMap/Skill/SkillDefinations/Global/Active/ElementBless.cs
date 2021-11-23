using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 各屬性祝福
    /// </summary>
    public class ElementBless : ISkill
    {
        public Elements element;
        public ElementBless(Elements e)
        {
            element = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 50000 + 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Rise", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int ElementAdd = 10 * skill.skill.Level;

            //原屬性值
            if (skill.Variable.ContainsKey("ElementRise_" + element.ToString()))
                skill.Variable.Remove("ElementRise_" + element.ToString());
            skill.Variable.Add("ElementRise_" + element.ToString(), ElementAdd);

            actor.Status.elements_skill[element] += ElementAdd;
            actor.Status.attackElements_skill[element] += ElementAdd;
            if (actor.Status.elements_skill[element] > 100)
                actor.Status.elements_skill[element] = 100;
            if (actor.Status.attackElements_skill[element] > 100)
                actor.Status.attackElements_skill[element] = 100;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            short value = (short)skill.Variable["ElementRise_" + element.ToString()];
            if (skill.Variable.ContainsKey("ElementRise_" + element.ToString()))
                skill.Variable.Remove("ElementRise_" + element.ToString());
            //原屬性值
            actor.Status.elements_skill[element] -= value;
            actor.Status.attackElements_skill[element] -= value;
            if (actor.Status.elements_skill[element] < 0)
                actor.Status.elements_skill[element] = 0;
            if (actor.Status.attackElements_skill[element] < 0)
                actor.Status.attackElements_skill[element] = 0;
        }
        #endregion
    }
}
