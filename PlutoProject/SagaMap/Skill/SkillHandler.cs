using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Item;
using SagaMap.Network.Client;
using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaDB.Skill;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        /// <summary>
        /// 取得锁定的目标
        /// </summary>
        /// <param name="map"></param>
        /// <param name="actor"></param>
        /// <returns></returns>
        public Actor GetdActor(Actor sActor, SkillArg arg)
        {
            if (sActor.type != ActorType.PC) return null;
            Actor target = null;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            target = map.GetActor((uint)sActor.TInt["targetID"]);
            if (target == null || target.HP <= 0)
            {
                if (((ActorPC)sActor).CInt["自动锁定模式"] == 0)
                {
                    MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("没有锁定的目标，输入/autolock开启与关闭自动锁定模式。");
                    return null;
                }
                else
                {
                    List<Actor> actors = map.GetActorsArea(sActor, (short)((arg.skill.Range + 2) * 100), false);
                    List<Actor> Targets = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (Instance.CheckValidAttackTarget(sActor, item))
                            Targets.Add(item);
                    }
                    if (Targets.Count < 1)
                    {
                        MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("技能范围内没有目标可以锁定。");
                        return null;
                    }
                    else
                        target = Targets[0];
                }
            }
            if (arg.skill.Range + 2 < Math.Max(Math.Abs(sActor.X - target.X) / 100, Math.Abs(sActor.Y - target.Y) / 100))
            {
                MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("【你当前锁定的目标】超出了技能的极限范围。");
                return null;
            }

            if (target.HP <= 0)
            {
                MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("锁定的目标已死亡或已不存在。");
                return null;
            }
            ushort dir = map.CalcDir(sActor.X, sActor.Y, target.X, target.Y);
            map.MoveActor(Map.MOVE_TYPE.STOP, sActor, new short[2] { sActor.X, sActor.Y }, dir, sActor.Speed, true);

            return target;
        }
        public static void SendSystemMessage(Actor pc, string message)
        {
            if (pc.type == ActorType.PC)
                MapClient.FromActorPC((ActorPC)pc).SendSystemMessage(message);
        }
        /// <summary>
        /// 检查是否有攻击者对目标释放的DEBUFF
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">目标</param>
        ///  <param name="type">0仅物理   1仅魔法</param>
        public void checkdebuff(Actor sActor, Actor dActor, SkillArg arg, byte type)
        {
            if (type == 0)//物理
            {
                if (sActor.Status.Additions.ContainsKey("ApplyPoison"))
                {
                    if (Global.Random.Next(0, 100) < sActor.TInt["ApplyPoisonRate"])
                    {
                        float factor = 1f;
                        if (sActor.TInt["毒素研究提升"] != 0)
                            factor = 1f + sActor.TInt["毒素研究提升"] * 0.5f;
                        int damage = Instance.CalcDamage(false, sActor, dActor, arg, DefType.MDef, Elements.Holy, 50, factor);
                        Poison p = new Poison(arg.skill, dActor, damage);
                        ApplyAddition(dActor, p);
                        ShowEffectOnActor(dActor, 4126);
                    }
                }
            }
            else if (type == 2)//魔法
            {

            }
            //通用
        }

        /// <summary>
        /// 检查卡片对伤害的影响
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">目标</param>
        ///  <param name="type">0仅物理   1仅魔法</param>
        public int checkirisbuff(Actor sActor, Actor dActor, SkillArg arg, byte type, int damage)
        {
            if (damage > 0 && dActor.Status.heal_attacked_iris > 0 && dActor.HP != damage)//圣母的加护
            {
                if (dActor.Status.heal_attacked_iris * 1 >= Global.Random.Next(1, 100))
                {
                    uint heal = (uint)(dActor.MaxHP * 0.1f);
                    dActor.HP += heal;
                    if (dActor.HP > dActor.MaxHP) dActor.HP = dActor.MaxHP;
                    ShowVessel(dActor, (int)-heal);
                    ShowEffectOnActor(dActor, 4345);
                }
            }
            if (sActor.type != ActorType.PC) return damage;
            ActorPC spc = (ActorPC)sActor;
            if (type == 0)//物理
            {

            }
            else if (type == 2)//魔法
            {
                if (damage < 0 && sActor.Status.heal_70up_iris > 0 && sActor.MP > sActor.MaxMP * 0.7)//治愈之音
                    damage += (int)(damage * 0.05 * sActor.Status.heal_70up_iris);
            }
            if (damage > 0 && sActor.Status.atk_70up_iris > 0 && sActor.HP > sActor.MaxHP * 0.7)//全力以赴
                damage += (int)(damage * 0.01 * sActor.Status.atk_70up_iris);
            if (damage > 0 && sActor.Status.atkup_job40_iris > 0 && ((ActorPC)sActor).JobLevel3 < 40)//勤奋好学
                damage += (int)(damage * 0.01 * sActor.Status.atkup_job40_iris);
            if (damage > 0 && sActor.Status.spweap_atkup_iris > 0 && spc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))//玩具达人
            {
                ItemType it = spc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType;
                if (it == ItemType.EXSWORD || it == ItemType.EXGUN || it == ItemType.ETC_WEAPON)
                    damage += (int)(damage * 0.01 * sActor.Status.spweap_atkup_iris);
            }
            /*if(damage > 0 && spc.PlayerTitleID >=21 && spc.PlayerTitleID <= 23)
            {
                if (10 >= Global.Random.Next(1, 100) && !sActor.Status.Additions.ContainsKey("黑喵称号CD"))
                {
                    OtherAddition skill = new OtherAddition(null, sActor, "黑喵称号CD", 10000);
                    ApplyAddition(sActor, skill);
                    Seals(sActor, dActor, 1);
                    ShowEffectOnActor(dActor, 5270); ;
                }
            }
            if (damage > 0 && sActor.Status.Mammon_iris > 0)
            {
                if(!sActor.Status.Additions.ContainsKey("玛蒙之欲") && spc.TInt["玛蒙之欲未解放"] == 0)
                {
                    if (sActor.Status.Mammon_iris * 1 >= Global.Random.Next(1, 500))
                    {
                        OtherAddition skill = new OtherAddition(null, sActor, "玛蒙之欲", 10000);
                        skill.OnAdditionEnd += (s, e) =>
                        {
                            OtherAddition s2 = new OtherAddition(null, sActor, "玛蒙之欲解放", 5000);
                            s2.OnAdditionEnd += (t, y) => spc.TInt["玛蒙之欲未解放"] = 0;
                            ApplyAddition(sActor, s2);
                        };
                        ApplyAddition(sActor, skill);
                        spc.TInt["玛蒙之欲未解放"] = 1;
                        ShowEffectOnActor(sActor, 4469); ;
                    }
                }
                else if(sActor.Status.Additions.ContainsKey("玛蒙之欲解放"))
                {
                    RemoveAddition(sActor, "玛蒙之欲解放");
                    spc.TInt["玛蒙之欲未解放"] = 0;
                    int damagebouns = spc.TInt["玛蒙之欲伤害"] / 5;
                    damage += damagebouns;
                    ShowEffectOnActor(dActor, 5282);
                }
            }
            */
            //if (damage > dActor.HP) damage = (int)dActor.HP;
            return damage;
        }

        /// <summary>
        /// 检查各类BUFF对伤害的影响
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">目标</param>
        ///  <param name="type">0仅物理   1仅魔法</param>
        public int checkbuff(Actor sActor, Actor dActor, SkillArg arg, byte type, int damage)
        {
            try
            {
                int d = damage;
                d = (int)(d * sActor.Status.DamageRate);

                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.Pet != null)
                    {
                        if (pc.Pet.Ride)
                        {
                            if (damage > 0)
                                d = (int)(damage * 2f);
                            else
                                d = (int)(damage * 0.3);
                        }
                    }
                    if (pc.Fictitious)
                    {
                        ShowVessel(dActor, 0, damage, 0);
                        ShowEffectOnActor(dActor, 4174);
                        if (damage > 0)
                            d = -damage;
                        else
                            d = damage;
                    }
                }
                if (dActor.Status.Additions.ContainsKey("替身术") && damage > 0)
                {
                    if (dActor.TInt["替身术记录X"] != 0 && dActor.TInt["替身术记录Y"] != 0)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                        d = 0;
                        byte x = (byte)dActor.TInt["替身术记录X"];
                        byte y = (byte)dActor.TInt["替身术记录Y"];
                        RemoveAddition(dActor, "替身术");
                        dActor.TInt["替身术记录X"] = 0;
                        dActor.TInt["替身术记录X"] = 0;
                        short px = Global.PosX8to16(x, map.Width);
                        short py = Global.PosY8to16(y, map.Height);
                        byte x1 = Global.PosX16to8(dActor.X, map.Width);
                        byte y1 = Global.PosY16to8(dActor.Y, map.Height);
                        ShowEffect(map, dActor, x1, y1, 4251);
                        Invisible inv = new Invisible(null, dActor, 10000);
                        ApplyAddition(sActor, inv);
                        map.TeleportActor(dActor, px, py);

                    }
                }
                if (dActor.Status.Additions.ContainsKey("ShieldReflect") &&
                    !(dActor.Status.Additions.ContainsKey("ShieldReflect") && sActor.Status.Additions.ContainsKey("ShieldReflect")) &&
                    sActor.type == ActorType.PC)//盾牌反射
                {
                    ShowEffectByActor(dActor, 5092);
                    //sActor.EP += 500;
                    CauseDamage(dActor, sActor, damage);
                    ShowVessel(sActor, damage);
                    d = 0;
                }
                if (sActor.Status.Additions.ContainsKey("ApplyPoison"))
                {
                    float fac = sActor.TInt["PoisonDamageUP"] / 100f;
                    if (dActor.Status.Additions.ContainsKey("Poison1"))
                        fac *= 1.5f;
                    int dp = CalcDamage(false, sActor, dActor, null, DefType.MDef, Elements.Dark, 50, fac);
                    d += dp;
                    Instance.ShowEffectOnActor(dActor, 8048);
                }
                if (sActor.Buff.魂之手)
                    d = damage * 2;
                if (dActor.Buff.魂之手)
                {
                    if (damage > 0)
                        d = damage * 3;
                    else
                        d = (int)(damage * 0.2f);
                }
                return d;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return 0;
            }

        }

        /// <summary>
        /// 对目标造成伤害（该函数统筹了CalcDamage() CauseDamage() ShowVessel()）
        /// </summary>
        /// <param name="IsPhyDamage">是否為物理傷害 1是 2不是</param>
        /// <param name="sActor">攻擊者</param>
        /// <param name="dActor">目標</param>
        /// <param name="arg"></param>
        /// <param name="defType">防禦類型</param>
        /// <param name="element">元素</param>
        /// <param name="eleValue">元素值</param>
        /// <param name="ATKBonus">倍率</param>
        /// <param name="ignore">無視防禦率</param>
        /// 
        public void DoDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, float ignore = 0)
        {
            DoDamage(IsPhyDamage, sActor, dActor, arg, defType, element, eleValue, ATKBonus, 0, 0, ignore = 0);
        }
        public void DoDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, int scribonus, int cridamagebonusfloat, float ignore = 0)
        {
            try
            {
                AttackResult res = AttackResult.Hit;
                int damage = CalcDamage(IsPhyDamage, sActor, dActor, arg, defType, element, eleValue, ATKBonus, out res, scribonus, cridamagebonusfloat, ignore);
                CauseDamage(sActor, dActor, damage);
                ShowVessel(dActor, damage, 0, 0, res);
                SkillHandler.RemoveAddition(sActor, "Relement");
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        /// <summary>
        /// 計算傷害（實際不造成傷害！！）
        /// </summary>
        /// <param name="IsPhyDamage">是否為物理傷害 1是 2不是</param>
        /// <param name="sActor">攻擊者</param>
        /// <param name="dActor">目標</param>
        /// <param name="arg"></param>
        /// <param name="defType">防禦類型</param>
        /// <param name="element">元素</param>
        /// <param name="eleValue">元素值</param>
        /// <param name="ATKBonus">倍率</param>
        /// <param name="ignore">無視防禦率</param>
        /// <returns>傷害</returns>
        public int CalcDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, float ignore = 0)
        {
            AttackResult res = AttackResult.Hit;
            return CalcDamage(IsPhyDamage, sActor, dActor, arg, defType, element, eleValue, ATKBonus, out res, 0, 0, ignore = 0);
        }
        public int CalcDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, int scribonus, int cridamagebonus, float ignore = 0)
        {
            AttackResult res = AttackResult.Hit;
            return CalcDamage(IsPhyDamage, sActor, dActor, arg, defType, element, eleValue, ATKBonus, out res, scribonus, cridamagebonus, ignore);
        }
        /// <summary>
        /// 計算傷害（實際不造成傷害！！）
        /// </summary>
        /// <param name="IsPhyDamage">是否為物理傷害 1是 2不是</param>
        /// <param name="sActor">攻擊者</param>
        /// <param name="dActor">目標</param>
        /// <param name="arg"></param>
        /// <param name="defType">防禦類型</param>
        /// <param name="element">元素</param>
        /// <param name="eleValue">元素值</param>
        /// <param name="ATKBonus">倍率</param>
        /// <param name="ignore">無視防禦率</param>
        /// <returns>傷害</returns>
        public int CalcDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, out AttackResult res, int scribonus, int cridamagebonus, float ignore = 0)
        {
            try
            {
                int damage = 0;
                int atk;
                int mindamage = 0;
                int maxdamage = 0;

                res = CalcAttackResult(sActor, dActor, sActor.Range > 3, 0, 0);
                if (dActor.Status.Additions.ContainsKey("Warn"))//警戒
                {
                    if (res == AttackResult.Critical)
                    {
                        res = AttackResult.Hit;
                    }
                }
                if (res == AttackResult.Critical)
                {
                    damage = (int)(((float)damage) * (float)(CalcCritBonus(sActor, dActor, 0)));
                    if (sActor.Status.Additions.ContainsKey("CriDamUp"))
                    {
                        float rate = (float)((float)(sActor.Status.Additions["CriDamUp"] as DefaultPassiveSkill).Variable["CriDamUp"] / 100.0f + 1.0f);
                        damage = (int)((float)damage * rate);
                    }
                }

                if (IsPhyDamage)
                {
                    if (arg == null)
                    {
                        mindamage = sActor.Status.min_atk1;
                        maxdamage = sActor.Status.max_atk1;
                    }
                    else
                    {
                        //獲取攻擊力
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
                    }
                    if (mindamage > maxdamage) maxdamage = mindamage;
                    //atk = (short)Global.Random.Next(mindamage, maxdamage);
                    //atk = (short)(atk * CalcElementBonus(sActor, dActor, element, 0, false) * ATKBonus);
                    atk = Global.Random.Next(mindamage, maxdamage);
                    //TODO: element bonus, range bonus
                    float eleBonus = CalcElementBonus(sActor, dActor, element, 0, false);

                    if (dActor.Status.Contract_Lv != 0)
                    {
                        Elements tmpele = Elements.Neutral;
                        switch (dActor.Status.Contract_Lv)
                        {
                            case 1:
                                tmpele = Elements.Fire;
                                break;
                            case 2:
                                tmpele = Elements.Water;
                                break;
                            case 3:
                                tmpele = Elements.Earth;
                                break;
                            case 4:
                                tmpele = Elements.Wind;
                                break;
                        }
                        if (tmpele == element)
                            eleBonus -= 0.15f;
                        else
                            eleBonus += 1.0f;

                    }
                    atk = (int)(Math.Ceiling(atk * eleBonus * ATKBonus));
                    damage = CalcPhyDamage(sActor, dActor, defType, atk, ignore);

                    damage = CheckBuffForDamage(sActor, dActor, damage);
                    if (sActor.Status.Additions.ContainsKey("FrameHart"))//火心
                    {
                        int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
                        damage = (int)((double)damage * (double)((double)rate / 100.0f));
                    }
                    if (sActor.Status.Additions.ContainsKey("ホークアイ"))//HAW站桩
                    {
                        damage = (int)(damage * ((sActor.Status.Additions["ホークアイ"] as DefaultBuff).Variable["ホークアイ"] / 100.0f));
                    }

                    if (sActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)sActor;
                        if (pc.Skills2_1.ContainsKey(310) || pc.DualJobSkill.Exists(x => x.ID == 310))//HAW2-1追魂箭
                        {
                            var duallv = 0;
                            if (pc.DualJobSkill.Exists(x => x.ID == 310))
                                duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 310).Level;

                            var mainlv = 0;
                            if (pc.Skills2_1.ContainsKey(310))
                                mainlv = pc.Skills2_1[310].Level;

                            int level = Math.Max(duallv, mainlv);
                            if (dActor.Buff.Stun ||
                                   dActor.Buff.Stone ||
                                   dActor.Buff.Frosen ||
                                   dActor.Buff.Poison ||
                                   dActor.Buff.Sleep ||
                                   dActor.Buff.SpeedDown ||
                                   dActor.Buff.Confused ||
                                   dActor.Buff.Paralysis ||
                                   dActor.Buff.STRDown ||
                                   dActor.Buff.VITDown ||
                                   dActor.Buff.INTDown ||
                                   dActor.Buff.DEXDown ||
                                   dActor.Buff.AGIDown ||
                                   dActor.Buff.MAGDown ||
                                   dActor.Buff.MaxHPDown ||
                                   dActor.Buff.MaxMPDown ||
                                   dActor.Buff.MaxSPDown ||
                                   dActor.Buff.MinAtkDown ||
                                   dActor.Buff.MaxAtkDown ||
                                   dActor.Buff.MinMagicAtkDown ||
                                   dActor.Buff.MaxMagicAtkDown ||
                                   dActor.Buff.DefDown ||
                                   dActor.Buff.DefRateDown ||
                                   dActor.Buff.MagicDefDown ||
                                   dActor.Buff.MagicDefRateDown ||
                                   dActor.Buff.ShortHitDown ||
                                   dActor.Buff.LongHitDown ||
                                   dActor.Buff.MagicHitDown ||
                                   dActor.Buff.ShortDodgeDown ||
                                   dActor.Buff.LongDodgeDown ||
                                   dActor.Buff.MagicAvoidDown ||
                                   dActor.Buff.CriticalRateDown ||
                                   dActor.Buff.CriticalDodgeDown ||
                                   dActor.Buff.HPRegenDown ||
                                   dActor.Buff.MPRegenDown ||
                                   dActor.Buff.SPRegenDown ||
                                   dActor.Buff.AttackSpeedDown ||
                                   dActor.Buff.CastSpeedDown ||
                                   dActor.Buff.SpeedDown ||
                                   dActor.Buff.Berserker)
                            {
                                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                                {
                                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                                    {
                                        damage = (int)(damage * (1.1f + 0.02f * level));
                                    }
                                }
                            }
                        }
                        if (pc.Skills2_2.ContainsKey(314) || pc.DualJobSkill.Exists(x => x.ID == 314))//GU2-2追魂刃
                        {
                            var duallv = 0;
                            if (pc.DualJobSkill.Exists(x => x.ID == 314))
                                duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 314).Level;

                            var mainlv = 0;
                            if (pc.Skills2_2.ContainsKey(314))
                                mainlv = pc.Skills2_2[314].Level;

                            int level = Math.Max(duallv, mainlv);
                            if (dActor.Buff.Stun ||
                                   dActor.Buff.Stone ||
                                   dActor.Buff.Frosen ||
                                   dActor.Buff.Poison ||
                                   dActor.Buff.Sleep ||
                                   dActor.Buff.SpeedDown ||
                                   dActor.Buff.Confused ||
                                   dActor.Buff.Paralysis ||
                                   dActor.Buff.STRDown ||
                                   dActor.Buff.VITDown ||
                                   dActor.Buff.INTDown ||
                                   dActor.Buff.DEXDown ||
                                   dActor.Buff.AGIDown ||
                                   dActor.Buff.MAGDown ||
                                   dActor.Buff.MaxHPDown ||
                                   dActor.Buff.MaxMPDown ||
                                   dActor.Buff.MaxSPDown ||
                                   dActor.Buff.MinAtkDown ||
                                   dActor.Buff.MaxAtkDown ||
                                   dActor.Buff.MinMagicAtkDown ||
                                   dActor.Buff.MaxMagicAtkDown ||
                                   dActor.Buff.DefDown ||
                                   dActor.Buff.DefRateDown ||
                                   dActor.Buff.MagicDefDown ||
                                   dActor.Buff.MagicDefRateDown ||
                                   dActor.Buff.ShortHitDown ||
                                   dActor.Buff.LongHitDown ||
                                   dActor.Buff.MagicHitDown ||
                                   dActor.Buff.ShortDodgeDown ||
                                   dActor.Buff.LongDodgeDown ||
                                   dActor.Buff.MagicAvoidDown ||
                                   dActor.Buff.CriticalRateDown ||
                                   dActor.Buff.CriticalDodgeDown ||
                                   dActor.Buff.HPRegenDown ||
                                   dActor.Buff.MPRegenDown ||
                                   dActor.Buff.SPRegenDown ||
                                   dActor.Buff.AttackSpeedDown ||
                                   dActor.Buff.CastSpeedDown ||
                                   dActor.Buff.SpeedDown ||
                                   dActor.Buff.Berserker)
                            {
                                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                                {
                                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD ||
                                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD ||
                                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER)
                                    {
                                        damage = (int)(damage * (1.1f + 0.02f * level));
                                    }
                                }
                            }
                        }
                    }
                    if (dActor.Status.NeutralDamegeDown_rate > 0 && element == Elements.Neutral)
                    {
                        damage = (int)(damage * (1.0f - (dActor.Status.NeutralDamegeDown_rate / 100.0f)));

                    }
                    if (dActor.Status.NeutralDamegeDown_rate > 0 && element != Elements.Neutral)
                    {
                        damage = (int)(damage * (1.0f - (dActor.Status.ElementDamegeDown_rate / 100.0f)));

                    }
                    if (arg.skill != null)
                    {
                        if (sActor.Status.doubleUpList.Contains((ushort)arg.skill.ID))
                        {
                            atk *= 2;
                        }
                    }
                    //if (damage > atk)
                    //    damage = atk;
                    if (arg == null)
                    {
                        damage = (int)(damage * (1f - sActor.Status.damage_atk1_discount));
                    }
                    else
                    {
                        switch (arg.type)
                        {
                            case ATTACK_TYPE.BLOW:
                                damage = (int)(damage * (1f - sActor.Status.damage_atk1_discount));
                                break;
                            case ATTACK_TYPE.SLASH:
                                damage = (int)(damage * (1f - sActor.Status.damage_atk2_discount));
                                break;
                            case ATTACK_TYPE.STAB:
                                damage = (int)(damage * (1f - sActor.Status.damage_atk3_discount));
                                break;
                        }
                    }
                    if (sActor.type == ActorType.PC && dActor.type == ActorType.PC)
                        damage = (int)(damage * Configuration.Instance.PVPDamageRatePhysic);
                    if (damage <= 0) damage = 1;



                    //计算暴击增益
                    if (scribonus != 0)
                    {
                        if (res == AttackResult.Critical)
                        {
                            damage = (int)(((float)damage) * (float)(CalcCritBonus(sActor, dActor, scribonus) + (float)cridamagebonus));
                            if (sActor.Status.Additions.ContainsKey("CriDamUp"))
                            {
                                float rate = (float)((float)(sActor.Status.Additions["CriDamUp"] as DefaultPassiveSkill).Variable["CriDamUp"] / 100.0f + 1.0f);
                                damage = (int)((float)damage * rate);
                            }
                        }
                    }

                    checkdebuff(sActor, dActor, arg, 0);
                }
                else
                {
                    mindamage = sActor.Status.min_matk;
                    maxdamage = sActor.Status.max_matk;
                    if (mindamage > maxdamage) maxdamage = mindamage;
                    atk = Global.Random.Next(mindamage, maxdamage);
                    if (sActor.type == ActorType.PC)
                    {
                        //wiz3转JOB3,属性对无属性魔法增伤
                        ActorPC pci = sActor as ActorPC;
                        float rates = 0;
                        //不管是主职还是副职, 只要习得技能,则进入增伤判定
                        if ((pci.Skills3.ContainsKey(986) || pci.DualJobSkill.Exists(x => x.ID == 986)) && element == Elements.Neutral)
                        {
                            //这里取副职的等级
                            var duallv = 0;
                            if (pci.DualJobSkill.Exists(x => x.ID == 986))
                                duallv = pci.DualJobSkill.FirstOrDefault(x => x.ID == 986).Level;

                            //这里取主职的等级
                            var mainlv = 0;
                            if (pci.Skills3.ContainsKey(986))
                                mainlv = pci.Skills3[986].Level;
                            rates = 0.02f + 0.002f * mainlv;
                            //int elements = (int)pci.WeaponElement[pci.WeaponElement];
                            int elements = pci.Status.attackElements_item[pci.WeaponElement]
                                + pci.Status.attackElements_skill[pci.WeaponElement]
                                + pci.Status.attackelements_iris[pci.WeaponElement];

                            //int elements = pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Dark] +
                            //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Earth] +
                            //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Fire] +
                            //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Holy] +
                            //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Neutral] +
                            //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Water] +
                            //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Wind];

                            if (elements > 0)
                            {
                                ATKBonus += rates * elements;
                            }

                        }
                    }
                    if (element != Elements.Neutral)
                    {
                        float eleBonuss = CalcElementBonus(sActor, dActor, element, 1, ((ATKBonus < 0) && !((dActor.Status.undead == true) && (element == Elements.Holy))));
                        if (sActor.Status.Contract_Lv != 0)//CAJOB40
                        {
                            Elements tmpele = Elements.Neutral;
                            switch (sActor.Status.Contract_Lv)
                            {
                                case 1:
                                    tmpele = Elements.Fire;
                                    break;
                                case 2:
                                    tmpele = Elements.Water;
                                    break;
                                case 3:
                                    tmpele = Elements.Earth;
                                    break;
                                case 4:
                                    tmpele = Elements.Wind;
                                    break;
                            }
                            if (tmpele == element)
                                eleBonuss += 0.5f;
                            else
                                eleBonuss -= 0.65f;

                        }
                        if (dActor.Status.Contract_Lv != 0)
                        {
                            Elements tmpele = Elements.Neutral;
                            switch (dActor.Status.Contract_Lv)
                            {
                                case 1:
                                    tmpele = Elements.Fire;
                                    break;
                                case 2:
                                    tmpele = Elements.Water;
                                    break;
                                case 3:
                                    tmpele = Elements.Earth;
                                    break;
                                case 4:
                                    tmpele = Elements.Wind;
                                    break;
                            }
                            if (tmpele == element)
                                eleBonuss -= 0.15f;
                            else
                                eleBonuss += 1.0f;

                        }
                        atk = (int)(atk * eleBonuss * ATKBonus);
                    }
                    else
                        atk = (int)(atk * 1f * ATKBonus);

                    damage = CalcMagDamage(sActor, dActor, defType, atk, ignore);

                    damage = CheckBuffForDamage(sActor, dActor, damage);
                    if (dActor.Status.NeutralDamegeDown_rate > 0 && element == Elements.Neutral)
                    {
                        damage = (int)(damage * (1.0f - (dActor.Status.NeutralDamegeDown_rate / 100.0f)));

                    }
                    if (dActor.Status.NeutralDamegeDown_rate > 0 && element != Elements.Neutral)
                    {
                        damage = (int)(damage * (1.0f - (dActor.Status.ElementDamegeDown_rate / 100.0f)));

                    }
                    if (sActor.type == ActorType.PC && dActor.type == ActorType.PC)
                    {
                        if (damage > 0)
                            damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                    }
                    if (dActor.Status.Additions.ContainsKey("BradStigma"))
                    {
                        int rate = (dActor.Status.Additions["BradStigma"] as DefaultBuff).Variable["BradStigma"];
                        //MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("你的血印技能，使你的暗屬攻擊加成(" + rate + "%)。");
                        damage = (int)((double)damage * (double)((double)rate / 100.0f));
                    }







                    if ((res == AttackResult.Critical || res == AttackResult.Hit) && sActor.Status.Additions.ContainsKey("WithinWeeks") && sActor.type == ActorType.PC)
                    {
                        ActorPC thispc = (ActorPC)sActor;
                        int level = thispc.CInt["WithinWeeksLevel"];
                        switch (thispc.CInt["WithinWeeksLevel"])
                        {
                            case 1:
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Silence, 5))
                                {
                                    Additions.Global.Silence skill = new SagaMap.Skill.Additions.Global.Silence(arg.skill, dActor, (int)(750 + 250 * level));
                                    SkillHandler.ApplyAddition(dActor, skill);
                                }
                                break;
                            case 2:
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.CannotMove, 5))
                                {
                                    Additions.Global.CannotMove skill = new SagaMap.Skill.Additions.Global.CannotMove(arg.skill, dActor, 1000);
                                    SkillHandler.ApplyAddition(dActor, skill);
                                }
                                break;
                            case 3:
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stiff, 5))
                                {
                                    Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(arg.skill, dActor, 1000);
                                    SkillHandler.ApplyAddition(dActor, skill);
                                }
                                break;
                            case 4:
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Confuse, 5))
                                {
                                    Additions.Global.Confuse skill = new SagaMap.Skill.Additions.Global.Confuse(arg.skill, dActor, 3000);
                                    SkillHandler.ApplyAddition(dActor, skill);
                                }
                                break;
                            case 5:
                                if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, 10 * level))
                                {
                                    Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(arg.skill, dActor, 2000);
                                    SkillHandler.ApplyAddition(dActor, skill);
                                }
                                break;
                        }
                    }



                    checkdebuff(sActor, dActor, arg, 1);
                }

                if(sActor.type==ActorType.PC)
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

                //友情的一击
                if (sActor.Status.Additions.ContainsKey("BlowOfFriendship"))
                {
                    damage = (int)(damage * 1.15f);
                }

                //竜眼放大
                if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
                {
                    int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                    damage = (int)((double)damage * (double)((double)rate / 100.0f));
                }
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.Party != null && pc.Status.pt_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.pt_dmg_up_iris / 100.0f));
                    }
                    if (pc.Status.Element_down_iris < 100 && element != Elements.Neutral)
                    {
                        damage = (int)(damage * (float)(pc.Status.Element_down_iris / 100.0f));
                    }

                    //iris卡种族减伤部分
                    if (sActor.Race == Race.HUMAN && pc.Status.human_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.human_dmg_down_iris / 100.0f));
                    }

                    else if (sActor.Race == Race.BIRD && pc.Status.bird_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.bird_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.ANIMAL && pc.Status.animal_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.animal_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.magic_c_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.PLANT && pc.Status.plant_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.plant_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.water_a_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.MACHINE && pc.Status.machine_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.machine_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.ROCK && pc.Status.rock_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.rock_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.ELEMENT && pc.Status.element_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.element_dmg_down_iris / 100.0f));
                    }
                    else if (sActor.Race == Race.UNDEAD && pc.Status.undead_dmg_down_iris < 100)
                    {
                        damage = (int)(damage * (float)(pc.Status.undead_dmg_down_iris / 100.0f));
                    }
                }

                    if (res == AttackResult.Miss)//取消MISS
                {
                    damage = (int)(damage * 0.6f);
                    res = AttackResult.Hit;
                }
                if ((res == AttackResult.Avoid && IsPhyDamage) || res == AttackResult.Guard) //res == AttackResult.Miss || 
                    damage = 0;
                return damage;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                res = AttackResult.Miss;
                return 0;
            }
        }
        public void ChangdeWeapons(Actor sActor, byte type)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (sActor.Status.Additions.ContainsKey("自由射击"))
                    RemoveAddition(sActor, "自由射击");
                if (sActor.Status.Additions.ContainsKey("弓术专注提升"))
                    RemoveAddition(sActor, "弓术专注提升");

                PC.StatusFactory.Instance.CalcRange(pc);
                if (pc.TInt["斥候远程模式"] == type) return;
                pc.TInt["斥候远程模式"] = type;
                uint sp = pc.SP;
                PC.StatusFactory.Instance.CalcStatus(pc);

                MapClient.FromActorPC(pc).SendStatusExtend();
                MapClient.FromActorPC(pc).SendRange();
                Instance.CastPassiveSkills(pc);
                pc.SP = sp;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_EQUIP, null, pc, true);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
            }
        }
        /// <summary>
        /// 實現傷害（沒有任何視覺特效！）
        /// </summary>
        /// <param name="sActor">攻擊者</param>
        /// <param name="dActor">目標者</param>
        /// <param name="damage">傷害</param>
        public void CauseDamage(Actor sActor, Actor dActor, int damage, bool ignoreShield = false)
        {
            if (dActor.HP < 1)
            {
                return;
            }
            damage = checkbuff(sActor, dActor, null, 3, damage);


            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                if (pc.Status.Additions.ContainsKey("HolyVolition"))//7月16日更新的光之意志BUFF
                {
                    dActor.HP = 1;
                    ShowEffectOnActor(pc, 4173);
                    damage = 0;
                }
                if (damage > dActor.HP && pc.TInt["副本复活标记"] == 4 && pc.TInt["单人复活次数"] > 0)
                {
                    pc.TInt["单人复活次数"] -= 1;
                    dActor.HP = dActor.MaxHP;
                    dActor.MP = dActor.MaxMP;
                    dActor.SP = dActor.MaxSP;
                    List<Actor> actors = GetActorsAreaWhoCanBeAttackedTargets(dActor, 300);
                    foreach (var item in actors)
                    {
                        if (CheckValidAttackTarget(dActor, item))
                        {
                            PushBack(dActor, item, 3);
                            ShowEffectOnActor(item, 5275);
                            if (!item.Status.Additions.ContainsKey("Stun"))
                            {
                                Stun stun = new Stun(null, item, 3000);
                                ApplyAddition(item, stun);
                            }
                        }
                    }
                    ShowEffectOnActor(pc, 4243);
                    damage = 0;
                    SendSystemMessage(pc, "你被使用了一次复活机会！剩余次数：" + pc.TInt["单人复活次数"].ToString());

                    CastPassiveSkills(pc);//重新计算被动

                    /*if (!pc.Tasks.ContainsKey("Recover"))//自然恢复
                    {
                        Tasks.PC.Recover reg = new Tasks.PC.Recover(MapClient.FromActorPC(pc));
                        pc.Tasks.Add("Recover", reg);
                        reg.Activate();
                    }*/

                    if (!pc.Status.Additions.ContainsKey("HolyVolition"))
                    {
                        DefaultBuff skill = new DefaultBuff(null, pc, "HolyVolition", 2000);
                        ApplyAddition(pc, skill);
                    }
                }
            }

            if (damage > dActor.HP)
                dActor.HP = 0;
            else
                dActor.HP = (uint)(dActor.HP - damage);

            if (dActor.HP > dActor.MaxHP)
                dActor.HP = dActor.MaxHP;



            //if (dActor.type == ActorType.PC && dActor.HP < 1)
            //ClientManager.EnterCriticalArea();
            ApplyDamage(sActor, dActor, damage);

            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
            //MapClient.FromActorPC((ActorPC)sActor).SendPartyMemberHPMPSP((ActorPC)sActor);

            //ClientManager.LeaveCriticalArea();
        }
        /// <summary>
        /// 搜索道具欄前20個，尋找可裝備的武器
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="requestType">可行的武器類型</param>
        /// <returns>bool</returns>
        public bool CheckWeapon(ActorPC pc, List<ItemType> requestType)
        {
            return false;//暂时去掉
            try
            {
                if (requestType.Count > 0)
                {
                    for (int y = 0; y < requestType.Count; y++)
                    {
                        Item item = null;
                        for (int i = 0; i < 20; i++)
                        {
                            if (pc.Inventory.Items[ContainerType.BODY][i].BaseData.itemType == requestType[y])
                            {
                                item = pc.Inventory.Items[ContainerType.BODY][i];
                                MapClient client = (MapClient.FromActorPC(pc));
                                if (client.CheckEquipRequirement(item) == 0)
                                {
                                    if (item.EquipSlot.Contains(EnumEquipSlot.LEFT_HAND) && item.EquipSlot.Contains(EnumEquipSlot.RIGHT_HAND)
                && item.EquipSlot.Count == 1
                && pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND)
                && !pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand)
                                        client.OnItemEquipt(item.Slot, 15);
                                    else client.OnItemEquipt(item.Slot, 0);
                                    EffectArg arg = new EffectArg();
                                    arg.effectID = 4177;
                                    arg.actorID = pc.ActorID;
                                    client.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, pc, true);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { SagaLib.Logger.ShowError(ex); return false; }
            return false;
        }

        public void CancelSkillCast(Actor actor)
        {

            //if(actor.type == ActorType.MOB) return;
            //if (Global.Random.Next(0, 100) < 33) 
            /*if (actor.type == ActorType.PC)
            {
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSkillDummy();
            }
            else
            {*/
            if (actor.Tasks.ContainsKey("SkillCast") && actor.TInt["CanNotInterrupted"] != 1)
            {
                if (actor.Tasks["SkillCast"].getActivated())
                {
                    if ((actor.Tasks["SkillCast"].NextUpdateTime - DateTime.Now).TotalMilliseconds > 200)
                    {
                        actor.Tasks["SkillCast"].Deactivate();
                        actor.Tasks.Remove("SkillCast");
                    }
                }
                /*SkillArg arg = new SkillArg();
                arg.sActor = actor.ActorID;
                arg.dActor = 0;
                arg.skill = SkillFactory.Instance.GetSkill(3311, 1);
                arg.x = 0;
                arg.y = 0;
                arg.hp = new List<int>();
                arg.sp = new List<int>();
                arg.mp = new List<int>();
                arg.hp.Add(0);
                arg.sp.Add(0);
                arg.mp.Add(0);
                arg.flag.Add(AttackFlag.NONE);
                //arg.affectedActors.Add(this.Character);
                arg.argType = SkillArg.ArgType.Active;*/
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL_CANCEL, null, actor, true);
                //}
            }
        }

        public void SendAttackMessage(byte type, Actor sActor, string Sender, string Content)
        {
            if (sActor.type == ActorType.PC)
            {
                Packets.Server.SSMG_CHAT_JOB p = new Packets.Server.SSMG_CHAT_JOB();
                p.Type = type;
                p.Sender = Sender;
                p.Content = Content;
                MapClient.FromActorPC((ActorPC)sActor).netIO.SendPacket(p);
            }
        }
        /// <summary>
        /// 附加圣印
        /// </summary>
        /// <param name="dActor">目标</param>
        public void Seals(Actor sActor, Actor dActor)
        {
            Seals(sActor, dActor, 1);
        }
        public void Seals(Actor sActor, Actor dActor, byte count)
        {
            if (sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).PossessionTarget != 0)//凭依时无效
                    return;
            }
            if (sActor.Status.Additions.ContainsKey("EvilSoul"))
            {
                return;
            }
            if (dActor != null)
            {
                if (sActor.Status.Additions.ContainsKey("Seals"))
                {
                    EffectArg arg = new EffectArg();
                    arg.effectID = 4238;
                    arg.actorID = dActor.ActorID;
                    if (sActor.type == ActorType.PC)
                        SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)sActor).map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);
                    dActor.IsSeals = 1;
                    for (int i = 0; i < count; i++)
                    {
                        Additions.Global.Seals Seals = new Additions.Global.Seals(null, dActor, 15000);
                        ApplyAddition(dActor, Seals);
                    }
                }
            }
        }

        /// <summary>
        /// 让Actor说话
        /// </summary>
        /// <param name="actor">说话者</param>
        /// <param name="message">内容</param>
        public void ActorSpeak(Actor actor, string message)
        {
            Activator2 s = new Activator2(actor, message, 500);
            s.Activate();
        }
        public void ActorSpeak(Actor actor, string message, int duetime)
        {
            Activator2 s = new Activator2(actor, message, duetime);
            s.Activate();
        }
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            string message;

            public Activator2(Actor caster, string message, int duetime)
            {
                this.caster = caster;
                this.message = message;
                dueTime = duetime;
            }
            public override void CallBack()
            {
                ChatArg arg = new ChatArg();
                arg.content = message;
                if (caster.type == ActorType.PC)
                    Manager.MapManager.Instance.GetMap(caster.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, caster, true);
                else
                    Manager.MapManager.Instance.GetMap(caster.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, caster, false);
                this.Deactivate();
            }
        }
        /// <summary>
        /// 让actor跳数值
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="hp">血量，正值为伤害，负值为恢复</param>
        /// <param name="mp">法力，正值为伤害，负值为恢复</param>
        /// <param name="sp">SP，正值为伤害，负值为恢复</param>
        public void ShowVessel(Actor actor, int hp = 0, int mp = 0, int sp = 0, AttackResult res = AttackResult.Hit)
        {
            bool tome = true;
            SkillArg arg = new SkillArg();
            arg.affectedActors.Add(actor);
            arg.Init();
            arg.sActor = actor.ActorID;
            arg.argType = SkillArg.ArgType.Item_Active;
            Item item0 = ItemFactory.Instance.GetItem(10000000);
            arg.item = item0;
            arg.hp[0] = hp;
            arg.mp[0] = mp;
            arg.sp[0] = sp;

            if (actor.HP == 0)
            {
                arg.flag[0] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                arg.argType = SkillArg.ArgType.Attack;
            }
            else if (hp > 0)
                arg.flag[0] |= AttackFlag.HP_DAMAGE;
            else if (hp < 0)
            {
                arg.item = ItemFactory.Instance.GetItem(10000000);
                arg.flag[0] |= AttackFlag.HP_HEAL;
                arg.argType = SkillArg.ArgType.Item_Active;
            }

            /*if (res == AttackResult.Critical)
            {
                arg.flag[0] |= AttackFlag.CRITICAL;
                arg.argType = SkillArg.ArgType.Attack;
            }
            if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard)
            {
                if (res == AttackResult.Miss)
                    arg.flag[0] = AttackFlag.MISS;
                else if (res == AttackResult.Avoid)
                    arg.flag[0] = AttackFlag.AVOID;
                else
                    arg.flag[0] = AttackFlag.GUARD;
                arg.argType = SkillArg.ArgType.Attack;
            }*/

            if (mp > 0)
                arg.flag[0] |= AttackFlag.MP_DAMAGE;
            else if (mp < 0)
                arg.flag[0] |= AttackFlag.MP_HEAL;
            if (sp > 0)
                arg.flag[0] |= AttackFlag.SP_DAMAGE;
            else if (sp < 0)
                arg.flag[0] |= AttackFlag.SP_HEAL;
            if (actor.HP == 0)
            {
                if (actor.type == ActorType.PC)
                {
                    arg.argType = SkillArg.ArgType.Item_Active;
                    actor.e.OnActorSkillUse(actor, arg);
                    tome = false;
                    arg.argType = SkillArg.ArgType.Attack;
                }
                if (actor.Status.Additions.ContainsKey("HolyVolition") && hp > 0)
                    arg.flag[0] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
            }


            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, actor, tome);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg, actor, true);
        }

        /// <summary>
        /// 武器装备破损
        /// </summary>
        /// <param name="pc">玩家</param>
        public void WeaponWorn(ActorPC pc)
        {
            if (!pc.Status.Additions.ContainsKey("DurDownCancel"))//试运行“防护保养”-2261
            {
                return;
            }
            uint rate = 2;
            if (pc.Status.Additions.ContainsKey("fish"))
                rate = 60;
            if (Global.Random.Next(0, 6000) < rate)
            {
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].PossessionedActor != null)
                        return;
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].maxDurability == 0)
                        return;
                    EffectArg arg = new EffectArg();
                    MapClient client = MapClient.FromActorPC(pc);
                    if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability <= 0 || pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability > pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].maxDurability)
                    {
                        client.SendSystemMessage("武器[" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.name + "]损毁！");
                        Packets.Server.SSMG_ITEM_DELETE p2;
                        p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot;
                        client.netIO.SendPacket(p2);
                        Item oriItem = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        client.ItemMoveSub(oriItem, ContainerType.BODY, oriItem.Stack);
                        if (oriItem.BaseData.repairItem == 0)
                            client.DeleteItem(pc.Inventory.LastItem.Slot, pc.Inventory.LastItem.Stack, true);
                        return;
                    }
                    arg.actorID = client.Character.ActorID;
                    arg.effectID = 8044;
                    client.Character.e.OnShowEffect(client.Character, arg);
                    pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability -= 1;
                    client.SendSystemMessage("武器[" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.name + "]耐久度下降！(" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability +
                      "/" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].maxDurability + ")");
                    client.SendItemInfo(pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
                }
            }
        }

        /// <summary>
        /// 防具装备破损
        /// </summary>
        /// <param name="pc">玩家</param>
        public void ArmorWorn(ActorPC pc)
        {
            if (!pc.Status.Additions.ContainsKey("DurDownCancel"))//试运行“防护保养”-2261
            {
                return;
            }
            if (Global.Random.Next(0, 3500) < 2)
            {
                EffectArg arg = new EffectArg();
                MapClient client = MapClient.FromActorPC(pc);
                EnumEquipSlot ArmorEnum = new EnumEquipSlot();
                switch (Global.Random.Next(1, 11))
                {
                    case 1:
                        ArmorEnum = EnumEquipSlot.BACK;
                        break;
                    case 2:
                        ArmorEnum = EnumEquipSlot.CHEST_ACCE;
                        break;
                    case 3:
                        ArmorEnum = EnumEquipSlot.FACE;
                        break;
                    case 4:
                        ArmorEnum = EnumEquipSlot.FACE_ACCE;
                        break;
                    case 5:
                        ArmorEnum = EnumEquipSlot.HEAD;
                        break;
                    case 6:
                        ArmorEnum = EnumEquipSlot.HEAD_ACCE;
                        break;
                    case 7:
                        ArmorEnum = EnumEquipSlot.LEFT_HAND;
                        break;
                    case 8:
                        ArmorEnum = EnumEquipSlot.LOWER_BODY;
                        break;
                    case 9:
                        ArmorEnum = EnumEquipSlot.SHOES;
                        break;
                    case 10:
                        ArmorEnum = EnumEquipSlot.SOCKS;
                        break;
                    case 11:
                        ArmorEnum = EnumEquipSlot.UPPER_BODY;
                        break;
                }
                if (pc.Inventory.Equipments.ContainsKey(ArmorEnum))
                {
                    if (pc.Inventory.Equipments[ArmorEnum].PossessionedActor != null)
                        return;
                    if (pc.Inventory.Equipments[ArmorEnum].maxDurability == 0)
                        return;
                    if (pc.Inventory.Equipments[ArmorEnum].Durability <= 0 || pc.Inventory.Equipments[ArmorEnum].Durability > pc.Inventory.Equipments[ArmorEnum].maxDurability)
                    {
                        client.SendSystemMessage("装备[" + pc.Inventory.Equipments[ArmorEnum].BaseData.name + "]损毁！");
                        Packets.Server.SSMG_ITEM_DELETE p2;
                        p2 = new SagaMap.Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = pc.Inventory.Equipments[ArmorEnum].Slot;
                        client.netIO.SendPacket(p2);
                        pc.Inventory.Equipments.Remove(ArmorEnum);
                        client.SendItems();
                        client.SendEquip();
                        return;
                    }
                    arg.actorID = client.Character.ActorID;
                    arg.effectID = 8044;
                    client.Character.e.OnShowEffect(client.Character, arg);
                    pc.Inventory.Equipments[ArmorEnum].Durability -= 1;
                    client.SendSystemMessage("装备[" + pc.Inventory.Equipments[ArmorEnum].BaseData.name + "]耐久度下降！(" + pc.Inventory.Equipments[ArmorEnum].Durability +
                      "/" + pc.Inventory.Equipments[ArmorEnum].maxDurability + ")");
                    client.SendItemInfo(pc.Inventory.Equipments[ArmorEnum]);
                }
            }
        }

        /// <summary>
        /// 特定装备耐久下降1
        /// </summary>
        /// <param name="pc"></param>
        public void EquipWorn(ActorPC pc, Item wornequip)
        {
            MapClient client = MapClient.FromActorPC(pc);
            if (client.Character.Account.GMLevel > 200) return;
            EffectArg arg = new EffectArg();
            if (wornequip.Durability < 2)
            {
                wornequip.Durability = 0;
                client.SendSystemMessage("装备[" + wornequip.BaseData.name + "]损坏！");
                client.OnItemMove(wornequip.Slot, ContainerType.BODY, wornequip.Stack, false);
            }
            else
            {
                wornequip.Durability--;
                client.SendSystemMessage("装备[" + wornequip.BaseData.name + "]耐久度下降！(" + wornequip.Durability + "/" + wornequip.maxDurability + ")");
            }
            //client.SendItems();
            client.SendEquip();
            arg.actorID = client.Character.ActorID;
            arg.effectID = 8044;
            client.Character.e.OnShowEffect(client.Character, arg);
            client.SendItemInfo(wornequip);
        }
        /// <summary>
        /// 随机装备耐久下降1
        /// </summary>
        /// <param name="pc"></param>
        public void RandomEquipWorn(ActorPC pc)
        {
            if (pc == null) return;
            List<Item> equips = new List<Item>();
            MapClient client = MapClient.FromActorPC(pc);
            EffectArg arg = new EffectArg();
            if (Global.Random.Next(0, 100) < 80 && (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY) || pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND)))
            {
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                    equips.Add(pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY]);
                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    equips.Add(pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND]);
            }
            else
            {
                foreach (Item i in pc.Inventory.Equipments.Values)
                {
                    if (i.Stack == 0)
                        continue;
                    equips.Add(i);
                }
            }
            if (equips.Count < 1) return;
            Item wornequip = equips[Global.Random.Next(0, (equips.Count - 1))];
            client.SendSystemMessage("装备[" + wornequip.BaseData.name + "]耐久度下降！");
            if (wornequip.Durability < 2)
            {
                wornequip.Durability = 0;
                client.SendSystemMessage("装备[" + wornequip.BaseData.name + "]损坏！");
                client.OnItemMove(wornequip.Slot, ContainerType.BODY, wornequip.Stack, false);
            }
            else
            {
                wornequip.Durability--;
                client.SendSystemMessage("装备[" + wornequip.BaseData.name + "]耐久度下降！(" + wornequip.Durability + "/" + wornequip.maxDurability + ")");
            }
            //client.SendItems();
            client.SendEquip();
            arg.actorID = client.Character.ActorID;
            arg.effectID = 8044;
            client.Character.e.OnShowEffect(client.Character, arg);
            client.SendItemInfo(wornequip);
        }
        public void Attack(Actor sActor, Actor dActor, SkillArg arg)
        {
            Attack(sActor, dActor, arg, 1f);
        }
        public void Attack(Actor sActor, Actor dActor, SkillArg arg, float factor)
        {
            if (!CheckStatusCanBeAttact(sActor, 1))
            {
                if (sActor.type == ActorType.PC)
                {
                    MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("無法行動的狀態。");
                }
                return;

            }
            int combo = GetComboCount(sActor);

            arg.sActor = sActor.ActorID;
            arg.dActor = dActor.ActorID;
            for (int i = 0; i < combo; i++)
            {
                arg.affectedActors.Add(dActor);
            }
            arg.type = sActor.Status.attackType;
            arg.delayRate = 1f + ((float)combo / 2);
            PhysicalAttack(sActor, arg.affectedActors, arg, sActor.WeaponElement, factor);
        }
        public void CriAttack(Actor sActor, Actor dActor, SkillArg arg)
        {

        }
        public int TryCast(Actor sActor, Actor dActor, SkillArg arg)
        {
            if (skillHandlers.ContainsKey(arg.skill.ID))
            {
                if (!CheckStatusCanBeAttact(sActor, 2))
                {
                    if (sActor.type == ActorType.PC)
                        MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("無法行動的狀態。");

                    return 0;
                }

                if (sActor.type == ActorType.PC)
                {
                    //if (dActor == null &&
                    //    arg.skill.ID != 3434//福音
                    //    )
                    //{
                    //    return 0;
                    //}
                    //Calc Direction Before Cast..
                    //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    //map.MoveActor(Map.MOVE_TYPE.STOP, sActor, new short[2] { sActor.X, sActor.Y }, sActor.Dir, sActor.Speed);

                    //Cancel Cloaking Skill
                    //if(dActor!=null)
                    //{
                    //    ActorPC spc = (ActorPC)sActor;

                    //    if (spc.PossessionTarget != 0)
                    //    {

                    //        Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    //        Actor TargetPossessionActor = map.GetActor(spc.PossessionTarget);

                    //        if (TargetPossessionActor.Status.Additions.ContainsKey("Cloaking"))
                    //            RemoveAddition(TargetPossessionActor, "Cloaking");

                    //    }
                    //    if (dActor.Status.Additions.ContainsKey("Cloaking"))
                    //        RemoveAddition(dActor, "Cloaking");

                    //    if (sActor.Status.Additions.ContainsKey("Cloaking"))
                    //        RemoveAddition(sActor, "Cloaking");
                    //}



                    return skillHandlers[arg.skill.ID].TryCast((ActorPC)sActor, dActor, arg);
                }
                else
                    return 0;
            }
            else
                return 0;
        }


        public void SetNextComboSkill(Actor actor, uint id)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                Network.Client.MapClient.FromActorPC(pc).nextCombo.Add(id);
            }
        }

        public void SkillCast(Actor sActor, Actor dActor, SkillArg arg)
        {
            arg.sActor = sActor.ActorID;
            if (arg.dActor != 0xFFFFFFFF)
                arg.dActor = dActor.ActorID;




            if (skillHandlers.ContainsKey(arg.skill.ID))
            {
                skillHandlers[arg.skill.ID].Proc(sActor, dActor, arg, arg.skill.Level);
                if (arg.affectedActors.Count == 0 && arg.dActor != arg.sActor && arg.dActor != 0 && arg.dActor != 0xffffffff)
                {


                    arg.affectedActors.Add(dActor);
                    arg.Init();
                }
            }
            else if (MobskillHandlers.ContainsKey(arg.skill.ID))
            {
                MobskillHandlers[arg.skill.ID].Proc(sActor, dActor, arg, arg.skill.Level);
                if (arg.affectedActors.Count == 0 && arg.dActor != arg.sActor && arg.dActor != 0 && arg.dActor != 0xffffffff)
                {
                    if (!CheckStatusCanBeAttact(sActor, 3))
                    {
                        return;
                    }
                    arg.affectedActors.Add(dActor);
                    arg.Init();
                }
            }
            else
            {
                arg.affectedActors.Add(dActor);
                arg.Init();
                Logger.ShowWarning("No defination for skill:" + arg.skill.Name + "(ID:" + arg.skill.ID + ")", null);
            }
        }

        private byte GetComboCount(Actor actor)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                byte combo = 1;

                if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    Item item = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    switch (item.BaseData.itemType)
                    {
                        case ItemType.DUALGUN:
                        case ItemType.CLAW:
                            combo = 2;
                            break;
                        default:
                            combo = 1;
                            break;
                    }
                }
                else
                    combo = 1;

                if (Global.Random.Next(0, 99) < actor.Status.combo_rate_skill)
                    combo = (byte)actor.Status.combo_skill;
                return combo;
            }
            else
                return 1;
        }

        public void CastPassiveSkills(ActorPC pc, bool ReCalcState = true)
        {
            //PC.StatusFactory.Instance.CalcStatusOnSkillEffect(pc);
            List<string> list = pc.Status.Additions.Keys.ToList();
            foreach (string i in list)
            {
                if (pc.Status.Additions[i].GetType() == typeof(Skill.Additions.Global.DefaultPassiveSkill))
                {
                    RemoveAddition(pc, pc.Status.Additions[i]);
                }
            }

            foreach (SagaDB.Skill.Skill i in pc.Skills.Values)
            {
                if (i.BaseData.active == false)
                {
                    if (skillHandlers.ContainsKey(i.ID))
                    {
                        SkillArg arg = new SkillArg();
                        arg.skill = i;
                        skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                    }
                }
            }

            if (!pc.Rebirth || pc.Job != pc.Job3)
            {
                foreach (SagaDB.Skill.Skill i in pc.Skills2.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }

                foreach (SagaDB.Skill.Skill i in pc.SkillsReserve.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
            }
            else
            {
                foreach (SagaDB.Skill.Skill i in pc.Skills2_1.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
                foreach (SagaDB.Skill.Skill i in pc.Skills2_2.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
                foreach (SagaDB.Skill.Skill i in pc.Skills3.Values)
                {
                    if (i.BaseData.active == false)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
                foreach (SagaDB.Skill.Skill i in pc.DualJobSkill)
                {
                    if (!i.BaseData.active)
                    {
                        if (skillHandlers.ContainsKey(i.ID))
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = i;
                            skillHandlers[i.ID].Proc(pc, pc, arg, i.Level);
                        }
                    }
                }
            }
            if (ReCalcState)
                PC.StatusFactory.Instance.CalcStatusOnSkillEffect(pc);
        }

        public void CheckBuffValid(ActorPC pc)
        {
            List<string> list = pc.Status.Additions.Keys.ToList();
            foreach (string i in list)
            {
                if (i == null)
                    continue;
                if (pc.Status.Additions[i].GetType() == typeof(Skill.Additions.Global.DefaultBuff))
                {
                    Additions.Global.DefaultBuff buff = (SagaMap.Skill.Additions.Global.DefaultBuff)pc.Status.Additions[i];
                    int result;
                    if (buff.OnCheckValid != null)
                    {
                        buff.OnCheckValid(pc, pc, out result);
                        if (result != 0)
                        {
                            RemoveAddition(pc, pc.Status.Additions[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Apply a addition to an actor
        /// </summary>
        /// <param name="actor">Actor which the addition should be applied to</param>
        /// <param name="addition">Addition to be applied</param>
        public static void ApplyAddition(Actor actor, Addition addition)
        {
            if (!addition.Enabled) return;
            if (actor.type == ActorType.PC)
            {
                if (((ActorPC)actor).Fictitious) return;
            }
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                //return;
                Addition oldaddition = actor.Status.Additions[addition.Name];
                if (oldaddition.MyType == Addition.AdditionType.Buff || oldaddition.MyType == Addition.AdditionType.Debuff)
                {
                    DefaultBuff oldbuff = (DefaultBuff)oldaddition;
                    DefaultBuff newbuff = (DefaultBuff)addition;
                    if (oldbuff.Variable.ContainsKey(addition.Name) || newbuff.Variable.ContainsKey(addition.Name))
                    {
                        if (oldbuff.Variable[addition.Name] == newbuff.Variable[addition.Name])
                        {
                            oldbuff.TotalLifeTime += addition.TotalLifeTime;
                            return;
                        }
                    }
                }
                //if (oldaddition.MyType == Addition.AdditionType.Debuff)
                //{
                //    DefaultDeBuff oldbuff = (DefaultDeBuff)oldaddition;
                //    DefaultDeBuff newbuff = (DefaultDeBuff)addition;
                //    if (oldbuff.Variable.ContainsKey(addition.Name) || newbuff.Variable.ContainsKey(addition.Name))
                //    {
                //        if (oldbuff.Variable[addition.Name] == newbuff.Variable[addition.Name])
                //        {
                //            oldbuff.TotalLifeTime += addition.TotalLifeTime;
                //            return;
                //        }
                //    }
                //}
                if (oldaddition.Activated)
                    oldaddition.AdditionEnd();
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                bool blocked = ClientManager.Blocked;
                if (!blocked)
                    ClientManager.EnterCriticalArea();

                actor.Status.Additions.Remove(addition.Name);
                actor.Status.Additions.Add(addition.Name, addition);

                if (!blocked)
                    ClientManager.LeaveCriticalArea();
            }
            else
            {
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                /*bool blocked = ClientManager.Blocked;
                if (!blocked)*/
                ClientManager.EnterCriticalArea();
                if (!actor.Status.Additions.ContainsKey(addition.Name))
                    actor.Status.Additions.Add(addition.Name, addition);

                //if (!blocked)
                ClientManager.LeaveCriticalArea();
            }
        }

        public static void RemoveAddition(Actor actor, string name)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            if (actor.Status.Additions.ContainsKey(name))
                RemoveAddition(actor, actor.Status.Additions[name]);
            if (!blocked)
                ClientManager.LeaveCriticalArea();
        }

        public static void RemoveAddition(Actor actor, string name, bool removeOnly)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            if (actor.Status.Additions.ContainsKey(name))
                RemoveAddition(actor, actor.Status.Additions[name], true);
            if (!blocked)
                ClientManager.LeaveCriticalArea();
        }

        public static void RemoveAddition(Actor actor, Addition addition)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            RemoveAddition(actor, addition, false);
            if (!blocked)
                ClientManager.LeaveCriticalArea();
        }

        public static void RemoveAddition(Actor actor, Addition addition, bool removeOnly)
        {
            if (actor.Status == null)
                return;
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                actor.Status.Additions.Remove(addition.Name);
                if (addition.Activated && !removeOnly)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        /// <summary>
        /// 击退函数
        /// </summary>
        /// <param name="ori">击退发动者</param>
        /// <param name="dest">被击退者</param>
        /// <param name="step">击退距离</param>
        public void PushBack(Actor ori, Actor dest, int step)
        {
            if (!dest.Status.Additions.ContainsKey("FortressCircleSEQ") &&
               !dest.Status.Additions.ContainsKey("SolidBody"))
            {
                PushBack(ori, dest, step, 3000);
            }

        }
        public void PushBack(Actor ori, Actor dest, int step, ushort speed, MoveType moveType = MoveType.RUN)
        {
            Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
            if (dest.type == ActorType.MOB)
            {
                SagaMap.ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)dest.e;
                if (eh.AI.Mode.Symbol || eh.AI.Mode.SymbolTrash)
                    return;
            }
            byte x = SagaLib.Global.PosX16to8(dest.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(dest.Y, map.Height);
            int deltaX = x - SagaLib.Global.PosX16to8(ori.X, map.Width);
            int deltaY = y - SagaLib.Global.PosY16to8(ori.Y, map.Height);
            while (deltaX == 0 && deltaY == 0)
            {
                deltaX = SagaLib.Global.Random.Next(-1, 1);
                deltaY = SagaLib.Global.Random.Next(-1, 1);
            }
            if (deltaX != 0)
                deltaX /= Math.Abs(deltaX);
            if (deltaY != 0)
                deltaY /= Math.Abs(deltaY);
            for (int i = 0; i < step; i++)
            {
                x = (byte)(x + deltaX);
                y = (byte)(y + deltaY);
                if (x >= map.Width || y >= map.Height || map.Info.walkable[x, y] != 2)
                {
                    x = (byte)(x - deltaX);
                    y = (byte)(y - deltaY);
                    break;
                }
            }
            short[] pos = new short[2];
            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
            if (moveType != MoveType.RUN)
                map.MoveActor(Map.MOVE_TYPE.START, dest, pos, speed, speed, true, moveType);
            else
                map.MoveActor(Map.MOVE_TYPE.START, dest, pos, speed, speed, true);
            if (dest.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)dest.e;
                mob.AI.OnPathInterupt();
            }
            if (dest.type == ActorType.PET || dest.type == ActorType.SHADOW)
            {
                ActorEventHandlers.PetEventHandler mob = (ActorEventHandlers.PetEventHandler)dest.e;
                mob.AI.OnPathInterupt();
            }
        }

        public void JumpBack(Actor ori, int step, ushort speed, MoveType moveType = MoveType.RUN)
        {
            Map map = Manager.MapManager.Instance.GetMap(ori.MapID);
            byte OutX, OutY;
            SkillHandler.Instance.GetTFrontPos(map, ori, out OutX, out OutY);
            byte x = SagaLib.Global.PosX16to8(ori.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(ori.Y, map.Height);
            int deltaX = x - OutX;
            int deltaY = y - OutY;
            if (deltaX != 0)
                deltaX /= Math.Abs(deltaX);
            if (deltaY != 0)
                deltaY /= Math.Abs(deltaY);
            for (int i = 0; i < step; i++)
            {
                x = (byte)(x + deltaX);
                y = (byte)(y + deltaY);
                if (x >= map.Width || y >= map.Height || map.Info.walkable[x, y] != 2)
                {
                    x = (byte)(x - deltaX);
                    y = (byte)(y - deltaY);
                    break;
                }
            }
            short[] pos = new short[2];
            pos[0] = SagaLib.Global.PosX8to16(x, map.Width);
            pos[1] = SagaLib.Global.PosY8to16(y, map.Height);
            if (moveType != MoveType.RUN)
                map.MoveActor(Map.MOVE_TYPE.START, ori, pos, speed, speed, true, moveType);
            else
                map.MoveActor(Map.MOVE_TYPE.START, ori, pos, speed, speed, true);

        }
        /// <summary>
        /// 检查技能是否符合装备条件
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool CheckSkillCanCastForWeapon(ActorPC pc, SkillArg arg)
        {
            if (arg.skill.BaseData.equipFlag.Value == 0)
                return true;
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                if (arg.skill.BaseData.equipFlag.Test((EquipFlags)Enum.Parse(typeof(EquipFlags), pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType.ToString())))
                    return true;
            }
            else if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
            {
                if (arg.skill.BaseData.equipFlag.Test((EquipFlags)Enum.Parse(typeof(EquipFlags), pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType.ToString())))
                    return true;
            }
            else if (arg.skill.BaseData.equipFlag.Test(EquipFlags.HAND))
                return true;
            List<ItemType> its = new List<ItemType>();
            Type flags = typeof(EquipFlags);
            foreach (EquipFlags item in Enum.GetValues(flags))
            {
                if (arg.skill.BaseData.equipFlag.Test(item) && Enum.IsDefined(typeof(ItemType), item.ToString()))
                    its.Add((ItemType)Enum.Parse(typeof(ItemType), item.ToString()));
            }
            if (its.Count > 0)
            {
                if (arg.dActor != 0)
                {
                    Actor dActor = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID).GetActor(arg.dActor);
                    if (dActor == null)
                        return false;
                    int range = Math.Max(Math.Abs(pc.X - dActor.X) / 100, Math.Abs(pc.Y - dActor.Y) / 100);
                    if (arg.skill.Range >= range)
                    {
                        if (CheckWeapon(pc, its)) return true;
                    }
                    else return false;
                }
                else if (CheckWeapon(pc, its)) return true;

            }

            return false;
        }
        /// <summary>
        /// 返回范围内可被攻击的对象
        /// </summary>
        /// <param name="caster">实际攻击者</param>
        /// <param name="actor">计算范围的实体</param>
        /// <param name="range">范围</param>
        /// <returns>可被攻击的对象</returns>

        public List<Actor> GetActorsAreaWhoCanBeAttackedTargets(Actor caster, Actor actor, short range)
        {
            List<Actor> actors = new List<Actor>();
            Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
            return GetVaildAttackTarget(caster, map.GetActorsArea(actor, range, false));
        }
        /// <summary>
        /// 返回范围内可被攻击的对象
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="range">范围</param>
        /// <returns>可被攻击的对象</returns>
        public List<Actor> GetActorsAreaWhoCanBeAttackedTargets(Actor sActor, short range)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            return GetVaildAttackTarget(sActor, map.GetActorsArea(sActor, range, false));
        }
        /// <summary>
        /// 返回可攻击的actors
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActors">被攻击者们</param>
        /// <returns>可攻击的actors</returns>
        public List<Actor> GetVaildAttackTarget(Actor sActor, List<Actor> dActors)
        {
            if (dActors.Count < 1) return dActors;
            List<Actor> actors = new List<Actor>();
            foreach (var item in dActors)
            {
                if (CheckValidAttackTarget(sActor, item))
                {
                    actors.Add(item);
                }
            }
            return actors;
        }
        /// <summary>
        /// 检查是施放者能否施放技能或功擊
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="type">0=魔法功擊,1=物理功擊,2=技能施放</param>
        /// <returns></returns>
        public bool CheckStatusCanBeAttact(Actor sActor, int type)
        {
            switch (type)
            {
                case 0:
                    //Type 0 = Magic
                    //Slienced Confused Frozen Sleep stone stun paralyse
                    if (
                 sActor.Status.Additions.ContainsKey("Silence") ||
                 sActor.Status.Additions.ContainsKey("Confused") ||
                 sActor.Status.Additions.ContainsKey("Frosen") ||
                 sActor.Status.Additions.ContainsKey("Stone") ||
                 sActor.Status.Additions.ContainsKey("Stun") ||
                 sActor.Status.Additions.ContainsKey("Sleep") ||
                 sActor.Status.Additions.ContainsKey("Paralyse") ||
                 sActor.Status.Additions.ContainsKey("SkillForbid")
                 )
                        return false;
                    break;
                case 1://Type 1 == Phy
                       //Confused Frozen Sleep stone stun paralyse +斷腕
                    if (
                            sActor.Status.Additions.ContainsKey("Confused") ||
                            sActor.Status.Additions.ContainsKey("Frosen") ||
                            sActor.Status.Additions.ContainsKey("Stone") ||
                            sActor.Status.Additions.ContainsKey("Stun") ||
                            sActor.Status.Additions.ContainsKey("Sleep") ||
                            sActor.Status.Additions.ContainsKey("Paralyse")
                        )
                        return false;
                    break;
                case 2:
                    //檢查能否施放
                    //Slienced Confused Frozen Sleep stone stun paralyse

                    if (
                        sActor.Status.Additions.ContainsKey("Silence") ||
                        sActor.Status.Additions.ContainsKey("Confused") ||
                        sActor.Status.Additions.ContainsKey("Frosen") ||
                        sActor.Status.Additions.ContainsKey("Stone") ||
                        sActor.Status.Additions.ContainsKey("Stun") ||
                        sActor.Status.Additions.ContainsKey("Sleep") ||
                        sActor.Status.Additions.ContainsKey("Paralyse") ||
                        sActor.Status.Additions.ContainsKey("SkillForbid")
                        )
                        return false;

                    break;
            }

            return true;


        }

        /// <summary>
        /// 检查是弓和箭是否通过条件
        /// </summary>
        /// <param name="pc">攻击者</param>
        /// <param name="number">消耗箭矢数量</param>
        /// <returns></returns>
        public int CheckPcBowAndArrow(ActorPC pc, int number = 1)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                {
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                            {
                                return 0;
                            }
                            else
                            {
                                return -55;
                            }
                        }
                        else
                            return -34;
                    }
                    else
                        return -34;
                }
                else
                    return -5;
            }
            else
                return -5;

        }

        /// <summary>
        /// 消耗特定箭矢
        /// </summary>
        /// <param name="pc">攻击者</param>
        /// <param name="number">消耗箭矢数量</param>
        /// <returns></returns>
        public void PcArrowDown(Actor sActor, int number = 1)
        {
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
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                                {
                                    Network.Client.MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, (ushort)number, false);
                                }

                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 检查是枪和子弹是否通过条件
        /// </summary>
        /// <param name="pc">攻击者</param>
        /// <param name="number">消耗弹药数量</param>
        /// <returns></returns>
        public int CheckPcGunAndBullet(ActorPC pc, int number = 1)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                {
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                            {

                                return 0;
                            }
                            else
                            {
                                return -56;
                            }
                        }

                        return -35;
                    }
                    return -35;
                }
                else
                    return -5;
            }
            else
                return -5;

        }

        /// <summary>
        /// 消耗特定子弹
        /// </summary>
        /// <param name="pc">攻击者</param>
        /// <param name="number">消耗子弹数量</param>
        /// <returns></returns>
        public void PcBulletDown(Actor sActor, int number = 1)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                                {
                                    Network.Client.MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, (ushort)number, false);
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 检查是远程装备是否通过条件
        /// </summary>
        /// <param name="pc">攻击者</param>
        /// <param name="number">消耗弹药数量</param>
        /// <returns></returns>
        public int CheckPcLongAttack(ActorPC pc, int number = 1)
        {
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
            {
                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                {
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                            {
                                return 0;
                            }
                            else
                            {
                                return -55;
                            }
                        }
                        else
                            return -34;
                    }
                    return -34;
                }
                else if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                {
                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                            {
                                return 0;
                            }
                            else
                            {
                                return -56;
                            }
                        }

                        return -35;
                    }
                    return -35;
                }
                else
                    return -5;
            }
            else
                return -5;

        }


        /// <summary>
        /// 消耗特定远程武器弹药
        /// </summary>
        /// <param name="pc">攻击者</param>
        /// <param name="number">消耗弹药数量</param>
        /// <returns></returns>
        public void PcArrowAndBulletDown(Actor sActor, int number = 1)
        {
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
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                                {
                                    Network.Client.MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, (ushort)number, false);
                                }

                            }
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
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack >= number)
                                {
                                    
                                        Network.Client.MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, (ushort)number, false);
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 检查是否可攻击
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">被攻击者</param>
        /// <returns></returns>
        public bool CheckValidAttackTarget(Actor sActor, Actor dActor)
        {
            if (sActor == dActor)
                return false;
            if (sActor == null || dActor == null)
                return false;
            if (dActor.type == ActorType.PC)
            {
                if (!((ActorPC)dActor).Online)
                    return false;
            }
            if (dActor.type == ActorType.SKILL)
                return false;
            if (dActor.type == ActorType.ITEM)
                return false;
            if (dActor.Buff.Dead)
                return false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                switch (dActor.type)
                {
                    case ActorType.MOB:
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                        if (eh.AI.Mode.Symbol)
                            return false;
                        return true;
                    case ActorType.ITEM:
                    case ActorType.SKILL:
                        return false;
                    case ActorType.PC:
                        {
                            //Logger.ShowInfo("skillhandler");
                            ActorPC target = (ActorPC)dActor;
                            if ((pc.Mode == PlayerMode.COLISEUM_MODE && target.Mode == PlayerMode.COLISEUM_MODE) ||
                                (pc.Mode == PlayerMode.WRP && target.Mode == PlayerMode.WRP) ||
                                (pc.Mode == PlayerMode.KNIGHT_WAR && target.Mode == PlayerMode.KNIGHT_WAR) ||
                                ((pc.Mode == PlayerMode.KNIGHT_EAST || pc.Mode == PlayerMode.KNIGHT_FLOWER || pc.Mode == PlayerMode.KNIGHT_NORTH
                                || pc.Mode == PlayerMode.KNIGHT_ROCK || pc.Mode == PlayerMode.KNIGHT_SOUTH || pc.Mode == PlayerMode.KNIGHT_WEST)
                                && (target.Mode == PlayerMode.KNIGHT_EAST || target.Mode == PlayerMode.KNIGHT_FLOWER || target.Mode == PlayerMode.KNIGHT_NORTH
                                || target.Mode == PlayerMode.KNIGHT_ROCK || target.Mode == PlayerMode.KNIGHT_SOUTH || target.Mode == PlayerMode.KNIGHT_WEST)
                                ))
                            {
                                if ((pc.Mode == PlayerMode.KNIGHT_EAST || pc.Mode == PlayerMode.KNIGHT_FLOWER || pc.Mode == PlayerMode.KNIGHT_NORTH
                                || pc.Mode == PlayerMode.KNIGHT_ROCK || pc.Mode == PlayerMode.KNIGHT_SOUTH || pc.Mode == PlayerMode.KNIGHT_WEST)
                                && (target.Mode == PlayerMode.KNIGHT_EAST || target.Mode == PlayerMode.KNIGHT_FLOWER || target.Mode == PlayerMode.KNIGHT_NORTH
                                || target.Mode == PlayerMode.KNIGHT_ROCK || target.Mode == PlayerMode.KNIGHT_SOUTH || target.Mode == PlayerMode.KNIGHT_WEST)
                                )
                                {
                                    //Logger.ShowInfo("skillhandler2");
                                    if (pc.Mode == target.Mode)
                                        return false;
                                }
                                //Logger.ShowInfo("skillhandler3");
                                if ((pc.Party == target.Party) && pc.Party != null)
                                    return false;
                                else
                                {
                                    if (target.PossessionTarget == 0)
                                        return true;
                                    else
                                        return false;
                                }
                                //Logger.ShowInfo("skillhandler4");
                            }
                            else
                                return false;
                        }
                    case ActorType.PET:
                        {
                            ActorPet pet = (ActorPet)dActor;
                            if ((pc.Mode == PlayerMode.COLISEUM_MODE && pet.Owner.Mode == PlayerMode.COLISEUM_MODE) ||
                               (pc.Mode == PlayerMode.WRP && pet.Owner.Mode == PlayerMode.WRP) ||
                               (pc.Mode == PlayerMode.KNIGHT_WAR && pet.Owner.Mode == PlayerMode.KNIGHT_WAR))
                            {
                                if (pc.Party == pet.Owner.Party)
                                    return false;
                                else
                                    return true;
                            }
                            else
                                return false;
                        }
                    case ActorType.SHADOW:
                        {
                            ActorShadow pet = (ActorShadow)dActor;
                            if ((pc.Mode == PlayerMode.COLISEUM_MODE && pet.Owner.Mode == PlayerMode.COLISEUM_MODE) ||
                               (pc.Mode == PlayerMode.WRP && pet.Owner.Mode == PlayerMode.WRP) ||
                               (pc.Mode == PlayerMode.KNIGHT_WAR && pet.Owner.Mode == PlayerMode.KNIGHT_WAR))
                            {
                                if (pc.Party == pet.Owner.Party)
                                    return false;
                                else
                                    return true;
                            }
                            else
                                return false;
                        }
                }
            }
            else if (sActor.type == ActorType.MOB)
            {
                bool isSlaveOfPc = false;
                ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)sActor.e;

                if (eh.AI.Master != null)
                {
                    if (eh.AI.Master.type == ActorType.PC)
                        isSlaveOfPc = true;
                    if (dActor.type == ActorType.MOB)
                    {
                        ActorEventHandlers.MobEventHandler deh = (SagaMap.ActorEventHandlers.MobEventHandler)dActor.e;
                        if (deh.AI.Master != null)
                        {
                            if (deh.AI.Master.ActorID == eh.AI.Master.ActorID)
                                return false;
                        }
                    }
                }
                if (!isSlaveOfPc)
                {
                    switch (dActor.type)
                    {
                        case ActorType.PC:
                            ActorPC pc = (ActorPC)dActor;
                            if (pc.PossessionTarget != 0)
                                return false;
                            else
                                return true;
                        case ActorType.PARTNER:
                        case ActorType.PET:
                        case ActorType.SHADOW:
                            return true;
                        case ActorType.MOB:
                            eh = (SagaMap.ActorEventHandlers.MobEventHandler)dActor.e;
                            if (eh.AI.Mode.Symbol)
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                }
                else
                {
                    switch (dActor.type)
                    {
                        case ActorType.MOB:
                            return true;
                        case ActorType.PARTNER:
                            return true;
                        default:
                            return false;
                    }
                }
            }
            else if (sActor.type == ActorType.PARTNER)
            {
                switch (dActor.type)
                {
                    case ActorType.MOB:
                        return true;
                    case ActorType.PC:
                    case ActorType.PET:
                    case ActorType.PARTNER:
                        return false;
                }
            }
            else if (sActor.type == ActorType.GOLEM)
            {
                switch (dActor.type)
                {
                    case ActorType.MOB:
                        return true;
                    case ActorType.PC:
                    case ActorType.PET:
                        return false;
                }
            }
            return false;
        }
    }
}
