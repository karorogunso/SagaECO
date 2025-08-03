
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 毒霧（スキャターポイズン）
    /// </summary>
    public class ScatterPoison : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (map.CheckActorSkillInRange(sActor.X, sActor.Y, 300))
            {
                return -17;
            }

            uint itemID = 10000302;//毒藥
            if (SkillHandler.Instance.CountItem(sActor, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(sActor, itemID, 1);
                return 0;
            }
            return -2;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            //actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
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
        }
        #endregion

        #region Timer
        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            float factor;
            Map map;
            int times, lifetime;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                skill = args.Clone();
                factor = 0.02f * level;
                int[] Times = { 0, 50, 30, 25, 22, 20 };
                lifetime = 35000 - 5000 * level;
                times = Times[level];
                ActorPC pc = caster as ActorPC;
                int PMlv = 0;
                //if (pc.Skills3.ContainsKey(994) || pc.DualJobSkill.Exists(x => x.ID == 994))
                //{
                //    //这里取副职的加成技能专精等级
                //    var duallv = 0;
                //    if (pc.DualJobSkill.Exists(x => x.ID == 994))
                //        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 994).Level;

                //    //这里取主职的加成技能等级
                //    var mainlv = 0;
                //    if (pc.Skills3.ContainsKey(994))
                //        mainlv = pc.Skills3[994].Level;

                //    //这里取等级最高的加成技能等级用来做倍率加成
                //    PMlv = Math.Max(duallv, mainlv);
                //    //ParryResult += pc.Skills[116].Level * 3;
                //    lifetime = lifetime * (int)(1.5f + 0.5f * PMlv);
                //    factor += 0.01f * PMlv;
                //}


                this.dueTime = 0;
                this.period = lifetime / times;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (times > 0 && lifetime > 0)
                    {
                        List<Actor> affected = map.GetActorsArea(actor, 200, false);
                        List<Actor> realAffected = new List<Actor>();
                        foreach (Actor act in affected)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, act))
                            {
                                realAffected.Add(act);
                            }
                        }
                        affected.Clear();
                        uint HP_Lost = (uint)(caster.MaxHP * factor);
                        foreach (Actor act in realAffected)
                        {
                            if (times > 0)
                            {
                                //affected.Add(act);
                                SkillHandler.Instance.FixAttack(caster, act, skill, caster.WeaponElement, HP_Lost);
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                                times--;
                            }
                            //else
                            //    continue;
                        }
                        lifetime -= this.period;

                        //SkillHandler.Instance.MagicAttack(caster, affected, skill, SkillHandler.DefType.DefIgnoreRight, Elements.Neutral, HP_Lost, 0, true);
                        //SkillHandler.Instance.PhysicalAttack(sActor, affected, skill, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, true);

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
                ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}



