
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
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
            if(sActor.MapID == 10054000)
            {
                SkillHandler.SendSystemMessage(sActor, "这里无法放置营火。");
                return -222;
            }

            uint itemID = 950000060;//材料
            if (SkillHandler.Instance.CountItem(sActor, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(sActor, itemID, 1);
                return 0;
            }
            return -57;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //建立設置型技能實體
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            actor.Name = "营火";
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
                try
                {
                    lifetime -= period;
                    if (lifetime > 0)
                    {
                        List<Actor> affected = map.GetActorsArea(actor, 200, false);
                        List<ActorPC> realAffected = new List<ActorPC>();
                        foreach (Actor act in affected)
                        {
                            if (act.type == ActorType.PC && act.HP > 0 && !act.Buff.Dead)
                                realAffected.Add((ActorPC)act);
                        }
                        foreach (var item in realAffected)
                        {
                            if(item.Motion == MotionType.SIT)
                            {
                                uint heal = (uint)(item.MaxHP * 0.1f);
                                item.HP += heal;
                                if (item.HP > item.MaxHP)
                                    item.HP = item.MaxHP;
                                SkillHandler.Instance.ShowVessel(item, (int)-heal);

                                if(item.Status.Additions.ContainsKey("坐下计数"))
                                {
                                    item.TInt["坐下计数次数"]++;
                                    if(item.TInt["坐下计数次数"] == 5)
                                    {
                                        OtherAddition 篝火效果 = new OtherAddition(null, item, "篝火效果", 150000);
                                        篝火效果.OnAdditionStart += (s, e) =>
                                        {
                                            item.Buff.経験値上昇 = true;
                                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                                            if (item.TInt["已获得篝火效果"] != 1)
                                            {
                                                item.Status.str_skill += 10;
                                                item.Status.vit_skill += 10;
                                                item.Status.agi_skill += 10;
                                                item.Status.int_skill += 10;
                                                item.Status.mag_skill += 10;
                                                item.Status.dex_skill += 10;
                                                item.TInt["已获得篝火效果"] = 1;
                                            }
                                            Network.Client.MapClient.FromActorPC(item).SendStatus();
                                        };
                                        篝火效果.OnAdditionEnd += (s, e) =>
                                        {
                                            item.Buff.経験値上昇 = false;
                                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                                            if (item.TInt["已获得篝火效果"] == 1)
                                            {
                                                item.Status.str_skill -= 10;
                                                item.Status.vit_skill -= 10;
                                                item.Status.agi_skill -= 10;
                                                item.Status.int_skill -= 10;
                                                item.Status.mag_skill -= 10;
                                                item.Status.dex_skill -= 10;
                                                item.TInt["已获得篝火效果"] = 0;
                                            }
                                        };
                                        SkillHandler.ApplyBuffAutoRenew(item, 篝火效果);
                                    }
                                }
                                if (!item.Status.Additions.ContainsKey("坐下计数"))
                                {
                                    OtherAddition sit = new OtherAddition(null, item, "坐下计数", 6000);
                                    sit.OnAdditionEnd += (s, e) =>
                                    {
                                        item.TInt["坐下计数次数"] = 0;
                                    };
                                    SkillHandler.ApplyBuffAutoRenew(item, sit);
                                }
                                else
                                    ((OtherAddition)item.Status.Additions["坐下计数"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, 6000);
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
            }
        }
        #endregion
    }
}



