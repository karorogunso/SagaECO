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
            if (dActor.Count > 12)
            {
                int ACount = dActor.Count;
                for (int i = 12; i < ACount; i++)
                {
                    dActor.RemoveAt(12);
                }
            }
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
            return PhysicalAttack(sActor, dActor, arg, DefType.Def, element, index, ATKBonus, false, 0 ,false);
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
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate)
        {
            if (dActor.Count == 0) return 0;
            if (sActor.Status == null)
                return 0;
            //if (physicalCounter >= 50)
            //    Logger.ShowDebug("Recurssion over 50 times!", Logger.defaultlogger);
            //Debug.Assert(physicalCounter < 50, "Recurssion over 50 times!");
            //physicalCounter++;
            if (sActor.type == ActorType.PC && Configuration.Instance.AtkMastery)//熟练度
            {
                MapClient mpc = MapClient.FromActorPC((ActorPC)sActor);
                if (((ActorPC)sActor).Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))//如果玩家右手有装备武器
                {
                    if (((ActorPC)sActor).DefLv < 100)//设置熟练度最大等级
                    {
                        ((ActorPC)sActor).DefPoint += 1;//每攻击一次加多少熟练
                        if (((ActorPC)sActor).DefPoint >= ((((ActorPC)sActor).DefLv * 2) * (((ActorPC)sActor).DefLv * 1)))//熟练度等级算法
                        {
                            ((ActorPC)sActor).DefLv += 1;//熟练度升级
                            EffectArg effectarg = new EffectArg();
                            effectarg.actorID = ((ActorPC)sActor).ActorID;
                            effectarg.effectID = 9913;
                            ((ActorPC)sActor).e.OnShowEffect((ActorPC)sActor, effectarg);
                            string WLLV = string.Format("物理攻击熟练度达到了Lv.{0}", ((ActorPC)sActor).DefLv);
                            mpc.SendSystemMessage(WLLV);
                        }
                    }
                }
            }


            int damage = 0;

            //calculate the ATK
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
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW)
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
                }
                //short dis = Map.Distance(sActor, i);
                AttackResult res = CalcAttackResult(sActor, i, sActor.Range > 2);
                //res = AttackResult.Miss;
                Actor target = i;
                
                if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard)
                {
                    if (res == AttackResult.Miss)
                        arg.flag[index + counter] = AttackFlag.MISS;
                    else if (res == AttackResult.Avoid)
                        arg.flag[index + counter] = AttackFlag.AVOID;
                    else
                        arg.flag[index + counter] = AttackFlag.GUARD;
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
                            atk = (short)Global.Random.Next(mindamage, maxdamage);
                            //TODO: element bonus, range bonus
                            atk = (short)(atk * CalcElementBonus(sActor, i, element, 50, false) * ATKBonus);
                            if (i.Buff.Frosen == true && element == Elements.Fire)
                            {
                                RemoveAddition(i, i.Status.Additions["Frosen"]);
                            }
                            if (i.Buff.Stone == true && element == Elements.Water)
                            {
                                RemoveAddition(i, i.Status.Additions["Stone"]);
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
                        //Calculate damage, after reduced by def
                        uint wlenddef = 0;
                        uint wlendmdef = 0;
                        if (defType == DefType.Def)
                        {
                            if (sActor.type == ActorType.MOB && isBossMob(sActor) && Configuration.Instance.BossSlash)
                                wlenddef = i.Status.def - (i.Status.def * (uint)Configuration.Instance.BossSlashRate / 100);
                            else if (sActor.type == ActorType.PC && Configuration.Instance.AtkMastery)
                                //wlenddef = i.Status.def - (i.Status.def * ((uint)((ActorPC)sActor).DefLv) / 200);//熟练度削防程度，200为削弱 熟练度/200 的防御
                                wlenddef = i.Status.def;
                            else
                                wlenddef = i.Status.def;
                            if (arg.skill != null)
                            {
                                if (arg.skill.ID == 2354)
                                    wlenddef = wlenddef / 2;
                            }
                            if (i.Status.Additions.ContainsKey("イレイザー") && i.Status.Purger_Lv > 0)//3转刺客10级技能
                            {
                                wlenddef += (uint)i.Status.Purger_Lv * 10;
                            }
                            if (res == AttackResult.Hit)
                                //damage = (int)(Math.Ceiling((float)(atk - i.Status.def_add) * (1f - (float)wlenddef / 100)));
                                //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。
                                damage = (int)(Math.Ceiling(atk * (1 / ((float)wlenddef / 50 + 1)) - i.Status.def_add));
                            else
                            {
                                atk = (int)(atk * 1.2f);
                                if (sActor.Status.cri_dmg_skill != 0)
                                    atk = (int)(atk * (1f + (float)(sActor.Status.cri_dmg_skill) / 100));
                                //damage = (int)(Math.Ceiling((float)(atk) * (1f - (float)wlenddef / 100)));
                                //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。暴击不无视右防。
                                damage = (int)(Math.Ceiling(atk * (1 / ((float)wlenddef / 50 + 1)) - i.Status.def_add));
                            }
                            //物理熟练测试数据
                            // MapClient mpc = MapClient.FromActorPC((ActorPC)sActor);

                            // string ooo = string.Format("物理熟练测试数据：enddef(实际防御):{0},damage(实际伤害):{1},mark(初伤害):{2},物理武器攻击熟练Lv:{3},i.Status.mdef(左防御):{4}", wlenddef, damage, atk, (uint)((ActorPC)sActor).Status.def_mastery_lv, i.Status.def);
                            // mpc.SendSystemMessage(ooo);
                        }
                        else if (defType == DefType.MDef)
                        {
                            if (sActor.type == ActorType.MOB && isBossMob(sActor) && Configuration.Instance.BossSlash)
                                wlenddef = i.Status.mdef - (i.Status.mdef * (uint)Configuration.Instance.BossSlashRate / 100);
                            else if (sActor.type == ActorType.PC && Configuration.Instance.AtkMastery)
                                //wlenddef = i.Status.mdef - (i.Status.mdef * ((uint)((ActorPC)sActor).DefLv) / 200);//熟练度削防
                                wlenddef = i.Status.mdef;
                            else
                                wlenddef = i.Status.mdef;
                            if (res == AttackResult.Hit)
                                //damage = (int)(Math.Ceiling((float)(atk - i.Status.mdef_add) * (1f - (float)wlendmdef / 100)));
                                //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。
                                damage = (int)(Math.Ceiling(atk * (1 / ((float)wlenddef / 50 + 1)) - i.Status.mdef_add));
                            else
                            {
                                atk = (int)(atk * 1.2f);
                                if (sActor.Status.cri_dmg_skill != 0)
                                    atk = (int)(atk * (1f + (float)(sActor.Status.cri_dmg_skill) / 100));
                                //damage = (int)(Math.Ceiling((float)(atk) * (1f - (float)wlendmdef / 100)));
                                //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。暴击不无视右防。
                                damage = (int)(Math.Ceiling(atk * (1 / ((float)wlenddef / 50 + 1)) - i.Status.mdef_add));
                            }
                        }
                        else if (defType == DefType.IgnoreLeft)
                        {
                            if (res == AttackResult.Hit)
                                damage = (int)(Math.Ceiling((float)(atk - i.Status.def_add)));
                            else
                            {
                                atk = (int)(atk * 1.2f);
                                if (sActor.Status.cri_dmg_skill != 0)
                                    atk = (int)(atk * (1f + (float)(sActor.Status.cri_dmg_skill) / 100));
                                damage = (int)(Math.Ceiling((float)(atk)));
                            }
                        }
                        else if (defType == DefType.IgnoreRight)
                        {
                            if (res == AttackResult.Hit)
                                //damage = (int)(Math.Ceiling((float)(atk) * (1f - (float)i.Status.def / 100)));
                                //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。
                                damage = (int)(Math.Ceiling(atk * (1 / ((float)wlenddef / 50 + 1))));
                            else
                            {
                                atk = (int)(atk * 1.2f);
                                if (sActor.Status.cri_dmg_skill != 0)
                                    atk = (int)(atk * (1f + (float)(sActor.Status.cri_dmg_skill) / 100));
                                //damage = (int)(Math.Ceiling((float)(atk) * (1f - (float)i.Status.def / 100)));
                                //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。
                                damage = (int)(Math.Ceiling(atk * (1 / ((float)wlenddef / 50 + 1))));
                            }
                        }
                        else if (defType == DefType.IgnoreAll)
                        {
                            damage = (int)atk;
                        }
                        if (damage > atk)
                            damage = 0;

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
                        damage -= (int)(stats.Vit / 3);

                        if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                        {
                            damage = (int)(damage * Configuration.Instance.PVPDamageRatePhysic);
                        }

                        if (target.type == ActorType.MOB && sActor.type == ActorType.PC)
                        {
                            ActorMob mob = (ActorMob)target;
                            Addition[] list = sActor.Status.Additions.Values.ToArray();
                            foreach (Addition add in list)
                            {
                                if (add.GetType() == typeof(Skill.Additions.Global.SomeTypeDamUp))
                                {
                                    Additions.Global.SomeTypeDamUp up = (Additions.Global.SomeTypeDamUp)add;
                                    if (up.MobTypes.ContainsKey(mob.BaseData.mobType))
                                    {
                                        damage += (int)(damage * ((float)up.MobTypes[mob.BaseData.mobType] / 100));
                                    }
                                }
                            }
                        }

                        if (damage <= 0) damage = 1;
                        if (i.Status.Additions.ContainsKey("エフィカス") && res == AttackResult.Critical)//3转刺客10级技能
                            damage += damage * (Global.Random.Next(0, 100) / 100);

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

                        bool ride = false;
                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Pet != null)
                                ride = pc.Pet.Ride;
                        }
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
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pcsActor = (ActorPC)sActor;
                            if (sActor.Status.Additions.ContainsKey("BurnRate") && SkillHandler.Instance.isEquipmentRight(pcsActor, SagaDB.Item.ItemType.CARD))//皇家贸易商
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
                        if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                        {
                            float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                            damage = (int)(damage * DamageUpRank);
                        }
                        if (target != null)//烈刃
                        {
                            if (sActor.Status.Additions.ContainsKey("HotBlade"))
                            {
                                target.HotBlade = 1;
                                Additions.Global.HotBlade HotBlade = new Additions.Global.HotBlade(null, target, 5000);
                                ApplyAddition(target, HotBlade);
                            }
                        }
                        //加伤处理下
                        if (target.Seals > 0)
                            damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                        if (sActor.Status.Additions.ContainsKey("ruthless") && (target.Buff.Stun || target.Buff.Stone || target.Buff.Frosen || target.Buff.Poison || target.Buff.Sleep || target.Buff.SpeedDown || target.Buff.Confused || target.Buff.Paralysis))
                            damage = (int)(damage * 1.6f);//无情打击
                        if(target.Status.Additions.ContainsKey("百鬼哭"))
                            damage = (int)(damage * 1.2f);//百鬼哭
                        if (target.HotBlade > 0)
                            damage = (int)(damage * (float)(1f + 0.03f * target.HotBlade));//烈刃
                        if (res == AttackResult.Critical)
                            damage = (int)(damage * sActor.CriDamageUP);//狂刃
                        //加伤处理上
                        //减伤处理下
                        if (target.Status.Additions.ContainsKey("铁壁"))
                            damage = (int)(damage * (float)0.4f);
                        if (target.Status.Additions.ContainsKey("EnergyShield"))//能量加护
                        {
                            if (target.type == ActorType.PC)
                                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["EnergyShieldlv"]));
                            else
                                damage = (int)(damage * (float)0.9f);
                        }
                        //减伤处理上
                        //吸血效果下
                        if (SuckBlood != 0)
                        {
                            if (sActor.type == ActorType.PC)
                                SkillHandler.Instance.Heal((ActorPC)sActor, (int)(damage * SuckBlood), 0, 0);
                        }
                        if (sActor.Status.Additions.ContainsKey("SuckBlood"))
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                if (((ActorPC)sActor).Skills.ContainsKey(941))//嗜血
                                {
                                    //if (SagaLib.Global.Random.Next(0, 1000) < 5 * ((ActorPC)sActor).Skills[941].Level)
                                    SkillHandler.Instance.Heal((ActorPC)sActor, (int)(damage * (0.005f + 0.005f * ((ActorPC)sActor).Skills[941].Level)), 0, 0);
                                }
                            }
                        }
                        //吸血效果上
                        if (target.HP != 0)
                        {
                            arg.hp[index + counter] = damage;
                            if (target.HP > damage)
                            {
                                //damage = (short)sActor.Status.min_atk1;
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                if (res == AttackResult.Critical)
                                    arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            }
                            else
                            {
                                damage = (int)target.HP;
                                if (!ride && !target.Buff.リボーン)
                                    arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                else
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                if (res == AttackResult.Critical)
                                    arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            }
                            //arg.flag[i] |=  AttackFlag.ATTACK_EFFECT;
                            if (target.HP != 0)
                                target.HP = (uint)(target.HP - damage);
                            if (target.HP == 0)
                            {
                                if (target.Status.Additions.ContainsKey("金刚"))
                                {
                                    Skill.Additions.Global.Vajay vajay = new Additions.Global.Vajay(arg.skill, target, 10000);
                                    target.HP = 1;
                                }
                                else if (target.Status.Additions.ContainsKey("Vajay"))
                                    target.HP = 1;
                            }

                            //处理反击
                            if (target.Status.Additions.ContainsKey("Counter"))
                            {
                                target.Status.Additions["Counter"].AdditionEnd();
                                SkillArg newArg = new SkillArg();
                                SkillHandler.Instance.Attack(target, sActor, newArg);
                                MapManager.Instance.GetMap(target.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, newArg, target, true);
                            }
                        }
                        else
                        {
                            if (!ride && !target.Buff.リボーン)
                                arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            else
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            if (res == AttackResult.Critical)
                                arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            arg.hp[index + counter] = damage;
                        }
                        if (i.type == ActorType.PC)
                        {
                            ActorPC pcs = (ActorPC)i;

                            if (i.Status.Additions.ContainsKey("剑斗士"))
                            {
                                if (Global.Random.Next(0, 100) >= 50)
                                    PhysicalAttack(sActor, i, arg, Elements.Neutral, 1.5f);
                            }

                            if (i.Status.absorb_hp > 0)
                            {
                                int heal = (int)(damage * i.Status.absorb_hp);
                                arg.affectedActors.Add(i);
                                arg.hp.Add(heal);
                                arg.sp.Add(0);
                                arg.mp.Add(0);
                                arg.flag.Add(AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE);
                                if (!i.Status.Additions.ContainsKey("Sacrifice") && damage < i.HP)
                                    i.HP += (uint)heal;
                                if (i.HP > i.MaxHP)
                                    i.HP = i.MaxHP;
                                Manager.MapManager.Instance.GetMap(i.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, i, true);
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

                            if (i.Status.Pressure_lv > 0)//3转剑35级技能
                            {
                                int level = i.Status.Pressure_lv;
                                float[] hprank = { 0, 0.2f, 0.2f, 0.25f, 0.25f, 0.3f };
                                if (i.HP < i.MaxHP * hprank[level])
                                {
                                    if (Global.Random.Next(0, 10) < level)
                                    {
                                        float[] rank = { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
                                        float[] rank2 = { 0, 0.1f, 0.1f, 0.15f, 0.15f, 0.2f };
                                        float factor = 3f + 0.3f * level;
                                        Map map2 = Manager.MapManager.Instance.GetMap(i.MapID);
                                        List<Actor> affected = map.GetActorsArea(i, 300, false);
                                        List<Actor> realAffected = new List<Actor>();
                                        foreach (Actor act in affected)
                                        {
                                            arg.argType = SkillArg.ArgType.Attack;
                                            arg.flag.Add(AttackFlag.HP_DAMAGE);
                                            arg.type = ATTACK_TYPE.BLOW;
                                            arg.hp.Add(damage);
                                            arg.sp.Add(0);
                                            arg.mp.Add(0);
                                            arg.affectedActors.Add(act);
                                            SkillHandler.Instance.PhysicalAttack(i, realAffected, arg, SagaLib.Elements.Neutral, factor);
                                            ShowEffect((ActorPC)i, act, 4002);
                                            ShowEffect((ActorPC)i, i, 4321);
                                        }
                                        if (Global.Random.Next(0, 100) < 100 * rank2[level])
                                        {
                                            foreach (Actor act in affected)
                                            {
                                                SkillHandler.Instance.PushBack(i, act, 4);
                                            }
                                        }

                                    }
                                }
                            }
                            if (i.Status.Additions.ContainsKey("Parry2") && i.Status.Parry_Lv != 0 && i.type == ActorType.PC)//3转骑士格挡
                            {
                                ActorPC pc = (ActorPC)i;
                                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) &&
    pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                                {
                                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
                pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                                    {
                                        float SutanOdds = i.Status.Parry_Lv * 0.05f;
                                        int SutanTime = 1000 + i.Status.Parry_Lv * 500;
                                        float[] ParryOdds = { 0, 0.15f, 0.25f, 0.35f, 0.65f, 0.75f };
                                        float[] ParryResult = { 0, 0.1f, 0.16f, 0.22f, 0.28f, 0.34f };
                                        SagaDB.Skill.Skill args = new SagaDB.Skill.Skill();
                                        if (Global.Random.Next(0, 100) < 100 * ParryOdds[i.Status.Parry_Lv])
                                        {
                                            damage = damage - (int)(damage * ParryOdds[i.Status.Parry_Lv]);
                                            if (SkillHandler.Instance.CanAdditionApply(i, sActor, SkillHandler.DefaultAdditions.Confuse, (int)(100 * SutanOdds)))
                                            {
                                                Additions.Global.硬直 skill = new SagaMap.Skill.Additions.Global.硬直(args, sActor, SutanTime);
                                                SkillHandler.ApplyAddition(sActor, skill);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (sActor.Status.Additions.ContainsKey("BloodLeech"))
                        {
                            Additions.Global.BloodLeech add = (Additions.Global.BloodLeech)sActor.Status.Additions["BloodLeech"];
                            int heal = (int)(damage * add.rate);
                            arg.affectedActors.Add(sActor);
                            arg.hp.Add(heal);
                            arg.sp.Add(0);
                            arg.mp.Add(0);
                            arg.flag.Add(AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE);
                            if (!sActor.Status.Additions.ContainsKey("Sacrifice"))
                                sActor.HP += (uint)heal;
                            if (sActor.HP > sActor.MaxHP)
                                sActor.HP = sActor.MaxHP;
                            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                        }
                    }
                }
                ApplyDamage(sActor, target, damage, doublehate);
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
            if (aspd > 960)
                aspd = 960;
            //2012.6.29日修正攻速为旧攻速的5/6。
            aspd = (short)(aspd * 5 / 6);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand)
                        arg.delay = 2400 - (uint)(2400 * aspd * 0.001f);
                    else
                        arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);
                }
                else
                    arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);
            }
            else
                arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);
            arg.delay = (uint)(arg.delay * arg.delayRate);
            if (sActor.Status.aspd_skill_perc != 0f)
                arg.delay = (uint)(arg.delay / sActor.Status.aspd_skill_perc);
            //physicalCounter--;
            return damage;
        }

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
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
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk, bool noReflect, float SuckBlood)
        {
            if (dActor.Count == 0) return 0;
            if (sActor.Status == null)
                return 0;
            //Logger.ShowDebug("Recurssion over 50 times!", Logger.defaultlogger);
            //Debug.Assert(magicalCounter < 50, "Recurssion over 50 times!");
            //magicalCounter++;
            if (sActor.Status.PlusElement_rate > 0)
                MATKBonus += sActor.Status.PlusElement_rate;

            if (sActor.type == ActorType.PC && Configuration.Instance.AtkMastery)
            {
                MapClient mpc2 = MapClient.FromActorPC((ActorPC)sActor);
                if (((ActorPC)sActor).Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (((ActorPC)sActor).MDefLv < 100)
                    {
                        ((ActorPC)sActor).MDefPoint += 1;
                        if (((ActorPC)sActor).MDefPoint >= ((((ActorPC)sActor).MDefLv * 4) * (((ActorPC)sActor).MDefPoint * 4)))
                        {
                            ((ActorPC)sActor).DefLv += 1;//熟练度升级
                            EffectArg effectarg = new EffectArg();
                            effectarg.actorID = ((ActorPC)sActor).ActorID;
                            effectarg.effectID = 9913;
                            ((ActorPC)sActor).MDefLv += 1;
                            string MFLV = string.Format("魔法攻击熟练度达到了Lv.{0}", ((ActorPC)sActor).MDefLv);
                            mpc2.SendSystemMessage(MFLV);
                        }
                    }
                }
            }

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
            mindamage = sActor.Status.min_matk;
            maxdamage = sActor.Status.max_matk;

            if (mindamage > maxdamage) maxdamage = mindamage;

            foreach (Actor i in dActor)
            {
                bool isPossession = false;
                bool isHost = false;
                bool isCri = false;
                if (sActor.type == ActorType.PC)
                {
                    if (((ActorPC)sActor).Skills.ContainsKey(601))//魔法暴击
                    {
                        if ((((ActorPC)sActor).Int * ((ActorPC)sActor).Skills[601].Level * 0.5) < SagaLib.Global.Random.Next(0, 100))
                            isCri = true;
                    }
                }
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
                        MagicAttack(sActor, possessionDamage, arg, element,elementValue, MATKBonus, oldcount);
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
                    MagicAttack(sActor, dst, arg, DefType.MDef, element,elementValue , MATKBonus, oldcount, false, true);
                    continue;
                }
                if (i.Status.reflex_odds > 0 && i.type == ActorType.PC)//3转骑士反射盾
                {
                    ActorPC pc = (ActorPC)i;
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) ||
    pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
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

                if (!setAtk)
                {
                    matk = Global.Random.Next(mindamage, maxdamage);
                    if (element != Elements.Neutral)
                    {
                        float eleBonus = CalcElementBonus(sActor, i, element, 50, MATKBonus < 0);
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
                uint mfenddef = 0;
                uint nfendmdef = 0;
                //uint matkup = 0;

                if (sActor.type == ActorType.MOB && isBossMob(sActor) && Configuration.Instance.BossSlash)//BOSS破防
                {
                    //mfenddef = i.Status.def - (i.Status.def * (uint)Configuration.Instance.BossSlashRate / 100);
                    //nfendmdef = i.Status.mdef - (i.Status.mdef * (uint)Configuration.Instance.BossSlashRate / 100);
                    mfenddef = i.Status.def;
                    nfendmdef = i.Status.mdef;

                }
                else if (sActor.type == ActorType.PC && Configuration.Instance.AtkMastery)
                {
                    //mfenddef = i.Status.def - (i.Status.def * ((uint)((ActorPC)sActor).MDefLv)) / 200;
                    //nfendmdef = i.Status.mdef - (i.Status.mdef * ((uint)((ActorPC)sActor).MDefLv)) / 200;
                    mfenddef = i.Status.def;
                    nfendmdef = i.Status.mdef;
                }
                else
                {
                    mfenddef = i.Status.def;
                    nfendmdef = i.Status.mdef;
                }
                //魔法伤害防御处理
                if (defType == DefType.MDef)
                    //damage = (int)(Math.Ceiling((float)(matk - i.Status.mdef_add) * (1f - (float)nfendmdef / 100)));
                    //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。
                    damage = (int)(Math.Ceiling(matk * (1 / ((float)nfendmdef / 50 + 1)) - i.Status.mdef_add));
                else if (defType == DefType.Def)
                    //damage = (int)(Math.Ceiling((float)(matk - i.Status.def_add) * (1f - (float)mfenddef / 100)));
                    //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。
                    damage = (int)(Math.Ceiling(matk * (1 / ((float)mfenddef / 50 + 1)) - i.Status.def_add));
                else if (defType == DefType.IgnoreLeft)
                    damage = (int)(Math.Ceiling((float)(matk - i.Status.mdef_add)));
                else if (defType == DefType.IgnoreRight)
                    //damage = (int)(Math.Ceiling((float)(matk) * (1f - (float)i.Status.mdef / 100)));
                    //2012.6.28修改左防减少伤害量算法，伤害比例为1/(左防/50+1)，左防为0是伤害比为1，左防50时伤害比例0.5，左防趋向无穷时伤害比例趋向0。右防在左防减免计算之后计算。
                    damage = (int)(Math.Ceiling(matk * (1 / ((float)nfendmdef / 50 + 1))));
                else
                    damage = (int)matk;
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
                    if (damage > 0)
                        damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                }

                if (target.type == ActorType.MOB && sActor.type == ActorType.PC && damage > 0)
                {
                    ActorMob mob = (ActorMob)target;
                    Addition[] list = sActor.Status.Additions.Values.ToArray();
                    foreach (Addition add in list)
                    {
                        if (add.GetType() == typeof(Skill.Additions.Global.SomeTypeDamUp))
                        {
                            Additions.Global.SomeTypeDamUp up = (Additions.Global.SomeTypeDamUp)add;
                            if (up.MobTypes.ContainsKey(mob.BaseData.mobType))
                            {
                                damage += (int)(damage * ((float)up.MobTypes[mob.BaseData.mobType] / 100));
                            }
                        }
                    }
                }
                if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                {
                    float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                    damage = (int)(damage * DamageUpRank);
                }

                if (damage < 0 && MATKBonus >= 0)
                    damage = 0;

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
                    if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.チャンプモンスターキラー状態)
                        damage = damage / 10;
                }

                if (sActor.type == ActorType.PC)
                {
                    int score = damage / 100;
                    if (score == 0 && damage != 0)
                        score = 1;
                    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, Math.Abs(score));
                }

                if (arg.argType == SkillArg.ArgType.Actor_Active)
                {
                    SkillHandler.Instance.Heal(target, -damage, 0, 0);
                    return 0;
                }

                if (target.HP != 0)
                {
                    if (damage < 0)
                        damage = damage / 2;
                    else
                    {
                        //加伤处理下
                        if (target.Seals > 0)
                            damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                        //加伤处理上
                        //减伤处理下
                        if (target.Status.Additions.ContainsKey("MagicShield"))//魔力加护
                        {
                            if (target.type == ActorType.PC)
                                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["MagicShieldlv"]));
                            else
                                damage = (int)(damage * (float)0.9f);
                        }
                        //减伤处理上
                        if (SuckBlood != 0 && damage !=0 )//吸血效果
                        {
                            if (sActor.type == ActorType.PC)
                                SkillHandler.Instance.Heal((ActorPC)sActor, (int)(damage * SuckBlood), 0, 0);
                        }
                        //吸血效果下
                        if (sActor.Status.Additions.ContainsKey("SuckBlood"))
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                if (((ActorPC)sActor).Skills.ContainsKey(941))//嗜血
                                {
                                    //if (SagaLib.Global.Random.Next(0, 1000) < 5 * ((ActorPC)sActor).Skills[941].Level)
                                    SkillHandler.Instance.Heal((ActorPC)sActor, (int)(damage * (0.005f + 0.005f * ((ActorPC)sActor).Skills[941].Level)), 0, 0);
                                }
                            }
                        }
                        //吸血效果上
                        if (isCri)//暴击
                        {
                            damage = damage * 2;
                            //arg.flag[index + counter] |= AttackFlag.CRITICAL;
                        }
                        damage = (int)(damage * (target.Status.Cardinal_Rank + 1f));//治疗率提升
                    }
                    arg.hp[index + counter] += damage;
                    if (damage >= 0)
                    {
                        if (target.HP > damage)
                        {
                            arg.flag[index + counter] = AttackFlag.HP_DAMAGE;
                        }
                        else
                        {
                            damage = (int)target.HP;
                            if (!ride && !target.Buff.リボーン)
                                arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            else
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                        }
                        if (isCri)//暴击
                        {
                            arg.flag[index + counter] |= AttackFlag.CRITICAL;
                        }
                    }
                    else
                    {
                        arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                    }
                    //arg.flag[i] |=  AttackFlag.ATTACK_EFFECT;

                    if (target.HP != 0)
                        target.HP = (uint)(target.HP - damage);
                    if (target.HP == 0)
                    {
                        if (target.Status.Additions.ContainsKey("金刚"))
                        {
                            Skill.Additions.Global.Vajay vajay = new Additions.Global.Vajay(arg.skill, target, 10000);
                            target.HP = 1;
                        }
                        else if (target.Status.Additions.ContainsKey("Vajay"))
                            target.HP = 1;
                    }
                    if (target.HP > target.MaxHP)
                        target.HP = target.MaxHP;
                }
                else
                {
                    arg.flag[index + counter] = AttackFlag.NO_DAMAGE;
                    arg.hp[index + counter] = 0;
                }
                if (i.Status.Additions.ContainsKey("見切り") && Global.Random.Next(0,100) < i.Status.Syaringan_rate)
                {
                    damage = 0;
                }
                
                ApplyDamage(sActor, target, damage);
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
                counter++;
                //return ;
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
        protected void ApplyDamage(Actor sActor, Actor dActor, int damage)
        {
            ApplyDamage(sActor, dActor, damage, false);
        }
        /// <summary>
        /// 对指定目标附加伤害
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="damage">伤害值</param>
        protected void ApplyDamage(Actor sActor, Actor dActor, int damage ,bool doublehate)
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
            if (dActor.type == ActorType.PC && damage > 0)
            {
                ArmorWorn((ActorPC)dActor);
            }
            if (dActor.type == ActorType.PC && dActor.Status.Additions.ContainsKey("AutoHeal"))//自动治疗
            {
                ActorPC pc = (ActorPC)dActor;
                if (pc.Skills3.ContainsKey(1109))
                {
                    if (pc.HP < dActor.MaxHP * pc.Skills3[1109].Level * 0.15 && pc.Skills.ContainsKey(3054))
                    {
                        SkillArg autoheal = new SkillArg();
                        SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3054, pc.Skills[3054].Level);
                        autoheal.sActor = pc.ActorID;
                        autoheal.dActor = pc.ActorID;
                        autoheal.skill = skill;
                        autoheal.argType = SkillArg.ArgType.Cast;
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).OnSkillCastComplete(autoheal);
                        //SkillCast(pc, pc, autoheal);
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
                    mob.AI.OnAttacked(attacker, damage);
                    if(doublehate)
                        mob.AI.OnAttacked(attacker, damage);
                }
                else
                {
                    ActorEventHandlers.PetEventHandler mob = (ActorEventHandlers.PetEventHandler)dActor.e;
                    mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage);
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
                    if (dActor.Status.Additions.ContainsKey("Meditatioon"))
                    {
                        dActor.Status.Additions["Meditatioon"].AdditionEnd();
                        dActor.Status.Additions.Remove("Meditatioon");
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
            //如果对象目标死亡
            if (dActor.HP == 0)
            {
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
                        ExperienceManager.Instance.ProcessMobExp(mob);
                    }
                    dActor.e.OnDie();
                }
            }
        }
    }
}
