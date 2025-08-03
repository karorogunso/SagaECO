using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Stun : DefaultBuff 
    {
        public Stun(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Stun", (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.Stun] / 100)))
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSStun免疫"))
                {
                    DefaultBuff BOSSStun免疫 = new DefaultBuff(skill, actor, "BOSSStun免疫", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSStun免疫);
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
            actor.Buff.Stun = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Stun = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
