using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class MPRecUp : DefaultBuff 
    {
        /// <summary>
        /// mp_recover_skill Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public MPRecUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "MPRecUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("MPRecUp"))
                this.Variable.Remove("MPRecUp");
            this.Variable.Add("MPRecUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.MPRecUp = true;
            actor.Buff.HolyFeather = true;
            actor.Status.mp_recover_skill += (short)skill.Variable["MPRecUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.mp_recover_skill -= (short)skill.Variable["MPRecUp"];
            actor.Buff.MPRecUp = false;
            actor.Buff.HolyFeather = false;
            skill.Variable["MPRecUp"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
