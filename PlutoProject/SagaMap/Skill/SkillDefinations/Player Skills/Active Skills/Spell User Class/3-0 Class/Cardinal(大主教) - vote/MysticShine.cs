using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    /// <summary>
    /// ミスティックシャイン
    /// </summary>
    public class MysticShine : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (pc.Status.Additions.ContainsKey("HerosProtection"))
                return -30;
            else if (map.CheckActorSkillInRange(pc.X, pc.Y, 300))
            {
                return -17;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = { 0, 15000, 2000, 25000, 30000, 35000 };
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "HerosProtection", lifetime[level]);
            SkillHandler.ApplyAddition(sActor, skill);
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
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 0.0f;
            int countMax = 3, count = 0;
            //int TotalLv = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 250;
                this.dueTime = 0;
                factor = 1.0f + 0.5f * level;

                switch (level)
                {
                    case 1:
                        countMax = 60;
                        break;
                    case 2:
                        countMax = 80;
                        break;
                    case 3:
                        countMax = 100;
                        break;
                    case 4:
                        countMax = 120;
                        break;
                    case 5:
                        countMax = 140;
                        break;
                }

            }



            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, 400, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor（即怪物）

                        //施加火属性魔法伤害
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Holy, factor);


                        actors = map.GetActorsArea(actor, 300, false);
                        affected = new List<Actor>();

                        foreach (Actor i in actors)
                        {


                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                            }
                        }
                        foreach (var item in affected)
                        {
                            //BOSS 退後無效
                            if (SkillHandler.Instance.isBossMob(item) == true)
                                continue;

                            SkillHandler.Instance.PushBack(caster, item, 2);
                        }

                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
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
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
