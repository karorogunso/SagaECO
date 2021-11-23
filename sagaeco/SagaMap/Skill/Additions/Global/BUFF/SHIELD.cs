using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class SHIELD : DefaultBuff 
    {
        /// <summary>
        /// 护盾值（受伤时优先扣除）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public SHIELD(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "shield", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("shield"))
                this.Variable.Remove("shield");
            this.Variable.Add("shield", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            actor.SHIELDHP += (uint)skill.Variable["shield"];
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            actor.SHIELDHP = 0;
        }
    }
}
