using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class HITMELLEDOWN : DefaultBuff 
    {
        /// <summary>
        /// hit_melee_skill down
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">下降百分比(10为10%)</param>
        public HITMELLEDOWN(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "HITMELLEDOWN", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("HITMELLEDOWN"))
                this.Variable.Remove("HITMELLEDOWN");
            this.Variable.Add("HITMELLEDOWN", actor.Status.hit_melee * (amount/100));
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.hit_melee_skill -= (short)skill.Variable["HITMELLEDOWN"];
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.hit_melee_skill += (short)skill.Variable["HITMELLEDOWN"];
            skill.Variable["HITMELLEDOWN"] = 0;
        }
    }
}
