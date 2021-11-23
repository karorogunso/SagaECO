using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    /// <summary>
    /// ヴェノムブラスト [后续技能]
    /// </summary>
    public class TimeBombSEQ : ISkill
    {
        #region ISkill Members
        Actor RealdActor;
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }



        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            //设定技能体位置
            actor.MapID = dActor.MapID;
            RealdActor = dActor;
            //actor.X = SagaLib.Global.PosX8to16((byte)sActor.X, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16((byte)sActor.Y, map.Height);
            //更改设定位置为怪物本身位置
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
            Activator timer = new Activator(sActor, actor, dActor, args, level);
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

            //float[] factors = new float[] { 0f, 0.02f, 0.04f, 0.01f, 0.04f, 0.05f, 100f };//治疗量=(使用者的)百分比比例
            float factor;

            public Activator(Actor caster, ActorSkill actor,Actor dActor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.dActor = dActor;
                this.skill = args.Clone();
                skilllevel = level;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                factor = 3.5f + 0.7f * level;
                //this.period = periods[level];
                this.dueTime = 1000;

            }

            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //ClientManager.EnterCriticalArea();
                try
                {
                    //Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                    //List<Actor> affected = map.GetActorsArea(actor, 50, true);
                    //List<Actor> realAffected = new List<Actor>();
                    //foreach (Actor act in affected)
                    //{
                    //    if (SkillHandler.Instance.CheckValidAttackTarget(caster, act))
                    //    {
                    //        realAffected.Add(act);
                    //    }
                    //}
                    SkillHandler.Instance.PhysicalAttack(caster, dActor, skill, caster.WeaponElement, factor);

                    //广播技能效果
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                    this.Deactivate();
                    //在指定地图删除技能体（技能效果结束）
                    map.DeleteActor(actor);

                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }

            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                actor.Buff.NoRegen = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                actor.Buff.NoRegen = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            #endregion
        }


    }


}
