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
        public enum AttackResult
        {
            Hit,
            Miss,
            Avoid,
            Critical,
            Guard,
            Parry,
        }

        public AttackResult CalcAttackResult(Actor sActor, Actor dActor, bool ranged)
        {
            return CalcAttackResult(sActor, dActor, ranged, 0, 0);
        }

        public AttackResult CalcAttackResult(Actor sActor, Actor dActor, bool ranged, int shitbonus, int scribonus)
        {
            //命中判定
            AttackResult res = AttackResult.Miss;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills2_2.ContainsKey(973) || pc.DualJobSkill.Exists(x => x.ID == 973))//厄运，一定概率强制回避物理技能
                {

                    //这里取副职的等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 973))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 973).Level;

                    //这里取主职的等级
                    var mainlv = 0;
                    if (pc.Skills2_2.ContainsKey(973))
                        mainlv = pc.Skills2_2[973].Level;

                    int godness = 0;
                    if (pc.Skills3.ContainsKey(1114) || pc.DualJobSkill.Exists(x => x.ID == 1114))//幸运女神
                    {

                        //这里取副职的等级
                        var duallv2 = 0;
                        if (pc.DualJobSkill.Exists(x => x.ID == 1114))
                            duallv2 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1114).Level;

                        //这里取主职的等级
                        var mainlv2 = 0;
                        if (pc.Skills2_2.ContainsKey(1114))
                            mainlv2 = pc.Skills2_2[1114].Level;

                        godness = Math.Max(duallv, mainlv) * 2;
                    }

                    if (SagaLib.Global.Random.Next(0, 99) < Math.Max(duallv, mainlv) * 4 + godness)
                    {
                        return AttackResult.Avoid;
                    }
                }
            }


            if (sActor.Status.Additions.ContainsKey("PrecisionFire"))
                return AttackResult.Hit;
            if (sActor.Status.Additions.ContainsKey("RoyalDealer"))
                return AttackResult.Hit;


            if (dActor.Status.Additions.ContainsKey("FortressCircleSEQ"))
            {
                return AttackResult.Miss;
            }
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
            //等级差计算

            hit = Math.Min(100.0f, ((shitbonus + sHit) / (dAvoid * 0.7f)) * 100.0f);



            var leveldiff = sActor.Level - dActor.Level;


            if (leveldiff < 0)
                hit *= (1.0f - (Math.Abs(sActor.Level - dActor.Level) / 100.0f * (1.0f - sActor.Status.level_hit_iris / 100.0f)));
            else
                hit *= (1.0f + (Math.Abs(sActor.Level - dActor.Level) / 100.0f * (1.0f - sActor.Status.level_avoid_iris / 100.0f)));



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

            cri = sActor.Status.hit_critical + sActor.Status.cri_skill + sActor.Status.cri_skill_rate + sActor.Status.cri_item + sActor.Status.hit_critical_iris;//cri_skill_rate仅影响暴击率，不影响暴击伤害
            criavd = dActor.Status.avoid_critical + dActor.Status.criavd_skill + dActor.Status.criavd_item + dActor.Status.avoid_critical_iris;

            if (dActor.Status.Additions.ContainsKey("CriUp"))//暴击标记
                cri += (5 + dActor.Status.Cri_Up_Lv * 5);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                //iris卡种族暴击率提升
                if (dActor.Race == Race.HUMAN && pc.Status.human_cri_up_iris > 0)
                {
                    cri += pc.Status.human_cri_up_iris;
                }
                if (dActor.Race == Race.BIRD && pc.Status.bird_cri_up_iris > 0)
                {
                    cri += pc.Status.bird_cri_up_iris;
                }
                else if (dActor.Race == Race.ANIMAL && pc.Status.animal_cri_up_iris > 0)
                {
                    cri += pc.Status.animal_cri_up_iris;
                }
                else if (dActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_cri_up_iris > 0)
                {
                    cri += pc.Status.magic_c_cri_up_iris;
                }
                else if (dActor.Race == Race.PLANT && pc.Status.plant_cri_up_iris > 0)
                {
                    cri += pc.Status.plant_cri_up_iris;
                }
                else if (dActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_cri_up_iris > 0)
                {
                    cri += pc.Status.water_a_cri_up_iris;
                }
                else if (dActor.Race == Race.MACHINE && pc.Status.machine_cri_up_iris > 0)
                {
                    cri += pc.Status.machine_cri_up_iris;
                }
                else if (dActor.Race == Race.ROCK && pc.Status.rock_cri_up_iris > 0)
                {
                    cri += pc.Status.rock_cri_up_iris;
                }
                else if (dActor.Race == Race.ELEMENT && pc.Status.element_cri_up_iris > 0)
                {
                    cri += pc.Status.element_cri_up_iris;
                }
                else if (dActor.Race == Race.UNDEAD && pc.Status.undead_cri_up_iris > 0)
                {
                    cri += pc.Status.undead_cri_up_iris;
                }
            }
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

            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                //iris卡对种族命中率提升
                if (dActor.Race == Race.HUMAN && pc.Status.human_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.human_hit_up_iris / 100.0f));
                }
                if (dActor.Race == Race.BIRD && pc.Status.bird_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.bird_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.ANIMAL && pc.Status.animal_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.animal_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.magic_c_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.PLANT && pc.Status.plant_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.plant_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.water_a_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.MACHINE && pc.Status.machine_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.machine_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.ROCK && pc.Status.rock_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.rock_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.ELEMENT && pc.Status.element_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.element_hit_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.UNDEAD && pc.Status.undead_hit_up_iris > 100)
                {
                    hit = (hit * (pc.Status.undead_hit_up_iris / 100.0f));
                }
            }



            if (hit < 5.0f) hit = 5.0f;
            if (hit > 95.0f) hit = 95.0f;
            int hit_res = Global.Random.Next(1, 100);
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                //iris卡种族回避率提升
                if (sActor.Race == Race.HUMAN && pc.Status.human_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.human_avoid_up_iris / 100.0f));
                }
                if (sActor.Race == Race.BIRD && pc.Status.bird_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.bird_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.ANIMAL && pc.Status.animal_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.animal_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.magic_c_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.PLANT && pc.Status.plant_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.plant_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.water_a_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.MACHINE && pc.Status.machine_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.machine_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.ROCK && pc.Status.rock_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.rock_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.ELEMENT && pc.Status.element_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.element_avoid_up_iris / 100.0f));
                }
                else if (sActor.Race == Race.UNDEAD && pc.Status.undead_avoid_up_iris > 100)
                {
                    hit_res = (int)(hit_res * (pc.Status.undead_avoid_up_iris / 100.0f));
                }
            }
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
                            if (Global.Random.Next(1, 100) <= (pc.Status.guard_item + pc.Status.guard_skill + pc.Status.guard_iris))
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

        public AttackResult CalcMagicAttackResult(Actor sActor, Actor dActor)
        {
            //命中判定
            AttackResult res = AttackResult.Hit;

            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills2_2.ContainsKey(960) || pc.DualJobSkill.Exists(x => x.ID == 960))//强运，一定概率强制回避魔法技能
                {

                    //这里取副职的等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 960))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 960).Level;

                    //这里取主职的等级
                    var mainlv = 0;
                    if (pc.Skills2_2.ContainsKey(960))
                        mainlv = pc.Skills2_2[960].Level;

                    int godness = 0;
                    if (pc.Skills3.ContainsKey(1114) || pc.DualJobSkill.Exists(x => x.ID == 1114))//幸运女神
                    {

                        //这里取副职的等级
                        var duallv2 = 0;
                        if (pc.DualJobSkill.Exists(x => x.ID == 1114))
                            duallv2 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1114).Level;

                        //这里取主职的等级
                        var mainlv2 = 0;
                        if (pc.Skills2_2.ContainsKey(1114))
                            mainlv2 = pc.Skills2_2[1114].Level;

                        godness = Math.Max(duallv, mainlv) * 2;
                    }

                    if (SagaLib.Global.Random.Next(0, 99) < Math.Max(duallv, mainlv) * 4 + godness)
                    {
                        return AttackResult.Avoid;
                    }
                }
            }

            if (dActor.Status.Additions.ContainsKey("見切り") && Global.Random.Next(0, 100) < dActor.Status.Syaringan_rate)
            {
                return AttackResult.Avoid;
            }
            return res;
        }

        public float CalcCritBonus(Actor sActor, Actor dActor, int scribonus = 0)
        {
            float res = 1.0f;
            int cri = scribonus, criavd = 0;
            if (sActor.type == ActorType.PC)
            {
                cri += ((ActorPC)sActor).Status.hit_critical + ((ActorPC)sActor).Status.cri_skill + ((ActorPC)sActor).Status.cri_item + ((ActorPC)sActor).Status.hit_critical_iris;
            }
            if (sActor.type == ActorType.MOB)
            {
                cri += ((ActorMob)sActor).Status.hit_critical + ((ActorMob)sActor).Status.cri_skill;
            }
            if (dActor.type == ActorType.PC)
            {
                criavd = ((ActorPC)dActor).Status.avoid_critical + ((ActorPC)dActor).Status.criavd_skill + ((ActorPC)dActor).Status.criavd_item + ((ActorPC)dActor).Status.avoid_critical_iris;
            }
            if (dActor.type == ActorType.MOB)
            {
                criavd = ((ActorMob)dActor).Status.avoid_critical + ((ActorMob)dActor).Status.criavd_skill;
            }
            if (dActor.Status.Additions.ContainsKey("CriUp"))//暴击标记
                cri += (5 + dActor.Status.Cri_Up_Lv * 5);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                //iris卡种族暴击率提升
                if (dActor.Race == Race.HUMAN && pc.Status.human_cri_up_iris > 0)
                {
                    cri += pc.Status.human_cri_up_iris;
                }
                if (dActor.Race == Race.BIRD && pc.Status.bird_cri_up_iris > 0)
                {
                    cri += pc.Status.bird_cri_up_iris;
                }
                else if (dActor.Race == Race.ANIMAL && pc.Status.animal_cri_up_iris > 0)
                {
                    cri += pc.Status.animal_cri_up_iris;
                }
                else if (dActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_cri_up_iris > 0)
                {
                    cri += pc.Status.magic_c_cri_up_iris;
                }
                else if (dActor.Race == Race.PLANT && pc.Status.plant_cri_up_iris > 0)
                {
                    cri += pc.Status.plant_cri_up_iris;
                }
                else if (dActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_cri_up_iris > 0)
                {
                    cri += pc.Status.water_a_cri_up_iris;
                }
                else if (dActor.Race == Race.MACHINE && pc.Status.machine_cri_up_iris > 0)
                {
                    cri += pc.Status.machine_cri_up_iris;
                }
                else if (dActor.Race == Race.ROCK && pc.Status.rock_cri_up_iris > 0)
                {
                    cri += pc.Status.rock_cri_up_iris;
                }
                else if (dActor.Race == Race.ELEMENT && pc.Status.element_cri_up_iris > 0)
                {
                    cri += pc.Status.element_cri_up_iris;
                }
                else if (dActor.Race == Race.UNDEAD && pc.Status.undead_cri_up_iris > 0)
                {
                    cri += pc.Status.undead_cri_up_iris;
                }
            }
            res = 1.0f + (float)(cri > criavd ? cri - criavd : 0) / 100.0f;
            res *= ((float)sActor.Status.hit_critical_rate_iris / 100.0f);
            return res;
        }
    }
}

