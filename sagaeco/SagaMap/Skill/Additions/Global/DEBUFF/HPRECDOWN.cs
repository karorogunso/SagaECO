using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class HPRecDown : DefaultBuff 
    {
        /// <summary>
        /// hp_recover_skill Down
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，debuffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Down Value</param>
        public HPRecDown(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "HPRecDown", (int)(lifetime * (1f + Math.Max((actor.Status.debuffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("HPRecDown"))
                this.Variable.Remove("HPRecDown");
            this.Variable.Add("HPRecDown", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.HP回復率減少 = true;
            actor.Status.hp_recover_skill -= (short)skill.Variable["HPRecDown"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.hp_recover_skill += (short)skill.Variable["HPRecDown"];
            actor.Buff.HP回復率減少 = false;
            skill.Variable["HPRecDown"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
