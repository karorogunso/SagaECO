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
using SagaMap.Titles;

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
            IgnoreDefLeft,
            IgnoreDefRight,
            IgnoreMDefLeft,
            IgnoreMDefRight,
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
        /// 对单一目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float ATKBonus ,int index)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return PhysicalAttack(sActor, list, arg, element,  index, ATKBonus);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="ATKBonus">攻击加成</param>
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
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, 0, false);
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
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus, int cridamagebonus, string effectname, int lifetime, float ignore = 0,int igoreAddDef = 0)
        {
            if (dActor.Count == 0 || sActor.Status == null) return 0;

            //if (dActor.Count >32)
            //{
            //    foreach (var item in dActor)
            //        DoDamage(true, sActor, item, arg, defType, element, index, ATKBonus);
            //    return 0; 
            //}
            int counter = 0;
            if (arg.affectedActors.Count>0)
            {
                counter = arg.affectedActors.Count;
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Extend(arg.affectedActors.Count);
            }
            else
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }

            # region 基础攻击力计算

            int damage = 0;
            int atk;
            int mindamage = 0;
            int maxdamage = 0;
            int mindamageM = 0;
            int maxdamageM = 0;
            
            Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);

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

            mindamageM = sActor.Status.min_matk;
            maxdamageM = sActor.Status.max_matk;

            if (sActor.type == ActorType.PARTNER)
            {
                ActorPartner partner = sActor as ActorPartner;
                if (partner.Owner != null && partner.Owner.Status != null)
                {
                    switch (arg.type)
                    {
                        case ATTACK_TYPE.BLOW:
                            mindamage = partner.Owner.Status.min_atk1;
                            maxdamage = partner.Owner.Status.max_atk1;
                            break;
                        case ATTACK_TYPE.SLASH:
                            mindamage = partner.Owner.Status.min_atk2;
                            maxdamage = partner.Owner.Status.max_atk2;
                            break;
                        case ATTACK_TYPE.STAB:
                            mindamage = partner.Owner.Status.min_atk3;
                            maxdamage = partner.Owner.Status.max_atk3;
                            break;
                    }
                    mindamageM = partner.Owner.Status.min_matk;
                    maxdamageM = partner.Owner.Status.max_matk;
                }
            }

            //check
            if (mindamage > maxdamage) maxdamage = mindamage;
            if (mindamageM > maxdamageM) maxdamageM = mindamageM;

            # endregion
            
            foreach (Actor i in dActor)
            {
                if (i.Status == null)
                    continue;
                #region 注释内容
                //投掷武器
                /*if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW)
                        {
                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                        }

                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD)
                        {
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
                }*/
                //判断命中结果
                //short dis = Map.Distance(sActor, i);
                //这个补全技能补正后去掉
                #endregion
                if (arg.argType == SkillArg.ArgType.Active)
                    shitbonus = 50;
                AttackResult res = CalcAttackResult(sActor, i, 0, sActor.Range > 3, shitbonus, scribonus);
                bool ismiss = false;
                if (res == AttackResult.Miss)
                {
                    res = AttackResult.Hit;
                    ismiss = true;
                }
               #region 注释卡片判定
                    /*
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
                }*/
                #endregion
                Actor target = i;
                //if (i.type == ActorType.PC)
                //{
                //    ActorPC me = (ActorPC)i;
                //    if (me.Status.Additions.ContainsKey("圣骑士的牺牲") && me.Party != null)
                //    {
                //        ActorPC victim = (ActorPC)map.Actors[(uint)me.TInt["牺牲者ActorID"]];
                //        if (victim == null) break;
                //        if (victim.Party != me.Party && (5 > Math.Max(Math.Abs(me.X - victim.X) / 100, Math.Abs(me.Y - victim.Y) / 100))) break;
                //        target = victim;
                //        ShowEffectByActor(i, 4345);
                //    }
                //}

                if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard)
                {
                    if (res == AttackResult.Miss)
                    {
                        arg.flag[index + counter] = AttackFlag.MISS;
                    }
                    else if (res == AttackResult.Avoid)
                        arg.flag[index + counter] = AttackFlag.AVOID;
                    else
                        arg.flag[index + counter] = AttackFlag.GUARD;
                    if (i.Status.Additions.ContainsKey("Parry"))//格挡
                    {
                        if (sActor == null)
                            return 0;
                        ActorPC pc = (ActorPC)i;
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            MapClient.FromActorPC(pc).SendSkillDummy(100, 1);
                            if (i.Status.Additions.ContainsKey("Parry"))
                                i.Status.Additions["Parry"].AdditionEnd();
                            ShowEffect(pc, pc, 4135);
                        }
                    }
                }
                else
                {
                    int restKryrie = 0;
                    if (i.Status.Additions.ContainsKey("MobKyrie"))//救援邀请，留着有参考价值
                    {
                        Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["MobKyrie"];
                        restKryrie = buf["MobKyrie"];
                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                        if (restKryrie > 0)
                        {
                            buf["MobKyrie"]--;
                            EffectArg arg3 = new EffectArg();
                            arg3.effectID = 4173;
                            arg3.actorID = i.ActorID;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg3, i, true);
                            if (restKryrie == 1)
                                RemoveAddition(i, "MobKyrie");
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
                            atk = (int)Global.Random.Next(mindamage, maxdamage);
                            //TODO: element bonus, range bonus
                            atk = (int)(atk * CalcElementBonus(sActor, i, element, 0, false) * ATKBonus);
                        }
                        else
                        {
                            atk = (int)Global.Random.Next(mindamage, maxdamage);
                            atk += (int)Global.Random.Next(mindamageM, maxdamageM);

                            atk = (int)(atk * CalcElementBonus(sActor, i, element, 0, false) * ATKBonus);
                            //atk = (int)ATKBonus;
                        }
                        //int igoreAddDef = 0;
                        if (sActor.TInt["刀锋之末破防"] > 0)
                            igoreAddDef += sActor.TInt["刀锋之末破防"];
                        if (sActor.TInt["幽怨之怒破防"] > 0)
                            igoreAddDef += sActor.TInt["幽怨之怒破防"];

                        if(i.Status.Additions.ContainsKey("八刀一闪破甲"))
                        {
                            ignore += 0.5f;
                            igoreAddDef += 50;
                            ShowEffectOnActor(i, 8057);
                        }

                        if(sActor.Status.Additions.ContainsKey("完美谢幕伤害提升") && sActor.TInt["完美谢幕破防提升Rate"] > 0)
                        {
                            ignore += sActor.TInt["完美谢幕破防提升Rate"] / 100f;
                            ShowEffectOnActor(i, 8050);
                        }

                        ignore += sActor.TInt["幽怨之怒破防"] / 100f;


                        if (igoreAddDef > 0)
                            damage = CalcPhyDamage(i, defType, atk, ignore, igoreAddDef / 100f);
                        else
                            damage = CalcPhyDamage(i, defType, atk, ignore);

                        if (damage > atk)
                            damage = atk;

                        if (i.type == ActorType.PARTNER) return 20;
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
                        damage = checkbuff(sActor, target, arg, 0, damage,true, element);
                        damage = checkirisbuff(sActor, target, arg, 0, damage);
                        if (damage <= 0 && !sActor.Buff.九尾狐魅惑) damage = 1;

                        if (res == AttackResult.Critical)
                            damage = (int)(((float)damage) * (1f + CalCriBonusRate(sActor, i, 0) / 100f));
                        if (ismiss)//取消MISS
                        {
                            damage = (int)(damage * 0.6f);
                            res = AttackResult.Hit;
                            ShowEffectOnActor(target, 8037);
                        }

                        checkdebuff(sActor, target, arg, 0);
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
                        if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)target;
                            if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.チャンプモンスターキラー状態)
                                damage = damage / 10;
                        }

                        if (sActor.type == ActorType.PC)
                        {
                            int score = damage / 100;
                            if (score == 0)
                                score = 1;
                            ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, score);
                        }
                        //加伤处理下
                        if (target.Seals > 0)
                            damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                        if (res == AttackResult.Critical && sActor.type == ActorType.PC && sActor.Status.Additions.ContainsKey("Scorponok"))
                            damage = (int)(damage * (float)(1f + 0.005f * ((ActorPC)sActor).TInt["Scorponok暴击"]));
                        if (res == AttackResult.Critical && sActor.type == ActorType.PC && sActor.Status.Additions.ContainsKey("浮游炮CD"))
                            damage = (int)(damage * (float)(1f + 0.005f * ((ActorPC)sActor).TInt["浮游炮暴击"]));
                        //加伤处理上
                        //减伤处理下
                        /*if (i.SHIELDHP > 0)//护盾
                        {
                            if (i.SHIELDHP >= damage)
                            {
                                i.SHIELDHP -= (uint)damage;
                                ShowEffectByActor(i, 4173);
                            }
                            else
                            {
                                i.SHIELDHP = 0;
                                damage -= (int)i.SHIELDHP;
                                ShowEffectByActor(i, 4267);
                            }
                        }*/
                        //减伤处理上
                        //吸血效果下
                        if (SuckBlood != 0)
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                int hp = (int)(damage * SuckBlood);
                                if (((ActorPC)sActor).TInt["SuckBlood"] > 0)
                                    hp = (int)(hp * (float)(1f + ((ActorPC)sActor).TInt["SuckBlood"] * 0.1f));
                                sActor.HP += (uint)hp;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                                Instance.ShowVessel(sActor, -hp);
                            }
                        }
                        //吸血效果上

                        //结算HP结果

                        
                        if (target.HP != 0)
                        {
                            if (damage >= 0)
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            else
                                arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                            if (res == AttackResult.Critical)
                                arg.flag[index + counter] |= AttackFlag.CRITICAL;

                            if (target.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)target;
                                if (pc.Skills.ContainsKey(14052) && !pc.Status.Additions.ContainsKey("无尽寒冬CD")) //精通：寒霜护甲
                                {
                                    uint totalHP = target.HP; //记录当前HP
                                    if (target.TInt["续命开关"] == 1 && target.SP > 0)
                                        totalHP += target.SP;
                                    if (target.Status.Additions.ContainsKey("圣盾加护") && target.SHIELDHP > 0)
                                        totalHP += target.SHIELDHP;
                                    if (damage >= totalHP)//伤害足以致死
                                    {
                                        damage = 0;

                                        //移除控制状态
                                        if (pc.Status.Additions.ContainsKey("Confuse")) RemoveAddition(pc, "Confuse");
                                        if (pc.Status.Additions.ContainsKey("Frosen")) RemoveAddition(pc, "Frosen");
                                        if (pc.Status.Additions.ContainsKey("Paralyse")) RemoveAddition(pc, "Paralyse");
                                        if (pc.Status.Additions.ContainsKey("Silence")) RemoveAddition(pc, "Silence");
                                        if (pc.Status.Additions.ContainsKey("Sleep")) RemoveAddition(pc, "Sleep");
                                        if (pc.Status.Additions.ContainsKey("Stone")) RemoveAddition(pc, "Stone");
                                        if (pc.Status.Additions.ContainsKey("Stun")) RemoveAddition(pc, "Stun");

                                        //PutSkill(pc, pc, 14022, pc.Skills2[14022].Level, 0, 0, true);
                                        SkillArg arg2 = new SkillArg();
                                        arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(14022, pc.Skills2[14022].Level);
                                        SkillCast(pc, pc, arg2);


                                        if (!pc.Status.Additions.ContainsKey("无尽寒冬CD"))
                                        {
                                            int cdtime = 240000;
                                            switch (pc.TInt["降温"])
                                            {
                                                case 1:
                                                    cdtime = 192000;
                                                    break;
                                                case 2:
                                                    cdtime = 156000;
                                                    break;
                                                case 3:
                                                    cdtime = 120000;
                                                    break;
                                            }
                                            OtherAddition skill = new OtherAddition(null, pc, "无尽寒冬CD", cdtime);
                                            ApplyAddition(pc, skill);
                                        }
                                    }
                                }
                            }
                            if (target.TInt["续命开关"] == 1 && damage > 0 && target.SP > 0)
                            {
                                if (target.SP > damage)
                                {
                                    target.SP -= (uint)damage;
                                    arg.flag[index + counter] = AttackFlag.SP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                    arg.sp[index + counter] = damage;
                                    ShowEffectOnActor(target, 4173, sActor);
                                    damage = 0;
                                }
                                else
                                {
                                    ShowEffectOnActor(target, 5445, sActor);
                                    damage -= (int)target.SP;
                                    target.SP = 0;
                                    target.Status.Additions.Remove("神圣光界");
                                }
                            }
                            if (damage > 0 && target.SHIELDHP > 0 && target.Status.Additions.ContainsKey("圣盾加护"))
                            {
                                if (target.SHIELDHP > damage)
                                {
                                    target.SHIELDHP -= (uint)damage;
                                    arg.flag[index + counter] = AttackFlag.MP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                    arg.mp[index + counter] = damage;
                                    ShowEffectOnActor(target, 4174);
                                    damage = 0;
                                }
                                else
                                {
                                    ShowEffectOnActor(target, 5447);
                                    damage -= (int)target.SHIELDHP;
                                    target.SHIELDHP = 0;
                                    RemoveAddition(target, "圣盾加护");
                                }
                            }
                            if (damage >= target.HP && target.Status.Additions.ContainsKey("HolyVolition"))//7月16日更新的光之意志BUFF
                            {
                                //arg.flag[index + counter] = arg.flag[index + counter] ^ AttackFlag.DIE;//位异或死亡FLAG
                                damage = 0;
                                target.HP = 1;
                                ShowEffectOnActor(target, 4173);
                            }

                            arg.hp[index + counter] = damage;

                            if (damage > target.HP && target.TInt["副本复活标记"] == 4 && target.TInt["单人复活次数"] > 0)
                            {
                                CancelSkillCast(target);
                                target.ClearTaskAddition();
                                //arg.flag[index + counter] = arg.flag[index + counter] ^ AttackFlag.DIE;//位异或死亡FLAG
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                target.TInt["单人复活次数"] -= 1;
                                target.HP = target.MaxHP;
                                target.MP = target.MaxMP;
                                target.SP = target.MaxSP;
                                List<Actor> actors = GetActorsAreaWhoCanBeAttackedTargets(target, 300);
                                foreach (var item in actors)
                                {
                                    if (CheckValidAttackTarget(target, item))
                                    {
                                        PushBack(target, item, 3);
                                        ShowEffectOnActor(item, 5275);
                                        if (!item.Status.Additions.ContainsKey("Stun"))
                                        {
                                            Stun stun = new Stun(null, item, 3000);
                                            ApplyAddition(item, stun);
                                        }
                                    }
                                }
                                ShowEffectOnActor(target, 4243);
                                damage = 0;
                                SendSystemMessage(target, "你被使用了一次复活机会！剩余次数：" + target.TInt["单人复活次数"].ToString());

                                CastPassiveSkills((ActorPC)target);//重新计算被动

                                if (!target.Status.Additions.ContainsKey("HolyVolition"))
                                {
                                    DefaultBuff skill = new DefaultBuff(null, target, "HolyVolition", 5000);
                                    ApplyAddition(target, skill);
                                }
                            }

                            if (damage >= target.HP && !ride && !target.Buff.リボーン)
                                arg.flag[index + counter] |= AttackFlag.DIE;

                            if (damage > target.HP)
                                target.HP = 0;
                            else
                            {
                                if (damage < 0) //治疗时，抹除过量值
                                    damage = -(Math.Min(-damage, (int)(target.MaxHP - target.HP)));
                                target.HP = (uint)(target.HP - damage);
                            }

                            //ClientManager.EnterCriticalArea();
                            ApplyDamage(sActor, target, damage, doublehate, arg);

                            counter++;
                        }

                    }
                }


                //ClientManager.LeaveCriticalArea();
                

                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
                //MapClient.FromActorPC((ActorPC)sActor).SendPartyMemberHPMPSP((ActorPC)sActor);
            }


            short aspd = (short)(sActor.Status.aspd);
            if (aspd > 900)
                aspd = 900;
            //2012.6.29日修正攻速为旧攻速的5/6。
            //aspd = (short)(aspd * 5 / 6);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);
            }
            else
                arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);
            arg.delay = (uint)(arg.delay * arg.delayRate);
            //physicalCounter--;
            //arg.Add(arg2);
            return damage;
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

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, DefType.MDef, element, elementValue, MATKBonus, index);
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
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, 0, index, setAtk, false, SuckBlood, WeaponAttack, ignore);
        }
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int mcridamagebonus, int index, bool setAtk, bool noReflect, float SuckBlood, bool WeaponAttack = false, float ignore = 0)
        {
            if (sActor.Status == null || dActor.Count == 0) return 0;
            //if (dActor.Count > 10)
            //{
            //    foreach (var item in dActor)
            //        DoDamage(false, sActor, item, arg, defType, element, index, MATKBonus);
            //    return 0;
            //}
            int damage = 0;

            int matk;
            int mindamage = 0;
            int maxdamage = 0;

            int counter = 0;
            Map map = MapManager.Instance.GetMap(dActor[0].MapID);
            if (arg.affectedActors.Count > 0)
            {
                counter = arg.affectedActors.Count;
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Extend(arg.affectedActors.Count);
            }
            else
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }
            mindamage = sActor.Status.min_matk;
            maxdamage = sActor.Status.max_matk;

            if(sActor.type == ActorType.PARTNER)
            {
                ActorPartner partner = sActor as ActorPartner;
                if(partner.Owner != null && partner.Owner.Status != null)
                {
                    mindamage = partner.Owner.Status.min_matk;
                    maxdamage = partner.Owner.Status.max_matk;
                }
            }

            if (mindamage > maxdamage) maxdamage = mindamage;

            foreach (Actor i in dActor)
            {
                bool isPossession = false;
                bool isHost = false;
                Actor target = i;
                if (i.Status == null)
                    continue;

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
                if (i.Status.Additions.ContainsKey("MagicReflect") && i != sActor && !noReflect)
                {
                    arg.Remove(i);
                    int oldcount = arg.flag.Count;
                    arg.Extend(1);
                    arg.affectedActors.Add(sActor);
                    List<Actor> dst = new List<Actor>();
                    dst.Add(sActor);
                    RemoveAddition(i, "MagicReflect");
                    MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, MATKBonus, oldcount, false, true);
                    continue;
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
                    if (sActor.Status.zenList.Contains((ushort)arg.skill.ID))
                    {
                        matk *= 2;
                    }
                    if (sActor.Status.darkZenList.Contains((ushort)arg.skill.ID))
                    {
                        matk *= 2;
                    }
                }
                else
                    matk = (int)MATKBonus;
                if (MATKBonus > 0)
                {
                    int igoreAddDef = 0;
                    if (sActor.TInt["刺骨毒牙破防"] > 0)
                        igoreAddDef += sActor.TInt["刺骨毒牙破防"];
                    if (sActor.TInt["幽怨之怒破防"] > 0)
                        igoreAddDef += sActor.TInt["幽怨之怒破防"];

                    if (i.Status.Additions.ContainsKey("八刀一闪破甲"))
                    {
                        ignore += 0.5f;
                        igoreAddDef += 50;
                        ShowEffectOnActor(i, 8057);
                    }
                    if (sActor.Status.Additions.ContainsKey("完美谢幕伤害提升") && sActor.TInt["完美谢幕破防提升Rate"] > 0)
                    {
                        ignore += sActor.TInt["完美谢幕破防提升Rate"] / 100f;
                        ShowEffectOnActor(i, 8050);
                    }
                    ignore += sActor.TInt["幽怨之怒破防"] / 100f;

                    if (igoreAddDef > 0)
                        damage = CalcPhyDamage(i, defType, matk, ignore, igoreAddDef / 100f);
                    else
                        damage = CalcPhyDamage(i, defType, matk, ignore);
                }
                else
                {
                    damage = matk;
                }


                //魔法会心判定
                AttackResult res = CalcAttackResult(sActor, i, 1, sActor.Range > 3, 0, 0);
                if (sActor.Status.Additions.ContainsKey("彼岸焚烧") && arg.skill.ID == 14010)//彼岸火湖下烈焰焚烧必定暴击   
                    res = AttackResult.Critical;
                if (res == AttackResult.Critical)  
                {
                    if (MATKBonus > 0)
                        damage = (int)(damage * (1f + CalCriBonusRate(sActor, i, 0) / 100f) + mcridamagebonus);
                    if (arg.skill.ID == 13102 || arg.skill.ID == 13109)  //圣疗术暴击时
                    {
                        damage *= 2;
                    }
                }
                if (res == AttackResult.Miss)//取消MISS
                {
                    damage = (int)(damage * 0.6f);
                    res = AttackResult.Hit;
                }
                if (i.Status.Additions.ContainsKey("Invincible"))//绝对壁垒
                {
                    res = AttackResult.Guard;
                    damage = 0;
                }

                damage = checkbuff(sActor, target, arg, 1, damage, true, element);
                damage = checkirisbuff(sActor, target, arg, 1, damage);
                if (i.Buff.Frosen == true && element == Elements.Fire)
                {
                    RemoveAddition(i, i.Status.Additions["Frosen"]);
                }
                if (i.Buff.Stone == true && element == Elements.Water)
                {
                    RemoveAddition(i, i.Status.Additions["Stone"]);
                }

                if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                {
                    if (((ActorPC)target).Mode != PlayerMode.NORMAL)
                        damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                }

                if (damage < 0 && MATKBonus >= 0 && !sActor.Buff.九尾狐魅惑)
                    damage = 0;


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

                if (sActor.type == ActorType.PC)
                {
                    int score = damage / 100;
                    if (score == 0 && damage != 0)
                        score = 1;
                    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, Math.Abs(score));
                }

                //加伤处理下
                if (target.Seals > 0)
                    damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                //加伤处理上
                //减伤处理下
                /*if (i.SHIELDHP > 0)//护盾
                {
                    if (i.SHIELDHP >= damage)
                    {
                        i.SHIELDHP -= (uint)damage;
                        ShowEffectByActor(i, 4174);
                    }
                    else
                    {
                        i.SHIELDHP = 0;
                        damage -= (int)i.SHIELDHP;
                        ShowEffectByActor(i, 4267);
                    }
                }*/
                //减伤处理上

                //吸血效果下
                if (SuckBlood != 0 && damage != 0)//吸血效果
                {
                    if (sActor.type == ActorType.PC)
                    {
                        int hp = (int)(damage * SuckBlood);
                        sActor.HP += (uint)hp;
                        if (sActor.HP > sActor.MaxHP)
                            sActor.HP = sActor.MaxHP;
                        Instance.ShowVessel(sActor, -hp);
                    }
                }
                //吸血效果上
                if (target.Status.Additions.ContainsKey("Sacrifice") && damage < 0)
                    damage = 0;


                if (target.HP != 0)
                {
                    if (damage >= 0)
                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE;
                    else
                        arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                    if (res == AttackResult.Critical)
                        arg.flag[index + counter] |= AttackFlag.CRITICAL;

                    if (target.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)target;
                        if (pc.Skills.ContainsKey(14052) && !pc.Status.Additions.ContainsKey("无尽寒冬CD")) //精通：寒霜护甲
                        {
                            uint totalHP = target.HP; //记录当前HP
                            if (target.TInt["续命开关"] == 1 && target.SP > 0)
                                totalHP += target.SP;
                            if (target.Status.Additions.ContainsKey("圣盾加护") && target.SHIELDHP > 0)
                                totalHP += target.SHIELDHP;
                            if (damage >= totalHP)//伤害足以致死
                            {
                                damage = 0;
                                //移除控制状态
                                if (pc.Status.Additions.ContainsKey("Confuse")) RemoveAddition(pc, "Confuse");
                                if (pc.Status.Additions.ContainsKey("Frosen")) RemoveAddition(pc, "Frosen");
                                if (pc.Status.Additions.ContainsKey("Paralyse")) RemoveAddition(pc, "Paralyse");
                                if (pc.Status.Additions.ContainsKey("Silence")) RemoveAddition(pc, "Silence");
                                if (pc.Status.Additions.ContainsKey("Sleep")) RemoveAddition(pc, "Sleep");
                                if (pc.Status.Additions.ContainsKey("Stone")) RemoveAddition(pc, "Stone");
                                if (pc.Status.Additions.ContainsKey("Stun")) RemoveAddition(pc, "Stun");
                            
                                SkillArg arg2 = new SkillArg();
                                arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(14022, pc.Skills2[14022].Level);
                                SkillCast(pc, pc, arg2);

                                //PutSkill(pc, pc, 14022, pc.Skills2[14022].Level, 0, 0, true);

                                if (!pc.Status.Additions.ContainsKey("无尽寒冬CD"))
                                {
                                    int cdtime = 240000;
                                    switch (pc.TInt["降温"])
                                    {
                                        case 1:
                                            cdtime = 192000;
                                            break;
                                        case 2:
                                            cdtime = 156000;
                                            break;
                                        case 3:
                                            cdtime = 120000;
                                            break;
                                    }
                                    OtherAddition skill = new OtherAddition(null, pc, "无尽寒冬CD", cdtime);
                                    ApplyAddition(pc, skill);
                                }
                            }
                        }
                    }
                    if (target.TInt["续命开关"] == 1 && damage > 0 && target.SP > 0)
                    {
                        if (target.SP > damage)
                        {
                            target.SP -= (uint)damage;
                            arg.flag[index + counter] = AttackFlag.SP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            arg.sp[index + counter] = damage;
                            ShowEffectOnActor(target, 4173, sActor);
                            damage = 0;
                        }
                        else
                        {
                            ShowEffectOnActor(target, 5445, sActor);
                            damage -= (int)target.SP;
                            target.SP = 0;
                            target.Status.Additions.Remove("神圣光界");
                        }
                    }
                    if (damage > 0 && target.SHIELDHP > 0 && target.Status.Additions.ContainsKey("圣盾加护"))
                    {
                        if (target.SHIELDHP > damage)
                        {
                            target.SHIELDHP -= (uint)damage;
                            arg.flag[index + counter] = AttackFlag.MP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            arg.mp[index + counter] = damage;
                            ShowEffectOnActor(target, 4174, sActor);
                            damage = 0;
                        }
                        else
                        {
                            ShowEffectOnActor(target, 5447, sActor);
                            damage -= (int)target.SHIELDHP;
                            target.SHIELDHP = 0;
                            RemoveAddition(target, "圣盾加护");
                        }
                    }
                    if (damage >= target.HP && target.Status.Additions.ContainsKey("HolyVolition"))//7月16日更新的光之意志BUFF
                    {
                        //arg.flag[index + counter] = arg.flag[index + counter] ^ AttackFlag.DIE;//位异或死亡FLAG
                        damage = 0;
                        target.HP = 1;
                        ShowEffectOnActor(target, 4173);
                    }
                    
                    arg.hp[index + counter] = damage;

                    if (damage > target.HP && target.TInt["副本复活标记"] == 4 && target.TInt["单人复活次数"] > 0)
                    {
                        CancelSkillCast(target);
                        target.ClearTaskAddition();
                        //arg.flag[index + counter] = arg.flag[index + counter] ^ AttackFlag.DIE;//位异或死亡FLAG
                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                        target.TInt["单人复活次数"] -= 1;
                        target.HP = target.MaxHP;
                        target.MP = target.MaxMP;
                        target.SP = target.MaxSP;
                        List<Actor> actors = GetActorsAreaWhoCanBeAttackedTargets(target, 300);
                        foreach (var item in actors)
                        {
                            if (CheckValidAttackTarget(target, item))
                            {
                                PushBack(target, item, 3);
                                ShowEffectOnActor(item, 5275, sActor);
                                if (!item.Status.Additions.ContainsKey("Stun"))
                                {
                                    Stun stun = new Stun(null, item, 3000);
                                    ApplyAddition(item, stun);
                                }
                            }
                        }
                        ShowEffectOnActor(target, 4243, sActor);
                        damage = 0;
                        SendSystemMessage(target, "你被使用了一次复活机会！剩余次数：" + target.TInt["单人复活次数"].ToString());

                        CastPassiveSkills((ActorPC)target);//重新计算被动

                        if (!target.Status.Additions.ContainsKey("HolyVolition"))
                        {
                            DefaultBuff skill = new DefaultBuff(null, target, "HolyVolition", 5000);
                            ApplyAddition(target, skill);
                        }

                        /*if (!pc.Tasks.ContainsKey("Recover"))//自然恢复
                        {
                            Tasks.PC.Recover reg = new Tasks.PC.Recover(MapClient.FromActorPC(pc));
                            pc.Tasks.Add("Recover", reg);
                            reg.Activate();
                        }*/


                    }
                    if (damage >= target.HP && !ride && !target.Buff.リボーン)
                        arg.flag[index + counter] |= AttackFlag.DIE;
                    counter++;

                    if (damage > target.HP)
                        target.HP = 0;
                    else
                    {
                        if (damage < 0) //治疗时，抹除过量值
                            damage = -(Math.Min(-damage, (int)(target.MaxHP - target.HP)));
                        target.HP = (uint)(target.HP - damage);
                    }

                    ApplyDamage(sActor, target, damage, arg);

                }
                

                //ClientManager.LeaveCriticalArea();
                
                MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
                
                //MapClient.FromActorPC((ActorPC)sActor).SendPartyMemberHPMPSP((ActorPC)sActor);
                
            }
            return damage;
            //magicalCounter--;
        }
        /// <summary>
        /// 对指定目标附加伤害
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="damage">伤害值</param>
        protected void ApplyDamage(Actor sActor, Actor dActor, int damage, SkillArg arg2 = null)
        {
            ApplyDamage(sActor, dActor, damage, false, arg2);
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

            if ((dActor.type == ActorType.MOB || dActor.type == ActorType.PET) && damage >= 0)
            {
                Actor attacker;

                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;

                    //称号 溜不溜
                    if (damage == 666 || damage == 6666 || damage == 66666)
                        MapClient.FromActorPC(pc).TitleProccess(pc, 124, 1);

                    if (pc.PartnerTartget != dActor)
                    RemoveAddition(pc, "玩家当前搭档目标");
                    if (!pc.Status.Additions.ContainsKey("玩家当前搭档目标"))
                    {
                        OtherAddition skill = new OtherAddition(null, pc, "玩家当前搭档目标", 20000,true);
                        skill.OnAdditionStart += (s, e) => { pc.PartnerTartget = dActor; };
                        skill.OnAdditionEnd += (s, e) => { pc.PartnerTartget = null; };
                        ApplyAddition(pc, skill);
                    }
                    else if(((OtherAddition)pc.Status.Additions["玩家当前搭档目标"]).endTime < DateTime.Now + new TimeSpan(0,0,0,10))
                        ((OtherAddition)pc.Status.Additions["玩家当前搭档目标"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 20);

                    if (pc.PossessionTarget != 0)
                    {
                        Actor possession = Manager.MapManager.Instance.GetMap(pc.MapID).GetActor(pc.PossessionTarget);
                        if (possession != null)

                            if (possession.type == ActorType.PC)
                                attacker = possession;
                            else
                                attacker = sActor;
                        else
                            attacker = sActor;
                    }
                    else
                        attacker = sActor;
                }
                else
                    attacker = sActor;
                if (dActor.type == ActorType.MOB)//仇恨添加
                {
                    ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)dActor.e;
                    mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage * 3);
                }
                else
                {
                    ActorEventHandlers.PetEventHandler mob = (ActorEventHandlers.PetEventHandler)dActor.e;
                    mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage * 3);
                }
            }
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Online)
                {
                    pc.LastAttackTime = DateTime.Now;
                    if (damage > 0)
                    {
                        pc.TInt["伤害统计"] += damage;
                    }
                    else if (dActor.HP < dActor.MaxHP)
                    {
                        if (dActor.MaxHP - dActor.HP > damage)
                        {
                            pc.TInt["治疗统计"] += -damage;
                        }
                        else
                        {
                            uint heal = dActor.MaxHP - dActor.HP;
                            pc.TInt["治疗统计"] += (int)heal;
                        }
                    }

                    List<SagaDB.Item.EnumEquipSlot> slot = new List<SagaDB.Item.EnumEquipSlot>() {  SagaDB.Item.EnumEquipSlot.RIGHT_HAND,
                        SagaDB.Item.EnumEquipSlot.CHEST_ACCE, SagaDB.Item.EnumEquipSlot.UPPER_BODY, SagaDB.Item.EnumEquipSlot.LEFT_HAND};
                    if(damage >= pc.Level || dActor.Status.def >= 90 || dActor.Status.mdef >= 90)
                    {
                        foreach (var item in slot)
                        {
                            if (pc.Inventory.Equipments.ContainsKey(item) && dActor.HP != dActor.MaxHP)
                            {
                                SagaDB.Item.Item equip = pc.Inventory.Equipments[item];
                                if (equip.EquipSlot[0] == item)
                                {
                                    if (!equip.Old && equip.Refine >= 10)
                                    {
                                        byte jobrate = 13;
                                        switch(pc.Job)
                                        {
                                            case PC_JOB.HAWKEYE:
                                                jobrate = 32;
                                                break;
                                            case PC_JOB.CARDINAL:
                                                jobrate = 7;
                                                break;
                                            case PC_JOB.GARDNER:
                                                jobrate = 12;
                                                break;
                                            case PC_JOB.FORCEMASTER:
                                                jobrate = 13;
                                                break;
                                        }
                                        byte rate = 1;
                                        if (equip.Refine > 30) rate = 20;
                                        if (equip.Refine > 60) rate = 40;
                                        if (equip.Refine > 90) rate = 100;
                                        if (equip.Refine > 110) rate = 200;
                                        if (Global.Random.Next(equip.Refine * jobrate * rate) == 1)
                                        {
                                            equip.Old = true;
                                            MapClient.FromActorPC(pc).SendSystemMessage("你的【" + equip.BaseData.name + "】的样子突然变得很奇怪！！");
                                            ShowEffectOnActor(pc, 5266);
                                            ShowEffectOnActor(pc, 5204);
                                            MapClient.FromActorPC(pc).SendItemInfo(equip);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if(dActor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)dActor;
                if(mob.TInt["森罗万触-始"] == 1)
                {
                    mob.TInt["森罗万触伤害累积"] += damage;
                }
            }
            if (dActor.type == ActorType.PC)
            {
                //如果凭依中受攻击解除凭依
                //TODO: 支援スキル使用時の憑依解除設定
                ActorPC pc = (ActorPC)dActor;
                if (pc.Online)
                {
                    if (damage >= 0)
                    {
                        pc.TInt["受伤害统计"] += damage;
                        MapClient client = MapClient.FromActorPC(pc);
                        if (client.Character.Buff.憑依準備)
                        {
                            client.Character.Buff.憑依準備 = false;
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
                    else
                        pc.TInt["受治疗统计"] += -damage;
                    List<SagaDB.Item.EnumEquipSlot> slot = new List<SagaDB.Item.EnumEquipSlot>() {  SagaDB.Item.EnumEquipSlot.RIGHT_HAND,
                        SagaDB.Item.EnumEquipSlot.CHEST_ACCE, SagaDB.Item.EnumEquipSlot.UPPER_BODY};
                    if (damage > pc.Level * 3)
                    {
                        foreach (var item in slot)
                        {
                            if (pc.Inventory.Equipments.ContainsKey(item) && dActor.HP != dActor.MaxHP)
                            {
                                SagaDB.Item.Item equip = pc.Inventory.Equipments[item];
                                if (!equip.Old)
                                {
                                    byte rate = 1;
                                    if (equip.Refine > 30) rate = 5;
                                    if (equip.Refine > 60) rate = 15;
                                    if (equip.Refine > 90) rate = 25;
                                    if (equip.Refine > 110) rate = 45;
                                    if (Global.Random.Next(equip.Refine * 60 * rate) == 1)
                                    {
                                        equip.Old = true;
                                        MapClient.FromActorPC(pc).SendSystemMessage("你的【" + equip.BaseData.name + "】的样子突然变得很奇怪！！");
                                        ShowEffectOnActor(pc, 5266);
                                        ShowEffectOnActor(pc, 5204);
                                        MapClient.FromActorPC(pc).SendItemInfo(equip);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            //如果对象目标死亡
            if (dActor.HP == 0)
            {
                if (!dActor.Buff.Dead)
                {
                    if (dActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)dActor;
                        if (pc.Status.Additions.ContainsKey("Reincarnate"))//7月17日更新的亡灵转生BUFF
                        {
                            dActor.Status.Additions["Reincarnate"].AdditionEnd();
                            dActor.Status.Additions.Remove("Reincarnate");
                            dActor.HP += dActor.HP = dActor.MaxHP;
                            if (dActor.HP > dActor.MaxHP) dActor.HP = dActor.MaxHP;
                            MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg2, sActor, true);
                            return;
                        }
                        if (pc.Pet != null)
                        {
                            if (pc.Pet.Ride)
                            {
                                ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)pc.e;
                                Packets.Client.CSMG_ITEM_MOVE p = new Packets.Client.CSMG_ITEM_MOVE();
                                p.data = new byte[11];
                                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.PET))
                                {
                                    SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET];
                                    if (item.Durability != 0) item.Durability--;
                                    eh.Client.SendItemInfo(item);
                                    eh.Client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.PET_FRIENDLY_DOWN, item.BaseData.name));
                                    EffectArg arg = new EffectArg();
                                    arg.actorID = eh.Client.Character.ActorID;
                                    arg.effectID = 8044;
                                    eh.OnShowEffect(eh.Client.Character, arg);
                                    p.InventoryID = item.Slot;
                                    p.Target = SagaDB.Item.ContainerType.BODY;
                                    p.Count = 1;
                                    eh.Client.OnItemMove(p);
                                }
                                //return;
                            }
                        }
                    }
                    if (dActor.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)dActor;
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)sActor;
                            ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)pc.e;

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

                            Map map = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                            List<Actor> eventactors = map.GetActorsArea(dActor, 3000, false, true);
                            List<ActorPC> owners = new List<ActorPC>();
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
                        //ExperienceManager.Instance.ProcessMobExp(mob);
                    }
                    if (sActor.type == ActorType.PC && dActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)sActor;
                        ActorPC dpc = (ActorPC)dActor;
                        Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                        if (pc.Mode == PlayerMode.KNIGHT_EAST || pc.Mode == PlayerMode.KNIGHT_WEST)
                            pc.Mode = PlayerMode.NORMAL;
                        if (dpc.Mode == PlayerMode.KNIGHT_EAST || dpc.Mode == PlayerMode.KNIGHT_WEST)
                            dpc.Mode = PlayerMode.NORMAL;
                        if(sActor != dActor)
                        map.Announce(dActor.Name + " 被 " + sActor.Name + " 推倒了。" );

                        if (pc.TInt["大逃杀模式"] == 1)
                        {
                            int Pcount = 0;
                            string winner = "";
                            foreach (var item in map.Actors.Values)
                            {
                                if (item.type == ActorType.PC && item.HP > 0)
                                {
                                    Pcount++;
                                    winner = item.name;
                                }
                            }
                            map.Announce("当前场上还剩下" + Pcount + "名生存者。");
                            if (Pcount == 1)
                                map.Announce("恭喜" + winner + "今晚吃鸡！");
                        }
                        /*pc.TInt["PVP连杀"]++;
                         * 
                        if (pc.TInt["PVP连杀"] > pc.TInt["PVP最大连杀"]) pc.TInt["PVP最大连杀"] = pc.TInt["PVP连杀"];
                        if (pc.TInt["PVP连杀"] > 2)
                            map.Announce(sActor.Name + "连杀了 "+ pc.TInt["PVP连杀"].ToString() +" 个人了！谁快来阻止他啊！！");
                        if (dpc.TInt["PVP连杀"] > 2)
                            map.Announce(sActor.Name + "终结了 " + dpc.Name + " 的" + dpc.TInt["PVP连杀"].ToString() + " 连杀！！");
                        dpc.TInt["PVP连杀"] = 0;*/
                    }
                    if (dActor.type == ActorType.PC && sActor != dActor)
                    {
                        ActorPC pc = (ActorPC)dActor;
                        if (pc.Online)
                        {
                            pc.TInt["死亡统计"]++;
                            if (pc.Party != null)
                                pc.Party.TInt["团队死亡"]++;
                        }
                        if(sActor.type == ActorType.PC)
                        {
                            ActorPC pc2 = (ActorPC)sActor;
                            if (pc2.Online)
                            {
                                pc2.TInt["击杀统计"]++;
                                /*MapClient.FromActorPC(pc2).SendSystemMessage("击杀数：" + pc2.TInt["击杀统计"].ToString() +
                                    " 最高连杀数：" + pc2.TInt["PVP最大连杀"].ToString() + " 死亡数：" + pc2.TInt["死亡统计"].ToString() +
                                    " 已造成伤害：" + pc2.TInt["伤害统计"].ToString() + " 已治疗：" + pc2.TInt["治疗统计"].ToString());*/
                            }
                        }
                    }
                    dActor.e.OnDie();
                }
            }
            TitleEventManager.Instance.OnApplyDamage(sActor, dActor, damage);
        }
    }
}
