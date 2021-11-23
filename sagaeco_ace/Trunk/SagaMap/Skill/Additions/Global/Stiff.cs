using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Stiff : DefaultBuff 
    {
        public Stiff(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Stiff", lifetime)
        {
            if (SkillHandler.Instance.isBossMob(actor))
            {
                if (!actor.Status.Additions.ContainsKey("BOSSImmunityStiff"))
                {
                    DefaultBuff BOSSImmunityStiff = new DefaultBuff(skill, actor, "BOSSImmunityStiff", 30000);
                    SkillHandler.ApplyAddition(actor, BOSSImmunityStiff);
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
            actor.Buff.Stiff = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            SkillHandler.Instance.CancelSkillCast(actor);
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.Stiff = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
