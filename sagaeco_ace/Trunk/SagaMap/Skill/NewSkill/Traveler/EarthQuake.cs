using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Traveler
{
    public class EarthQuake : ISkill
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
            float factor = 1.0f;
            int count = 0;
            byte x, y;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(23006,1);
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                x = SagaLib.Global.PosX16to8(actor.X, map.Width);
                y = SagaLib.Global.PosY16to8(actor.Y, map.Height);
                this.skill.x = x;
                this.skill.y = y;
                this.skill.dActor = 0xffffffff;
                this.period = 350;
                this.dueTime = 0;
                ActorPC Me = (ActorPC)caster;

            }



            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count < 15)
                    {
                        for (int j = -count; j <= count; j++)
                        {
                            for (int k = -count; k <= count; k++)
                            {
                                if (j * j + k * k <= count * count
                                    && j * j + k * k > (count - 1) * (count - 1)
                                    && (j+k)%2==0)//多了会卡
                                {
                                    SkillArg s = this.skill.Clone();
                                    s.x = (byte)(x + j);
                                    s.y = (byte)(y + k);
                                    List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(s.x, map.Width), SagaLib.Global.PosY8to16(s.y, map.Height), 300);
                                    List<Actor> affected = new List<Actor>();
                                    s.affectedActors.Clear();
                                    foreach (Actor i in actors)
                                    {
                                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                        {
                                            affected.Add(i);
                                        }
                                    }
                                    SkillHandler.Instance.MagicAttack(caster, affected, s, Elements.Earth, factor);

                                    //广播技能效果
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, s, actor, false);
                                   
                                }
                            }
                        }
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
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
