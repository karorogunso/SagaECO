using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class ShieldWater : DefaultBuff 
    {
        /// <summary>
        /// 水属性盾
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间</param>
        /// <param name="amount">上升属性值</param>
        public ShieldWater(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=20)
            : base(skill, actor, "ShieldWater", lifetime)
        {
            if (amount > 99) amount = 99;
            if (amount < 1) amount = 1;
            if (this.Variable.ContainsKey("ShieldWater"))
                this.Variable.Remove("ShieldWater");
            this.Variable.Add("ShieldWater", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.ShieldWater = true;
            actor.Status.elements_skill[SagaLib.Elements.Water] += skill.Variable["ShieldWater"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.ShieldWater = false;
            actor.Status.elements_skill[SagaLib.Elements.Water] -= skill.Variable["ShieldWater"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
