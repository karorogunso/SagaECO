using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class ShieldEarth : DefaultBuff 
    {
        /// <summary>
        /// 地属性盾
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间</param>
        /// <param name="amount">上升属性值</param>
        public ShieldEarth(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=20)
            : base(skill, actor, "ShieldEarth", lifetime)
        {
            if (amount > 99) amount = 99;
            if (amount < 1) amount = 1;
            if (this.Variable.ContainsKey("ShieldEarth"))
                this.Variable.Remove("ShieldEarth");
            this.Variable.Add("ShieldEarth", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.ShieldEarth = true;
            actor.Status.elements_skill[SagaLib.Elements.Earth] += skill.Variable["ShieldEarth"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.ShieldEarth = false;
            actor.Status.elements_skill[SagaLib.Elements.Earth] -= skill.Variable["ShieldEarth"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
