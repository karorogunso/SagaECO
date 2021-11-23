using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class DEXDOWN : DefaultBuff 
    {
        /// <summary>
        /// DEX DOWN
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">下降数</param>
        public DEXDOWN(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount = 10)
            : base(skill, actor, "DEXDOWN",lifetime, 1000)
        {
            if (this.Variable.ContainsKey("DEXDOWN"))
                this.Variable.Remove("DEXDOWN");
            this.Variable.Add("DEXDOWN", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.dex_skill -= (short)skill.Variable["DEXDOWN"];
            actor.Buff.DEX減少 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.dex_skill += (short)skill.Variable["DEXDOWN"];
            skill.Variable["DEXDOWN"] = 0;
            actor.Buff.DEX減少 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
