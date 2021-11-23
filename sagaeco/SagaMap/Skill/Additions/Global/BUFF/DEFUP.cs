using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class DefUp : DefaultBuff 
    {
        /// <summary>
        /// Physical Def Left Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public DefUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "DefUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("DefUp"))
                this.Variable.Remove("DefUp");
            this.Variable.Add("DefUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.DefUp = true;
            actor.Status.def_skill += (short)skill.Variable["DefUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.def_skill -= (short)skill.Variable["DefUp"];
            actor.Buff.DefUp = false;
            skill.Variable["DefUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
