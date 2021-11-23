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
        /// <summary>
        /// 检查BUFF对伤害的影响
        /// </summary>
        /// <param name="dActor">目标</param>
        /// <param name="damage">输入伤害</param>
        /// <returns></returns>
        public int CheckBuffForDamage(Actor sActor, Actor dActor, int damage)
        {
            int d = damage;
            if (dActor.Status.Additions.ContainsKey("Invincible"))//绝对壁垒
                damage = 0;
            //技能以及状态判定
            if (sActor.type == ActorType.PC)
            {
                ActorPC pcsActor = (ActorPC)sActor;
                if (sActor.Status.Additions.ContainsKey("BurnRate"))// && SkillHandler.Instance.isEquipmentRight(pcsActor, SagaDB.Item.ItemType.CARD))//皇家贸易商
                {
                    //副职不存在3371于是不进行判定
                    if (pcsActor.Skills3.ContainsKey(3371))
                    {
                        if (pcsActor.Skills3[3371].Level > 1)
                        {
                            int[] gold = { 0, 0, 100, 250, 500, 1000 };
                            if (pcsActor.Gold > gold[pcsActor.Skills3[3371].Level])
                            {
                                pcsActor.Gold -= gold[pcsActor.Skills3[3371].Level];
                                damage += gold[pcsActor.Skills3[3371].Level];
                            }
                        }
                    }
                }
            }
            if (sActor.type == ActorType.PC && dActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)dActor;
                if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
                    damage = damage / 10;
            }

            //if (sActor.type == ActorType.PC)
            //{
            //    int score = damage / 100;
            //    if (score == 0)
            //        score = 1;
            //    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, score);
            //}
            if (dActor.Status.Additions.ContainsKey("DamageUp"))//伤害标记
            {
                float DamageUpRank = dActor.Status.Damage_Up_Lv * 0.1f + 1.1f;
                damage = (int)(damage * DamageUpRank);
            }

            if (dActor.Status.PhysiceReduceRate > 0)//物理抗性
            {
                if (dActor.Status.PhysiceReduceRate > 1)
                    damage = (int)((float)damage / dActor.Status.PhysiceReduceRate);
                else
                    damage = (int)((float)damage / (1.0f + dActor.Status.PhysiceReduceRate));
            }

            //加伤处理下
            if (dActor.Seals > 0)
                damage = (int)(damage * (float)(1f + 0.05f * dActor.Seals));//圣印
            if (sActor.Status.Additions.ContainsKey("ruthless") &&
                (dActor.Buff.Stun || dActor.Buff.Stone || dActor.Buff.Frosen || dActor.Buff.Poison ||
                dActor.Buff.Sleep || dActor.Buff.SpeedDown || dActor.Buff.Confused || dActor.Buff.Paralysis))
            {
                if (sActor.type == ActorType.PC)
                {
                    float rate = 1f + (((ActorPC)sActor).TInt["ruthless"] * 0.1f);
                    damage = (int)(damage * rate);//无情打击
                }
            }
            //加伤处理上

            //减伤处理下
            if (dActor.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                damage = (int)(damage * (float)0f);
            if (dActor.Status.Additions.ContainsKey("EnergyShield"))//能量加护
            {
                if (dActor.type == ActorType.PC)
                    damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)dActor).TInt["EnergyShieldlv"]));
                else
                    damage = (int)(damage * (float)0.9f);
            }
            if (dActor.Status.Additions.ContainsKey("Counter"))
            {
                damage /= 2;
            }

            if (dActor.Status.Additions.ContainsKey("Blocking") && dActor.Status.Blocking_LV != 0 && dActor.type == ActorType.PC)//3转骑士格挡
            {
                ActorPC pc = (ActorPC)dActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) &&
                    pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                    {
                        int SutanOdds = dActor.Status.Blocking_LV * 5;
                        int SutanTime = 1000 + dActor.Status.Blocking_LV * 500;
                        int ParryOdds = new int[] { 0, 15, 25, 35, 65, 75 }[dActor.Status.Blocking_LV];
                        float ParryResult = 4 + 6 * dActor.Status.Blocking_LV;
                        SagaDB.Skill.Skill args = new SagaDB.Skill.Skill();
                        //不管是主职还是副职,检查盾牌专精是否存在
                        if (pc.Skills.ContainsKey(116) || pc.DualJobSkill.Exists(x => x.ID == 116))
                        {
                            //这里取副职的盾牌专精等级
                            var duallv = 0;
                            if (pc.DualJobSkill.Exists(x => x.ID == 116))
                                duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 116).Level;

                            //这里取主职的盾牌专精等级
                            var mainlv = 0;
                            if (pc.Skills.ContainsKey(116))
                                mainlv = pc.Skills[116].Level;

                            //这里取等级最高的盾牌专精等级用来做运算
                            int level = Math.Max(duallv, mainlv);

                            ParryResult += level * 3;
                            //ParryResult += pc.Skills[116].Level * 3;
                        }
                        if (Global.Random.Next(1, 100) <= ParryOdds)
                        {
                            damage = damage - (int)(damage * ParryResult / 100.0f);
                            if (SkillHandler.Instance.CanAdditionApply(dActor, sActor, SkillHandler.DefaultAdditions.Stun, SutanOdds))
                            {
                                Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(args, sActor, 1000 + 500 * dActor.Status.Blocking_LV);
                                SkillHandler.ApplyAddition(sActor, skill);
                            }
                        }
                    }
                }
            }
            //减伤处理上

            //开始处理最终伤害放大


            //杀戮放大
            if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));




            //火心仅对物理伤害放大，取消

            //竜眼放大
            if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
            {

                int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                damage = (int)((double)damage * (double)((double)rate / 100));
            }
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Party != null && sActor.Status.pt_dmg_up_iris > 100)
                {
                    damage = (int)((float)damage * (float)(sActor.Status.pt_dmg_up_iris / 100.0f));
                }
                //iris卡种族增伤部分
                if (dActor.Race == Race.HUMAN && pc.Status.human_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.human_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.BIRD && pc.Status.bird_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.bird_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.ANIMAL && pc.Status.animal_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.animal_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.magic_c_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.PLANT && pc.Status.plant_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.plant_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.water_a_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.MACHINE && pc.Status.machine_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.machine_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.ROCK && pc.Status.rock_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.rock_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.ELEMENT && pc.Status.element_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.element_dmg_up_iris / 100.0f));
                }
                else if (dActor.Race == Race.UNDEAD && pc.Status.undead_dmg_up_iris > 100)
                {
                    damage = (int)(damage * (float)(pc.Status.undead_dmg_up_iris / 100.0f));
                }
            }
            if (sActor.WeaponElement == Elements.Holy)
            {
                if (dActor.Status.Additions.ContainsKey("Oratio"))
                {
                    damage = (int)((float)damage * 1.25f);
                }
            }

            return damage;
        }
        /// <summary>
        /// 只计算面板左右防御的影响 不考虑任何面板外的状态和判定
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="defType">可以对物理技能进行魔法类防御判定</param>
        /// <param name="atk"></param>
        /// <param name="ignore">根据deftype无视相应防御类型的百分比值，不可超过100%，即左防50的目标ignore0.5后只计算25左防，若是右放200则计算100</param>
        /// <returns></returns>
        public int CalcPhyDamage(Actor sActor, Actor dActor, DefType defType, int atk, float ignore, AttackResult res = AttackResult.Hit)
        {
            int damage, def1 = 0, def2 = 0;
            switch (defType)
            {
                case DefType.Def:
                    def1 = dActor.Status.def;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.def_skill;
                    def2 = dActor.Status.def_add;
                    if (dActor is ActorMob)
                        def2 += dActor.Status.def_add_skill;
                    break;
                case DefType.MDef:
                    def1 = dActor.Status.mdef;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.mdef_skill;
                    def2 = dActor.Status.mdef_add;
                    if (dActor is ActorMob)
                        def2 += dActor.Status.mdef_add_skill;
                    break;
                case DefType.IgnoreLeft:
                    def1 = 0;
                    def2 = dActor.Status.def_add;
                    if (dActor is ActorMob)
                        def2 += dActor.Status.def_add_skill;
                    break;
                case DefType.IgnoreRight:
                    def1 = dActor.Status.def;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.def_skill;
                    def2 = 0;
                    break;
                case DefType.IgnoreAll:
                    def1 = 0;
                    def2 = 0;
                    break;
            }
            if (res == AttackResult.Critical)
                def2 = 0;

            //damage = (int)(atk * (1.0 - (def2 * (1.0 + def1 / 100.0) * a) / (def2 * (1.0 + def1 / 100.0) * a + 1.0)));
            if (sActor.Status.Purger_Lv > 0)
            {
                def1 = Math.Max(0, def1 - (10 * sActor.Status.Purger_Lv));
                //def1 -= (10 * sActor.Status.Purger_Lv);
                def2 = (int)((float)def2 * (float)(1.0f - (float)sActor.Status.Purger_Lv / 10.0f));
            }
            if (def1 < 0)
            {
                def1 = 0;
            }
            if (def2 < 0)
            {
                def2 = 0;
            }
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = dActor as ActorPC;
                damage = (int)((float)atk * (1.0f - (float)((float)def1 / 100.0f)) - def2 - (float)((float)(pc.Vit + pc.Status.vit_rev + pc.Status.vit_item + pc.Status.vit_chip + pc.Status.vit_mario + pc.Status.vit_skill) / 3.0f));
            }
            else
            {
                //这个算法经过考察是错误的.那就是说玩家攻击怪物和怪物攻击玩家应用的公式是不一样的
                //damage = (int)((float)atk * (1.0f - (float)((float)def1 / 100.0f)) - def2);
                float divright = atk > def2 ? (float)(atk - def2) : 1.0f;
                float dmgreduce = 1.0f;
                if (sActor.type == ActorType.PC && sActor.Status.DefRatioAtk)
                    dmgreduce = (1.0f + (float)(def1 + dActor.Status.vit_skill) / 100.0f);
                else
                    dmgreduce = (1.0f - ((float)def1 / 100.0f));
                damage = (int)(divright * dmgreduce);
            }
            return damage;
        }
        /// <summary>
        ///  只计算面板左右防御的影响 不考虑任何面板外的状态和判定
        /// </summary>
        /// <param name="dActor"></param>
        /// <param name="defType">可以对魔法攻击进行物理防御判定</param>
        /// <param name="atk"></param>
        /// <param name="ignore">根据deftype无视相应防御类型的百分比值，不可超过100%，即左防50的目标ignore0.5后只计算25左防，若是右放200则计算100</param>
        /// <returns></returns>
        public int CalcMagDamage(Actor sActor, Actor dActor, DefType defType, int atk, float ignore)
        {
            int damage = 0, def1 = 0, def2 = 0;
            //double a = 0.008;
            switch (defType)
            {
                case DefType.Def:
                    def1 = dActor.Status.def;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.def_skill;
                    def2 = dActor.Status.def_add;
                    if (dActor is ActorMob)
                        def2 += dActor.Status.def_add_skill;
                    break;
                case DefType.MDef:
                    def1 = dActor.Status.mdef;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.mdef_skill;
                    def2 = dActor.Status.mdef_add;
                    if (dActor is ActorMob)
                        def2 += dActor.Status.mdef_add_skill;
                    break;
                case DefType.IgnoreLeft:
                    def1 = 0;
                    def2 = dActor.Status.mdef_add;
                    if (dActor is ActorMob)
                        def2 += dActor.Status.mdef_add_skill;
                    break;
                case DefType.IgnoreRight:
                    def1 = dActor.Status.mdef;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.mdef_skill;
                    def2 = 0;
                    break;
                case DefType.IgnoreAll:
                    def1 = 0;
                    def2 = 0;
                    break;
                case DefType.DefIgnoreRight:
                    def1 = dActor.Status.def;
                    if (dActor is ActorMob)
                        def1 += dActor.Status.def_skill;
                    def2 = 0;
                    break;
            }
            if (sActor.Status.ForceMaster_Lv > 0 && defType == DefType.MDef)
            {
                int[] defdown = new int[] { 24, 30, 36, 42, 50 };
                def1 -= defdown[sActor.Status.ForceMaster_Lv - 1];
                //def2 = (int)((float)def2 * (float)(1.0f - defdown[sActor.Status.ForceMaster_Lv - 1]));
            }
            if (def1 < 0)
            {
                def1 = 0;
            }
            if (def2 < 0)
            {
                def2 = 0;
            }
            if (dActor.type == ActorType.PC)
            {

                //damage = (int)(atk * (1.0 - (def2 * (1.0 + def1 / 100.0) * a) / (def2 * (1.0 + def1 / 100.0) * a + 1.0)));
                damage = (int)((float)atk * (1.0f - (float)((float)def1 / 100.0f)) - def2);
            }

            else
            {
                float divright = atk > def2 ? (float)(atk - def2) : 1.0f;
                float dmgreduce = (1.0f - ((float)def1 / 100.0f));
                damage = (int)(divright * dmgreduce);
            }
            return damage;
        }

        int checkPositive(float num)
        {
            if (num > 0)
                return (int)num;
            return 0;
        }
    }
}
