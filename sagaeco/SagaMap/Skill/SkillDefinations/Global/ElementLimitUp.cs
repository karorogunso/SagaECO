
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
    /// 各屬性守護
    /// </summary>
    public class ElementLimitUp : ISkill
    {
        public Elements element;
        public ElementLimitUp(Elements e)
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
            bool active = true;
            DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor,element.ToString()+ "LimitUp", active);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            int ElementAdd = 5 * skill.skill.Level;

            //原屬性值
            if (skill.Variable.ContainsKey("ElementAdd"))
                skill.Variable.Remove("ElementAdd");
            skill.Variable.Add("ElementAdd", ElementAdd);

            actor.Status.elements_skill[element] += ElementAdd;
        }
        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //原屬性值
            actor.Status.elements_skill[element] -= (short)skill.Variable["ElementAdd"];
        }
        #endregion
    }
}

