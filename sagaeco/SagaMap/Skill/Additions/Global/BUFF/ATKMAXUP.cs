using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class AtkMaxUp : DefaultBuff 
    {
        /// <summary>
        /// Physical Atk 1 2 3 Max Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public AtkMaxUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "AtkMaxUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("AtkMaxUp"))
                this.Variable.Remove("AtkMaxUp");
            this.Variable.Add("AtkMaxUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.AtkMaxUp = true;
            actor.Status.max_atk1_skill += (short)skill.Variable["AtkMaxUp"];
            actor.Status.max_atk2_skill += (short)skill.Variable["AtkMaxUp"];
            actor.Status.max_atk3_skill += (short)skill.Variable["AtkMaxUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.max_atk1_skill -= (short)skill.Variable["AtkMaxUp"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["AtkMaxUp"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["AtkMaxUp"];
            actor.Buff.AtkMaxUp = false;
            skill.Variable["AtkMaxUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
