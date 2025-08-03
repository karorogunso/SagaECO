using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Confuse : DefaultBuff 
    {
        public Confuse(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Confuse", (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.Confused] / 100)))
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSConfuse免疫"))
                {
                    DefaultBuff BOSSConfuse免疫 = new DefaultBuff(skill, actor, "BOSSConfuse免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSConfuse免疫);
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
            actor.Buff.Confused = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Confused = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
