
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 混沌之門（カオスゲイト）
    /// </summary>
    public class ChaosGait : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            try
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                if (map.CheckActorSkillInRange(dActor.X, dActor.Y, 200))
                {
                    return -17;
                }
            }
            catch (Exception)
            {
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //建立設置型技能實體
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //設定技能位置
            actor.MapID = dActor.MapID;
            actor.X =dActor.X ;
            actor.Y = dActor.Y ;
            //設定技能的事件處理器，由於技能體不需要得到消息廣播，因此建立空處理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地圖註冊技能Actor
            map.RegisterActor(actor);
            //設置Actor隱身屬性為False
            actor.invisble = false;
            //設置系
            actor.Stackable = false;
            //廣播隱身屬性改變事件，以便讓玩家看到技能實體
            map.OnActorVisibilityChange(actor);
            //建立技能效果處理物件
            Activator timer = new Activator(sActor,dActor, actor, args, level);
            timer.Activate();
        }
        #endregion

        #region Timer
        private class Activator : MultiRunTask
        {
            Actor sActor;
            Actor dActor;
            ActorSkill actor;
            SkillArg skill;
            float factor;
            Map map;
            int times;
            int nowTimes;
            public Activator(Actor _sActor,Actor _dActor, ActorSkill _Actor, SkillArg _args, byte level)
            {
                sActor = _sActor;
                dActor = _dActor;
                actor = _Actor;
                skill = _args.Clone();
                factor = 0.1f + 0.5f * level;
                int[] _times = { 0, 4, 4, 5, 5, 6 };
                times = _times[level];
                nowTimes = 0;
                this.dueTime = 1000;
                this.period = 1000;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
            }
            public override void CallBack()
            {
                //同步鎖，表示之後的代碼是執行緒安全的，也就是，不允許被第二個執行緒同時訪問
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (nowTimes < times)
                    {
                        Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                        List<Actor> affected = map.GetActorsArea(dActor, 200, true);
                        List<Actor> realAffected = new List<Actor>();
                        foreach (Actor act in affected)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                            {
                                realAffected.Add(act);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(sActor, realAffected, skill, SagaLib.Elements.Dark, factor);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        nowTimes++;
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
                //解開同步鎖
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}



