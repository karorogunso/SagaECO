
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 黑夜降临：超大范围全体附加暗防御属性降低闪避 降低闪避未实装
    /// </summary>
    public class S43106 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(pc.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                {
                    return 0;
                }
                return -2;
            }
            else
            {
                return -2;
            }
        }
        Activator timer;
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(dActor, 700, true);
            ActorPC me = (ActorPC)sActor;
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
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
            timer = new Activator(sActor, actor, args, level);
            timer.Activate();

        }
        #endregion
        private class Activator : MultiRunTask
        {
            public ActorSkill actor;
            Actor caster;
            SkillArg skill;
            public Map map;
            int countMax = 3, count = 0;
            int lifeTime = 0;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;
                this.lifeTime = 10000;
                countMax = lifeTime / period;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, 300, true);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor

                        skill.affectedActors.Clear();
                        foreach (Actor act in actors)
                        {
                            if (act.type == ActorType.PC || act.type == ActorType.PET)
                            {
                                ActorPC m = (ActorPC)act;
                                if (!m.Status.Additions.ContainsKey("ShieldDark"))
                                {
                                    ShieldDark sd = new ShieldDark(skill.skill, m, lifeTime - count * period);
                                    SkillHandler.ApplyAddition(m, sd);
                                }
                            }
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, act))
                            {
                                if (act.Darks != 1)
                                {
                                    Manager.MapManager.Instance.GetMap(caster.MapID).SendEffect(act, 5081);
                                    act.Darks = 1;
                                }
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
                //解开同步锁
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
            void TimerEventHandler(Actor actor, DefaultBuff skill)
            {
                if (!SkillHandler.Instance.isInRange(this.actor, actor, 200))
                {
                    skill.AdditionEnd();
                }
            }
        }
    }
}
