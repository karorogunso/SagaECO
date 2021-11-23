using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class AtkMinToMax : DefaultBuff
    {
        /// <summary>
        /// Physical Atk 1 2 3 Min To Max
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="percentage">up percentage</param>
        public AtkMinToMax(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int percentage = 100)
            : base(skill, actor, "AtkMinToMax", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("AtkMinToMax"))
                this.Variable.Remove("AtkMinToMax");
            this.Variable.Add("AtkMinToMax", percentage);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.AtkMinUp = true;
            actor.Status.atk_min_to_max_per += (ushort)(skill.Variable["AtkMinToMax"]);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.AtkMinUp = false;
            actor.Status.atk_min_to_max_per -= (ushort)(skill.Variable["AtkMinToMax"]);
            skill.Variable["AtkMinToMax"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
