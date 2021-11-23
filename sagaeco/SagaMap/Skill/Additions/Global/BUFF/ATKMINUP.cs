using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class AtkMinUp : DefaultBuff 
    {
        /// <summary>
        /// Physical Atk 1 2 3 Min Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public AtkMinUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "AtkMinUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("AtkMinUp"))
                this.Variable.Remove("AtkMinUp");
            this.Variable.Add("AtkMinUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.AtkMinUp = true;
            actor.Status.min_atk1_skill += (short)skill.Variable["AtkMinUp"];
            actor.Status.min_atk2_skill += (short)skill.Variable["AtkMinUp"];
            actor.Status.min_atk3_skill += (short)skill.Variable["AtkMinUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkMinUp"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkMinUp"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkMinUp"];
            actor.Buff.AtkMinUp = false;
            skill.Variable["AtkMinUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
