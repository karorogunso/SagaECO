using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class MAtkMinToMax : DefaultBuff
    {
        /// <summary>
        /// Magic Atk Min To Max
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="percentage">up percentage</param>
        public MAtkMinToMax(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int percentage = 100)
            : base(skill, actor, "MAtkMinToMax", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("MAtkMinToMax"))
                this.Variable.Remove("MAtkMinToMax");
            this.Variable.Add("MAtkMinToMax", percentage);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.MAtkMinUp = true;
            actor.Status.matk_min_to_max_per += (ushort)(skill.Variable["MAtkMinToMax"]);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.MAtkMinUp = false;
            actor.Status.matk_min_to_max_per -= (ushort)(skill.Variable["MAtkMinToMax"]);
            skill.Variable["MAtkMinToMax"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
