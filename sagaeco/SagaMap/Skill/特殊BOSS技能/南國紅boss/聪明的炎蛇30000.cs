using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30000 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 6f;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
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
            Activa timer = new Activa(sActor, dActor, actor, args, level);
            timer.Activate();
        }
        public static Actor last;
        private class Activa : MultiRunTask
        {
            
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 3f;
            int count = 0;
            int maxcount = 10;

            public Activa(Actor caster, Actor dactor, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                last = dactor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(30001, 1);
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 3000;

            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        List<Actor> actors = map.GetRoundAreaActors(last.X, last.Y, 500,false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        if (count != 5)
                        {
                            foreach (Actor i in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i) && i != last)
                                {
                                    affected.Add(i);
                                    break;
                                }
                            }
                            if (affected.Count != 0)
                            {
                                SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Fire, factor);

                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, last, false);
                                last = affected[0];
                                count++;
                            }
                            else
                                count = maxcount;
                        }
                        else
                        {
                            actors = map.GetRoundAreaActors(last.X, last.Y, 800, false);
                            foreach (Actor i in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, i) && i != last)
                                {
                                    FireAT at = new FireAT(skill.skill, caster, i, 2500, 0, skill);
                                    SkillHandler.ApplyAddition(i, at);
                                }
                            }
                            map.SendEffect(last, 4031);
                            count = maxcount;
                        }
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
                ClientManager.LeaveCriticalArea();
            }
        }

        class FireAT : DefaultBuff
        {
            int count = 0;
            public FireAT(SagaDB.Skill.Skill skill, Actor sActor, Actor dActor, int lifetime, int damage, SkillArg arg)
                : base(skill, sActor, dActor, "FireAT", lifetime, 200, damage, arg)
            {

                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this.OnUpdate2 += this.TimerUpdate;

            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {

            }

            void TimerUpdate(Actor sActor, Actor dActor, DefaultBuff skill, SkillArg arg, int damage)
            {
                int maxcount = 3;
                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        float factor = 6f;
                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            SkillHandler.Instance.MagicAttack(sActor, dActor, arg, Elements.Fire, factor);
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, last, false);
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
