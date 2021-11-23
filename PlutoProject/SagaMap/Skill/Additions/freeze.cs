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
            : base(skill, actor, "Frosen", lifetime = (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.Frosen] / 100)), 1000)
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSFrosen免疫"))
                {
                    DefaultBuff BOSSFrosen免疫 = new DefaultBuff(skill, actor, "BOSSFrosen免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSFrosen免疫);
                }
                else
                    this.Enabled = false;
            }
            if (actor.Status.Additions.ContainsKey("DebuffDef"))
                this.Enabled = false;

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
            skill.Variable.Add("WaterFrosenElement", actor.Elements[SagaLib.Elements.Water]);
            actor.Elements[SagaLib.Elements.Water] += 100;
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            actor.SpeedCut = 0;
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            actor.Elements[SagaLib.Elements.Water] = skill.Variable["WaterFrosenElement"];
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            /*int reduce = 10 / (skill.lifeTime / 10);
            if (skill.Variable["WaterFrosenElement"] > 0)
            {
                skill.Variable["WaterFrosenElement"] -= reduce;
                actor.Elements[SagaLib.Elements.Water] -= reduce;
                if(actor.type == ActorType.PC)
                SagaMap.Manager.MapClientManager.Instance.FindClient((ActorPC)actor).OnPlayerElements();
            }*/

        }
    }
}
