using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30020 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
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
            float factor = 1.0f;
            int countMax = 10, count = 0;
            int TotalLv = 0;
            byte x, y;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 300;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        count++;
                    }
                    else
                    {
                        if (caster.type == ActorType.MOB)
                            SkillHandler.Instance.ActorSpeak(caster, "你这么厉害！本小姐可不是吃素的哦，尝尝我的血魔枪吧！");
                        ActorSkill actor2 = new ActorSkill(skill.skill, caster);
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        //设定技能体位置
                        actor2.MapID = caster.MapID;
                        actor2.X = actor.X;
                        actor2.Y = actor.Y;
                        //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
                        actor2.e = new ActorEventHandlers.NullEventHandler();
                        //在指定地图注册技能体Actor
                        map.RegisterActor(actor2);
                        //设置Actor隐身属性为非
                        actor2.invisble = false;
                        //广播隐身属性改变事件，以便让玩家看到技能体
                        map.OnActorVisibilityChange(actor);
                        //設置系
                        actor2.Stackable = false;
                        //创建技能效果处理对象
                        Activator2 timer = new Activator2(caster, actor2, skill, 1);
                        timer.Activate();
                        this.Deactivate();
                        //在指定地图删除技能体（技能效果结束）
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
        private class Activator2 : MultiRunTask
        {

            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 1.0f;
            int Xcount = 0;
            byte x, y;
            public Activator2(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                x = SagaLib.Global.PosX16to8(actor.X, map.Width);
                y = SagaLib.Global.PosY16to8(actor.Y, map.Height);
                this.skill.x = x;
                this.skill.y = y;
                this.skill.dActor = 0xffffffff;
                this.period = 150;
                this.dueTime = 0;

            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (Xcount < 15)
                    {
                        for (int j = -Xcount; j <= Xcount; j++)
                        {
                            for (int k = -Xcount; k <= Xcount; k++)
                            {
                                if (j * j + k * k <= Xcount * Xcount
                                    && j * j + k * k > (Xcount - 1) * (Xcount - 1)
                                    && (j + k) % 2 == 0 && j * 3 % Xcount == 0)//多了会卡
                                {
                                    SkillArg s = this.skill.Clone();
                                    s.x = (byte)(x + j);
                                    s.y = (byte)(y + k);
                                    EffectArg arg = new EffectArg();
                                    arg.effectID = 5376;
                                    arg.actorID = 0xFFFFFFFF;
                                    arg.x = s.x;
                                    arg.y = s.y;
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, actor, true);
                                    List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(s.x, map.Width), SagaLib.Global.PosY8to16(s.y, map.Height), 300);
                                    List<Actor> affected = new List<Actor>();
                                    s.affectedActors.Clear();
                                    foreach (Actor i in actors)
                                    {
                                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                        {
                                            int damage = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Fire, 0, 8f);
                                            SkillHandler.Instance.CauseDamage(caster, i, damage);
                                            SkillHandler.Instance.ShowVessel(i, damage);
                                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(i.MapID), i, 8011);
                                            //炎鬼缠身 sc2 = new 炎鬼缠身(skill.skill, caster, i, 10000, 0);
                                            //SkillHandler.ApplyAddition(i, sc2);
                                        }
                                    }

                                }
                            }
                        }
                        Xcount++;
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
                    Deactivate();
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
