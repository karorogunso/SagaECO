using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class ActorShadow : ActorPet
    {
        public ActorShadow(ActorPC creator)
        {
            this.baseData = new SagaDB.Mob.MobData();
            this.baseData.level = creator.Level;
            this.Status.attackType = creator.Status.attackType;
            this.Status.aspd = creator.Status.aspd;
            this.Status.def = creator.Status.def;
            this.Status.def_add = creator.Status.def_add;
            this.Status.mdef = creator.Status.mdef;
            this.Status.mdef_add = creator.Status.mdef_add;
            this.Status.min_atk1 = creator.Status.min_atk1;
            this.Status.max_atk1 = creator.Status.max_atk1;
            this.Status.min_atk2 = creator.Status.min_atk2;
            this.Status.max_atk2 = creator.Status.max_atk2;
            this.Status.min_atk3 = creator.Status.min_atk3;
            this.Status.max_atk3 = creator.Status.max_atk3;
            this.Status.min_matk = creator.Status.min_matk;
            this.Status.max_matk = creator.Status.max_matk;

            this.Status.hit_melee = creator.Status.hit_melee;
            this.Status.hit_ranged = creator.Status.hit_ranged;
            this.Status.avoid_melee = creator.Status.avoid_melee;
            this.Status.avoid_ranged = creator.Status.avoid_ranged;
            this.MaxHP = 1;
            this.HP = 1;
            this.type = ActorType.SHADOW;
            this.sightRange = 1500;
            this.Owner = creator;
            this.Speed = 100;
        }
    }
}
