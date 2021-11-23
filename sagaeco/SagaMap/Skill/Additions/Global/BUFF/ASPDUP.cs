using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class AspdUp : DefaultBuff 
    {
        /// <summary>
        /// aspd_rate_skill Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public AspdUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "AspdUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("AspdUp"))
                this.Variable.Remove("AspdUp");
            this.Variable.Add("AspdUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.AspdUp = true;
            actor.Status.aspd_rate_skill += (short)skill.Variable["AspdUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.aspd_rate_skill -= (short)skill.Variable["AspdUp"];
            actor.Buff.AspdUp = false;
            skill.Variable["AspdUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
