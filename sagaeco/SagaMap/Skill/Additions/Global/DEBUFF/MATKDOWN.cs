using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class MATKDOWN : DefaultBuff 
    {
        /// <summary>
        /// Magic Atk 1 2 3 Min&Max Down
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">下降百分比(10为10%)</param>
        public MATKDOWN(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "MATKDOWN", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("max_matk_down"))
                this.Variable.Remove("max_matk_down");
            this.Variable.Add("max_matk_down", actor.Status.min_matk * (amount / 100));
            if (this.Variable.ContainsKey("min_matk_down"))
                this.Variable.Remove("min_matk_down");
            this.Variable.Add("min_matk_down", actor.Status.max_matk * (amount / 100));

            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
 
            actor.Buff.最大魔法攻撃力減少 = true;
            actor.Buff.最小魔法攻撃力減少 = true;
            actor.Status.min_matk_skill -= (short)skill.Variable["min_matk_down"];
            actor.Status.max_matk_skill -= (short)skill.Variable["max_matk_down"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.min_matk_skill += (short)skill.Variable["min_matk_down"];
            actor.Status.max_matk_skill += (short)skill.Variable["max_matk_down"];
            actor.Buff.最大魔法攻撃力減少 = false;
            actor.Buff.最小魔法攻撃力減少 = false;
            skill.Variable["max_matk_down"] = 0;
            skill.Variable["min_matk_down"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
