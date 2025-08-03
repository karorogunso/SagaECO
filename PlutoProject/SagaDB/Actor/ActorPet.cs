using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB.Actor
{
    public class ActorPet : ActorMob, IStats
    {
        ActorPC owner;
        bool ride;
        Mob.MobData limits;
        bool union;
        public ActorPet()
        {
        }

        public ActorPet(uint mobID, Item.Item pet)
        {
            this.type = ActorType.PET;
            this.baseData = Mob.MobFactory.Instance.GetPetData(mobID);
            this.limits = Mob.MobFactory.Instance.GetPetLimit(mobID);
            if (pet.HP > limits.hp)
                pet.HP = (short)limits.hp;
            this.MaxHP = (uint)(this.baseData.hp + pet.HP);
            this.HP = this.MaxHP;
            this.MaxMP = (uint)(this.baseData.mp + pet.MP);
            this.MP = this.MaxMP;
            this.MaxSP = (uint)(this.baseData.sp + pet.SP);
            this.SP = this.MaxSP;
            this.Name = this.baseData.name;
            this.Speed = this.baseData.speed;
            this.Status.attackType = this.baseData.attackType;
            if (pet.ASPD> limits.aspd)
                pet.ASPD = limits.aspd;
            this.Status.aspd = (short)(this.baseData.aspd + pet.ASPD);
            if (pet.CSPD > limits.cspd)
                pet.CSPD = limits.cspd;
            this.Status.cspd = (short)(this.baseData.cspd + pet.CSPD);
            this.Status.def = (ushort)(this.baseData.def);
            if (pet.Def > limits.def_add)
                pet.Def = (short)limits.def_add;
            this.Status.def_add = (short)(this.baseData.def_add + pet.Def);
            this.Status.mdef = (ushort)(this.baseData.mdef);
            if (pet.MDef > limits.mdef_add)
                pet.MDef = (short)limits.mdef_add;
            this.Status.mdef_add = (short)(this.baseData.mdef_add + pet.MDef);
            if (pet.Atk1 > limits.atk_max)
                pet.Atk1 = (short)limits.atk_max;
            this.Status.min_atk1 = (ushort)(this.baseData.atk_min + pet.Atk1);
            this.Status.max_atk1 = (ushort)(this.baseData.atk_max + pet.Atk1);
            this.Status.min_atk2 = (ushort)(this.baseData.atk_min + pet.Atk2);
            this.Status.max_atk2 = (ushort)(this.baseData.atk_max + pet.Atk2);
            this.Status.min_atk3 = (ushort)(this.baseData.atk_min + pet.Atk3);
            this.Status.max_atk3 = (ushort)(this.baseData.atk_max + pet.Atk3);
            if (pet.MAtk > limits.matk_max)
                pet.MAtk = (short)limits.matk_max;
            this.Status.min_matk = (ushort)(this.baseData.matk_min + pet.MAtk);
            this.Status.max_matk = (ushort)(this.baseData.matk_max + pet.MAtk);

            if (pet.HitMelee > limits.hit_melee)
                pet.HitMelee = (short)limits.hit_melee;
            this.Status.hit_melee = (ushort)(this.baseData.hit_melee + pet.HitMelee);
            if (pet.HitRanged > limits.hit_ranged)
                pet.HitRanged = (short)limits.hit_ranged;
            this.Status.hit_ranged = (ushort)(this.baseData.hit_ranged + pet.HitRanged);
            if (pet.AvoidMelee > limits.avoid_melee)
                pet.AvoidMelee = (short)limits.avoid_melee;
            this.Status.avoid_melee = (ushort)(this.baseData.avoid_melee + pet.AvoidMelee);
            if (pet.AvoidRanged > limits.avoid_ranged)
                pet.AvoidRanged = (short)limits.avoid_ranged;
            
            this.Status.avoid_ranged = (ushort)(this.baseData.avoid_ranged + pet.AvoidRanged);
            this.sightRange = 1500;

            this.PictID = pet.PictID;
        }
        /*
        public ActorPet(uint mobID)
        {
            this.type = ActorType.PET;
            this.baseData = Mob.MobFactory.Instance.GetPetData(mobID);
            this.limits = Mob.MobFactory.Instance.GetPetLimit(mobID);
            this.MaxHP = this.baseData.hp;
            this.HP = this.MaxHP;
            this.MaxMP = this.baseData.mp;
            this.MP = this.MaxMP;
            this.MaxSP = this.baseData.sp;
            this.SP = this.MaxSP;
            this.Name = this.baseData.name;
            this.Speed = this.baseData.speed;
            this.Status.attackType = this.baseData.attackType;
            this.Status.aspd = this.baseData.aspd;
            this.Status.cspd = this.baseData.cspd;
            this.Status.def = this.baseData.def;
            this.Status.def_add = (short)this.baseData.def_add;
            this.Status.mdef = this.baseData.mdef;
            this.Status.mdef_add = (short)this.baseData.mdef_add;
            this.Status.min_atk1 = this.baseData.atk_min;
            this.Status.max_atk1 = this.baseData.atk_max;
            this.Status.min_atk2 = this.baseData.atk_min;
            this.Status.max_atk2 = this.baseData.atk_max;
            this.Status.min_atk3 = this.baseData.atk_min;
            this.Status.max_atk3 = this.baseData.atk_max;
            this.Status.min_matk = this.baseData.matk_min;
            this.Status.max_matk = this.baseData.matk_max;

            this.Status.hit_melee = this.baseData.hit_melee;
            this.Status.hit_ranged = this.baseData.hit_ranged;
            this.Status.avoid_melee = this.baseData.avoid_melee;
            this.Status.avoid_ranged = this.baseData.avoid_ranged;
            this.sightRange = 1500;
        }*/

        public ActorPC Owner { get { return this.owner; } set { this.owner = value; } }
        public bool Ride { get { return this.ride; } set { this.ride = value; } }
        public uint PetID { get { return this.BaseData.id; } }

        public Mob.MobData Limits { get { return this.limits; } }

        /// <summary>
        /// 是否为联合宠物
        /// </summary>
        public bool IsUnion { get { return this.union; } }
    }
}
