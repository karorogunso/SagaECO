using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Vulnerable : DefaultDeBuff 
    {
        /// <summary>
        /// Vulnerable
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">伤害值</param>
        public Vulnerable(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 10)
            : base(skill, actor, "Vulnerable", lifetime, 1000)
        {
            if (Variable.ContainsKey("Vulnerable"))
                Variable.Remove("Vulnerable");
            Variable.Add("Vulnerable", amount);
            
            OnAdditionStart += StartEvent;
            OnAdditionEnd += EndEvent;
        }

        void StartEvent(Actor actor, DefaultDeBuff skill)
        {

        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {

        }
    }
}
