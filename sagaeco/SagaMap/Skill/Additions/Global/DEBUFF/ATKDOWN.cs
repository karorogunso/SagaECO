using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class ATKDOWN : DefaultBuff 
    {
        /// <summary>
        /// Physical Atk 1 2 3 Min&Max Down
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">下降百分比(10为10%)</param>
        public ATKDOWN(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "ATKDOWN", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("min_atk1_down"))
                this.Variable.Remove("min_atk1_down");
            this.Variable.Add("min_atk1_down", actor.Status.min_atk1* (amount/100));
            if (this.Variable.ContainsKey("min_atk2_down"))
                this.Variable.Remove("min_atk2_down");
            this.Variable.Add("min_atk2_down", actor.Status.min_atk2 * (amount / 100));
            if (this.Variable.ContainsKey("min_atk3_down"))
                this.Variable.Remove("min_atk3_down");
            this.Variable.Add("min_atk3_down", actor.Status.min_atk3 * (amount / 100));

            if (this.Variable.ContainsKey("max_atk1_down"))
                this.Variable.Remove("max_atk1_down");
            this.Variable.Add("max_atk1_down", actor.Status.max_atk1 * (amount / 100));
            if (this.Variable.ContainsKey("max_atk2_down"))
                this.Variable.Remove("max_atk2_down");
            this.Variable.Add("max_atk2_down", actor.Status.max_atk2 * (amount / 100));
            if (this.Variable.ContainsKey("max_atk3_down"))
                this.Variable.Remove("max_atk3_down");
            this.Variable.Add("max_atk3_down", actor.Status.max_atk3 * (amount / 100));


            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.最大攻撃力減少 = true;
            actor.Buff.最小攻撃力減少 = true;
            actor.Status.min_atk1_skill -= (short)skill.Variable["min_atk1_down"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["min_atk2_down"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["min_atk3_down"];
            actor.Status.max_atk1_skill -= (short)skill.Variable["max_atk1_down"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["max_atk2_down"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["max_atk3_down"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.min_atk1_skill += (short)skill.Variable["min_atk1_down"];
            actor.Status.min_atk2_skill += (short)skill.Variable["min_atk2_down"];
            actor.Status.min_atk3_skill += (short)skill.Variable["min_atk3_down"];
            actor.Status.max_atk1_skill += (short)skill.Variable["max_atk1_down"];
            actor.Status.max_atk2_skill += (short)skill.Variable["max_atk2_down"];
            actor.Status.max_atk3_skill += (short)skill.Variable["max_atk3_down"];
            actor.Buff.最大攻撃力減少 = false;
            actor.Buff.最小攻撃力減少 = false;
            skill.Variable["min_atk1_down"] = 0;
            skill.Variable["min_atk2_down"] = 0;
            skill.Variable["min_atk3_down"] = 0;
            skill.Variable["max_atk1_down"] = 0;
            skill.Variable["max_atk2_down"] = 0;
            skill.Variable["max_atk3_down"] = 0;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
