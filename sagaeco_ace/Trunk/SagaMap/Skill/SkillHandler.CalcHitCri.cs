using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Skill.Additions.Global;

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
            Parry,
        }

        AttackResult CalcAttackResult(Actor sActor, Actor dActor, bool ranged)
        {
            return CalcAttackResult(sActor, dActor, ranged, 0, 0);
        }

        AttackResult CalcAttackResult(Actor sActor, Actor dActor, bool ranged, int shitbonus, int scribonus)
        {
            //命中判定
            AttackResult res = AttackResult.Miss;

            if (sActor.Status.Additions.ContainsKey("PrecisionFire"))
                return AttackResult.Hit;

            float cri = 0f;
            float criavd = 0f;
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

            #region 计算是否自己自己丢失目标

            // 弃用, 来源于不知名的wiki
            //int leveldiff = Math.Abs(sActor.Level - dActor.Level);
            //hit = ((float)(shitbonus + sHit) / ((float)dAvoid * 0.7f)) * 100.0f;
            //if (hit > 100.0f)
            //    hit = 100.0f;
            //if (sActor.Level > dActor.Level)
            //    hit *= (1.0f + (float)((float)leveldiff / (float)100));
            //else
            //    hit *= (1.0f - (float)((float)leveldiff / (float)100));

            float hit = 0;
            //命中率的算法 by wiki http://eco.chouhuangbi.com/doku.php?id=ecowiki:战斗
            hit = Math.Min(100.0f, (100.0f + shitbonus + sHit - dAvoid)) + sActor.Level - dActor.Level;

            if (sActor.Status.Additions.ContainsKey("DarkLight"))
            {
                hit *= (1.0f - (float)(sActor.Status.Additions["DarkLight"] as DefaultBuff).Variable["DarkLight"] / 100.0f);
            }
            if (sActor.Status.Additions.ContainsKey("FlashLight"))
            {
                hit *= (1.0f - ((float)(sActor.Status.Additions["FlashLight"] as DefaultBuff).skill.Level * 6.0f) / 100.0f);
            }

            // 围攻不会降低目标的闪避率,也不会增加自身的命中率 by 野芙
            //if (dActor.Status.attackingActors.Count > 1)
            //{
            //    hit += (dActor.Status.attackingActors.Count - 1) * 10;
            //}

            cri = sActor.Status.hit_critical + sActor.Status.cri_skill + sActor.Status.cri_item;
            criavd = dActor.Status.avoid_critical + dActor.Status.criavd_skill + dActor.Status.criavd_item;

            if (dActor.Status.Additions.ContainsKey("CriUp"))//暴击标记
                cri += (5 + dActor.Status.Cri_Up_Lv * 5);

            float cribonus = scribonus + (cri > criavd ? cri - criavd : 1);

            if (Global.Random.Next(1, 100) <= cribonus)
            {
                res = AttackResult.Critical;
                hit += 35;
            }
            else
            {
                res = AttackResult.Hit;
            }

            if (hit < 5.0f) hit = 5.0f;
            if (hit > 95.0f) hit = 95.0f;

            int hit_res = Global.Random.Next(1, 100);

            #endregion

            if (hit_res > hit)
            {
                return AttackResult.Miss;
            }
            else
            {
                if (Global.Random.Next(1, 100) <= 5)
                    return AttackResult.Avoid;

                bool guard = false;
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                        {
                            if (Global.Random.Next(1, 100) <= (pc.Status.guard_item + pc.Status.guard_skill))
                                guard = true;
                        }
                    }
                }
                else if (dActor.type == ActorType.MOB)
                {
                    if (Global.Random.Next(1, 100) <= (dActor.Status.guard_skill))
                        guard = true;
                }
                if (guard)
                {
                    return AttackResult.Guard;
                }
                else if (dActor.Status.Additions.ContainsKey("見切り") && Global.Random.Next(0, 100) < dActor.Status.Syaringan_rate)
                {
                    return AttackResult.Avoid;
                }
                else if (dActor.Status.Additions.ContainsKey("Parry"))
                {
                    if (Global.Random.Next(1, 100) <= 30)
                        return AttackResult.Parry;
                }
            }

            if (sActor.Status.Additions.ContainsKey("AffterCritical"))
            {
                res = AttackResult.Critical;
                RemoveAddition(sActor, "AffterCritical");
            }

            if (sActor.Status.MissRevenge_hit)
            {
                sActor.Status.MissRevenge_hit = false;
                res = AttackResult.Critical;
            }

            return res;
        }

        AttackResult CalcMagicAttackResult(Actor sActor, Actor dActor)
        {
            //命中判定
            AttackResult res = AttackResult.Hit;

            if (dActor.Status.Additions.ContainsKey("見切り") && Global.Random.Next(0, 100) < dActor.Status.Syaringan_rate)
            {
                return AttackResult.Avoid;
            }
            return res;
        }
        float CalcCritBonus(Actor sActor, Actor dActor, int scribonus)
{
    float res = 1.0f;
    int cri = scribonus, criavd = 0;
    if (sActor.type == ActorType.PC)
    {
        cri = ((ActorPC)sActor).Status.hit_critical + ((ActorPC)sActor).Status.cri_skill + ((ActorPC)sActor).Status.cri_item;
    }
    if (sActor.type == ActorType.MOB)
    {
        cri = ((ActorMob)sActor).Status.hit_critical + ((ActorMob)sActor).Status.cri_skill;
    }
    if (dActor.type == ActorType.PC)
    {
        criavd = ((ActorPC)dActor).Status.avoid_critical + ((ActorPC)dActor).Status.criavd_skill + ((ActorPC)dActor).Status.criavd_item;
    }
    if (dActor.type == ActorType.MOB)
    {
        criavd = ((ActorMob)dActor).Status.avoid_critical + ((ActorMob)dActor).Status.criavd_skill;
    }
    if (dActor.Status.Additions.ContainsKey("CriUp"))//暴击标记
        cri += 5 + dActor.Status.Cri_Up_Lv * 5;

    res = 1.0f + (float)(cri > criavd ? cri - criavd : 0) / 100.0f;
    return res;
}
    }
}

