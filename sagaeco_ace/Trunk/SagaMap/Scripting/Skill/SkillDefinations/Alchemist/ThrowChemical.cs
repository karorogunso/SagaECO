
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 藥品投擲（薬品投擲）
    /// </summary>
    public class ThrowChemical : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //建立設置型技能實體
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //設定技能位置
            actor.MapID = dActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //設定技能的事件處理器，由於技能體不需要得到消息廣播，因此建立空處理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地圖註冊技能Actor
            map.RegisterActor(actor);
            //設置Actor隱身屬性為False
            actor.invisble = false;
            //廣播隱身屬性改變事件，以便讓玩家看到技能實體
            map.OnActorVisibilityChange(actor);
            //建立技能效果處理物件
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();
        }
        #endregion

        #region Timer
        private class Activator : MultiRunTask
        {
            Actor sActor;
            ActorSkill actor;
            SkillArg skill;
            float factor;
            Map map;
            int lifetime;
            int rate;
            public Activator(Actor _sActor, ActorSkill _dActor, SkillArg _args, byte level)
            {
                int[] periods = { 0, 15000, 15000, 20000, 20000, 18000 };
                sActor = _sActor;
                actor = _dActor;
                skill = _args.Clone();
                factor = 0.1f * level;
                this.dueTime = 1000;
                this.period = periods[level];
                lifetime = 20000;
                rate = 40 - 5 * level;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
            }
            public override void CallBack(object o)
            {
                //同步鎖，表示之後的代碼是執行緒安全的，也就是，不允許被第二個執行緒同時訪問
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (lifetime > 0)
                    {
                        lifetime -= this.period;
                        List<Actor> affected = map.GetActorsArea(actor, 150, false);
                        foreach (Actor act in affected)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                            {
                                switch (skill.skill.Level)
                                {
                                    case 1:
                                        if(SkillHandler.Instance.CanAdditionApply(sActor,act, SkillHandler.DefaultAdditions.鈍足 ,rate))
                                        {
                                            鈍足 s1 = new 鈍足(skill.skill, act, 10000);
                                            SkillHandler.ApplyAddition(act, s1);
                                        }
                                        break;
                                    case 2:
                                        if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Silence, rate))
                                        {
                                            Silence s2 = new Silence(skill.skill, act, 10000);
                                            SkillHandler.ApplyAddition(act, s2);
                                        }
                                        break;
                                    case 3:
                                        if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Poison , rate))
                                        {
                                            Poison s3 = new Poison(skill.skill, act, 10000);
                                            SkillHandler.ApplyAddition(act, s3);
                                        }
                                        break;
                                    case 4:
                                        if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Confuse , rate))
                                        {
                                            Confuse s4 = new Confuse(skill.skill, act, 10000);
                                            SkillHandler.ApplyAddition(act, s4);
                                        }
                                        break;
                                    case 5:
                                        if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stun , rate))
                                        {
                                            Stun s5 = new Stun(skill.skill, act, 10000);
                                            SkillHandler.ApplyAddition(act, s5);
                                        }
                                        break;
                                }
                            }
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
                //解開同步鎖
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}



