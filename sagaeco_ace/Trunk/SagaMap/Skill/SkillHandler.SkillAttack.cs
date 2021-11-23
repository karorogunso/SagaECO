using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill
{
    public partial class SkillHandler
    {
        internal int physicalCounter = 0;
        internal int magicalCounter = 0;
        public enum DefType
        {
            Def,
            MDef,
            IgnoreAll,
            IgnoreLeft,
            IgnoreRight,
            MDefIgnoreLeft,
            MdefIgnoreRight,
            DefIgnoreLeft,
            DefIgnoreRight
        }

        /// <summary>
        /// 对单一目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float ATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return PhysicalAttack(sActor, list, arg, element, ATKBonus);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, float ATKBonus)
        {
            return PhysicalAttack(sActor, dActor, arg, element, 0, ATKBonus);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="index">arg中参数偏移</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, int index, float ATKBonus)
        {
            //if(index >0)
            /*if (dActor.Count > 12)
            {
                int ACount = dActor.Count;
                for (int i = 12; i < ACount; i++)
                {
                    dActor.RemoveAt(12);
                }
            }*/
            return PhysicalAttack(sActor, dActor, arg, DefType.Def, element, index, ATKBonus, false);
        }
        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="index">arg中参数偏移</param>
        ///<param name="defType">使用的防御类型</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, false, 0, false);
        }

        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, SuckBlood, doublehate, 0, 0);
        }

        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, SuckBlood, doublehate, shitbonus, scribonus, "noeffect", 0);
        }

        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus, string effectname, int lifetime, float ignore = 0)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, SuckBlood, doublehate, shitbonus, scribonus, 0, "noeffect", 0);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="index">arg中参数偏移</param>
        ///<param name="defType">使用的防御类型</param>
        /// <param name="ATKBonus">攻击加成</param>
        ///  <param name="ignore">无视防御比</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus, int cridamagebonus, string effectname, int lifetime, float ignore = 0)
        {
            if (dActor.Count == 0) return 0;
            if (sActor.Status == null)
                return 0;

            int damage = 0;

            int atk;
            int mindamage = 0;
            int maxdamage = 0;
            int counter = 0;
            Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);

            if (index == 0)
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }

            switch (arg.type)
            {
                case ATTACK_TYPE.BLOW:
                    mindamage = sActor.Status.min_atk1;
                    maxdamage = sActor.Status.max_atk1;
                    break;
                case ATTACK_TYPE.SLASH:
                    mindamage = sActor.Status.min_atk2;
                    maxdamage = sActor.Status.max_atk2;
                    break;
                case ATTACK_TYPE.STAB:
                    mindamage = sActor.Status.min_atk3;
                    maxdamage = sActor.Status.max_atk3;
                    break;
            }
            if (sActor.Status.Additions.ContainsKey("破戒"))
            {
                mindamage = sActor.Status.min_matk;
                maxdamage = sActor.Status.max_matk;
            }
            if (mindamage > maxdamage) maxdamage = mindamage;
            foreach (Actor i in dActor)
            {
                if (i.Status == null)
                    continue;
                //投掷武器
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW ||
                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD)
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Stack > 0)
                                MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                        }
                    }
                }

                //弓箭，枪
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                        {
                            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                                {
                                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                                        MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                                }
                                else
                                {
                                    if (counter == 0)
                                        arg.result = -1;
                                    continue;
                                }
                            }
                            else
                            {
                                if (counter == 0)
                                    arg.result = -1;
                                continue;
                            }
                        }
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                        {
                            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                                {
                                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                                        MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                                }
                                else
                                {
                                    if (counter == 0)
                                        arg.result = -1;
                                    continue;
                                }
                            }
                            else
                            {
                                if (counter == 0)
                                    arg.result = -1;
                                continue;
                            }
                        }
                    }
                }
                //判断命中结果
                //short dis = Map.Distance(sActor, i);
                //if (arg.argType == SkillArg.ArgType.Active)
                //    shitbonus = 50;
                AttackResult res = CalcAttackResult(sActor, i, sActor.Range > 3, shitbonus, scribonus);
                //res = AttackResult.Miss;
                Actor target = i;

                if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard || res == AttackResult.Parry)
                {
                    if (res == AttackResult.Miss)
                        arg.flag[index + counter] = AttackFlag.MISS;
                    else if (res == AttackResult.Avoid)
                        arg.flag[index + counter] = AttackFlag.AVOID;
                    else if (res == AttackResult.Parry)
                        arg.flag[index + counter] = AttackFlag.AVOID2;
                    else
                        arg.flag[index + counter] = AttackFlag.GUARD;

                    try
                    {
                        string y = "普通攻击";
                        if (arg != null)
                        {
                            if (arg.skill != null)
                                y = arg.skill.Name;
                        }
                        //string s = "物理伤害";
                        SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "被你 " + res.ToString());
                        SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "被 " + res.ToString());
                    }
                    catch (Exception ex)
                    {
                        SagaLib.Logger.ShowError(ex);
                    }
                }
                else
                {
                    int restKryrie = 0;
                    if (i.type == ActorType.PC)
                    {
                        ActorPC me = (ActorPC)i;
                        if (me.Skills.ContainsKey(956))//古代剑法
                        {
                            byte TotalLv = me.Skills[956].BaseData.lv;
                            int nr = SagaLib.Global.Random.Next(0, 1000);
                            if ((TotalLv * 5) > nr)
                            {
                                if (i.Status.Additions.ContainsKey("ConSword"))
                                {
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                                    EffectArg arg2 = new EffectArg();
                                    arg2.effectID = 4173;
                                    arg2.actorID = i.ActorID;
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                                    restKryrie = 1;
                                }
                            }
                        }
                    }

                    if (i.Status.Additions.ContainsKey("MobKyrie"))
                    {
                        Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["MobKyrie"];
                        restKryrie = buf["MobKyrie"];
                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                        if (restKryrie > 0)
                        {
                            buf["MobKyrie"]--;
                            EffectArg arg2 = new EffectArg();
                            arg2.effectID = 4173;
                            arg2.actorID = i.ActorID;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                            if (restKryrie == 1)
                                SkillHandler.RemoveAddition(i, "MobKyrie");
                        }
                    }
                    if (restKryrie == 0)
                    {
                        bool isPossession = false;
                        bool isHost = false;
                        if (i.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)i;
                            if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
                            {
                                isPossession = true;
                                isHost = true;
                            }
                            if (pc.PossessionTarget != 0)
                            {
                                isPossession = true;
                                isHost = false;
                            }
                        }
                        //处理凭依伤害
                        if (isHost && isPossession && ATKBonus > 0)
                        {
                            List<Actor> possessionDamage = ProcessAttackPossession(i);
                            if (possessionDamage.Count > 0)
                            {
                                arg.Remove(i);
                                int oldcount = arg.flag.Count;
                                arg.Extend(possessionDamage.Count);
                                foreach (Actor j in possessionDamage)
                                {
                                    if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
                                        arg.affectedActors.Add(i);
                                    else
                                        arg.affectedActors.Add(j);
                                }
                                PhysicalAttack(sActor, possessionDamage, arg, element, oldcount, ATKBonus);
                                continue;
                            }
                        }
                        if (!setAtk)
                        {
                            atk = Global.Random.Next(mindamage, maxdamage);
                            //TODO: element bonus, range bonus
                            atk = (int)(Math.Ceiling(atk * CalcElementBonus(sActor, i, element, 0, false) * ATKBonus));
                            if (i.Buff.Frosen == true && element == Elements.Fire)
                            {
                                RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
                            }
                            if (i.Buff.Stone == true && element == Elements.Water)
                            {
                                RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
                            }
                            if (arg.skill != null)
                            {
                                if (sActor.Status.doubleUpList.Contains((ushort)arg.skill.ID))
                                {
                                    atk *= 2;
                                }
                            }
                        }
                        else
                            atk = (int)ATKBonus;

                        damage = CalcPhyDamage(sActor, i, defType, atk, ignore, res);

                        if (damage > atk)
                            damage = atk;

                        IStats stats = (IStats)i;
                        switch (arg.type)
                        {
                            case ATTACK_TYPE.BLOW:
                                damage = (int)(damage * (1f - i.Status.damage_atk1_discount));
                                break;
                            case ATTACK_TYPE.SLASH:
                                damage = (int)(damage * (1f - i.Status.damage_atk2_discount));
                                break;
                            case ATTACK_TYPE.STAB:
                                damage = (int)(damage * (1f - i.Status.damage_atk3_discount));
                                break;

                        }


                        if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                        {
                            damage = (int)(damage * Configuration.Instance.PVPDamageRatePhysic);
                        }

                        if (damage <= 0) damage = 1;


                        if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
                        {
                            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
                            if (Global.Random.Next(0, 99) < buf["Rate"])
                            {
                                Actor dst = map.GetActor((uint)buf["Target"]);
                                if (dst != null)
                                {
                                    target = dst;
                                    arg.affectedActors[index + counter] = target;
                                }
                            }
                        }

                        if (sActor.Status.Additions.ContainsKey("HpLostDamUp") && !setAtk)
                        {
                            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)sActor.Status.Additions["HpLostDamUp"];
                            if (sActor.HP > buf["HPLost"])
                            {
                                sActor.HP -= (uint)buf["HPLost"];
                                damage += buf["DamUp"];
                                SkillArg tmp = new SkillArg();
                                tmp.sActor = sActor.ActorID;
                                tmp.dActor = 0xffffffff;
                                tmp.x = arg.x;
                                tmp.y = arg.y;
                                tmp.argType = SkillArg.ArgType.Active;
                                tmp.autoCast = arg.autoCast;
                                tmp.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2200, 1);
                                tmp.affectedActors.Add(sActor);
                                tmp.Init();
                                tmp.hp[0] = buf["HPLost"];
                                tmp.flag[0] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, tmp, sActor, true);
                            }
                        }

                        //计算暴击增益
                        if (res == AttackResult.Critical)
                        {
                            damage = (int)(((float)damage) * (float)(CalcCritBonus(sActor, target, scribonus) + (float)cridamagebonus));
                            if (sActor.Status.Additions.ContainsKey("CriDamUp"))
                            {
                                float rate = (float)((float)(sActor.Status.Additions["CriDamUp"] as DefaultPassiveSkill).Variable["CriDamUp"] / 100.0f + 1.0f);
                                damage = (int)((float)damage * rate);
                            }
                        }

                        //宠判定？
                        bool ride = false;
                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Pet != null)
                                ride = pc.Pet.Ride;
                        }
                        //宠物成长
                        if (res == AttackResult.Critical)
                        {
                            if (sActor.type == ActorType.PET)
                                ProcessPetGrowth(sActor, PetGrowthReason.CriticalHit);
                            if (i.type == ActorType.PET && damage > 0)
                                ProcessPetGrowth(i, PetGrowthReason.PhysicalBeenHit);
                            if (i.type == ActorType.PC && damage > 0)
                            {
                                ActorPC pc = (ActorPC)target;

                                if (ride)
                                {
                                    ProcessPetGrowth(pc.Pet, PetGrowthReason.PhysicalBeenHit);
                                }
                            }
                        }
                        else
                        {
                            if (sActor.type == ActorType.PET)
                                ProcessPetGrowth(sActor, PetGrowthReason.PhysicalHit);
                            if (i.type == ActorType.PET && damage > 0)
                            {
                                ProcessPetGrowth(i, PetGrowthReason.PhysicalBeenHit);
                            }
                            if (i.type == ActorType.PC && damage > 0)
                            {
                                ActorPC pc = (ActorPC)target;

                                if (ride)
                                {
                                    ProcessPetGrowth(pc.Pet, PetGrowthReason.PhysicalBeenHit);
                                }
                            }
                        }

                        //技能以及状态判定
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pcsActor = (ActorPC)sActor;
                            if (sActor.Status.Additions.ContainsKey("BurnRate"))// && SkillHandler.Instance.isEquipmentRight(pcsActor, SagaDB.Item.ItemType.CARD))//皇家贸易商
                            {
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
                        if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)target;
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
                        if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                        {
                            float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                            damage = (int)(damage * DamageUpRank);
                        }

                        if (target.Status.PhysiceReduceRate > 0)//物理抗性
                        {
                            if (target.Status.PhysiceReduceRate > 1)
                                damage = (int)((float)damage / target.Status.PhysiceReduceRate);
                            else
                                damage = (int)((float)damage / (1.0f + target.Status.PhysiceReduceRate));
                        }

                        //加伤处理下
                        if (target.Seals > 0)
                            damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                        if (sActor.Status.Additions.ContainsKey("ruthless") &&
                            (target.Buff.Stun || target.Buff.Stone || target.Buff.Frosen || target.Buff.Poison ||
                            target.Buff.Sleep || target.Buff.SpeedDown || target.Buff.Confused || target.Buff.Paralysis))
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                float rate = 1f + (((ActorPC)sActor).TInt["ruthless"] * 0.1f);
                                damage = (int)(damage * rate);//无情打击
                            }
                        }
                        //加伤处理上

                        //减伤处理下
                        if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                            damage = (int)(damage * (float)0f);
                        if (target.Status.Additions.ContainsKey("EnergyShield"))//能量加护
                        {
                            if (target.type == ActorType.PC)
                                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["EnergyShieldlv"]));
                            else
                                damage = (int)(damage * (float)0.9f);
                        }
                        if (target.Status.Additions.ContainsKey("Counter"))
                        {
                            damage /= 2;
                        }

                        if (target.Status.Additions.ContainsKey("Blocking") && target.Status.Blocking_LV != 0 && target.type == ActorType.PC)//3转骑士格挡
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) &&
                                pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
                                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                                {
                                    int SutanOdds = target.Status.Blocking_LV * 5;
                                    int SutanTime = 1000 + i.Status.Blocking_LV * 500;
                                    int ParryOdds = new int[] { 0, 15, 25, 35, 65, 75 }[target.Status.Blocking_LV];
                                    float ParryResult = 4 + 6 * target.Status.Blocking_LV;
                                    SagaDB.Skill.Skill args = new SagaDB.Skill.Skill();
                                    if (pc.Skills.ContainsKey(116))
                                    {
                                        ParryResult += pc.Skills[116].Level * 3;
                                    }
                                    if (Global.Random.Next(1, 100) <= ParryOdds)
                                    {
                                        damage = damage - (int)(damage * ParryResult / 100.0f);
                                        if (SkillHandler.Instance.CanAdditionApply(target, sActor, SkillHandler.DefaultAdditions.Stun, SutanOdds))
                                        {
                                            Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(args, sActor, 1000 + 500 * target.Status.Blocking_LV);
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

                        //火心放大
                        if (sActor.Status.Additions.ContainsKey("FrameHart"))
                        {
                            int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
                            damage = (int)((double)damage * (double)((double)rate / 100));
                        }

                        //竜眼放大
                        if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
                        {
                            int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                            damage = (int)((double)damage * (double)((double)rate / 100));
                        }

                        //最终伤害放大处理结束

                        //处理无法恢复
                        if (target.Buff.NoRegen == true && damage < 0)
                            damage = 0;

                        //吸血效果下
                        if (SuckBlood != 0 && damage != 0)
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                int hp = (int)(damage * SuckBlood);
                                if (((ActorPC)sActor).TInt["SuckBlood"] > 0)
                                    hp = (int)(hp * (float)(1f + ((ActorPC)sActor).TInt["SuckBlood"] * 0.1f));
                                sActor.HP += (uint)hp;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                                SkillHandler.Instance.ShowVessel(sActor, -hp);

                                try
                                {
                                    string y1 = "普通攻击";
                                    if (arg != null)
                                    {
                                        if (arg.skill != null)
                                            y1 = arg.skill.Name;
                                    }
                                    SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y1 + "", "受到了 " + (-damage).ToString() + " 点" + "恢复效果");
                                }
                                catch (Exception ex) { SagaLib.Logger.ShowError(ex); }

                            }
                        }
                        //吸血效果上

                        if (i.type == ActorType.PC)
                        {
                            ActorPC pcs = (ActorPC)i;

                            if (i.Status.Additions.ContainsKey("剑斗士"))
                            {
                                if (Global.Random.Next(0, 100) >= 50 && i.HP > damage)
                                    PhysicalAttack(i, sActor, arg, Elements.Neutral, 1.5f);
                            }

                            if (i.Status.Additions.ContainsKey("Bounce") && Global.Random.Next(0, 100) < 35 && pcs.Skills3.ContainsKey(2497))//黒薔薇の棘
                            {
                                byte skilllv = pcs.Skills3[2497].Level;
                                float rank = 0;
                                int damage1 = 0;
                                if (sActor.type == ActorType.PC)
                                {
                                    rank = 0.4f + 0.2f * skilllv;
                                }
                                else if (sActor.type == ActorType.MOB)
                                {
                                    rank = 2.0f + 0.2f * skilllv;
                                }
                                damage1 = (int)(damage * rank);
                                arg.affectedActors.Add(sActor);
                                arg.hp.Add(damage1);
                                arg.sp.Add(0);
                                arg.mp.Add(0);
                                arg.flag.Add(AttackFlag.HP_DAMAGE);
                                if (sActor.HP < damage1 + 1)
                                {
                                    sActor.HP -= sActor.HP + 1;
                                }
                                else
                                    sActor.HP -= (uint)damage1;
                            }

                            if (i.Status.Additions.ContainsKey("冰封坚韧"))
                            {
                                ShowVessel(sActor, 35);
                                if (sActor.HP - 35 < 1)
                                    sActor.HP = 1;
                                else
                                    sActor.HP -= 35;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                            }
                        }
                        try
                        {
                            string y = "普通攻击";
                            if (arg != null)
                            {
                                if (arg.skill != null)
                                    y = arg.skill.Name;
                            }
                            string s = "物理伤害";
                            if (res == AttackResult.Critical)
                                s = "物理暴击伤害";
                            if (damage < 0)
                                s = "治疗效果";
                            if (damage > 0)
                                SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "受到了 " + damage.ToString() + " 点" + s);
                            SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "造成了 " + damage.ToString() + " 点" + s);
                        }
                        catch (Exception ex) { SagaLib.Logger.ShowError(ex); }

                        //伤害结算之前附加中毒效果,如果有涂毒而且目标没中毒的话
                        if (sActor.Status.Additions.ContainsKey("AppliePoison") && !i.Status.Additions.ContainsKey("Poison"))
                        {
                            if (SkillHandler.Instance.CanAdditionApply(sActor, i, DefaultAdditions.Poison, 95))
                            {
                                Poison poi = new Poison(arg.skill, i, 15000);
                                SkillHandler.ApplyAddition(i, poi);
                            }
                        }

                        //结算HP结果
                        if (target.HP != 0)
                        {
                            arg.hp[index + counter] = damage;
                            if (target.HP > damage)
                            {
                                //damage = (short)sActor.Status.min_atk1;
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                if (res == AttackResult.Critical)
                                    arg.flag[index + counter] |= AttackFlag.CRITICAL;

                                //处理反击
                                if (target.Status.Additions.ContainsKey("Counter"))
                                {
                                    SkillArg newArg = new SkillArg();
                                    float rate = (target.Status.Additions["Counter"] as DefaultBuff).Variable["Counter"] / 100.0f;
                                    SkillHandler.Instance.Attack(target, sActor, newArg, rate);
                                    target.Status.Additions["Counter"].AdditionEnd();
                                    MapManager.Instance.GetMap(target.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, newArg, target, true);
                                }
                            }
                            else
                            {
                                damage = (int)target.HP;
                                if (!ride && !target.Buff.Reborn)
                                    arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                else
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                if (res == AttackResult.Critical)
                                    arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            }
                            //arg.flag[i] |=  AttackFlag.ATTACK_EFFECT;
                            if (target.HP != 0)
                                target.HP = (uint)(target.HP - damage);
                        }
                        else
                        {
                            if (!ride && !target.Buff.Reborn)
                                arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            else
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            if (res == AttackResult.Critical)
                                arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            arg.hp[index + counter] = damage;
                        }

                        //吸血？
                        if (sActor.Status.Additions.ContainsKey("BloodLeech") && !sActor.Buff.NoRegen)
                        {
                            Additions.Global.BloodLeech add = (Additions.Global.BloodLeech)sActor.Status.Additions["BloodLeech"];
                            int heal = (int)(damage * add.rate);
                            arg.affectedActors.Add(sActor);
                            arg.hp.Add(heal);
                            arg.sp.Add(0);
                            arg.mp.Add(0);
                            arg.flag.Add(AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE);

                            sActor.HP += (uint)heal;
                            if (sActor.HP > sActor.MaxHP)
                                sActor.HP = sActor.MaxHP;
                            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                        }
                    }
                }

                ApplyDamage(sActor, target, damage, doublehate, arg);
                if ((res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard) && dActor.Count == 1)//弓3转23级技能
                {
                    if (sActor.Status.MissRevenge_rate > 0 && Global.Random.Next(0, 100) < sActor.Status.MissRevenge_rate)
                    {
                        sActor.Status.MissRevenge_hit = true;
                        arg.sActor = sActor.ActorID;
                        arg.dActor = i.ActorID;
                        arg.type = sActor.Status.attackType;
                        arg.delayRate = 1f;
                        PhysicalAttack(sActor, target, arg, Elements.Neutral, 1f);
                    }
                }
                counter++;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
            }

            short aspd = (short)(sActor.Status.aspd + sActor.Status.aspd_skill);
            if (aspd > 800)
                aspd = 800;

            arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);

            arg.delay = (uint)(arg.delay * arg.delayRate);

            if (sActor.Status.aspd_skill_perc >= 1f)
                arg.delay = (uint)(arg.delay / sActor.Status.aspd_skill_perc);

            return damage;
        }

        public int WeaponMagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, DefType.MDef, element, 50, MATKBonus, 0, false, false, 0, true);
        }
        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
        }
        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float MATKBonus, float ignore)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, DefType.MDef, element, 50, MATKBonus, 0, false, false, 0, false, ignore);
        }
        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, element, elementValue, MATKBonus);
        }

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus);
        }

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, defType, element, elementValue, MATKBonus);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, elementValue, MATKBonus, 0);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, 0);//Use element holy to represent not using this param.
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus, int index)
        {
            return MagicAttack(sActor, dActor, arg, DefType.MDef, element, elementValue, MATKBonus, index);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, false);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, float MATKBonus, int index, bool setAtk)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus, index, setAtk);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, setAtk, false);
        }
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk, bool noReflect)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, setAtk, false, 0);
        }
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk, bool noReflect, float SuckBlood, bool WeaponAttack = false, float ignore = 0)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, 0, index, setAtk, false, 0);
        }
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int mcridamagebonus, int index, bool setAtk, bool noReflect, float SuckBlood, bool WeaponAttack = false, float ignore = 0)
        {
            if (dActor.Count == 0)
                return 0;
            if (sActor.Status == null)
                return 0;

            if (sActor.Status.PlusElement_rate > 0)
                MATKBonus += sActor.Status.PlusElement_rate;

            int damage = 0;

            //calculate the MATK
            int matk;
            int mindamage = 0;
            int maxdamage = 0;
            int counter = 0;
            Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);
            if (index == 0)
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }
            if (WeaponAttack)
            {
                mindamage = ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].MAtk + ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.matk;

                maxdamage = mindamage;
            }
            else
            {
                mindamage = sActor.Status.min_matk;
                maxdamage = sActor.Status.max_matk;
            }

            if (mindamage > maxdamage) maxdamage = mindamage;

            foreach (Actor i in dActor)
            {
                Actor target = i;
                if (i.Status == null)
                    continue;
                int restKryrie = 0;
                if (i.Status.Additions.ContainsKey("DispelField"))
                {
                    Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["DispelField"];
                    restKryrie = buf["DispelField"];
                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                    if (restKryrie > 0)
                    {
                        buf["DispelField"]--;
                        EffectArg arg2 = new EffectArg();
                        arg2.effectID = 4173;
                        arg2.actorID = i.ActorID;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                        if (restKryrie == 1)
                            SkillHandler.RemoveAddition(i, "DispelField");
                    }
                }
                if (restKryrie == 0)
                {
                    //判断命中结果
                    //short dis = Map.Distance(sActor, i);
                    //if (arg.argType == SkillArg.ArgType.Active)
                    //    shitbonus = 50;
                    AttackResult res = CalcMagicAttackResult(sActor, i);
                    //res = AttackResult.Miss;

                    if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard || res == AttackResult.Parry)
                    {
                        if (res == AttackResult.Miss)
                            arg.flag[index + counter] = AttackFlag.MISS;
                        else if (res == AttackResult.Avoid)
                            arg.flag[index + counter] = AttackFlag.AVOID;
                        else if (res == AttackResult.Parry)
                            arg.flag[index + counter] = AttackFlag.AVOID2;
                        else
                            arg.flag[index + counter] = AttackFlag.GUARD;

                        try
                        {
                            string y = "普通攻击";
                            if (arg != null)
                            {
                                if (arg.skill != null)
                                    y = arg.skill.Name;
                            }
                            //string s = "物理伤害";
                            SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "被你 " + res.ToString());
                            SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "被 " + res.ToString());
                        }
                        catch (Exception ex)
                        {
                            SagaLib.Logger.ShowError(ex);
                        }
                    }
                    else
                    {

                        if (i.Status.Additions.ContainsKey("MagicReflect") && i != sActor && !noReflect)
                        {
                            arg.Remove(i);
                            int oldcount = arg.flag.Count;
                            arg.Extend(1);
                            arg.affectedActors.Add(sActor);
                            List<Actor> dst = new List<Actor>();
                            dst.Add(sActor);
                            RemoveAddition(i, "MagicReflect");
                            MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, MATKBonus, oldcount, setAtk, true);
                            continue;
                        }
                        if (i.Status.reflex_odds > 0 && i.type == ActorType.PC)//3转骑士反射盾
                        {
                            ActorPC pc = (ActorPC)i;
                            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                                {
                                    if (Global.Random.Next(0, 100) < 100 * i.Status.reflex_odds)
                                    {
                                        arg.Remove(i);
                                        int oldcount = arg.flag.Count;
                                        arg.Extend(1);
                                        arg.affectedActors.Add(sActor);
                                        List<Actor> dst = new List<Actor>();
                                        dst.Add(sActor);
                                        MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, MATKBonus, oldcount, false, true);
                                        continue;
                                    }
                                }
                            }
                        }

                        bool isPossession = false;
                        bool isHost = false;

                        if (i.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)i;
                            if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
                            {
                                isPossession = true;
                                isHost = true;
                            }
                            if (pc.PossessionTarget != 0)
                            {
                                isPossession = true;
                                isHost = false;
                            }
                        }
                        //处理凭依伤害
                        if (isHost && isPossession && MATKBonus > 0)
                        {
                            List<Actor> possessionDamage = ProcessAttackPossession(i);
                            if (possessionDamage.Count > 0)
                            {
                                arg.Remove(i);
                                int oldcount = arg.flag.Count;
                                arg.Extend(possessionDamage.Count);
                                foreach (Actor j in possessionDamage)
                                {
                                    if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
                                        arg.affectedActors.Add(i);
                                    else
                                        arg.affectedActors.Add(j);
                                }
                                MagicAttack(sActor, possessionDamage, arg, element, elementValue, MATKBonus, oldcount);
                                continue;
                            }
                        }

                        if (!setAtk)
                        {
                            matk = Global.Random.Next(mindamage, maxdamage);
                            if (element != Elements.Neutral)
                            {
                                float eleBonus = CalcElementBonus(sActor, i, element, 1, ((MATKBonus < 0) && !((i.Status.undead == true) && (element == Elements.Holy))));
                                matk = (int)(matk * eleBonus * MATKBonus);
                            }
                            else
                                matk = (int)(matk * 1f * MATKBonus);
                        }
                        else
                            matk = (int)MATKBonus;

                        if (MATKBonus > 0)
                        {
                            damage = CalcMagDamage(sActor, i, defType, matk, ignore);
                        }
                        else
                        {
                            damage = matk;
                        }

                        //AttackResult res = AttackResult.Hit;
                        //AttackResult res = CalcAttackResult(sActor, target, true);
                        //if (res == AttackResult.Critical)
                        //    res = AttackResult.Hit;
                        if (i.Buff.Frosen == true && element == Elements.Fire)
                        {
                            RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
                        }
                        if (i.Buff.Stone == true && element == Elements.Water)
                        {
                            RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
                        }

                        if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                        {
                            if (damage > 0)
                                damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                        }

                        if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                        {
                            float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                            damage = (int)(damage * DamageUpRank);
                        }
                        if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                            damage = (int)(damage * (float)0f);

                        if (target.Status.MagicRuduceRate > 0)//魔法抵抗力
                        {
                            if (target.Status.MagicRuduceRate > 1)
                                damage = (int)((float)damage / target.Status.MagicRuduceRate);
                            else
                                damage = (int)((float)damage / (1.0f + target.Status.MagicRuduceRate));
                        }

                        if (damage <= 0 && MATKBonus >= 0)
                            damage = 1;

                        if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
                        {
                            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
                            if (Global.Random.Next(0, 99) < buf["Rate"])
                            {
                                Actor dst = map.GetActor((uint)buf["Target"]);
                                if (dst != null)
                                {
                                    target = dst;
                                    arg.affectedActors[index + counter] = target;
                                }
                            }
                        }
                        if (sActor.type == ActorType.PET)
                            ProcessPetGrowth(sActor, PetGrowthReason.SkillHit);
                        if (i.type == ActorType.PET && damage > 0)
                            ProcessPetGrowth(i, PetGrowthReason.MagicalBeenHit);

                        bool ride = false;
                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Pet != null)
                                ride = pc.Pet.Ride;
                        }

                        if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)target;
                            if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
                                damage = damage / 10;
                        }

                        //if (sActor.type == ActorType.PC)
                        //{
                        //    int score = damage / 100;
                        //    if (score == 0 && damage != 0)
                        //        score = 1;
                        //    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, Math.Abs(score));
                        //}

                        //减伤处理下
                        if (target.Status.Additions.ContainsKey("无敌"))
                            damage = 0;
                        if (target.Status.Additions.ContainsKey("MagicShield"))//魔力加护
                        {
                            if (target.type == ActorType.PC)
                                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["MagicShieldlv"]));
                            else
                                damage = (int)(damage * (float)0.9f);
                        }
                        if (target.Status.MagicRuduceRate != 0)
                        {
                            damage = (int)(damage * (float)1f - target.Status.MagicRuduceRate);
                        }
                        //减伤处理上
                        //开始处理最终伤害放大

                        if (!setAtk)
                        {
                            //杀戮放大
                            if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                                damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));

                            //火心放大
                            if (sActor.Status.Additions.ContainsKey("FrameHart"))
                            {
                                int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
                                damage = (int)((double)damage * (double)((double)rate / 100));
                            }
                            //竜眼放大
                            if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
                            {
                                int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                                damage = (int)((double)damage * (double)((double)rate / 100));
                            }
                            //极大放大
                            if (sActor.Status.Additions.ContainsKey("Zensss") && !sActor.ZenOutLst.Contains(arg.skill.ID))
                            {
                                float zenbonus = (float)((sActor.Status.Additions["Zensss"] as DefaultBuff).Variable["Zensss"] / 10);
                                //MATKBonus *= zenbonus;
                                damage = (int)((float)damage * zenbonus);
                            }
                        }

                        //最终伤害放大处理结束

                        //吸血效果下
                        if (SuckBlood != 0 && damage != 0 && !sActor.Buff.NoRegen)//吸血效果
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                int hp = (int)(damage * SuckBlood);
                                sActor.HP += (uint)hp;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                                SkillHandler.Instance.ShowVessel(sActor, -hp);

                                try
                                {
                                    string y1 = "攻击";
                                    if (arg != null)
                                    {
                                        if (arg.skill != null)
                                            y1 = arg.skill.Name;
                                    }
                                    SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y1 + "", "受到了 " + (-damage).ToString() + " 点" + "治疗效果");
                                }
                                catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
                            }
                        }
                        //吸血效果上

                        try
                        {
                            string s = "魔法伤害";
                            string y = "攻击";
                            if (arg != null)
                            {
                                if (arg.skill != null)
                                    y = arg.skill.Name;
                            }
                            if (damage < 0)
                            {
                                if (target.Buff.NoRegen)
                                    damage = 0;
                                s = "治疗效果";
                                SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y + "", "接受了 " + (-damage).ToString() + " 点" + s);
                            }
                            else
                                SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "受到了 " + damage.ToString() + " 点" + s);
                            SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "造成了 " + (damage >= 0 ? damage.ToString() : (-damage).ToString()) + " 点" + s);
                        }
                        catch (Exception ex)
                        {
                            SagaLib.Logger.ShowError(ex);
                        }
                        arg.hp[index + counter] += damage;
                        if (damage > 0)
                        {
                            if (target.HP > damage)
                            {
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE;
                            }
                            else
                            {
                                damage = (int)target.HP;
                                if (!ride && !target.Buff.Reborn)
                                    arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                else
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            }
                            //魔法攻击什么时候能暴击了?
                            //if (res == AttackResult.Critical)
                            //    arg.flag[index + counter] |= AttackFlag.CRITICAL;
                        }
                        else
                        {
                            arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                        }
                        //arg.flag[i] |=  AttackFlag.ATTACK_EFFECT;

                        if (target.HP != 0)
                            target.HP = (uint)(target.HP - damage);
                        if (target.HP > target.MaxHP)
                            target.HP = target.MaxHP;
                        //}
                        //else
                        //{
                        //    arg.flag[index + counter] = AttackFlag.NO_DAMAGE;
                        //    arg.hp[index + counter] = 0;
                        //}
                    }
                }
                ApplyDamage(sActor, target, damage, arg);
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
                counter++;
                //return ;
            }
            return damage;
            //magicalCounter--;
        }

        public int FixAttackList(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, List<int> DamageList)
        {
            if (dActor.Count == 0)
                return 0;
            if (sActor.Status == null)
                return 0;
            int index = 0;
            int damage = 0;
            int counter = 0;

            Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);
            if (index == 0)
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }

            foreach (Actor i in dActor)
            {
                Actor target = i;
                if (i.Status == null)
                    continue;
                int restKryrie = 0;
                if (i.Status.Additions.ContainsKey("DispelField"))
                {
                    Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["DispelField"];
                    restKryrie = buf["DispelField"];
                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                    if (restKryrie > 0)
                    {
                        buf["DispelField"]--;
                        EffectArg arg2 = new EffectArg();
                        arg2.effectID = 4173;
                        arg2.actorID = i.ActorID;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                        if (restKryrie == 1)
                            SkillHandler.RemoveAddition(i, "DispelField");
                    }
                }
                if (restKryrie == 0)
                {
                    bool isPossession = false;
                    bool isHost = false;

                    if (i.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)i;
                        if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
                        {
                            isPossession = true;
                            isHost = true;
                        }
                        if (pc.PossessionTarget != 0)
                        {
                            isPossession = true;
                            isHost = false;
                        }
                    }
                    //处理凭依伤害
                    if (isHost && isPossession && DamageList[counter] > 0)
                    {
                        List<Actor> possessionDamage = ProcessAttackPossession(i);
                        if (possessionDamage.Count > 0)
                        {
                            arg.Remove(i);
                            int oldcount = arg.flag.Count;
                            arg.Extend(possessionDamage.Count);
                            foreach (Actor j in possessionDamage)
                            {
                                if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
                                    arg.affectedActors.Add(i);
                                else
                                    arg.affectedActors.Add(j);
                            }
                            MagicAttack(sActor, possessionDamage, arg, DefType.MDef, element, elementValue, DamageList[counter], oldcount, true);
                            continue;
                        }
                    }
                    if (i.Status.Additions.ContainsKey("MagicReflect") && i != sActor)
                    {
                        arg.Remove(i);
                        int oldcount = arg.flag.Count;
                        arg.Extend(1);
                        arg.affectedActors.Add(sActor);
                        List<Actor> dst = new List<Actor>();
                        dst.Add(sActor);
                        RemoveAddition(i, "MagicReflect");
                        MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, DamageList[counter], oldcount, true, true);
                        continue;
                    }
                    if (i.Status.reflex_odds > 0 && i.type == ActorType.PC)//3转骑士反射盾
                    {
                        ActorPC pc = (ActorPC)i;
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD || pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                            {
                                if (Global.Random.Next(0, 100) < 100 * i.Status.reflex_odds)
                                {
                                    arg.Remove(i);
                                    int oldcount = arg.flag.Count;
                                    arg.Extend(1);
                                    arg.affectedActors.Add(sActor);
                                    List<Actor> dst = new List<Actor>();
                                    dst.Add(sActor);
                                    MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, DamageList[counter], oldcount, true, true);
                                    continue;
                                }
                            }
                        }
                    }

                    damage = DamageList[counter];

                    //AttackResult res = AttackResult.Hit;
                    AttackResult res = CalcAttackResult(sActor, target, true);
                    if (res == AttackResult.Critical)
                        res = AttackResult.Hit;
                    if (i.Buff.Frosen == true && element == Elements.Fire)
                    {
                        RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
                    }
                    if (i.Buff.Stone == true && element == Elements.Water)
                    {
                        RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
                    }

                    if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                    {
                        if (damage > 0)
                            damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                    }

                    if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                    {
                        float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                        damage = (int)(damage * DamageUpRank);
                    }
                    if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                    {
                        damage = (int)(damage * (float)0f);
                    }
                    if (target.Status.MagicRuduceRate > 0)//魔法抵抗力
                    {
                        if (target.Status.MagicRuduceRate > 1)
                            damage = (int)((float)damage / target.Status.MagicRuduceRate);
                        else
                            damage = (int)((float)damage / (1.0f + target.Status.MagicRuduceRate));
                    }

                    if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
                    {
                        Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
                        if (Global.Random.Next(0, 99) < buf["Rate"])
                        {
                            Actor dst = map.GetActor((uint)buf["Target"]);
                            if (dst != null)
                            {
                                target = dst;
                                arg.affectedActors[index + counter] = target;
                            }
                        }
                    }
                    if (sActor.type == ActorType.PET)
                        ProcessPetGrowth(sActor, PetGrowthReason.SkillHit);
                    if (i.type == ActorType.PET && damage > 0)
                        ProcessPetGrowth(i, PetGrowthReason.MagicalBeenHit);

                    bool ride = false;
                    if (target.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)target;
                        if (pc.Pet != null)
                            ride = pc.Pet.Ride;
                    }

                    if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)target;
                        if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
                            damage = damage / 10;
                    }


                    //加伤处理下

                    if (target.Seals > 0)
                        damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                    //加伤处理上
                    //减伤处理下
                    if (target.Status.Additions.ContainsKey("无敌"))
                        damage = 0;
                    if (target.Status.Additions.ContainsKey("MagicShield"))//魔力加护
                    {
                        if (target.type == ActorType.PC)
                            damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["MagicShieldlv"]));
                        else
                            damage = (int)(damage * (float)0.9f);
                    }
                    if (target.Status.MagicRuduceRate != 0)
                    {
                        damage = (int)(damage * (float)1f - target.Status.MagicRuduceRate);
                    }
                    //减伤处理上
                    //开始处理最终伤害放大

                    //杀戮放大
                    if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                        damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));

                    //火心放大
                    if (sActor.Status.Additions.ContainsKey("FrameHart"))
                    {
                        int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
                        damage = (int)((double)damage * (double)((double)rate / 100));
                    }
                    //竜眼放大
                    if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
                    {
                        int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                        damage = (int)((double)damage * (double)((double)rate / 100));
                    }
                    //极大放大
                    if (sActor.Status.Additions.ContainsKey("Zensss") && !sActor.ZenOutLst.Contains(arg.skill.ID))
                    {
                        float zenbonus = (float)((sActor.Status.Additions["Zensss"] as DefaultBuff).Variable["Zensss"] / 10);
                        //MATKBonus *= zenbonus;
                        damage = (int)((float)damage * zenbonus);
                    }
                    //最终伤害放大处理结束

                    try
                    {
                        string s = "魔法伤害";
                        string y = "攻击";
                        if (arg != null)
                        {
                            if (arg.skill != null)
                                y = arg.skill.Name;
                        }
                        if (damage < 0)
                        {
                            if (target.Buff.NoRegen)
                                damage = 0;
                            s = "治疗效果";
                            SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y + "", "接受了 " + (-damage).ToString() + " 点" + s);
                        }
                        else
                            SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "受到了 " + damage.ToString() + " 点" + s);
                        SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "造成了 " + (damage >= 0 ? damage.ToString() : (-damage).ToString()) + " 点" + s);
                    }
                    catch (Exception ex)
                    {
                        SagaLib.Logger.ShowError(ex);
                    }
                    arg.hp[index + counter] += damage;
                    if (damage > 0)
                    {
                        if (target.HP > damage)
                        {
                            arg.flag[index + counter] = AttackFlag.HP_DAMAGE;
                        }
                        else
                        {
                            damage = (int)target.HP;
                            if (!ride && !target.Buff.Reborn)
                                arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            else
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                        }
                        if (res == AttackResult.Critical)
                            arg.flag[index + counter] |= AttackFlag.CRITICAL;
                    }
                    else
                    {
                        arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                    }

                    if (target.HP != 0)
                        target.HP = (uint)(target.HP - damage);
                    if (target.HP > target.MaxHP)
                        target.HP = target.MaxHP;

                }
                ApplyDamage(sActor, target, damage, arg);
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
                counter++;
                //return ;
            }
            return damage;
            //magicalCounter--;
        }
        public Dictionary<Actor, int> CalcMagicAttackWithoutDamage(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            Dictionary<Actor, int> dmgtable = new Dictionary<Actor, int>();
            int index = 0;
            if (sActor.Status.PlusElement_rate > 0)
                MATKBonus += sActor.Status.PlusElement_rate;

            int damage;

            //calculate the MATK
            int matk;
            int mindamage = 0;
            int maxdamage = 0;
            int counter = 0;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            mindamage = sActor.Status.min_matk;
            maxdamage = sActor.Status.max_matk;


            if (mindamage > maxdamage) maxdamage = mindamage;

            foreach (Actor i in dActor)
            {
                damage = 0;
                Actor target = i;
                if (i.Status == null)
                    continue;


                bool isPossession = false;
                bool isHost = false;

                if (i.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)i;
                    if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
                    {
                        isPossession = true;
                        isHost = true;
                    }
                    if (pc.PossessionTarget != 0)
                    {
                        isPossession = true;
                        isHost = false;
                    }
                }

                matk = Global.Random.Next(mindamage, maxdamage);
                if (element != Elements.Neutral)
                {
                    float eleBonus = CalcElementBonus(sActor, i, element, 1, ((MATKBonus < 0) && !((i.Status.undead == true) && (element == Elements.Holy))));
                    matk = (int)(matk * eleBonus * MATKBonus);
                }
                else
                    matk = (int)(matk * 1f * MATKBonus);


                if (MATKBonus > 0)
                {
                    damage = CalcMagDamage(sActor, i, DefType.MDef, matk, 0);
                }
                else
                {
                    damage = matk;
                }

                //AttackResult res = AttackResult.Hit;
                AttackResult res = CalcAttackResult(sActor, target, true);
                if (res == AttackResult.Critical)
                    res = AttackResult.Hit;
                if (i.Buff.Frosen == true && element == Elements.Fire)
                {
                    RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
                }
                if (i.Buff.Stone == true && element == Elements.Water)
                {
                    RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
                }

                if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                {
                    if (damage > 0)
                        damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                }

                if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                    damage = (int)(damage * (float)1f);

                if (damage <= 0 && MATKBonus >= 0)
                    damage = 1;

                if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
                {
                    Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
                    if (Global.Random.Next(0, 99) < buf["Rate"])
                    {
                        Actor dst = map.GetActor((uint)buf["Target"]);
                        if (dst != null)
                        {
                            target = dst;
                            arg.affectedActors[index + counter] = target;
                        }
                    }
                }

                bool ride = false;
                if (target.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)target;
                    if (pc.Pet != null)
                        ride = pc.Pet.Ride;
                }

                if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                {
                    ActorMob mob = (ActorMob)target;
                    if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
                        damage = damage / 10;
                }

                if (!dmgtable.ContainsKey(target))
                    dmgtable.Add(target, damage);
                else
                    dmgtable[target] += damage;

                counter++;
            }
            return dmgtable;
        }
        /// <summary>
        /// 对指定目标附加伤害
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="damage">伤害值</param>
        public void ApplyDamage(Actor sActor, Actor dActor, int damage, SkillArg arg2 = null)
        {
            ApplyDamage(sActor, dActor, damage, false);
        }
        /// <summary>
        /// 对指定目标附加伤害
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="damage">伤害值</param>
        protected void ApplyDamage(Actor sActor, Actor dActor, int damage, bool doublehate, SkillArg arg2 = null)
        {
            if ((DateTime.Now - dActor.Status.attackStamp).TotalSeconds > 5)
            {
                dActor.Status.attackStamp = DateTime.Now;
                dActor.Status.attackingActors.Clear();
                if (!dActor.Status.attackingActors.Contains(sActor))
                    dActor.Status.attackingActors.Add(sActor);
            }
            else
            {
                if (!dActor.Status.attackingActors.Contains(sActor))
                    dActor.Status.attackingActors.Add(sActor);
            }
            if (sActor.type == ActorType.PC)
            {
                WeaponWorn((ActorPC)sActor);
            }
            if (dActor.type == ActorType.PC && damage > (dActor.MaxHP / 100))
            {
                ArmorWorn((ActorPC)dActor);
            }

            //3转剑35级技能
            if (dActor.Status.Pressure_lv > 0)
            {
                int level = dActor.Status.Pressure_lv;
                float[] hprank = { 0.2f, 0.2f, 0.25f, 0.25f, 0.3f };
                float[] rank = { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
                float[] rank2 = { 0, 0.1f, 0.1f, 0.15f, 0.15f, 0.2f };
                float factor = 3f + 0.3f * level;
                ActorPC pc = (ActorPC)dActor;
                if (pc.Skills3.ContainsKey(1113))
                {
                    for (int i = 1; i < level; i++)
                    {
                        if (pc.HP < (uint)((float)pc.MaxHP * hprank[i - 1]) && pc.HP > damage)
                        {
                            if (SagaLib.Global.Random.Next(0, 100) <= (level * 10))
                            {
                                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                                List<Actor> affected = map.GetActorsArea(dActor, 400, false);
                                List<Actor> realAffected = new List<Actor>();
                                arg2 = new SkillArg();
                                arg2.Init();
                                arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(1113, pc.Skills3[1113].Level);
                                foreach (Actor act in affected)
                                {
                                    arg2.argType = SkillArg.ArgType.Attack;
                                    arg2.flag.Add(AttackFlag.HP_DAMAGE);
                                    arg2.type = ATTACK_TYPE.BLOW;
                                    arg2.hp.Add(damage);
                                    arg2.sp.Add(0);
                                    arg2.mp.Add(0);
                                    arg2.affectedActors.Add(act);
                                    SkillHandler.Instance.PhysicalAttack(dActor, realAffected, arg2, SagaLib.Elements.Neutral, factor);
                                    ShowEffect((ActorPC)dActor, act, 4002);
                                    ShowEffect((ActorPC)dActor, dActor, 4321);
                                    SkillHandler.Instance.PushBack(dActor, act, 4);
                                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, DefaultAdditions.Stun, level * 10))
                                    {
                                        Stun stun = new Stun(arg2.skill, act, 4000);
                                        SkillHandler.ApplyAddition(act, stun);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //如果玩家有被动buff AutoHeal
            //自动治疗
            if (dActor.type == ActorType.PC && dActor.Status.Additions.ContainsKey("AutoHeal") && !dActor.Buff.NoRegen)
            {
                ActorPC pc = (ActorPC)dActor;
                //如果玩家加了被动技能AutoHeal,并且玩家有Healing这个技能
                if (pc.Skills3.ContainsKey(1109) && pc.Skills.ContainsKey(3054))
                {
                    //获取AutoHeal的等级
                    int level = pc.Skills3[1109].Level;
                    //声明触发治疗标记
                    bool active = false;
                    //触发治疗的血线
                    float[] activerate = new float[] { 0.2f, 0.4f, 0.6f, 0.7f, 0.8f };

                    //遍历触发血线数组
                    for (int i = 1; i < level; i++)
                    {
                        //如果玩家当前的血量 小于触发血线
                        if (pc.HP < (uint)((float)dActor.MaxHP * activerate[i - 1]) && pc.HP > damage)
                        {
                            // 40%机率触发治疗
                            if (SagaLib.Global.Random.Next(1, 100) <= 40)
                            {
                                active = true;
                                break;
                            }
                        }
                    }
                    if (active)
                    {
                        //自动咏唱Healing
                        SkillArg autoheal = new SkillArg();
                        SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3054, pc.Skills[3054].Level);
                        autoheal.sActor = pc.ActorID;
                        autoheal.dActor = pc.ActorID;
                        autoheal.skill = skill;
                        autoheal.argType = SkillArg.ArgType.Cast;
                        autoheal.useMPSP = false;
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).OnSkillCastComplete(autoheal);
                    }
                }
            }

            //不屈の闘志
            if (dActor.type == ActorType.PC && dActor.Status.Additions.ContainsKey("不屈の闘志"))
            {
                ActorPC pc = (ActorPC)dActor;
                //如果玩家加了被动技能 不屈的斗志
                if (pc.Skills3.ContainsKey(1100))
                {
                    //获取AutoHeal的等级
                    int level = pc.Skills3[1100].Level;
                    //触发治疗的血线
                    float[] activerate = new float[] { 0.08f, 0.16f, 0.24f, 0.32f, 0.4f };
                    //治疗量
                    float[] recoveryrate = new float[] { 0.24f, 0.22f, 0.20f, 0.18f, 0.16f };
                    //遍历触发血线数组
                    for (int i = 1; i < level; i++)
                    {
                        //如果玩家当前的血量 小于触发血线
                        if (pc.HP < (uint)((float)dActor.MaxHP * activerate[i - 1]) && pc.HP > damage)
                        {
                            if (dActor.Buff.NoRegen)
                                break;
                            arg2 = new SkillArg();
                            arg2.Init();
                            int hpheal = (int)(dActor.MaxHP * recoveryrate[i - 1]);
                            int mpheal = (int)(dActor.MaxMP * recoveryrate[i - 1]);
                            int spheal = (int)(dActor.MaxSP * recoveryrate[i - 1]);
                            arg2.hp.Add(hpheal);
                            arg2.mp.Add(mpheal);
                            arg2.sp.Add(spheal);
                            arg2.flag.Add(AttackFlag.HP_HEAL | AttackFlag.SP_HEAL | AttackFlag.MP_HEAL | AttackFlag.NO_DAMAGE);
                            dActor.HP += (uint)hpheal;
                            dActor.MP += (uint)mpheal;
                            dActor.SP += (uint)spheal;
                            if (dActor.HP > dActor.MaxHP)
                                dActor.HP = dActor.MaxHP;
                            if (dActor.MP > dActor.MaxMP)
                                dActor.MP = dActor.MaxMP;
                            if (dActor.SP > dActor.MaxSP)
                                dActor.SP = dActor.MaxSP;
                            Manager.MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg2, dActor, true);
                        }
                    }
                }
            }

            if ((dActor.type == ActorType.MOB || dActor.type == ActorType.PET) && damage >= 0)
            {
                Actor attacker;

                //凭依中仇恨转移到寄主
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.PossessionTarget != 0)
                    {
                        Actor possession = Manager.MapManager.Instance.GetMap(pc.MapID).GetActor(pc.PossessionTarget);
                        if (possession != null)
                        {
                            if (possession.type == ActorType.PC)
                                attacker = possession;
                            else
                                attacker = sActor;
                        }
                        else
                            attacker = sActor;
                    }
                    else
                        attacker = sActor;
                }
                else
                    attacker = sActor;
                if (dActor.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)dActor.e;
                    if (sActor.Status.Additions.ContainsKey("柔和魔法"))
                        mob.AI.OnAttacked(attacker, damage / 2);
                    else
                        mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage * 2);
                }
                else
                {
                    ActorEventHandlers.PetEventHandler mob = (ActorEventHandlers.PetEventHandler)dActor.e;
                    if (sActor.Status.Additions.ContainsKey("柔和魔法"))
                        mob.AI.OnAttacked(attacker, damage / 2);
                    else
                        mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage * 2);
                }
            }

            if (dActor.type == ActorType.PC)
            {
                //如果凭依中受攻击解除凭依
                //TODO: 支援スキル使用時の憑依解除設定
                ActorPC pc = (ActorPC)dActor;
                if (pc.Online)
                {
                    MapClient client = MapClient.FromActorPC(pc);
                    if (client.Character.Buff.GetReadyPossession)
                    {
                        client.Character.Buff.GetReadyPossession = false;
                        client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, client.Character, true);
                        if (client.Character.Tasks.ContainsKey("Possession"))
                        {
                            client.Character.Tasks["Possession"].Deactivate();
                            client.Character.Tasks.Remove("Possession");
                        }
                    }
                    if (dActor.Status.Additions.ContainsKey("Hiding"))
                    {
                        dActor.Status.Additions["Hiding"].AdditionEnd();
                        dActor.Status.Additions.Remove("Hiding");
                    }
                    if (dActor.Status.Additions.ContainsKey("fish"))
                    {
                        dActor.Status.Additions["fish"].AdditionEnd();
                        dActor.Status.Additions.Remove("fish");
                    }
                    if (dActor.Status.Additions.ContainsKey("Cloaking"))
                    {
                        dActor.Status.Additions["Cloaking"].AdditionEnd();
                        dActor.Status.Additions.Remove("Cloaking");
                    }
                    if (dActor.Status.Additions.ContainsKey("IAmTree"))
                    {
                        dActor.Status.Additions["IAmTree"].AdditionEnd();
                        dActor.Status.Additions.Remove("IAmTree");
                    }
                    if (dActor.Status.Additions.ContainsKey("Invisible"))
                    {
                        dActor.Status.Additions["Invisible"].AdditionEnd();
                        dActor.Status.Additions.Remove("Invisible");
                    }
                }
            }

            //魔力吸收
            if (sActor.Status.Additions.ContainsKey("Desist") && damage >= 0)
            {
                float desistfactor = ((float)(sActor.Status.Additions["Desist"] as DefaultBuff).Variable["Desist"] / 100.0f);
                int mpdesist = (int)Math.Floor((float)damage * desistfactor);
                if (sActor.MaxMP < (sActor.MP + mpdesist))
                    sActor.MP = sActor.MaxMP;
                else
                    sActor.MP += (uint)mpdesist;
                MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }

            //如果对象目标死亡
            if (dActor.HP == 0)
            {
                if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                {
                    if (sActor.KillingMarkSoulUse)
                        sActor.KillingMarkCounter = Math.Min(++sActor.KillingMarkCounter, 10);
                    else
                        sActor.KillingMarkCounter = Math.Min(++sActor.KillingMarkCounter, 20);
                }
                if (!dActor.Buff.Dead)
                {
                    if (dActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)dActor;
                        if (pc.Pet != null)
                        {
                            if (pc.Pet.Ride)
                            {
                                ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)pc.e;
                                Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                                p.data = new byte[11];
                                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.PET))
                                {
                                    SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET];
                                    if (item.Durability != 0) item.Durability--;
                                    eh.Client.SendItemInfo(item);
                                    eh.Client.SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.PET_FRIENDLY_DOWN, item.BaseData.name));
                                    EffectArg arg = new EffectArg();
                                    arg.actorID = eh.Client.Character.ActorID;
                                    arg.effectID = 8044;
                                    eh.OnShowEffect(eh.Client.Character, arg);
                                    p.InventoryID = item.Slot;
                                    p.Target = SagaDB.Item.ContainerType.BODY;
                                    p.Count = 1;
                                    eh.Client.OnItemMove(p);
                                }
                                return;
                            }
                        }
                        //凭依者死亡自动解除凭依
                        if (pc.PossessionTarget != 0)
                        {
                            Packets.Client.CSMG_POSSESSION_CANCEL p = new SagaMap.Packets.Client.CSMG_POSSESSION_CANCEL();
                            p.PossessionPosition = PossessionPosition.NONE;
                            MapClient.FromActorPC(pc).OnPossessionCancel(p);
                        }
                        if (((ActorPC)dActor).Mode != PlayerMode.COLISEUM_MODE && ((ActorPC)dActor).Mode != PlayerMode.KNIGHT_WAR)
                            ExperienceManager.Instance.DeathPenalty((ActorPC)dActor);
                        //处理WRP
                        if (sActor.type == ActorType.PC)
                        {
                            Map map = MapManager.Instance.GetMap(pc.MapID);
                            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Wrp))
                                ExperienceManager.Instance.ProcessWrp((ActorPC)sActor, pc);
                        }
                    }
                    if (dActor.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)dActor;
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)sActor;
                            ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)pc.e;
                            Map map;// = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                            List<Actor> eventactors;// = map.GetActorsArea(dActor, 3000, false, true);
                            List<ActorPC> owners;// = new List<ActorPC>();
                            if (SkillHandler.Instance.isBossMob(mob) && ((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.SpawnDelay >= 1800000)
                            {
                                map = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                                eventactors = map.GetActorsArea(dActor, 12700, false, true);
                                eventactors = eventactors.Where(x => x.type == ActorType.PC && (x as ActorPC).Online).ToList();
                                foreach (var item in eventactors)
                                {
                                    MapClient.FromActorPC((ActorPC)item).SendAnnounce("本次: [" + mob.Name + "]已经完成击杀,设置系召唤系技能造成的伤害不被统计为你的个人伤害.");
                                    MapClient.FromActorPC((ActorPC)item).SendAnnounce("正在分配击杀......");
                                }
                                owners = new List<ActorPC>();
                                foreach (var item in ((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable)
                                {
                                    Actor act = eventactors.FirstOrDefault(x => x.ActorID == item.Key);
                                    if (act != null && act.type == ActorType.PC && (act as ActorPC).Online)
                                    {
                                        MapClient.FromActorPC((ActorPC)act).SendAnnounce(string.Format("本次击杀: [{0}],你贡献了: {1}点伤害", mob.Name, item.Value));
                                        owners.Add((ActorPC)act);
                                        if ((act as ActorPC).PossesionedActors.Count > 0)
                                        {
                                            foreach (var pitem in (act as ActorPC).PossesionedActors)
                                            {
                                                if (!pitem.Online)
                                                    continue;
                                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.Where(x => x.Key == pitem.ActorID).ToList().Count == 0 && pitem.MapID == mob.MapID)
                                                {
                                                    MapClient.FromActorPC(pitem).SendAnnounce(string.Format("本次击杀: [{0}],你未提供伤害贡献,但是你混到了凭依击杀", mob.Name));
                                                    owners.Add((ActorPC)pitem);
                                                }
                                            }
                                        }
                                        if ((act as ActorPC).PossessionTarget != 0)
                                        {
                                            if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.Where(x => x.Key == (act as ActorPC).PossessionTarget).ToList().Count == 0 && act.MapID == mob.MapID)
                                            {
                                                MapClient cli = MapClient.FromActorPC((ActorPC)eventactors.FirstOrDefault(x => x.ActorID == (act as ActorPC).PossessionTarget));
                                                if (cli != null)
                                                {
                                                    cli.SendAnnounce(string.Format("本次击杀: [{0}],你没有提供伤害贡献,但是你混到了凭依跑者击杀", mob.Name));
                                                    owners.Add((ActorPC)eventactors.First(x => x.ActorID == (act as ActorPC).PossessionTarget));
                                                }
                                            }
                                        }
                                        if ((act as ActorPC).Party != null)
                                        {
                                            foreach (var ptitem in (act as ActorPC).Party.Members)
                                            {
                                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.Where(x => x.Key == ptitem.Value.ActorID).ToList().Count == 0 && ptitem.Value.Online && ptitem.Value.MapID == mob.MapID)
                                                {
                                                    MapClient.FromActorPC((ActorPC)ptitem.Value).SendAnnounce(string.Format("本次击杀: [{0}],你没有提供伤害贡献,但是你混到了组队击杀", mob.Name));
                                                    owners.Add(ptitem.Value);
                                                }
                                            }
                                        }
                                    }
                                }
                                foreach (var ac in owners)
                                {

                                    if ((ac as ActorPC) == null)
                                        continue;
                                    if (!(ac as ActorPC).Online)
                                        continue;
                                    ActorEventHandlers.PCEventHandler ieh = (ActorEventHandlers.PCEventHandler)(ac as ActorPC).e;
                                    ieh.Client.EventMobKilled(mob);
                                    ieh.Client.QuestMobKilled(mob, false);
                                }
                            }
                            else
                            {
                                //处理任务信息
                                if (pc.Party != null)
                                {
                                    foreach (ActorPC tmp in pc.Party.Members.Values)
                                    {
                                        if (!tmp.Online) continue;
                                        if (tmp == pc) continue;
                                        ((ActorEventHandlers.PCEventHandler)tmp.e).Client.QuestMobKilled(mob, true);
                                    }
                                    eh.Client.QuestMobKilled(mob, false);
                                }
                                else
                                {
                                    eh.Client.QuestMobKilled(mob, false);
                                }
                            }
                            map = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                            eventactors = map.GetActorsArea(dActor, 3000, false, true);
                            owners = new List<ActorPC>();
                            foreach (Actor ac in eventactors)
                            {
                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.ContainsKey(ac.ActorID) && ac.type == ActorType.PC)
                                    owners.Add((ActorPC)ac);
                            }
                            foreach (ActorPC i in owners)
                            {
                                if (!i.Online) continue;
                                if (i == null) continue;
                                ActorEventHandlers.PCEventHandler ieh = (ActorEventHandlers.PCEventHandler)i.e;
                                ieh.Client.EventMobKilled(mob);
                            }
                        }
                        ExperienceManager.Instance.ProcessMobExp(mob);
                    }

                    try
                    {
                        string y1 = "攻击";
                        if (arg2 != null)
                        {
                            if (arg2.skill != null)
                                y1 = arg2.skill.Name;
                        }
                        SendAttackMessage(2, dActor, "从 " + sActor.Name + " 处的 " + y1 + "", "导致你死亡了");
                        SendAttackMessage(3, sActor, "你的 " + y1 + " 对 " + dActor.Name + "", "令其死亡了");
                    }
                    catch (Exception ex)
                    {
                        SagaLib.Logger.ShowError(ex);
                    }
                    dActor.e.OnDie();
                }
            }
        }
    }
}
