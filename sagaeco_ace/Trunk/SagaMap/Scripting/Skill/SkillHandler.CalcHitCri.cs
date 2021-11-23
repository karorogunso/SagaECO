using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {
        enum AttackResult
        {
            Hit,
            Miss,
            Avoid,
            Critical,
            Guard,
        }
        //物理攻击暴击
        AttackResult CalcAttackResult(Actor sActor, Actor dActor, bool ranged)
        {
            AttackResult res = AttackResult.Hit;
            if (sActor.Status.MissRevenge_hit)
            {
                sActor.Status.MissRevenge_hit = false;
                return res = AttackResult.Critical;
            }
            //计算暴击
            int cri = 5 + sActor.Status.cri_skill;
            int hit_cri = 0;
            if (dActor.Status.Additions.ContainsKey("CriUp"))//暴击标记
                cri += 5 + dActor.Status.Cri_Up_Lv * 5;
            if (Global.Random.Next(0, 99) <= cri)
            {
                res = AttackResult.Critical;
                hit_cri = 35;
            }

            //计算命中            
            int sHit = 0, dAvoid = 0;
            if (ranged)
            {
                sHit = sActor.Status.hit_ranged;
                dAvoid = dActor.Status.avoid_ranged;
            }
            else
            {
                sHit = sActor.Status.hit_melee;
                dAvoid = dActor.Status.avoid_melee;
            }
            if (sActor.type == ActorType.MOB && dActor.type == ActorType.PC)
            {
                ActorMob mob = (ActorMob)sActor;
                Addition[] list = dActor.Status.Additions.Values.ToArray();
                foreach (Addition i in list)
                {
                    if (i.GetType() == typeof(Skill.Additions.Global.SomeTypeAvoUp))
                    {
                        Additions.Global.SomeTypeAvoUp up = (Additions.Global.SomeTypeAvoUp)i;
                        if (up.MobTypes.ContainsKey(mob.BaseData.mobType))
                        {
                            dAvoid += (int)(dAvoid * ((float)up.MobTypes[mob.BaseData.mobType] / 100));
                        }
                    }
                }
            }
            if (dActor.type == ActorType.MOB && sActor.type == ActorType.PC)
            {
                ActorMob mob = (ActorMob)dActor;
                Addition[] list = sActor.Status.Additions.Values.ToArray();
                foreach (Addition i in list)
                {
                    if (i.GetType() == typeof(Skill.Additions.Global.SomeTypeHitUp))
                    {
                        Additions.Global.SomeTypeHitUp up = (Additions.Global.SomeTypeHitUp)i;
                        if (up.MobTypes.ContainsKey(mob.BaseData.mobType))
                        {
                            sHit += (int)(sHit * ((float)up.MobTypes[mob.BaseData.mobType] / 100));
                        }
                    }
                }
            }
            

            int hit = 50 + (sHit - dAvoid);//基础Hit%
            hit = hit + (sActor.Level - dActor.Level) + hit_cri;//+hit_skill...... 补正Hit%
            //hit=hit*hit_skill2
            
            if (hit < 5) hit = 5;
            if (hit > 95) hit = 95;

            if (dActor.Status.attackingActors.Count > 2)
            {
                hit += (dActor.Status.attackingActors.Count - 2) * 15;
            }

            //如果有防御板技能
            if (dActor.Status.Additions.ContainsKey("Parry"))
            {
                dActor.Status.Additions["Parry"].AdditionEnd();
                hit = 30;
            }

            int hit_res = Global.Random.Next(0, 99);
            if (hit_res <= hit)
            {
                if (res != AttackResult.Critical)
                    res = AttackResult.Hit;
            }
            else
            {
                bool guard = false;
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                        {
                            if (Global.Random.Next(0, 99) <= pc.Status.guard_item)
                                guard = true;
                        }
                    }
                }
                if (guard)
                {
                    res = AttackResult.Guard;
                }
                else
                {
                    if (Global.Random.Next(0, 99) <= 30)
                        res = AttackResult.Avoid;
                    else
                        res = AttackResult.Miss;
                }

            }
            if (dActor.Status.Additions.ContainsKey("見切り") && Global.Random.Next(0, 100) < dActor.Status.Syaringan_rate)
            {
                res = AttackResult.Avoid;
            }
            if (sActor.Status.Additions.ContainsKey("PrecisionFire"))
                res = AttackResult.Hit;
            else
            {
                if (sActor.Status.Additions.ContainsKey("AffterCritical"))
                {
                    res = AttackResult.Critical;
                    RemoveAddition(sActor, "AffterCritical");
                }
            }
            return res;
        }
    }
}

