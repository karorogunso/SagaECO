using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class WeaponWind : DefaultBuff 
    {
        /// <summary>
        /// 风属性武器
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间</param>
        /// <param name="amount">上升属性值</param>
        public WeaponWind(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 20)
            : base(skill, actor, "WeaponWind", lifetime)
        {
            if (amount > 99) amount = 99;
            if (amount < 1) amount = 1;
            if (this.Variable.ContainsKey("WeaponWind"))
                this.Variable.Remove("WeaponWind");
            this.Variable.Add("WeaponWind", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.WeaponWind = true;
            actor.Status.attackelements_skill[SagaLib.Elements.Wind] += skill.Variable["WeaponWind"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.WeaponWind = false;
            actor.Status.attackelements_skill[SagaLib.Elements.Wind] -= skill.Variable["WeaponWind"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
