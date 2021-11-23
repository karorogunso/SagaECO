using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 大地束縛（バインド）
    /// </summary>
    public class Bind:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (args.x >= map.Width || args.y >= map.Height)
                return -6;
            if (map.Info.earth[args.x, args.y] > 0)
                return 0;
            else
                return -12;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = dActor.MapID;
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
            //创建技能效果处理对象
            Activator timer = new Activator(actor, args, level);
            timer.Activate();
        }
        #endregion

        #region Timer
        private class Activator : MultiRunTask
        {
            Actor actor;
            SkillArg skill;
            Map map;
            byte level;
            int lifetime;
            public Activator(Actor _actor, SkillArg _args, byte _level)
            {
                level=_level;
                actor = _actor;
                skill = _args;
                this.dueTime = 0;
                this.period = 1000;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                lifetime = 5000 + 1000 * level;
            }
            public override void CallBack(object o)
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (lifetime > 0)
                    {
                        lifetime -= this.period;
                        try
                        {
                            List<Actor> actors = map.GetActorsArea(actor, 300, false);
                            List<Actor> affected = new List<Actor>();
                            //取得有效Actor（即怪物）
                            int rate = 5 + 5 * level;

                            foreach (Actor i in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(actor, i))
                                {
                                    if (SkillHandler.Instance.CanAdditionApply(actor, i, SkillHandler.DefaultAdditions.鈍足, rate))
                                    {
                                        Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(skill.skill, i, lifetime);
                                        SkillHandler.ApplyAddition(i, skill2);
                                    }
                                }
                            }
                            //广播技能效果
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                        
                    }
                    else
                    {
                        this.Deactivate();
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
        #endregion
    }
}
