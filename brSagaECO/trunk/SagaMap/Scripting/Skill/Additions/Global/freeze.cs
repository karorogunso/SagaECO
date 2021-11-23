using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Freeze : DefaultBuff
    {
        public Freeze(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Frosen", lifetime = (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.Frosen] / 100 - actor.Status.Tenacity)), 100)
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
            this.OnUpdate += this.UpdateEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (skill.Variable.ContainsKey("WaterFrosenElement"))
                skill.Variable.Remove("WaterFrosenElement");
            skill.Variable.Add("WaterFrosenElement", 100);
            actor.Elements[SagaLib.Elements.Water] += 100;
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            actor.Elements[SagaLib.Elements.Water] -= skill.Variable["WaterFrosenElement"];
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            int reduce = 100 / (skill.lifeTime / 100);
            if (skill.Variable["WaterFrosenElement"] > 0)
            {
                skill.Variable["WaterFrosenElement"] -= reduce;
                actor.Elements[SagaLib.Elements.Water] -= reduce;
                if(actor.type == ActorType.PC)
                SagaMap.Manager.MapClientManager.Instance.FindClient((ActorPC)actor).OnPlayerElements();
            }

        }
    }
}
