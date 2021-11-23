using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class ActorSkill : ActorMob
    {
        Skill.Skill skill;
        Actor caster;
        bool stackable = false;
        public ActorSkill(Skill.Skill skill, Actor caster)
        {
            this.type = ActorType.SKILL;
            this.skill = skill;
            this.caster = caster;
        }

        public Skill.Skill Skill { get { return this.skill; } set { this.skill = value; } }
        public Actor Caster { get { return this.caster; } }
        public bool Stackable { get { return this.stackable; } set { this.stackable = value; } }
    }
}
