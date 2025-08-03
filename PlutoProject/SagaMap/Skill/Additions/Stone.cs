using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Stone : DefaultBuff 
    {
        public Stone(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Stone", (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.Stone] / 100)), 100)
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSStone免疫"))
                {
                    DefaultBuff BOSSStone免疫 = new DefaultBuff(skill, actor, "BOSSStone免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSStone免疫);
                }
                else
                    this.Enabled = false;
            }
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Stone = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (skill.Variable.ContainsKey("StoneFrosenElement"))
                skill.Variable.Remove("StoneFrosenElement");
            skill.Variable.Add("StoneFrosenElement", 100 - actor.Elements[SagaLib.Elements.Earth]);
            actor.Elements[SagaLib.Elements.Earth] = 100;
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Stone = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            actor.Elements[SagaLib.Elements.Earth] -= skill.Variable["StoneFrosenElement"];
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            //int reduce = 100 / (skill.lifeTime / 100);
            //if (skill.Variable["StoneFrosenElement"] > 0)
            //{
            //    skill.Variable["StoneFrosenElement"] -= reduce;
            //    actor.Elements[SagaLib.Elements.Earth] -= reduce;
            //    if (actor.type == ActorType.PC)
            //        SagaMap.Manager.MapClientManager.Instance.FindClient((ActorPC)actor).OnPlayerElements();
            //}

        }
    }
}
