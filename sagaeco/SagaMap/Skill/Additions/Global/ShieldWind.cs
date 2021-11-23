using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class ShieldWind : DefaultBuff 
    {
        /// <summary>
        /// 风属性盾
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间</param>
        /// <param name="amount">上升属性值</param>
        public ShieldWind(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 20)
            : base(skill, actor, "ShieldWind", lifetime)
        {
            if (amount > 99) amount = 99;
            if (amount < 1) amount = 1;
            if (this.Variable.ContainsKey("ShieldWind"))
                this.Variable.Remove("ShieldWind");
            this.Variable.Add("ShieldWind", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.ShieldWind = true;
            actor.Status.elements_skill[SagaLib.Elements.Wind] += skill.Variable["ShieldWind"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.ShieldWind = false;
            actor.Status.elements_skill[SagaLib.Elements.Wind] -= skill.Variable["ShieldWind"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
