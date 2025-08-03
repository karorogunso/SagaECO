using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class HolyHealing : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("HolyHealing"))
            {
                return -30;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillCD cd = new SkillCD(args.skill, sActor, "HolyHealing", 30000);
            SkillHandler.ApplyAddition(sActor, cd);

            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
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
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();
            EffectArg arg = new EffectArg();
            arg.effectID = 5065;
            arg.actorID = 0xFFFFFFFF;
            arg.x = args.x;
            arg.y = args.y;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
        }
        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 0.7f;
            int countMax = 10, count = 0;
            //int TotalLv = 0;


            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 2000;
                this.dueTime = 1500;
                this.factor += 0.1f * level;


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
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor（即怪物）

                        //施加火属性魔法伤害
                        skill.affectedActors.Clear();
                        ActorPC Me = (ActorPC)caster;

                        foreach (Actor i in actors)
                        {
                            if (i.type == Me.type)
                            {
                                affected.Add(i);
                                map.SendEffect(i, 5263);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, SkillHandler.DefType.IgnoreAll, Elements.Holy, -factor);

                        //广播技能效果
                        skill.dActor = caster.ActorID;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, true);
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
        }

    }
}
