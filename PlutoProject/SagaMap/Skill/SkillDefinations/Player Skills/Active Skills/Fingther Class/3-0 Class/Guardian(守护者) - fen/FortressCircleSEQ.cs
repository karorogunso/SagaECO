using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Scripting;

namespace SagaMap.Skill.SkillDefinations.Guardian
{
    /// <summary>
    /// フォートレスサークル
    /// </summary>
    public class FortressCircleSEQ : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
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
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            //actor.X = SagaLib.Global.PosX8to16((byte)sActor.X, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16((byte)sActor.Y, map.Height);
            //更改设定位置为玩家本身位置
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = false;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.OnActorVisibilityChange(actor);
            //設置系
            actor.Stackable = false;
            //创建技能效果处理对象
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();


        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            byte skilllevel;

            float[] factors = new float[] { 0f, 0.02f, 0.04f, 0.01f, 0.04f, 0.05f, 100f };//治疗量=(使用者的)百分比比例
            float factor = 0f;
            int countMax = 13, count = 0, lifetime = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                skilllevel = level;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                int[] periods = new int[] { 0, 1000, 1000, 500, 1000, 1000, 100 };//回复间隔
                int[] lifetimes = new int[] { 0, 5, 5, 8, 8, 13, 100 };
                lifetime = 1000 * lifetimes[level];//持续时间
                //factor = factors[level] + caster.Status.Cardinal_Rank;
                factor = caster.MaxHP * factors[level];
                ActorPC pc = caster as ActorPC;
                if (pc.Skills2_1.ContainsKey(3170) || pc.DualJobSkill.Exists(x => x.ID == 3170))
                {
                    //这里取副职的加成技能专精等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 3170))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3170).Level;

                    //这里取主职的加成技能等级
                    var mainlv = 0;
                    if (pc.Skills2_1.ContainsKey(3170))
                        mainlv = pc.Skills2_1[3170].Level;

                    //这里取等级最高的加成技能等级用来做倍率加成
                    int maxlv = Math.Max(duallv, mainlv);
                    //ParryResult += pc.Skills[116].Level * 3;
                    factor += caster.MaxHP * 0.01f * maxlv;
                }
                this.period = periods[level];
                this.dueTime = 0;

            }

            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, 200, false);
                        List<Actor> affected = new List<Actor>();
                        SkillArg args = this.skill;
                        //取得有效Actor（即怪物）

                        //施加火属性魔法伤害
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (i is ActorPC)
                                if (!SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                {

                                    if (i.Buff.NoRegen)
                                        return;

                                    i.HP += (uint)factor;
                                    int[] totals = new int[] { 0, 6, 6, 9, 9, 14, 100 };
                                    int lifetime = 1000 * (totals[skilllevel]);
                                    if (caster == i && !i.Status.Additions.ContainsKey("FortressCircleSEQ"))
                                    {
                                        DefaultBuff skill = new DefaultBuff(args.skill, i, "FortressCircleSEQ", lifetime);
                                        skill.OnAdditionStart += this.StartEventHandler;
                                        skill.OnAdditionEnd += this.EndEventHandler;
                                        SkillHandler.ApplyAddition(i, skill);
                                    }
                                    if (i.HP > i.MaxHP)
                                        i.HP = i.MaxHP;
                                    //args.affectedActors.Add(i);
                                    //args.Init();
                                    //if (args.flag.Count > 0)
                                    //{
                                    //    args.flag[0] |= SagaLib.AttackFlag.HP_HEAL | SagaLib.AttackFlag.NO_DAMAGE;
                                    //    args.hp[0] = ((int)factor);
                                    //}
                                    SkillHandler.Instance.ShowVessel(i, (int)(-factor));
                                    Map map = Manager.MapManager.Instance.GetMap(i.MapID);
                                    SkillHandler.Instance.ShowEffect(map, i, 5262);
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, i, true);
                                }

                        }

                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                        //在指定地图删除技能体（技能效果结束）
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁ClientManager.LeaveCriticalArea();
            }


            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                //actor.Speed = 0;
                //SkillArg args =  this.skill;
                int[] lifetimes = new int[] { 0, 5, 5, 8, 8, 13, 100 };
                int lifetime = 1000 * lifetimes[skill.skill.Level];

                int[] times = { 0, 5, 5, 6, 6, 7 };
                skill["MobKyrie"] = times[skill.skill.Level];
                if (actor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)actor;
                    Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.SKILL_STATUS_ENTER, skill.skill.Name));
                }
                uint physicaldef = 100;//物理防御比
                uint magicdef = 100;//魔法防御比

                if (skill.Variable.ContainsKey("PHYSICALDEF"))
                    skill.Variable.Remove("PHYSICALDEF");
                skill.Variable.Add("PHYSICALDEF", (int)physicaldef);
                actor.Status.PhysiceReduceRate += (short)physicaldef;

                if (skill.Variable.ContainsKey("MAGICDEF"))
                    skill.Variable.Remove("MAGICDEF");
                skill.Variable.Add("MAGICDEF", (int)magicdef);
                actor.Status.MagicRuduceRate += (short)magicdef;
                Stone skill2 = new SagaMap.Skill.Additions.Global.Stone(skill.skill, actor, 14000);
                actor.Buff.Stone = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }

            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                //SkillArg args = this.skill;
                RemoveAddition(actor, "Stone");

                actor.Status.PhysiceReduceRate -= (short)skill.Variable["PHYSICALDEF"];
                if (skill.Variable.ContainsKey("PHYSICALDEF"))
                    skill.Variable.Remove("PHYSICALDEF");
                actor.Status.MagicRuduceRate -= (short)skill.Variable["MAGICDEF"];
                if (skill.Variable.ContainsKey("MAGICDEF"))
                    skill.Variable.Remove("MAGICDEF");

                actor.Buff.Stone = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }

            public void RemoveAddition(Actor actor, String additionName)
            {
                if (actor.Status.Additions.ContainsKey(additionName))
                {
                    Addition addition = actor.Status.Additions[additionName];
                    actor.Status.Additions.Remove(additionName);
                    if (addition.Activated)
                    {
                        addition.AdditionEnd();
                    }
                    addition.Activated = false;
                }
            }
            #endregion
        }
    }
}
