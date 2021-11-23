using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Scripting;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// フォートレスサークル
    /// </summary>
    public class StarLove : ISkill
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
            actor.MapID = dActor.MapID;
            //actor.X = SagaLib.Global.PosX8to16((byte)sActor.X, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16((byte)sActor.Y, map.Height);
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
            Activator timer = new Activator(sActor, actor,dActor, args, level);
            timer.Activate();


        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dActor;
            SkillArg skill;
            Map map;
            byte skilllevel;
            float factor = 24.0f;
            int countMax = 1, count = 0, lifetime = 0;

            public Activator(Actor caster, ActorSkill actor, Actor dActor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.dActor = dActor;
                this.dueTime = 650;
                this.skill = args.Clone();
                skilllevel = level;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                lifetime = 500;//持续时间
                //factor = factors[level] + caster.Status.Cardinal_Rank;
                ActorPC pc = caster as ActorPC;
                
                this.period = 500;

            }

            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                        List<Actor> actors = Manager.MapManager.Instance.GetMap(caster.MapID).GetActorsArea(dActor, 200, true);
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                int damage1 = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor);
                                int damage2 = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor);
                                int enddamage = damage1 + damage2;
                                SkillHandler.Instance.FixAttack(caster, i, skill, SagaLib.Elements.Neutral, enddamage);
                                SkillHandler.Instance.ShowVessel(i, enddamage);
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
                //ClientManager.LeaveCriticalArea();
            }
            #endregion
        }
    }
}
