
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    /// <summary>
    /// 營火（焚き火）
    /// </summary>
    public class EventCampfire : ISkill
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
            int lifetime = 0;
            public Activator(Actor _sActor, ActorSkill _dActor, SkillArg _args, byte level)
            {
                sActor = _sActor;
                actor = _dActor;
                skill = _args.Clone();
                factor = 0.1f * level;
                this.dueTime = 1000;
                this.period = 5000;
                lifetime = 60000;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
            }
            public override void CallBack()
            {
                //同步鎖，表示之後的代碼是執行緒安全的，也就是，不允許被第二個執行緒同時訪問
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (lifetime > 0)
                    {
                        lifetime -= this.period;
                        uint HP_Recovery = (uint)skill.skill.Level * 10;
                        List<Actor> affected = map.GetActorsArea(actor, 200, false);
                        List<Actor> realAffected = new List<Actor>();
                        foreach (Actor act in affected)
                        {
                            if (act.type == ActorType.PC || act.type== ActorType.PET || act.type== ActorType.SHADOW)
                            {
                                //List<Actor> al=new List<Actor>();
                                //al.Add(act);
                                //List<Elements> el=new List<Elements>();
                                //el.Add(Elements.Holy);
                                //SkillHandler.Instance.MagicAttack(sActor, al, skill, SkillHandler.DefType.IgnoreAll, el, -HP_Recovery, 0, true); 
                                realAffected.Add(act);
                            }
                        }
                        SkillHandler.Instance.FixAttack(sActor, realAffected, skill, Elements.Holy, -HP_Recovery);
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
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



