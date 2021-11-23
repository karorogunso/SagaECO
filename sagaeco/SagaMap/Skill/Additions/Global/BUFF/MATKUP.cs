using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class MAtkUp : DefaultBuff 
    {
        /// <summary>
        /// Magic Atk Min&Max Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public MAtkUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "MAtkUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("MAtkUp"))
                this.Variable.Remove("MAtkUp");
            this.Variable.Add("MAtkUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.MAtkMinUp = true;
            actor.Buff.MAtkMaxUp = true;
            actor.Status.min_matk_skill += (short)skill.Variable["MAtkUp"];
            actor.Status.max_matk_skill += (short)skill.Variable["MAtkUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.min_matk_skill -= (short)skill.Variable["MAtkUp"];
            actor.Status.max_matk_skill -= (short)skill.Variable["MAtkUp"];
            actor.Buff.MAtkMinUp = false;
            actor.Buff.MAtkMaxUp = false;
            skill.Variable["MAtkUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
