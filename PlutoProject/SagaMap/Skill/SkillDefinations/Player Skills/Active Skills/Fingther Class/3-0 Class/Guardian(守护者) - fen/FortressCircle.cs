using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.Guardian
{
    /// <summary>
    /// フォートレスサークル
    /// </summary>
    public class FortressCircle : ISkill
    {
        #region ISkill Members
        public List<int> range = new List<int>();
        //public FortressCircle()
        //{

        //}
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            //Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (CheckPossible(pc))
                return 0;
            else
                return -5;
        }

        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SPEAR ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER ||
                        pc.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return true;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //wiki没写明威力,先假定满级是350%
            float factor = 1.0f + 0.5f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定范围200,也就是以玩家为中心5*5范围
            List<Actor> affected = map.GetActorsArea(sActor, 200, false);
            List<Actor> realAffected = new List<Actor>();
            range.Add(SkillHandler.Instance.CalcPosHashCode(1, 0, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(2, 0, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(-1, 0, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(-2, 0, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(1, 1, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(0, 1, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(-1, 1, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(0, 2, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(1, -1, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(0, -1, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(-1, -1, 2));
            range.Add(SkillHandler.Instance.CalcPosHashCode(0, -2, 2));
            foreach (Actor act in affected)
            {

                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    int XDiff, YDiff;
                    SkillHandler.Instance.GetXYDiff(map, sActor, act, out XDiff, out YDiff);
                    if (range.Contains(SkillHandler.Instance.CalcPosHashCode(XDiff, YDiff, 2)))
                    {
                        realAffected.Add(act);
                        SkillHandler.Instance.PushBack(sActor, act, 4);
                    }
                }
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, factor);
            //SkillArg args2 = args.Clone();
            //SkillHandler.Instance.MagicAttack(sActor, realAffected, args2, SagaLib.Elements.Earth, factor);
            //args.AddSameActor(args2);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2536, level, 0));
            //后续治疗结界逻辑有错误,改用多重写法
            ////创建设置型技能技能体
            //ActorSkill actor = new ActorSkill(args.skill, sActor);
            ////Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ////设定技能体位置
            //actor.MapID = sActor.MapID;
            ////actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            ////actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            ////更改设定位置为玩家本身位置
            //actor.X = sActor.X;
            //actor.Y = sActor.Y;
            ////设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            //actor.e = new ActorEventHandlers.NullEventHandler();
            ////在指定地图注册技能体Actor
            //map.RegisterActor(actor);
            ////设置Actor隐身属性为非
            //actor.invisble = false;
            ////广播隐身属性改变事件，以便让玩家看到技能体
            //map.OnActorVisibilityChange(actor);
            ////設置系
            //actor.Stackable = false;
            ////创建技能效果处理对象
            //SkillArg args3 = args.Clone();
            //Activator timer = new Activator(sActor, actor, args3, level);
            //args.AddSameActor(args3);
            //timer.Activate();
        }

        #endregion

        #region Timer

        //private class Activator : MultiRunTask
        //{
        //    ActorSkill actor;
        //    Actor caster;
        //    SkillArg skill;
        //    Map map;
        //    float[] factors = new float[] { 0f, -0.2f, -0.4f, -0.1f, -0.4f, -0.5f, -100f };
        //    float factor = 0f;
        //    int countMax = 13, count = 0, lifetime = 0;
        //    int[] lifetimes = new int[] { 0, 5000, 5000, 8000, 10000, 13000 };

        //    public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
        //    {
        //        this.actor = actor;
        //        this.caster = caster;
        //        this.skill = args.Clone();
        //        map = Manager.MapManager.Instance.GetMap(actor.MapID);
        //        int[] periods = new int[] { 0, 1000, 1000, 500, 1000, 1000, 100 };
        //        lifetime = lifetimes[level];
        //        //百分比治疗不该受到任何提升
        //        //factor = factors[level] + caster.Status.Cardinal_Rank;
        //        factor = factors[level] * this.caster.MaxHP;
        //        this.period = periods[level];
        //        this.dueTime = 0;

        //    }

        //    public override void CallBack()
        //    {
        //        //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
        //        ClientManager.EnterCriticalArea();
        //        try
        //        {
        //            if (count < countMax)
        //            {
        //                //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
        //                List<Actor> actors = map.GetActorsArea(actor, 200, false);
        //                List<Actor> affected = new List<Actor>();
        //                //取得有效Actor（即怪物）

        //                //施加光属性魔法伤害
        //                skill.affectedActors.Clear();
        //                foreach (Actor i in actors)
        //                {
        //                    if (i is ActorPC)
        //                        if (!SkillHandler.Instance.CheckValidAttackTarget(caster, i))
        //                        {
        //                            affected.Add(i);
        //                        }


        //                }
        //                SkillHandler.Instance.MagicAttack(caster, affected, skill, SkillHandler.DefType.IgnoreAll, Elements.Holy, factor);
        //                foreach (var item in affected)
        //                {
        //                    if (item is ActorPC)
        //                    {
        //                        ActorPC pc = item as ActorPC;
        //                        if (!pc.Status.Additions.ContainsKey("GospelBonus"))
        //                        {
        //                            DefaultBuff gospelBonus = new DefaultBuff(skill.skill, item, "GospelBonus", lifetime);
        //                            gospelBonus.OnAdditionStart += gospelBonus_OnAdditionStart;
        //                            gospelBonus.OnAdditionEnd += gospelBonus_OnAdditionEnd;
        //                            SkillHandler.ApplyAddition(item, gospelBonus);
        //                        }
        //                    }
        //                }

        //                //广播技能效果
        //                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
        //                count++;
        //            }
        //            else
        //            {
        //                this.Deactivate();
        //                //在指定地图删除技能体（技能效果结束）
        //                map.DeleteActor(actor);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.ShowError(ex);
        //        }
        //        //解开同步锁
        //        ClientManager.LeaveCriticalArea();
        //    }

        //    void gospelBonus_OnAdditionStart(Actor actor, DefaultBuff skill)
        //    {
        //        //该技能治疗不存在任何buff
        //        //int level = skill.skill.Level;
        //        //int max_atk1_add = (int)(30 + 30 * skill.skill.Level);
        //        //int max_atk2_add = (int)(30 + 30 * skill.skill.Level);
        //        //int max_atk3_add = (int)(30 + 30 * skill.skill.Level);
        //        //int min_atk1_add = (int)(30 + 30 * skill.skill.Level);
        //        //int min_atk2_add = (int)(30 + 30 * skill.skill.Level);
        //        //int min_atk3_add = (int)(30 + 30 * skill.skill.Level);
        //        //int max_matk_add = (int)(30 + 30 * skill.skill.Level);
        //        //int min_matk_add = (int)(30 + 30 * skill.skill.Level);
        //        //int def_add = 30 + 10 * level;
        //        //int mdef_add = 30 + 10 * level;
        //        //uint hpbonus = (uint)((float)actor.MaxHP * (((float)(10 + 5 * level) / 100.0f)));
        //        //uint mpbonus = (uint)((float)actor.MaxMP * (((float)(10 + 5 * level) / 100.0f)));
        //        //uint spbonus = (uint)((float)actor.MaxSP * (((float)(10 + 5 * level) / 100.0f)));

        //        //if (skill.Variable.ContainsKey("GospelBonusHP"))
        //        //    skill.Variable.Remove("GospelBonusHP");
        //        //skill.Variable.Add("GospelBonusHP", (int)hpbonus);
        //        //actor.Status.hp_skill += (short)hpbonus;

        //        //if (skill.Variable.ContainsKey("GospelBonusMP"))
        //        //    skill.Variable.Remove("GospelBonusMP");
        //        //skill.Variable.Add("GospelBonusMP", (int)mpbonus);
        //        //actor.Status.mp_skill += (short)mpbonus;

        //        //if (skill.Variable.ContainsKey("GospelBonusSP"))
        //        //    skill.Variable.Remove("GospelBonusSP");
        //        //skill.Variable.Add("GospelBonusSP", (int)spbonus);
        //        //actor.Status.sp_skill += (short)spbonus;

        //        ////大傷
        //        //if (skill.Variable.ContainsKey("GospelMax_ATK1"))
        //        //    skill.Variable.Remove("GospelBonus_Max_ATK1");
        //        //skill.Variable.Add("GospelBonus_Max_ATK1", max_atk1_add);
        //        //actor.Status.max_atk1_skill += (short)max_atk1_add;

        //        //if (skill.Variable.ContainsKey("GospelBonus_Max_ATK2"))
        //        //    skill.Variable.Remove("GospelBonus_Max_ATK2");
        //        //skill.Variable.Add("GospelBonus_Max_ATK2", max_atk2_add);
        //        //actor.Status.max_atk2_skill += (short)max_atk2_add;

        //        //if (skill.Variable.ContainsKey("GospelBonus_Max_ATK3"))
        //        //    skill.Variable.Remove("GospelBonus_Max_ATK3");
        //        //skill.Variable.Add("GospelBonus_Max_ATK3", max_atk3_add);
        //        //actor.Status.max_atk3_skill += (short)max_atk3_add;

        //        ////小伤
        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_ATK1"))
        //        //    skill.Variable.Remove("GospelBonus_Min_ATK1");
        //        //skill.Variable.Add("GospelBonus_Min_ATK1", min_atk1_add);
        //        //actor.Status.min_atk1_skill += (short)min_atk1_add;

        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_ATK2"))
        //        //    skill.Variable.Remove("GospelBonus_Min_ATK2");
        //        //skill.Variable.Add("GospelBonus_Min_ATK2", min_atk2_add);
        //        //actor.Status.min_atk2_skill += (short)min_atk2_add;

        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_ATK3"))
        //        //    skill.Variable.Remove("GospelBonus_Min_ATK3");
        //        //skill.Variable.Add("GospelBonus_Min_ATK3", min_atk3_add);
        //        //actor.Status.min_atk3_skill += (short)min_atk3_add;
        //        ////魔伤
        //        //if (skill.Variable.ContainsKey("GospelBonus_Max_MATK"))
        //        //    skill.Variable.Remove("GospelBonus_Max_MATK");
        //        //skill.Variable.Add("GospelBonus_Max_MATK", max_matk_add);
        //        //actor.Status.max_matk_skill += (short)max_matk_add;

        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_MATK"))
        //        //    skill.Variable.Remove("GospelBonus_Min_MATK");
        //        //skill.Variable.Add("GospelBonus_Min_MATK", min_matk_add);
        //        //actor.Status.min_matk_skill += (short)min_matk_add;

        //        ////防御
        //        //if (skill.Variable.ContainsKey("GospelBonus_Def"))
        //        //    skill.Variable.Remove("GospelBonus_Def");
        //        //skill.Variable.Add("GospelBonus_Def", def_add);
        //        //actor.Status.def_add_skill += (short)def_add;

        //        //if (skill.Variable.ContainsKey("GospelBonus_MDef"))
        //        //    skill.Variable.Remove("GospelBonus_MDef");
        //        //skill.Variable.Add("GospelBonus_MDef", mdef_add);
        //        //actor.Status.mdef_add_skill += (short)mdef_add;

        //        //actor.Buff.MaxAtkUp = true;
        //        //actor.Buff.MaxMagicAtkUp = true;
        //        //actor.Buff.MinAtkUp = true;
        //        //actor.Buff.MinMagicAtkUp = true;
        //        //actor.Buff.MaxHPUp = true;
        //        //actor.Buff.MaxSPUp = true;
        //        //actor.Buff.MaxMPUp = true;
        //        //actor.Buff.DefUp = true;
        //        //actor.Buff.MagicDefUp = true;

        //        //PC.StatusFactory.Instance.CalcHPMPSP((ActorPC)actor);
        //        //Network.Client.MapClient.FromActorPC((ActorPC)actor).SendPlayerInfo();
        //        Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        //    }

        //    void gospelBonus_OnAdditionEnd(Actor actor, DefaultBuff skill)
        //    {
        //        //同样也不会存在任何需要取消的效果
        //        //actor.Status.hp_skill -= (short)skill.Variable["GospelBonusHP"];
        //        //if (skill.Variable.ContainsKey("GospelBonusHP"))
        //        //    skill.Variable.Remove("GospelBonusHP");

        //        //actor.Status.mp_skill -= (short)skill.Variable["GospelBonusMP"];
        //        //if (skill.Variable.ContainsKey("GospelBonusMP"))
        //        //    skill.Variable.Remove("GospelBonusMP");

        //        //actor.Status.sp_skill -= (short)skill.Variable["GospelBonusSP"];
        //        //if (skill.Variable.ContainsKey("GospelBonusSP"))
        //        //    skill.Variable.Remove("GospelBonusSP");

        //        ////大傷
        //        //actor.Status.max_atk1_skill -= (short)skill.Variable["GospelBonus_Max_ATK1"];
        //        //if (skill.Variable.ContainsKey("GospelMax_ATK1"))
        //        //    skill.Variable.Remove("GospelBonus_Max_ATK1");
        //        //actor.Status.max_atk2_skill -= (short)skill.Variable["GospelBonus_Max_ATK2"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Max_ATK2"))
        //        //    skill.Variable.Remove("GospelBonus_Max_ATK2");
        //        //actor.Status.max_atk3_skill -= (short)skill.Variable["GospelBonus_Max_ATK3"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Max_ATK3"))
        //        //    skill.Variable.Remove("GospelBonus_Max_ATK3");

        //        ////小傷
        //        //actor.Status.min_atk1_skill -= (short)skill.Variable["GospelBonus_Min_ATK1"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_ATK1"))
        //        //    skill.Variable.Remove("GospelBonus_Min_ATK1");
        //        //actor.Status.min_atk2_skill -= (short)skill.Variable["GospelBonus_Min_ATK2"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_ATK2"))
        //        //    skill.Variable.Remove("GospelBonus_Min_ATK2");
        //        //actor.Status.min_atk3_skill -= (short)skill.Variable["GospelBonus_Min_ATK3"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_ATK3"))
        //        //    skill.Variable.Remove("GospelBonus_Min_ATK3");

        //        ////魔伤
        //        //actor.Status.max_matk_skill -= (short)skill.Variable["GospelBonus_Max_MATK"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Max_MATK"))
        //        //    skill.Variable.Remove("GospelBonus_Max_MATK");
        //        //actor.Status.min_matk_skill -= (short)skill.Variable["GospelBonus_Min_MATK"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Min_MATK"))
        //        //    skill.Variable.Remove("GospelBonus_Min_MATK");

        //        ////防御
        //        //actor.Status.def_add_skill -= (short)skill.Variable["GospelBonus_Def"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_Def"))
        //        //    skill.Variable.Remove("GospelBonus_Def");
        //        //actor.Status.mdef_add_skill -= (short)skill.Variable["GospelBonus_MDef"];
        //        //if (skill.Variable.ContainsKey("GospelBonus_MDef"))
        //        //    skill.Variable.Remove("GospelBonus_MDef");

        //        //actor.Buff.MaxAtkUp = false;
        //        //actor.Buff.MaxMagicAtkUp = false;
        //        //actor.Buff.MinAtkUp = false;
        //        //actor.Buff.MinMagicAtkUp = false;
        //        //actor.Buff.MaxHPUp = false;
        //        //actor.Buff.MaxSPUp = false;
        //        //actor.Buff.MaxMPUp = false;
        //        //actor.Buff.DefUp = false;
        //        //actor.Buff.MagicDefUp = false;

        //        //PC.StatusFactory.Instance.CalcHPMPSP((ActorPC)actor);
        //        //Network.Client.MapClient.FromActorPC((ActorPC)actor).SendPlayerInfo();
        //        Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        //    }
        //}
        #endregion
    }
}
