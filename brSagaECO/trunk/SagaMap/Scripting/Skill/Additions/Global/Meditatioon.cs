using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class Meditatioon : DefaultBuff
    {
        public Meditatioon(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "Meditatioon", lifetime, 1000)
        {
            this.OnUpdate += this.UpdateEvent;
        }
        void UpdateEvent(Actor actor, DefaultBuff skill)
        {
            uint HP_ADD = (uint)(SagaLib.Global.Random.Next(actor.Status.min_matk, actor.Status.max_matk) * 2f + 1f * skill.skill.Level);
            actor.HP += HP_ADD;
            if (actor.HP > actor.MaxHP)
                actor.HP = actor.MaxHP;
            actor.e.OnHPMPSPUpdate(actor);
        }
    }
}
