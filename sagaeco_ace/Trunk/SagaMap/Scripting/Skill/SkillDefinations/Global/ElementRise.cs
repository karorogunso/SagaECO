
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
    public class ElementRise : ISkill
    {
        public Elements element;
        public ElementRise(Elements e)
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
            int lifetime = 5000 + 1000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, element.ToString() + "Rise", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int ElementAdd = 10 * skill.skill.Level;

            //原屬性值
            if (skill.Variable.ContainsKey("ElementRise_Element"))
                skill.Variable.Remove("ElementRise_Element");
            skill.Variable.Add("ElementRise_Element", actor.Elements[element]);
                                        
            actor.Elements[element] += ElementAdd;
            if (actor.Elements[element] > 100)
            {
                actor.Elements[element] = 100;
            }
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //原屬性值
            actor.Elements[element] = (short)skill.Variable["ElementRise_Element"];
        }
        #endregion
    }
}
