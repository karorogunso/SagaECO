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
using SagaMap.Titles;

namespace SagaMap.Skill
{
    public partial class SkillHandler : Singleton<SkillHandler>
    {
        /// <summary> 
        /// 技能效果委托列表（增伤）
        /// </summary>
        public static IConcurrentDictionary<string, BuffCallBack> OnCheckBuffList = new IConcurrentDictionary<string, BuffCallBack>();
        /// <summary>
        /// 技能效果委托列表（减伤）
        /// </summary>
        public static IConcurrentDictionary<string, BuffCallBack> OnCheckBuffListReduce = new IConcurrentDictionary<string, BuffCallBack>();
        public delegate int BuffCallBack(Actor sActor, Actor dActor, int damage);

        /// <summary>
        /// 设定称号进度（仅对玩家有效）
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="ID">称号ID</param>
        /// <param name="value">进度</param>
        /// <param name="needdead">是否需要死亡</param>
        public void SetTitleProccess(Actor actor, uint ID, uint value, bool needdead = false)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = actor as ActorPC;
                MapClient.FromActorPC(pc).SetTitleProccess(pc, ID, value);
            }
        }
        /// <summary>
        /// 推送称号进度（仅对玩家有效）
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="ID">称号ID</param>
        /// <param name="value">进度</param>
        /// <param name="needdead">是否需要死亡</param>
        ///
        public void TitleProccess(Actor actor, uint ID, uint value, bool needdead = false)
        {
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = actor as ActorPC;
                MapClient.FromActorPC(pc).TitleProccess(pc, ID, value, needdead);
            }
        }

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

            if(sActor.type == ActorType.PC && target.type == ActorType.PC)
            {
                if (((ActorPC)sActor).Mode == ((ActorPC)target).Mode)
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
        /// <summary>
        /// 取得Actor的地图
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public static Map GetActorMap(Actor actor)
        {
            return Manager.MapManager.Instance.GetMap(actor.MapID);
        }
        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="message"></param>
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
                        Poison1 p = new Poison1(null, dActor, 10000, damage);
                        ApplyAddition(dActor, p);
                        ShowEffectOnActor(dActor, 4126, sActor);
                    }
                }
            }
            else if (type == 2)//魔法
            {

            }
            //通用
        }
        /// <summary>
        /// 回复目标指定血量
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="heal">回复量的绝对值</param>
        public void HealHP(Actor actor, int heal)
        {
            heal = Math.Abs(heal);
            actor.HP += (uint)heal;
            if (actor.HP > actor.MaxHP) actor.HP = actor.MaxHP;
            ShowVessel(actor, -heal);
        }
        /// <summary>
        /// 回复目标指定百分比的血量
        /// </summary>
        /// <param name="actor">目标</param>
        /// <param name="rate">目标血量的百分比</param>
        public void HealHPRate(Actor actor, float rate)
        {
            if (rate > 1f) rate /= 100f;
            int heal = (int)(actor.MaxHP * rate);
            actor.HP += (uint)heal;
            if (actor.HP > actor.MaxHP) actor.HP = actor.MaxHP;
            ShowVessel(actor, -heal);
        }
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
                    ShowEffectOnActor(dActor, 4345, sActor);
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

        /// 检查各类BUFF对伤害的影响
        /// </summary>
        /// <param name="sActor">攻击者</param>
        /// <param name="dActor">目标</param>
        ///  <param name="type">0仅物理   1仅魔法</param>
        public int checkbuff(Actor sActor, Actor dActor, SkillArg arg, byte type, int damage, bool applybuff = true,Elements ele = Elements.Neutral)
        {
            try
            {
                int d = damage;
                d = (int)(d * sActor.Status.DamageRate);

                //旧方法
                foreach (var item in sActor.OnBuffCallBackList.Values)
                    d = item(sActor, dActor, d);
                foreach (var item in dActor.OnBuffCallBackList.Values)
                    d = item(sActor, dActor, d);

                //新方法
                foreach (var item in OnCheckBuffList.Values)
                    d = item(sActor, dActor, d);
                foreach (var item in OnCheckBuffListReduce.Values)
                    d = item(sActor, dActor, d);

                if (dActor.Status.Additions.ContainsKey("凛冽寒风减速") && ele == Elements.Water)
                    d *= 2;
                if (dActor.Status.Additions.ContainsKey("神托：丰收") && ele == Elements.Earth)
                    d = (int)(d * (1f - dActor.TInt["地属性伤害降低"] / 100f));
                if (dActor.Status.Additions.ContainsKey("神托：霜寒") && ele == Elements.Water)
                    d = (int)(d * (1f - dActor.TInt["水属性伤害降低"] / 100f));
                if (dActor.Status.Additions.ContainsKey("神托：阳炎") && ele == Elements.Fire)
                    d = (int)(d * (1f - dActor.TInt["火属性伤害降低"] / 100f));
                if (dActor.Status.Additions.ContainsKey("神托：雷鸣") && ele == Elements.Wind)
                    d = (int)(d * (1f - dActor.TInt["风属性伤害降低"] / 100f));

                if (sActor.Status.Additions.ContainsKey("弹药装填伤害提升"))
                {
                    float rate = sActor.TInt["弹药装填伤害提升Rate"] * 0.01f;
                    d += (int)(d * rate);
                    ShowEffectOnActor(dActor, 5293, sActor);
                }
                if (dActor.TInt["番茄护盾"] > 0 && applybuff && !dActor.Status.Additions.ContainsKey("番茄护盾CD") && d > 0 && sActor != dActor && Global.Random.Next(0, 100) < dActor.TInt["番茄护盾"])
                {
                    ApplyAddition(dActor, new OtherAddition(null, dActor, "番茄护盾CD", 20000));
                    d /= 2;
                    ShowEffectOnActor(dActor, 4198, sActor);
                }
                if (applybuff && sActor.TInt["幽怨魂灵Lv5"] == 1 && d > 0 && sActor != dActor)
                {
                    if (!sActor.Status.Additions.ContainsKey("幽怨之击CD"))
                    {
                        OtherAddition 幽怨之击CD = new OtherAddition(null, sActor, "幽怨之击CD", 30000);
                        ApplyAddition(sActor, 幽怨之击CD);
                        int damageA = 0;
                        damageA = CalcDamage(false, sActor, dActor, null, DefType.MDef, Elements.Dark, 50, 7.5f) + CalcDamage(true, sActor, dActor, null, DefType.Def, Elements.Dark, 50, 7.5f);
                        CauseDamage(sActor, dActor, damageA);
                        ShowVessel(dActor, damageA);
                        ShowEffectOnActor(sActor, 5467);
                    }
                }
                if (applybuff && sActor.TInt["寒流之息Lv5"] == 1 && d > 0)
                {
                    if (!sActor.Status.Additions.ContainsKey("寒流之息CD"))
                    {
                        OtherAddition 寒流之息CD = new OtherAddition(null, sActor, "寒流之息CD", 60000);
                        ApplyAddition(sActor, 寒流之息CD);
                        OtherAddition 寒流之息BUFF = new OtherAddition(null, sActor, "寒流之息BUFF", 30000);
                        ApplyAddition(sActor, 寒流之息BUFF);
                        ShowEffectOnActor(sActor, 4184);
                    }
                    if (sActor.Status.Additions.ContainsKey("寒流之息BUFF"))
                    {
                        d += (int)(d * 0.1f);
                        ShowEffectOnActor(dActor, 5050, sActor);
                    }
                }
                if (applybuff && damage > 0 && dActor.Status.Additions.ContainsKey("圣光加护") && dActor.HP != damage)
                {
                    uint heal = (uint)dActor.TInt["圣光加护治疗量"];
                    dActor.TInt["圣光加护次数"]++;
                    dActor.HP += heal;
                    if (dActor.HP > dActor.MaxHP) dActor.HP = dActor.MaxHP;
                    ShowVessel(dActor, (int)-heal);
                    ShowEffectOnActor(dActor, 4173, sActor);
                    if (dActor.TInt["圣光加护次数"] > 15)
                        RemoveAddition(dActor, "圣光加护");
                }
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.Job == PC_JOB.HAWKEYE && pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if ((pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.RIFLE ||
                             pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.GUN ||
                             pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.DUALGUN ||
                             pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.BOW) && pc.TInt["斥候远程模式"] == 1)
                        {
                            byte baseRange = (byte)(3 + sActor.TInt["稳固射击范围增加"]);
                            byte Range = (byte)(Math.Max(Math.Abs(sActor.X - dActor.X) / 100, Math.Abs(sActor.Y - dActor.Y) / 100));
                            if (Range > baseRange)
                                d = (int)(d * 0.95f);
                            if (Range > baseRange + 1)
                                d = (int)(d * 0.85f);
                            if (Range > baseRange + 2)
                                d = (int)(d * 0.75f);
                            if (Range > baseRange + 3)
                                d = (int)(d * 0.65f);

                            if (Range <= 2 && (pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.GUN ||
                             pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.DUALGUN) && sActor.TInt["手枪律动范围"] > 0)
                            {
                                d = (int)(d * (1f + (sActor.TInt["手枪律动范围"] / 100f)));
                                if (applybuff)
                                    ShowEffectOnActor(dActor, 8052, sActor);
                            }

                            if (pc.Status.Additions.ContainsKey("自由射击") && pc.Status.Additions.ContainsKey("蓝蝶"))
                                d += (int)(d * sActor.TInt["蓝蝶提升%"] / 100f);
                        }
                    }
                }

                if(sActor.Status.Additions.ContainsKey("自由射击"))
                    if (sActor.TInt["自由射击Value"] > 1)
                        d += (int)((float)d * (sActor.TInt["自由射击Value"] - 1) * 5f / 100f);

                if (sActor.Status.Additions.ContainsKey("残忍") && dActor.Status.Additions.ContainsKey("Stun"))
                    d += (int)(d * sActor.TInt["残忍提升%"] / 100f);

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
                        if (applybuff)
                        {
                            ShowVessel(dActor, 0, damage, 0);
                            ShowEffectOnActor(dActor, 4174, sActor);
                        }

                        if (damage > 0)
                            d = -damage;
                        else
                            d = damage;
                    }
                }
                if(dActor.type == ActorType.PC && dActor.Status.Additions.ContainsKey("冰霜之焰") && !dActor.Status.Additions.ContainsKey("冰霜之焰CD") && Global.Random.Next(0,100) < 2)
                {
                    OtherAddition cd = new OtherAddition(null, dActor, "冰霜之焰CD", 10000);
                    ApplyAddition(dActor, cd);
                    ActorPC pc = (ActorPC)dActor;
                    float factor = 0f;
                    if(pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.EFFECT) && pc.Inventory.Equipments[EnumEquipSlot.EFFECT].Name == "阿鲁卡多之焰")
                    {
                        factor = 0.1f * pc.Inventory.Equipments[EnumEquipSlot.EFFECT].Refine;
                        if (factor > 5f)
                            factor = 5f;
                    }
                    Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                    List<Actor> actors = map.GetActorsArea(dActor, 200, false);
                    List<Actor> targets = new List<Actor>();
                    ShowEffectOnActor(dActor, 5078);
                    foreach (var item in actors)
                    {
                        if (CheckValidAttackTarget(dActor, item))
                        {
                            ShowEffectOnActor(item, 5284);
                            int da = CalcDamage(true, dActor, item, null, DefType.MDef, Elements.Dark, 50, factor);
                            da += CalcDamage(false, dActor, item, null,DefType.MDef, Elements.Dark, 50, factor);
                            CauseDamage(dActor, item, da);
                            ShowVessel(item, da);
                            if (!item.Status.Additions.ContainsKey("冰霜之焰冰冻CD"))
                            {
                                Freeze f = new Freeze(null, item, 5000);
                                ApplyAddition(item, f);
                                OtherAddition fcd = new OtherAddition(null, item, "冰霜之焰冰冻CD", 80000);
                                ApplyAddition(item, fcd);
                            }
                        }
                    }
                }
                if(dActor.Status.Additions.ContainsKey("暗影猎杀"))
                {
                    dActor.TInt["暗影猎杀"] += damage;
                    ShowEffectOnActor(dActor, 5121);
                }
                if (applybuff && dActor.Status.Additions.ContainsKey("替身术") && damage > 0)
                {
                    if (dActor.TInt["替身术记录X"] != 0 && dActor.TInt["替身术记录Y"] != 0)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                        d = 0;
                        byte x = (byte)dActor.TInt["替身术记录X"];
                        byte y = (byte)dActor.TInt["替身术记录Y"];
                        RemoveAddition(dActor, "替身术");
                        dActor.TInt["替身术记录X"] = 0;
                        dActor.TInt["替身术记录Y"] = 0;
                        short px = Global.PosX8to16(x, map.Width);
                        short py = Global.PosY8to16(y, map.Height);
                        byte x1 = Global.PosX16to8(dActor.X, map.Width);
                        byte y1 = Global.PosY16to8(dActor.Y, map.Height);
                        ShowEffect(map, dActor, x1, y1, 4251);
                        Invisible inv = new Invisible(null, dActor, 10000);
                        ApplyAddition(sActor, inv);
                        //map.TeleportActor(dActor, px, py);

                    }
                }
                if (applybuff && dActor.Status.Additions.ContainsKey("ShieldReflect") &&
                    !(dActor.Status.Additions.ContainsKey("ShieldReflect") && sActor.Status.Additions.ContainsKey("ShieldReflect")) &&
                    sActor.type == ActorType.PC)//盾牌反射
                {
                    ShowEffectByActor(dActor, 5092);
                    //sActor.EP += 500;
                    CauseDamage(dActor, sActor, damage);
                    ShowVessel(sActor, damage);
                    d = 0;
                }
                if (applybuff && sActor.Status.Additions.ContainsKey("ApplyPoison") && type != 1)//魔法攻击不会触发
                {
                    float fac = sActor.TInt["PoisonDamageUP"] / 100f;
                    if (dActor.Status.Additions.ContainsKey("Poison1"))
                        fac *= 1.5f;
                    int dp = CalcDamage(false, sActor, dActor, null, DefType.MDef, Elements.Dark, 50, fac);
                    d += dp;
                    Instance.ShowEffectOnActor(dActor, 8048, sActor);
                }
                if (sActor.TInt["临时攻击上升"] > 0)
                {
                    d = (int)(d + d * sActor.TInt["临时攻击上升"] / 100f);
                }
                if (applybuff && dActor.Buff.九尾狐魅惑)
                {
                    dActor.HP += (uint)damage;
                    if (dActor.HP > dActor.MaxHP)
                        dActor.HP = dActor.MaxHP;
                    ShowVessel(dActor, (int)-damage);
                    ShowEffectOnActor(dActor, 5291, sActor);
                }
                if (dActor.Status.Additions.ContainsKey("Allavoid"))
                    d = 0;
                if (dActor.Status.Additions.ContainsKey("无敌"))
                    d = 0;
                if (dActor.Status.Additions.ContainsKey("Invincible"))
                    d = 0;
                if (sActor.Status.Additions.ContainsKey("疾风斩伤害上升"))
                {
                    if (sActor.Buff.三转2足ATKUP)
                        d = (int)(d * 1.65f);
                    else
                        d = (int)(d * 1.3f);
                    if (applybuff)
                        ShowEffectOnActor(dActor, 8052, sActor);
                }
                /*if (dActor.Status.Additions.ContainsKey("Vulnerable"))  //易伤
                {
                    d += (int)(d * ((Vulnerable)dActor.Status.Additions["Vulnerable"]).Variable["Vulnerable"] / 100f);
                    //if (applybuff)
                        //ShowEffectOnActor(dActor, 5111);
                }*/
                if (sActor.Status.Additions.ContainsKey("勇战之进行曲Buff"))
                {
                    d = (int)(d * (100 + sActor.TInt["勇战之进行曲Value"]) / 100f);
                    if (applybuff)
                        ShowEffectOnActor(dActor, 8052, sActor);
                }
                if (dActor.Status.Additions.ContainsKey("勇战之进行曲Buff"))
                {
                    d = (int)(d * (100 - sActor.TInt["勇战之进行曲Value"]) / 100f);
                    if (applybuff)
                        ShowEffectOnActor(dActor, 8035, sActor);
                }
                if (dActor.type == ActorType.MOB)
                {
                    ActorMob Mob = (ActorMob)dActor;
                    if (applybuff && Mob.TInt["鱼人加护"] == 1)
                    {
                        int heal = (int)(Mob.MaxHP * 0.01f);
                        CauseDamage(sActor, Mob, heal);
                        ShowVessel(Mob, -heal);
                    }
                }
                if (dActor.Status.ReduceDamage > 0 && d > 0)//南辰流
                    d = (int)(d - d * dActor.Status.ReduceDamage);
                if (dActor.Status.Additions.ContainsKey("怪物格挡"))
                {
                    d = 0;
                    if (applybuff)
                        ShowEffectOnActor(dActor, 5092, sActor);
                }
                if (dActor.Status.Additions.ContainsKey("反击风暴"))
                {
                    if (!sActor.Status.Additions.ContainsKey("Stun"))
                    {
                        if (applybuff)
                        {
                            int daa = (int)(sActor.MaxHP * 0.5f);
                            CauseDamage(dActor, sActor, daa);
                            ShowVessel(sActor, -daa);
                        }
                        d = 0;
                        if (applybuff)
                        {
                            ShowEffectOnActor(dActor, 4198, sActor);
                            ShowEffectOnActor(sActor, 5372);
                            Stun stun = new Stun(null, sActor, 2000);
                            ApplyAddition(sActor, stun);
                        }
                    }
                }
                if (applybuff && dActor.Status.Additions.ContainsKey("琴音干扰"))
                {
                    Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                    List<Actor> actors = map.GetActorsArea(dActor, 2000, false);
                    List<Actor> targets = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (CheckValidAttackTarget(dActor, item))
                            targets.Add(item);
                    }
                    if (targets.Count > 0)
                    {
                        Actor target = sActor;
                        int daa = d;
                        daa = Math.Abs(daa);

                        if (target.type == ActorType.PC)
                        {
                            ActorPC tpc = (ActorPC)target;
                            tpc.TInt["琴音干扰的伤害"] += daa;
                            if (!tpc.Status.Additions.ContainsKey("琴音干扰伤害计时"))
                            {
                                OtherAddition tskill = new OtherAddition(null, tpc, "琴音干扰伤害计时", 3000);
                                tskill.OnAdditionEnd += (s, e) =>
                                {
                                    if (!(s as ActorPC).Buff.Sit)
                                    {
                                        int da = tpc.TInt["琴音干扰的伤害"];
                                        CauseDamage(dActor, target, da);
                                        ShowVessel(target, da);
                                        ShowEffectOnActor(target, 5290, sActor);
                                        SendSystemMessage(target, "还想着骚操作！受到了来自 琴音干扰 的" + tpc.TInt["琴音干扰的伤害"] + "伤害。");
                                        TitleProccess(target, 83, 1, true);
                                    }
                                    else
                                    {
                                        int da = (int)(tpc.TInt["琴音干扰的伤害"] * 0.3);
                                        CauseDamage(dActor, target, da);
                                        ShowVessel(target, da);
                                        ShowEffectOnActor(target, 5290, sActor);
                                        SendSystemMessage(target, "心无杂念！只受到了来自 琴音干扰 的" + tpc.TInt["琴音干扰的伤害"] + "的30%伤害。");
                                        TitleProccess(target, 82, 1);
                                    }
                                    tpc.TInt["琴音干扰的伤害"] = 0;
                                };
                                ApplyAddition(tpc, tskill);
                                SendSystemMessage(target, "即将受到 琴音干扰 的来自其他玩家的伤害！3秒后伤害触发，坐下状态可以避免受到伤害。已累积伤害：" + tpc.TInt["琴音干扰的伤害"]);
                            }
                            else
                            {
                                ((OtherAddition)tpc.Status.Additions["琴音干扰伤害计时"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 3000);
                                SendSystemMessage(target, "琴音干扰 的伤害被累积！触发时间重置至3秒后。已累积伤害：" + tpc.TInt["琴音干扰的伤害"]);
                            }
                            ShowEffectOnActor(target, 5290, sActor);
                        }
                    }
                }
                if (applybuff && dActor.Status.Additions.ContainsKey("恶鬼之道"))
                {
                    Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                    List<Actor> actors = map.GetActorsArea(dActor, 2000, false);
                    List<Actor> targets = new List<Actor>();
                    foreach (var item in actors)
                    {
                        if (CheckValidAttackTarget(dActor, item))
                            targets.Add(item);
                    }
                    if (targets.Count > 0)
                    {
                        Actor target = targets[Global.Random.Next(0, targets.Count - 1)];
                        int daa = d;
                        daa = Math.Abs(daa);
                        ShowEffectOnActor(dActor, 4198, sActor);
                        if (target.type == ActorType.PC)
                        {
                            ActorPC tpc = (ActorPC)target;
                            tpc.TInt["琴音干扰的伤害"] += daa;
                            if (!tpc.Status.Additions.ContainsKey("琴音干扰伤害计时"))
                            {
                                OtherAddition tskill = new OtherAddition(null, tpc, "琴音干扰伤害计时", 5000);
                                tskill.OnAdditionEnd += (s, e) =>
                                {
                                    int da = tpc.TInt["琴音干扰的伤害"] / 2;
                                    ShowEffectByActor(target, 4385);
                                    List<Actor> assa = Instance.GetActorsAreaWhoCanBeAttackedTargets(dActor, target, 300, true);
                                    foreach (var item in assa)
                                    {
                                        CauseDamage(dActor, item, da);
                                        ShowVessel(item, da);
                                        SendSystemMessage(item, "受到了来自 恶鬼之道 的" + tpc.TInt["琴音干扰的伤害"] + "反制伤害。");
                                    }
                                    tpc.TInt["琴音干扰的伤害"] = 0;
                                };
                                ApplyAddition(tpc, tskill);
                                SendSystemMessage(target, "即将受到 恶鬼之道 的反制伤害！5秒后伤害触发。已累积伤害：" + tpc.TInt["琴音干扰的伤害"]);
                            }
                            else
                            {
                                ((OtherAddition)tpc.Status.Additions["琴音干扰伤害计时"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 5000);
                                SendSystemMessage(target, "恶鬼之道 的反制伤害被累积！触发时间重置至5秒后。已累积伤害：" + tpc.TInt["琴音干扰的伤害"]);
                            }
                            ShowEffectOnActor(target, 8021, sActor);
                        }
                    }
                }
                if (dActor.Status.ParryRate > 0 && d > 0)//格挡
                {
                    if (Global.Random.Next(0, 100) < dActor.Status.ParryRate || dActor.Status.Additions.ContainsKey("无刀取"))
                    {
                        float ParryReduceRate = 0.3f;
                        if (dActor.type == ActorType.PC)
                        {
                            ActorPC dPc = (ActorPC)dActor;
                            if (dPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                                if (dPc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.SHIELD)
                                    ParryReduceRate = 0.7f;
                        }
                        d = (int)(d * (1f - ParryReduceRate));
                        if (applybuff)
                        {
                            ShowEffectOnActor(dActor, 5092, sActor);
                            if (dActor.Status.Additions.ContainsKey("无刀取"))
                            {
                                RemoveAddition(dActor, "无刀取");
                                if (dActor.Status.Additions.ContainsKey("硬直"))
                                    RemoveAddition(dActor, "硬直");
                                if (!dActor.Status.Additions.ContainsKey("格挡成功"))
                                {
                                    OtherAddition 格挡成功 = new OtherAddition(null, dActor, "格挡成功", 5000);
                                    ApplyAddition(dActor, 格挡成功);
                                    MapClient.FromActorPC((ActorPC)dActor).SendSystemMessage("进入了可以发动『明镜斩』的状态。");
                                    ShowEffectOnActor(dActor, 5179, sActor);
                                }
                            }
                        }
                    }
                }
                if (dActor.Status.Additions.ContainsKey("巨龙撞击"))
                {
                    d = (int)(d + d * 0.5f);
                    if (applybuff)
                        ShowEffectOnActor(dActor, 5293, sActor);
                }
                if (dActor.TInt["袈裟斩伤害加深"] > 0)
                {
                    d = d + (d * dActor.TInt["袈裟斩伤害加深"] / 100);
                    if (applybuff)
                        ShowEffectOnActor(dActor, 5293, sActor);
                }

                if (applybuff && dActor.Status.Additions.ContainsKey("瘴气兵装"))
                {
                    if (sActor.HP > 0)
                        addbuff(sActor);
                }
                if (sActor.Status.Additions.ContainsKey("沙月剧毒丸伤害提升"))
                {
                    d = (int)(d + d * 0.35f);
                    if (applybuff)
                        ShowEffectOnActor(dActor, 5294, sActor);
                }
                /*if (sActor.Status.Additions.ContainsKey("完美谢幕伤害提升"))
                {
                    d = d + (d * sActor.TInt["完美谢幕伤害提升Rate"] / 100);
                    ShowEffectOnActor(dActor, 8050);
                }*/
                if (applybuff && sActor.Status.Additions.ContainsKey("完美谢幕BUFF" + sActor.ActorID))
                {
                    if (dActor.Status.Additions.ContainsKey("完美谢幕目标DEBUFF" + sActor.ActorID))
                    {
                        sActor.TInt["完美谢幕累计伤害"] += d;
                        ShowEffectOnActor(dActor, 8052, sActor);
                    }
                }
                if (applybuff && sActor.TInt["重锤火花提升%"] > 0)
                {
                    if (Global.Random.Next(0, 100) < 1)
                    {
                        if (!dActor.Status.Additions.ContainsKey("重锤火花晕眩CD"))
                        {
                            OtherAddition skill = new OtherAddition(null, dActor, "重锤火花晕眩CD", 50000);
                            ApplyAddition(dActor, skill);
                            if (!dActor.Status.Additions.ContainsKey("Stun"))
                            {
                                Stun stun = new Stun(null, dActor, 5000);
                                ApplyAddition(dActor, stun);
                            }
                        }
                    }
                }
                //心眼
                if (applybuff && sActor.TInt["心眼Rate"] > 0 && d > 0 && dActor.HP > 0)
                {
                    if (Global.Random.Next(0, 100) < sActor.TInt["心眼Rate"])
                    {
                        if (!sActor.Status.Additions.ContainsKey("心眼CD") && !sActor.Status.Additions.ContainsKey("心眼持续时间"))
                        {
                            OtherAddition cd = new OtherAddition(null, sActor, "心眼CD", 12000);
                            ApplyAddition(sActor, cd);
                            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                            short[] pos = map.GetRandomPosAroundPos(dActor.X, dActor.Y, 500);
                            while (map.GetActorsArea(pos[0], pos[1], 200, false).Contains(sActor))
                                pos = map.GetRandomPosAroundPos(dActor.X, dActor.Y, 500);
                            map.SendEffect(sActor, Global.PosX16to8(pos[0], map.Width), Global.PosY16to8(pos[1], map.Height), 7916);
                            /*-------------------心眼技能体-----------------*/
                            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31131, 1), sActor);
                            actor2.Name = "心眼技能体";
                            actor2.MapID = sActor.MapID;
                            actor2.X = pos[0];
                            actor2.Y = pos[1];
                            actor2.e = new ActorEventHandlers.NullEventHandler();
                            map.RegisterActor(actor2);
                            actor2.invisble = false;
                            map.OnActorVisibilityChange(actor2);
                            actor2.Stackable = false;
                            OtherAddition lifetime = new OtherAddition(null, sActor, "心眼持续时间", 10000);
                            lifetime.OnAdditionEnd += (s, e) =>
                            {
                                if (map.GetActor(actor2.ActorID) != null)
                                    map.DeleteActor(actor2);
                            };
                            ApplyAddition(sActor, lifetime);
                            /*-------------------魔法阵的技能体-----------------*/
                        }
                    }
                }
                /*if (dActor.Buff.黑暗压制 && d < 0)
                {
                    d /= 3;
                }*/

                if (applybuff && dActor.Status.Additions.ContainsKey("生命之重奏曲Buff"))
                {
                    if (Global.Random.Next(0, 99) < dActor.TInt["生命之重奏曲Rate"])
                    {
                        int hpheal = (int)(dActor.MaxHP * dActor.TInt["生命之重奏曲HealRate"] / 100f);
                        dActor.HP += (uint)hpheal;
                        if (dActor.HP > dActor.MaxHP)
                            dActor.HP = dActor.MaxHP;
                        ShowVessel(dActor, -hpheal);
                    }
                }
                if (applybuff && sActor.Status.Additions.ContainsKey("沙月剧毒丸吸血"))//吸血
                {
                    uint heal = (uint)(d * 0.03f);
                    sActor.HP += heal;
                    if (sActor.HP > sActor.MaxHP)
                        sActor.HP = sActor.MaxHP;
                    ShowVessel(sActor, (int)-heal);
                }
                if (applybuff && d > 0 && sActor.Buff.单枪匹马)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (!sActor.Status.Additions.ContainsKey("单枪匹马CD") && pc.Party == null)//吸血
                    {
                        OtherAddition cd = new OtherAddition(null, sActor, "单枪匹马CD", 10000);
                        cd.OnAdditionEnd += (s, e) =>
                        {
                            ShowEffectOnActor(sActor, 4154);
                        };
                        ApplyAddition(sActor, cd);
                        sActor.HP += (uint)d;
                        if (sActor.HP > sActor.MaxHP)
                            sActor.HP = sActor.MaxHP;
                        ShowVessel(sActor, -d);
                        ShowEffectOnActor(sActor, 4118);
                    }
                    else
                    {
                        uint heal = (uint)(d / 7);
                        if (sActor.Buff.Dead)
                            heal = 0;
                        if (pc.Party != null)
                            heal /= 2;
                        sActor.HP += heal;
                        if (sActor.HP > sActor.MaxHP)
                            sActor.HP = sActor.MaxHP;
                        ShowVessel(sActor, (int)-heal);
                    }
                    if (sActor.type == ActorType.PC)
                    {
                        if (((ActorPC)sActor).Job == PC_JOB.CARDINAL)
                            d = (int)(d * 1.3f);
                    }
                }
                if (applybuff && sActor.Status.Additions.ContainsKey("黑暗毒血吸血BUFF"))
                {
                    if (d > 0)
                    {
                        uint heal = (uint)(d / 20);
                        sActor.HP += heal;
                        if (sActor.HP > sActor.MaxHP)
                            sActor.HP = sActor.MaxHP;
                        ShowVessel(sActor, (int)-heal);
                    }
                }
                //if (d > dActor.HP) d = (int)dActor.HP;

                if (applybuff && dActor.TInt["海神的诅咒"] == 1)
                {
                    if (dActor.SettledSlave.Count > 0)
                    {
                        int c = 0;
                        foreach (var item in dActor.SettledSlave)
                        {
                            if (!item.Buff.Dead)
                                c++;
                        }
                        if (c > 0)
                        {
                            uint heal = (uint)d;

                            int da = (int)(heal / 2);
                            CauseDamage(dActor, sActor, da);
                            ShowVessel(sActor, da);
                            ShowEffectOnActor(dActor, 5458, sActor);
                            ShowEffectOnActor(sActor, 5457);

                            dActor.HP += heal;
                            if (dActor.HP > dActor.MaxHP)
                                dActor.HP = dActor.MaxHP;
                            ShowVessel(dActor, (int)-heal);
                        }
                    }
                }
                if (sActor.TInt["移动施法"] == 1)
                {
                    d /= 2;
                }
                return d;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return 0;
            }

        }

        void addbuff(Actor dActor)
        {
            int ran = SagaLib.Global.Random.Next(1, 6);//随机1-6，来选择降低哪个属性
            int value = 30;//定好降多少
            switch (ran)
            {
                case 1://降低STR
                    if (!dActor.Status.Additions.ContainsKey("STRDOWN"))
                    {
                        STRDOWN sd = new STRDOWN(null, dActor, 180000, value);
                        SkillHandler.ApplyAddition(dActor, sd);
                    }
                    break;
                case 2:
                    if (!dActor.Status.Additions.ContainsKey("AGIDOWN"))
                    {
                        AGIDOWN sd = new AGIDOWN(null, dActor, 180000, value);
                        SkillHandler.ApplyAddition(dActor, sd);
                    }
                    break;
                case 3:
                    if (!dActor.Status.Additions.ContainsKey("VITDOWN"))
                    {
                        VITDOWN sd = new VITDOWN(null, dActor, 180000, value);
                        SkillHandler.ApplyAddition(dActor, sd);
                    }
                    break;
                case 4:
                    if (!dActor.Status.Additions.ContainsKey("INTDOWN"))
                    {
                        INTDOWN sd = new INTDOWN(null, dActor, 180000, value);
                        SkillHandler.ApplyAddition(dActor, sd);
                    }
                    break;
                case 5:
                    if (!dActor.Status.Additions.ContainsKey("DEXDOWN"))
                    {
                        DEXDOWN sd = new DEXDOWN(null, dActor, 180000, value);
                        SkillHandler.ApplyAddition(dActor, sd);
                    }
                    break;
                case 6:
                    if (!dActor.Status.Additions.ContainsKey("MAGDOWN"))
                    {
                        MAGDOWN sd = new MAGDOWN(null, dActor, 180000, value);
                        SkillHandler.ApplyAddition(dActor, sd);
                    }
                    break;
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
        public void DoDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, float ignore = 0)
        {
            try
            {
                AttackResult res = AttackResult.Hit;
                int damage = CalcDamage(IsPhyDamage, sActor, dActor, arg, defType, element, eleValue, ATKBonus, out res, true, ignore);
                CauseDamage(sActor, dActor, damage);
                ShowVessel(dActor, damage, 0, 0, res);
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
        public int CalcDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, float ignore = 0, bool applybuff = false)
        {
            AttackResult res = AttackResult.Hit;
            return CalcDamage(IsPhyDamage, sActor, dActor, arg, defType, element, eleValue, ATKBonus, out res, applybuff, ignore);
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
        public int CalcDamage(bool IsPhyDamage, Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int eleValue, float ATKBonus, out AttackResult res, bool applybuff = false, float ignore = 0)
        {
            try
            {
                int damage = 0;
                int atk;
                int mindamage = 0;
                int maxdamage = 0;

                res = CalcAttackResult(sActor, dActor, 0, sActor.Range > 3);
                //if (res == AttackResult.Critical)
                //    damage = (int)((damage) * (1f + CalCriBonusRate(sActor, dActor, 0) / 100f));

                if (IsPhyDamage)
                {
                    if (arg == null)
                    {
                        mindamage = sActor.Status.min_atk1;
                        maxdamage = sActor.Status.max_atk1;

                        if (sActor.type == ActorType.PARTNER)
                        {
                            ActorPartner partner = sActor as ActorPartner;
                            if (partner.Owner != null && partner.Owner.Status != null)
                            {
                                mindamage = partner.Owner.Status.min_atk1;
                                maxdamage = partner.Owner.Status.max_atk1;
                            }
                        }
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
                            }
                        }

                    }
                    if (mindamage > maxdamage) maxdamage = mindamage;
                    atk = Global.Random.Next(mindamage, maxdamage);
                    atk = (int)(atk * CalcElementBonus(sActor, dActor, element, eleValue, 0, false) * ATKBonus);

                    int igoreAddDef = 0;
                    if (sActor.TInt["刀锋之末破防"] > 0)
                        igoreAddDef += sActor.TInt["刀锋之末破防"];
                    if (sActor.TInt["幽怨之怒破防"] > 0)
                        igoreAddDef += sActor.TInt["幽怨之怒破防"];

                    if (igoreAddDef > 0)
                    {
                        if (atk >= 0)
                            damage = CalcPhyDamage(dActor, defType, atk, ignore, igoreAddDef / 100f);
                        else
                            damage = -CalcPhyDamage(dActor, defType, -atk, ignore, igoreAddDef / 100f);
                    }
                    else
                    {
                        if (atk >= 0)
                            damage = CalcPhyDamage(dActor, defType, atk, ignore);
                        else
                            damage = -CalcPhyDamage(dActor, defType, atk, ignore);
                    }



                    damage = CheckBuffForDamage(dActor, damage);
                    if (damage > atk)
                        damage = atk;
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
                    if (damage == 0) damage = 1;
                    damage = checkbuff(sActor, dActor, null, 0, damage, applybuff, element);
                    checkdebuff(sActor, dActor, arg, 0);
                }
                else
                {
                    mindamage = sActor.Status.min_matk;
                    maxdamage = sActor.Status.max_matk;

                    if (sActor.type == ActorType.PARTNER)
                    {
                        ActorPartner partner = sActor as ActorPartner;
                        if (partner.Owner != null && partner.Owner.Status != null)
                        {
                            mindamage = partner.Owner.Status.min_matk;
                            maxdamage = partner.Owner.Status.max_matk;
                        }
                    }


                    if (mindamage > maxdamage) maxdamage = mindamage;
                    atk = Global.Random.Next(mindamage, maxdamage);
                    if (element != Elements.Neutral)
                    {
                        float eleBonus = CalcElementBonus(sActor, dActor, element, 50, 1, ((ATKBonus < 0) && !((dActor.Status.undead == true) && (element == Elements.Holy))));
                        atk = (int)(atk * eleBonus * ATKBonus);
                    }
                    else
                        atk = (int)(atk * 1f * ATKBonus);


                    int igoreAddDef = 0;
                    if (sActor.TInt["刺骨毒牙破防"] > 0)
                        igoreAddDef += sActor.TInt["刺骨毒牙破防"];
                    if (sActor.TInt["幽怨之怒破防"] > 0)
                        igoreAddDef += sActor.TInt["幽怨之怒破防"];

                    if (igoreAddDef > 0)
                    {
                        if (atk >= 0)
                            damage = CalcMagDamage(dActor, defType, atk, ignore, igoreAddDef / 100f);
                        else
                            damage = -CalcMagDamage(dActor, defType, -atk, ignore, igoreAddDef / 100f);
                    }
                    else
                    {
                        if (atk >= 0)
                            damage = CalcMagDamage(dActor, defType, atk, ignore);
                        else
                            damage = -CalcMagDamage(dActor, defType, -atk, ignore);
                    }

                    damage = CheckBuffForDamage(dActor, damage);
                    if (sActor.type == ActorType.PC && dActor.type == ActorType.PC)
                    {
                        if (damage > 0)
                            damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                    }
                    damage = checkbuff(sActor, dActor, null, 1, damage, applybuff, element);
                    checkdebuff(sActor, dActor, arg, 1);
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


                MapClient.FromActorPC(pc).SendStatusExtend();
                MapClient.FromActorPC(pc).SendRange();
                Instance.CastPassiveSkills(pc);
                PC.StatusFactory.Instance.CalcStatus(pc);
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
                dActor.HP = 0;
                dActor.Buff.Dead = true;
                //return;
            }
            //damage = (int)dActor.HP;
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                if (!ignoreShield && pc.Skills.ContainsKey(14052) && !pc.Status.Additions.ContainsKey("无尽寒冬CD")) //精通：寒霜护甲
                {
                    uint totalHP = dActor.HP; //记录当前HP
                    if (dActor.TInt["续命开关"] == 1 && dActor.SP > 0)
                        totalHP += dActor.SP;
                    if (dActor.Status.Additions.ContainsKey("圣盾加护") && dActor.SHIELDHP > 0)
                        totalHP += dActor.SHIELDHP;
                    if (damage >= totalHP)//伤害足以致死
                    {
                        damage = 0;

                        //移除控制状态
                        if (dActor.Status.Additions.ContainsKey("Confuse")) RemoveAddition(dActor, "Confuse");
                        if (dActor.Status.Additions.ContainsKey("Frosen")) RemoveAddition(dActor, "Frosen");
                        if (dActor.Status.Additions.ContainsKey("Paralyse")) RemoveAddition(dActor, "Paralyse");
                        if (dActor.Status.Additions.ContainsKey("Silence")) RemoveAddition(dActor, "Silence");
                        if (dActor.Status.Additions.ContainsKey("Sleep")) RemoveAddition(dActor, "Sleep");
                        if (dActor.Status.Additions.ContainsKey("Stone")) RemoveAddition(dActor, "Stone");
                        if (dActor.Status.Additions.ContainsKey("Stun")) RemoveAddition(dActor, "Stun");

                        //SkillArg arg2 = new SkillArg();
                        //arg2.skill = SkillFactory.Instance.GetSkill(14022, pc.Skills2[14022].Level);
                        //SkillCast(dActor, dActor, arg2);

                        PutSkill(dActor, dActor, 14022, pc.Skills2[14022].Level, 0, 0, true);

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

            if (dActor.TInt["续命开关"] == 1 && damage > 0 && dActor.SP > 0 && !ignoreShield)
            {
                if (dActor.SP > damage)
                {
                    dActor.SP -= (uint)damage;
                    ShowVessel(dActor, 0, 0, damage);
                    ShowEffectOnActor(dActor, 4173, sActor);
                    damage = 0;
                }
                else
                {
                    ShowEffectOnActor(dActor, 5445, sActor);
                    ShowVessel(dActor, 0, 0, (int)dActor.SP);
                    damage -= (int)dActor.SP;
                    dActor.SP = 0;
                    dActor.Status.Additions.Remove("神圣光界");
                }
            }
            if (damage > 0 && dActor.SHIELDHP > 0 && !ignoreShield && dActor.Status.Additions.ContainsKey("圣盾加护"))
            {
                if (dActor.SHIELDHP > damage)
                {
                    dActor.SHIELDHP -= (uint)damage;
                    ShowVessel(dActor, 0, damage, 0);
                    ShowEffectOnActor(dActor, 4174, sActor);
                    damage = 0;
                }
                else
                {
                    ShowEffectOnActor(dActor, 5447, sActor);
                    damage -= (int)dActor.SHIELDHP;
                    dActor.SHIELDHP = 0;
                    RemoveAddition(dActor, "圣盾加护");
                }
            }

            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                if (damage >= dActor.HP && pc.Status.Additions.ContainsKey("HolyVolition"))//7月16日更新的光之意志BUFF
                {
                    dActor.HP = 1;
                    ShowEffectOnActor(pc, 4173, sActor);
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
                            ShowEffectOnActor(item, 5275, sActor);
                            if (!item.Status.Additions.ContainsKey("Stun"))
                            {
                                Stun stun = new Stun(null, item, 3000);
                                ApplyAddition(item, stun);
                            }
                        }
                    }
                    ShowEffectOnActor(pc, 4243, sActor);
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
                        DefaultBuff skill = new DefaultBuff(null, pc, "HolyVolition", 5000);
                        ApplyAddition(pc, skill);
                    }
                }
            }

            if (damage > dActor.HP)
                dActor.HP = 0;
            else
            {
                if (damage < 0) //治疗时，抹除过量值
                    damage = -(Math.Min(-damage, (int)(dActor.MaxHP - dActor.HP)));
                dActor.HP = (uint)(dActor.HP - damage);
            }


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

        public Boolean CancelSkillCast(Actor actor,Actor Caster = null)
        {
            if (Caster != null && Caster.type == ActorType.PC)
            {
                if (actor.TInt["CanNotInterrupted"] == 1)
                {
                    SendSystemMessage(Caster, "这个技能无法被打断");
                    return false;
                }
                if (actor.Status.Additions.ContainsKey("不可打断"))
                {
                    SendSystemMessage(Caster, "目标处于『不可打断』状态");
                    return false;
                }
            }
            if (actor.Tasks.ContainsKey("SkillCast") && actor.TInt["CanNotInterrupted"] != 1 && !actor.Status.Additions.ContainsKey("不可打断"))
                if (actor.Tasks["SkillCast"].getActivated())
                    if ((actor.Tasks["SkillCast"].NextUpdateTime - DateTime.Now).TotalMilliseconds > 200)
                    {
                        if (actor.type != ActorType.PC)
                        {
                            OtherAddition buff = new OtherAddition(null, actor, "不可打断", 45000);
                            ApplyAddition(actor, buff);
                        }
                        actor.Tasks["SkillCast"].Deactivate();
                        actor.Tasks.Remove("SkillCast");
                        Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL_CANCEL, null, actor, true);
                        return true;
                    }

            return false;
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
        /// 增加殺意
        /// </summary>
        /// <param name="sActor">目標</param>
        /// <param name="count">層數</param>
        public void Homicidal(Actor sActor, byte count)
        {
            if (sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).PossessionTarget != 0)//凭依时无效
                    return;
            }
            if (sActor != null)
            {
                sActor.IsHomicidal = 1;
                for (int i = 0; i < count; i++)
                {
                    Additions.Global.Homicidal Homicidal = new Additions.Global.Homicidal(null, sActor, 6000);
                    ApplyAddition(sActor, Homicidal);
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
            arg.argType = SkillArg.ArgType.Actor_Active;
            //Item item0 = ItemFactory.Instance.GetItem(10000000);
            //arg.item = item0;
            arg.hp[0] = hp;
            arg.mp[0] = mp;
            arg.sp[0] = sp;

            if (actor.HP == 0)
            {
                arg.flag[0] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                arg.argType = SkillArg.ArgType.Attack;
            }
            else if (hp > 0 || hp == 0 && mp == 0 && sp == 0)
                arg.flag[0] |= AttackFlag.HP_DAMAGE;
            else if (hp < 0)
            {
                //arg.item = ItemFactory.Instance.GetItem(10000000);
                arg.flag[0] |= AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                arg.argType = SkillArg.ArgType.Actor_Active;
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
                    arg.argType = SkillArg.ArgType.Actor_Active;
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
            uint rate = 2;
            if (pc.Status.Additions.ContainsKey("fish"))
                rate = 6000;
            if (Global.Random.Next(0, 6000) < rate)
            {
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    EffectArg arg = new EffectArg();
                    MapClient client = MapClient.FromActorPC(pc);
                    if (pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability <= 0 || pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Durability > pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].maxDurability)
                    {
                        client.SendSystemMessage("武器[" + pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.name + "]损毁！");
                        /*Packets.Server.SSMG_ITEM_DELETE p2;
                        p2 = new Packets.Server.SSMG_ITEM_DELETE();
                        p2.InventorySlot = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot;
                        client.netIO.SendPacket(p2);*/
                        Item oriItem = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        client.ItemMoveSub(oriItem, ContainerType.BODY, oriItem.Stack);
                        //client.DeleteItem(pc.Inventory.LastItem.Slot, pc.Inventory.LastItem.Stack, true);
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
                //称号进程
                if (wornequip.EquipSlot.Count > 0)
                {
                    if (wornequip.EquipSlot[0] == EnumEquipSlot.UPPER_BODY)
                    {
                        client.TitleProccess(client.Character, 77, 1);
                        client.TitleProccess(client.Character, 78, 1);
                    }
                }

                //损坏装备
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
            if (sActor.Status.Additions.ContainsKey("Stun") || sActor.Status.Additions.ContainsKey("Sleep") || sActor.Status.Additions.ContainsKey("Frosen") ||
                sActor.Status.Additions.ContainsKey("Stone"))
                return;
            int combo = GetComboCount(sActor);
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Job == PC_JOB.GLADIATOR) //战士职业特性
                {
                    pc.SP += (uint)(50 * combo);
                    if (sActor.Status.Additions.ContainsKey("重锤火花"))
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                        {
                            SagaDB.Item.ItemType tp = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType;
                            if (tp == SagaDB.Item.ItemType.AXE || tp == SagaDB.Item.ItemType.HAMMER)
                                pc.SP += (uint)(50 * combo);
                        }
                    }
                    if (pc.SP > pc.MaxSP)
                        pc.SP = pc.MaxSP;
                    Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                }
                if (pc.Job == PC_JOB.HAWKEYE)
                {
                    if (pc.TInt["斥候远程模式"] != 1)
                    {
                        if (combo > 1)
                        {
                            switch (pc.TInt["狂乱之舞攻击次数LV"])
                            {
                                case 1:
                                    factor *= 0.45f;
                                    break;
                                case 2:
                                    factor *= 0.6f;
                                    break;
                                case 3:
                                    factor *= 0.75f;
                                    break;
                            }

                        }
                    }
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if ((pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.DUALGUN) && pc.TInt["斥候远程模式"] == 1)
                            factor *= 0.7f;
                        if ((pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.RIFLE) && pc.TInt["斥候远程模式"] == 1)
                            factor *= (1f - (0.15f * combo));
                        if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.TInt["斥候远程模式"] == 1 && sActor.Status.Additions.ContainsKey("自由射击"))
                            {
                                pc.MP += 1;
                                if (pc.MP > pc.MaxMP)
                                    pc.MP = pc.MaxMP;
                            } 
                        }
                    }
                    else
                        pc.TInt["斥候远程模式"] = 0;
                    if (pc.TInt["斥候远程模式"] != 1 && !sActor.Status.Additions.ContainsKey("自由射击"))
                    {
                        pc.TInt["回复SP"]++;
                        if (pc.TInt["回复SP"] >= 2)
                        {
                            pc.TInt["回复SP"] = 0;
                            pc.SP += 1;
                            if (pc.SP > pc.MaxSP)
                                pc.SP = pc.MaxSP;
                        }
                    }
                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                }
            }

            arg.sActor = sActor.ActorID;
            arg.dActor = dActor.ActorID;
            List<Actor> targets = new List<Actor>();
            for (int i = 0; i < combo; i++)
            {
                targets.Add(dActor);
            }
            arg.type = sActor.Status.attackType;
            arg.delayRate = 1f + combo * 0.05f;
            if (sActor.TInt["DamageRate"] > 0)
            {
                factor = sActor.TInt["DamageRate"] / 100f;
            }

            PhysicalAttack(sActor, targets, arg, Elements.Neutral, factor);
        }
        public void CriAttack(Actor sActor, Actor dActor, SkillArg arg)
        {

        }
        public int TryCast(Actor sActor, Actor dActor, SkillArg arg)
        {
            if (skillHandlers.ContainsKey(arg.skill.ID))
            {
                if (sActor.type == ActorType.PC)
                    return skillHandlers[arg.skill.ID].TryCast((ActorPC)sActor, dActor, arg);
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
            TitleEventManager.Instance.OnSkillCastComplete(sActor, dActor, arg);
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
                            if (pc.Job != PC_JOB.CARDINAL)
                                combo = 2;
                            else combo = 1;
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
                if (actor.Status.Additions.ContainsKey("速战诀"))
                    combo = 2;
                if (pc.Job == PC_JOB.HAWKEYE)
                {
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        if ((pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.RIFLE) && pc.TInt["斥候远程模式"] == 1)
                        {
                            if (pc.TInt["连射节奏当前次数"] < pc.TInt["连射节奏最大次数"])
                                pc.TInt["连射节奏当前次数"]++;
                            combo = (byte)pc.TInt["连射节奏当前次数"];
                            if (combo == 0) combo = 1;
                            if (!pc.Status.Additions.ContainsKey("连射节奏重置"))
                            {
                                OtherAddition 连射节奏重置 = new OtherAddition(null, pc, "连射节奏重置", 30000);
                                连射节奏重置.OnAdditionEnd += (s, e) =>
                                {
                                    if (pc.TInt["连射节奏当前次数"] > 0)
                                        pc.TInt["连射节奏当前次数"] = 0;
                                }
                                ; ApplyAddition(pc, 连射节奏重置);
                            }
                        }
                        if ((pc.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.DUALGUN) && pc.TInt["斥候远程模式"] == 1)
                            combo = 2;
                    }
                    if (pc.TInt["斥候远程模式"] != 1)
                    {
                        if (pc.TInt["狂乱之舞攻击次数"] != 0)
                            combo = (byte)pc.TInt["狂乱之舞攻击次数"];
                    }
                }
                return combo;
            }
            else
                return 1;
        }

        public void CastPassiveSkills(ActorPC pc)
        {
            var itemsToRemove = pc.Status.Additions.Where(kvp => kvp.Value.GetType() == typeof(DefaultPassiveSkill));
            foreach (var i in itemsToRemove)
                RemoveAddition(pc, pc.Status.Additions[i.Key]);


            //if (i.GetType() == typeof(DefaultPassiveSkill))
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

            foreach (SagaDB.Skill.Skill i in pc.SkillsEquip.Values)
            {
                if (i.BaseData.active == false)
                {
                    if (skillHandlers.ContainsKey(i.ID))
                    {
                        SkillArg arg = new SkillArg();
                        i.NoSave = true;
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
            }
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
        /// 为目标附加指定的addtion，自动在目标存在key时续时间。
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="addition"></param>
        public static void ApplyBuffAutoRenew(Actor actor, Addition addition)
        {
            if (!addition.Enabled) return;
            if (actor.type == ActorType.PC)
            {
                if (((ActorPC)actor).Fictitious) return;
            }
            if (actor.Status.Additions.ContainsKey(addition.Name))
            {
                DateTime NewTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, addition.RestLifeTime);
                if (addition.MyType == Addition.AdditionType.Other)
                    ((OtherAddition)addition).endTime = NewTime;
                if (addition.MyType == Addition.AdditionType.Debuff)
                    ((DefaultDeBuff)addition).endTime = NewTime;
                if (addition.MyType == Addition.AdditionType.Stable)
                {
                    ActorPC pc = actor as ActorPC;
                    ((StableAddition)addition).endTime = NewTime;
                    if (!pc.PendingAddtions.ContainsKey(addition.Name))
                        pc.PendingAddtions.Add(addition.Name, ((StableAddition)addition).endTime);
                    else
                        pc.PendingAddtions[addition.Name] = ((StableAddition)addition).endTime;
                }
            }
            else
            {
                if (addition.IfActivate)
                {
                    addition.AdditionStart();
                    addition.StartTime = DateTime.Now;
                    addition.Activated = true;
                }
                ClientManager.EnterCriticalArea();
                if (!actor.Status.Additions.ContainsKey(addition.Name))
                    actor.Status.Additions.Add(addition.Name, addition);

                if (addition.MyType == Addition.AdditionType.Stable && actor.type == ActorType.PC)
                {
                    ActorPC pc = actor as ActorPC;
                    if (!pc.PendingAddtions.ContainsKey(addition.Name))
                        pc.PendingAddtions.Add(addition.Name, ((StableAddition)addition).endTime);
                    else
                        pc.PendingAddtions[addition.Name] = ((StableAddition)addition).endTime;
                }
                ClientManager.LeaveCriticalArea();
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
                return;
                /*Addition oldaddition = actor.Status.Additions[addition.Name];
                if(oldaddition.MyType== Addition.AdditionType.Buff)
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
                if (oldaddition.MyType == Addition.AdditionType.Debuff)
                {
                    DefaultDeBuff oldbuff = (DefaultDeBuff)oldaddition;
                    DefaultDeBuff newbuff = (DefaultDeBuff)addition;
                    if (oldbuff.Variable.ContainsKey(addition.Name) || newbuff.Variable.ContainsKey(addition.Name))
                    {
                        if (oldbuff.Variable[addition.Name] == newbuff.Variable[addition.Name])
                        {
                            oldbuff.TotalLifeTime += addition.TotalLifeTime;
                            return;
                        }
                    }
                }
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
                    ClientManager.LeaveCriticalArea();*/
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

                //让StableAddition与PendingAddtions共存亡！但不必担心end后PendingAddtions还存在的问题，因为过时的addition不会再存入数据库。
                if (addition.MyType == Addition.AdditionType.Stable && actor.type == ActorType.PC)
                {
                    ActorPC pc = actor as ActorPC;
                    if (!pc.PendingAddtions.ContainsKey(addition.Name))
                        pc.PendingAddtions.Add(addition.Name, ((StableAddition)addition).endTime);
                    else
                        pc.PendingAddtions[addition.Name] = ((StableAddition)addition).endTime;
                }

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
        public void PushBack(Actor ori, Actor dest, int step)
        {
            PushBack(ori, dest, step, 3000);
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
        /// 取得范围队友
        /// </summary>
        /// <param name="sActor"></param>
        /// <param name="range"></param>
        /// <param name="includeSourceActor"></param>
        /// <returns></returns>
        public List<ActorPC> GetPartyMembersAround(Actor sActor, short range,bool includeSourceActor = true)
        {
            List<ActorPC> pcs = new List<ActorPC>();
            if (sActor.type != ActorType.PC)
                return pcs;
            ActorPC pc = sActor as ActorPC;
            if (pc.Party == null)
                pcs.Add(pc);
            else
            {
                Map map = GetActorMap(sActor);
                List<Actor> actors = map.GetActorsArea(sActor, range, includeSourceActor);
                foreach (var item in actors)
                {
                    if (item.type == ActorType.PC && ((ActorPC)item).Party == pc.Party && ((ActorPC)item).Online && !item.Buff.Dead)
                        pcs.Add((ActorPC)item);
                }
            }
            return pcs;
        }
        /// <summary>
        /// 根据坐标返回指定范围内可攻击的目标
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<Actor> GetAreaActorByPosWhoCanBeAttackedTargets(Actor caster,byte x, byte y,short range)
        {
            List<Actor> actors = new List<Actor>();
            Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
            actors = map.GetActorsArea(Global.PosX8to16(x, map.Width), Global.PosY8to16(y, map.Height), range);
            return GetVaildAttackTarget(caster, actors);
        }

        /// <summary>
        /// 返回范围内可被攻击的对象
        /// </summary>
        /// <param name="caster">实际攻击者</param>
        /// <param name="actor">计算范围的实体</param>
        /// <param name="range">范围</param>
        /// <returns>可被攻击的对象</returns>
        public List<Actor> GetActorsAreaWhoCanBeAttackedTargets(Actor caster, Actor actor, short range,bool includeSourceActor = false)
        {
            List<Actor> actors = new List<Actor>();
            Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
            return GetVaildAttackTarget(caster, map.GetActorsArea(actor, range, includeSourceActor));
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
