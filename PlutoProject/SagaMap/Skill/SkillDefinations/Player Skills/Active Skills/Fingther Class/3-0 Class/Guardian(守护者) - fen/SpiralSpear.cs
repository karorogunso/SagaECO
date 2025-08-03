﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Guardian
{
    /// <summary>
    /// スパイラルスピア
    /// </summary>
    public class SpiralSpear : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            short range = new short[] { 150, 150, 150, 250, 150, 250 }[args.skill.Level];
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.CheckActorSkillInRange(dActor.X, dActor.Y, range))
            {
                return -17;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = dActor.MapID;
            actor.X = dActor.X;
            actor.Y = dActor.Y;
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
            Activator timer = new Activator(sActor, dActor, actor, args, level);
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
            int countMax = 0, count = 0;
            short range = 0;
            float factor = 0;
            int lifetime = 0;

            public Activator(Actor caster, Actor theDActor, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;

                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.dueTime = 0;
                countMax = new int[] { 0, 4, 4, 5, 7, 12 }[level];
                factor = 1.3f + 0.2f * level;
                this.lifetime = 2500;
                this.period = lifetime / countMax;
                this.range = new short[] { 150, 150, 150, 250, 150, 250 }[level];
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, range, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor（即怪物）

                        //施加魔法伤害
                        skill.affectedActors.Clear();

                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {

                                //int elements = 0;
                                //if (caster.WeaponElement != SagaLib.Elements.Neutral)
                                //{
                                //    elements = caster.Status.attackElements_item[caster.WeaponElement]
                                //                        + caster.Status.attackElements_skill[caster.WeaponElement]
                                //                        + caster.Status.attackelements_iris[caster.WeaponElement];
                                //}
                                //else
                                //{
                                //    elements = 0;
                                //}
                                //int damaga = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.Def, caster.WeaponElement, elements, factor);
                                //SkillHandler.Instance.CauseDamage(caster, i, damaga);
                                //SkillHandler.Instance.ShowVessel(i, damaga);

                                affected.Add(i);
                                //SkillHandler.Instance.PhysicalAttack(caster, i, skill, caster.WeaponElement, factor);
                            }
                        }

                        SkillHandler.Instance.PhysicalAttack(caster, affected, skill, caster.WeaponElement, factor);

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
                //解开同步锁
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
