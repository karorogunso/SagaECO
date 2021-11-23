using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.NewBoss
{
    public class WaterBall : ISkill
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
            /*
            uint NextSkillID = 22000;
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillID, 1, 0));
            */
            
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {

            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 0.1f;
            int count = 0;
            int maxcount = 30;
            Actor last;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                last = actor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(22009, 1);
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 400;
                this.dueTime = 0;
                ActorPC Me = (ActorPC)caster;

            }



            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        List<Actor> actors = map.GetRoundAreaActors(last.X, last.Y, 500);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
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
                            SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Water, factor);

                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, last, false);
                            last = affected[0];
                            count++;
                        }
                        else
                            count = maxcount;
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
        #endregion
    }
}
