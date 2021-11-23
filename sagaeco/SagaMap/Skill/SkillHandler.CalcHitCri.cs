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
        public enum AttackResult
        {
            Hit,
            Miss,
            Avoid,//仅在完美闪避时触发
            Critical,
            Guard,
        }

        public float CalcCriRate(Actor sActor, Actor dActor, int criratebonus = 0)
        {

            int cri = 0, criavd = 0;

            if (sActor.type == ActorType.PC)
            {
                cri = ((ActorPC)sActor).Status.hit_critical;
            }
            if (sActor.type == ActorType.MOB)
            {
                cri = ((ActorMob)sActor).Status.hit_critical + ((ActorMob)sActor).Status.hit_critical_skill;
            }
            if (dActor != null)
            {
                if (dActor.type == ActorType.PC)
                    criavd = ((ActorPC)dActor).Status.avoid_critical;
                if (dActor.type == ActorType.MOB)
                    criavd = ((ActorMob)dActor).Status.avoid_critical + ((ActorMob)dActor).Status.avoid_critical_skill;
            }
            else
            {
                criavd = 0;
            }
            float crirate = CalcRawCriRate(cri, criavd); //基础Cri%
            crirate = 1f * criratebonus + crirate * (100f - criratebonus) / 100f;
            if (crirate < 1f) crirate = 1f;
            if (crirate > 100f) crirate = 100f;
            if (criratebonus != 100)//暴击强制100
                crirate = crirate / 2;//暴击率最大50%
            if (sActor.type == ActorType.PC)//月神影响
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Status.Additions.ContainsKey("雲切暴击率上升"))
                    crirate += 20;
            }
            return crirate;
        }
        /// <summary>
        /// 攻击结果类型判定,只计算actor内固定变量的影响 不考虑任何addition
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="dActor"></param>
        /// <param name="skilltype">0-物理，1-魔法</param>
        /// <param name="ranged"></param>
        /// <param name="shitbonus">命中率百分比补正%</param>
        /// <param name="scribonus">会心率百分比补正%不含会心伤害增益</param>
        /// <returns></returns>
        AttackResult CalcAttackResult(Actor sActor, Actor dActor, int skilltype, bool ranged, int hitratebonus = 0, int criratebonus = 0)
        {
            int sHit = 0, dAvoid = 0;

            # region 命中判定
            AttackResult res = AttackResult.Miss;

            if (skilltype == 1) //魔法攻击判定
            {
                res = AttackResult.Hit;
            }
            else //物理攻击判定
            {
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
                float hitrate = CalcRawHitRate(sHit, dAvoid);  //基础Hit%
                hitrate = 1f * hitratebonus + hitrate * (100f - hitratebonus) / 100f; //hitratebonus补正
                if (dActor.Status.attackingActors.Count > 1) //围殴补正
                {
                    hitrate += (dActor.Status.attackingActors.Count - 1) * 10;
                }
                if (hitrate < 1f) hitrate = 1f;
                if (hitrate > 100f) hitrate = 100f;

                int hit_res = Global.Random.Next(0, 100);
                if (hit_res < hitrate)
                {
                    bool guard = false;
                    if (dActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)dActor;
                        if (Global.Random.Next(0, 100) < (pc.Status.guard))
                            guard = true;                          
                    }
                    if (dActor.type == ActorType.MOB)
                    {
                        if (Global.Random.Next(0, 100) < (dActor.Status.guard_skill))
                            guard = true;
                    }
                    if (guard)
                    {
                        res = AttackResult.Guard;
                    }
                    else
                    {
                        res = AttackResult.Hit;
                    }
                }
                else
                {
                    res = AttackResult.Miss;
                }
                if (dActor.Status.Additions.ContainsKey("Parry"))//格挡
                    res = AttackResult.Guard;
                if (dActor.Status.Additions.ContainsKey("Invincible"))//绝对壁垒
                    res = AttackResult.Guard;
                if (dActor.Status.Additions.ContainsKey("Allavoid"))
                    res = AttackResult.Avoid;
            }
            #endregion

            #region 会心判定（仅准对hit条件下）
            if (res == AttackResult.Hit)
            {
                float crirate = CalcCriRate(sActor, dActor, criratebonus);

                if (sActor.type == ActorType.PC)//月神影响
                {
                    ActorPC pc = (ActorPC)sActor; 
                    if (pc.Status.Additions.ContainsKey("弓术专注提升"))
                    {
                        switch (sActor.TInt["弓术专注暴击伤害"])
                        {
                            case 10:
                                if (Global.Random.Next(0, 100) < 10)
                                    res = AttackResult.Critical;
                                break;
                            case 15:
                                if (Global.Random.Next(0, 100) < 15)
                                    res = AttackResult.Critical;
                                break;
                            case 20:
                                if (Global.Random.Next(0, 100) < 20)
                                    res = AttackResult.Critical;
                                break;
                        }
                    }
                }
                if (Global.Random.Next(0, 100) < (crirate))
                    res = AttackResult.Critical;
                else
                {

                }
            }
            #endregion
            return res;
        }

        /// <summary>
        /// 会心伤害增益百分比%计算(%)
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="dActor"></param>
        /// <param name="skilltype">0-物理，1-魔法</param>
        /// <returns></returns>
        public float CalCriBonusRate(Actor sActor, Actor dActor, int skilltype)
        {
            float cribonus = 0;
            int cri = 0, criavd = 0;

            if (sActor.type == ActorType.PC)
            {
                cri = ((ActorPC)sActor).Status.hit_critical;
            }
            if (sActor.type == ActorType.MOB)
            {
                cri = ((ActorMob)sActor).Status.hit_critical + ((ActorMob)sActor).Status.hit_critical_skill;
            }
            if (dActor != null)
            {
                if (dActor.type == ActorType.PC)
                {
                    criavd = ((ActorPC)dActor).Status.avoid_critical;
                }
                if (dActor.type == ActorType.MOB)
                {
                    criavd = ((ActorMob)dActor).Status.avoid_critical + ((ActorMob)dActor).Status.avoid_critical_skill;
                }
            }
            else
                criavd = 0;
            //cribonus = 0.75f * CalcRawCriRate(cri, criavd) + 25f;
            if (skilltype == 1)
                cribonus = 0.75f * sActor.Status.hit_magic / 10 + 20f + (float)sActor.TInt["雲切暴击伤害提升"] + sActor.TInt["魔杖暴击伤害提升"];
            else
                cribonus = 0.75f * (sActor.Status.hit_melee + sActor.Status.hit_ranged) / 20 + 20f + (float)sActor.TInt["雲切暴击伤害提升"] + sActor.TInt["魔杖暴击伤害提升"];
            cribonus += sActor.TInt["弓术专注暴击伤害"]+ sActor.TInt["幽怨之怒暴击伤害"];
            return cribonus;
        }

        /// <summary>
        /// Calculate Raw Hit Rate (%)
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="avoid"></param>
        /// <returns></returns>
        float CalcRawHitRate(int hit, int avoid)
        {
            float r = 100f;
            if (hit > 500)
                hit = 500;
            if (avoid > 300)
                avoid = 300;
            if (hit > avoid)
            {
                r = r * (hit - 0.5f * avoid + 0.1f) / (hit - 0.125f * avoid + 0.5f);
            }
            else
            {
                r = r * (0.5f * avoid + 0.1f) / (1.875f * avoid - hit + 0.5f);
            }
            return r;
        }

        /// <summary>
        /// Calculate Raw Critical Rate (%)
        /// </summary>
        /// <param name="cri"></param>
        /// <param name="criavd"></param>
        /// <returns></returns>
        public float CalcRawCriRate(int cri, int criavd)
        {
            float r = 100f;
            if (cri > 500)
                cri = 500;
            if (criavd > 300)
                criavd = 300;
            r = r * (cri + 0.01f) / (cri + 7f / 3f * criavd + 1f);
            int maxrate = cri/2 + 10;
            if (r > maxrate)
                r = maxrate;
            return r;
        }
    }
}

