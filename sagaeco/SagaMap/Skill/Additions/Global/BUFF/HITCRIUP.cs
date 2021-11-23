using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;

namespace SagaMap.Skill.Additions.Global
{
    public class HitCriUp : DefaultBuff 
    {
        /// <summary>
        /// hit_critical_skill Up
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="actor"></param>
        /// <param name="lifetime">持续时间，buffee状态时间补正，至少10%持续时间</param>
        /// <param name="amount">Up Value</param>
        public HitCriUp(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int amount=10)
            : base(skill, actor, "HitCriUp", (int)(lifetime * (1f + Math.Max((actor.Status.buffee_bonus / 100), -0.9f))))
        {
            if (this.Variable.ContainsKey("HitCriUp"))
                this.Variable.Remove("HitCriUp");
            this.Variable.Add("HitCriUp", amount);
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.HitCriUp = true;
            actor.Status.hit_critical_skill += (short)skill.Variable["HitCriUp"];
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            PC.StatusFactory.Instance.CalcStatus((ActorPC)actor);
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendPlayerInfo();
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Status.hit_critical_skill -= (short)skill.Variable["HitCriUp"];
            actor.Buff.HitCriUp = false;
            skill.Variable["HitCriUp"] = 0;
            PC.StatusFactory.Instance.CalcStatus((ActorPC)actor);
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendPlayerInfo();
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
