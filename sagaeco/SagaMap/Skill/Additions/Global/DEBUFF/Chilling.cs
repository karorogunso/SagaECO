using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class Chilling : DefaultDeBuff
    {
        /// <summary>
        /// 颤栗（aspd&cspd下降，无叠加）
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">aspd&cspd下降百分数</param>
        public Chilling(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "Chilling", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))))
        {
            amount = Math.Min(amount, 100);
            if (this.Variable.ContainsKey("ChillingAspd"))
                this.Variable.Remove("ChillingAspd");
            this.Variable.Add("ChillingAspd", (int)(actor.Status.aspd_bs * amount / 100f));
            if (this.Variable.ContainsKey("ChillingCspd"))
                this.Variable.Remove("ChillingCspd");
            this.Variable.Add("ChillingCspd", (int)(actor.Status.cspd_bs * amount / 100f));
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Chilling = true;
            if (skill.Variable["ChillingAspd"] > 0)
            {
                actor.Status.aspd_skill -= (short)skill.Variable["ChillingAspd"];                
            }
            if (skill.Variable["ChillingCspd"] > 0)
            {
                actor.Status.cspd_skill -= (short)skill.Variable["ChillingCspd"];
            }
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultDeBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Chilling = false;
            if (skill.Variable["ChillingAspd"] > 0)
            {
                actor.Status.aspd_skill += (short)skill.Variable["ChillingAspd"];
            }
            if (skill.Variable["ChillingCspd"] > 0)
            {
                actor.Status.cspd_skill += (short)skill.Variable["ChillingCspd"];
            }
            skill.Variable["ChillingAspd"] = 0;
            skill.Variable["ChillingCspd"] = 0;
            //map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
