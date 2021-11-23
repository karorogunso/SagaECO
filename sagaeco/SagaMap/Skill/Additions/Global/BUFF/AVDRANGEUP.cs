using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class AvdRangeUp : DefaultBuff 
    {
        /// <summary>
        /// avoid_ranged_skill Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public AvdRangeUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "AvdRangeUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("AvdRangeUp"))
                this.Variable.Remove("AvdRangeUp");
            this.Variable.Add("AvdRangeUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.AvdRangeUp = true;
            actor.Status.avoid_ranged_skill += (short)skill.Variable["AvdRangeUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["AvdRangeUp"];
            actor.Buff.AvdRangeUp = false;
            skill.Variable["AvdRangeUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
