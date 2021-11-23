
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 流行演奏（ポップス）
    /// </summary>
    public class PopMusic : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.STRINGS) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
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

            //註冊施放者的Buff
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "MAGINTDEXUPCircle_Caster", 30000 + 30000 * level);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
            if(sActor.skillsong!=null)
            {
                map.DeleteActor(sActor.skillsong);
            }
            sActor.skillsong = actor;
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Playing = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Playing = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax = 3, count = 0;
            int lifeTime = 0;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;
                this.lifeTime = 30000 + 30000 * level;
                countMax = (30000 + 30000 * level) / period;
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
                        List<Actor> actors = map.GetActorsArea(actor, 200, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor

                        skill.affectedActors.Clear();
                        foreach (Actor act in actors)
                        {
                            if (act.type == ActorType.PC || act.type == ActorType.PET)
                            {
                                if (!act.Status.Additions.ContainsKey("PopMusic"))
                                {
                                    DefaultBuff skill2 = new DefaultBuff(skill.skill, act, "PopMusic", lifeTime - count * period, 200);
                                    skill2.OnAdditionStart += this.StartEventHandler;
                                    skill2.OnAdditionEnd += this.EndEventHandler;
                                    skill2.OnUpdate += this.TimerEventHandler;
                                    SkillHandler.ApplyAddition(act, skill2);
                                }
                            }
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
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                short[] DEX = { 6, 7, 8, 9, 10 };
                short[] INT = { 5, 6, 7, 9, 10 };
                short[] MAG = { 5, 6, 7, 9, 10 };

                //DEX
                if (skill.Variable.ContainsKey("PopMusic_DEX"))
                    skill.Variable.Remove("PopMusic_DEX");
                skill.Variable.Add("PopMusic_DEX", DEX[level - 1]);
                actor.Status.dex_skill += DEX[level - 1];
                //INT
                if (skill.Variable.ContainsKey("PopMusic_INT"))
                    skill.Variable.Remove("PopMusic_INT");
                skill.Variable.Add("PopMusic_INT", INT[level - 1]);
                actor.Status.int_skill += INT[level - 1];
                //MAG
                if (skill.Variable.ContainsKey("PopMusic_MAG"))
                    skill.Variable.Remove("PopMusic_MAG");
                skill.Variable.Add("PopMusic_MAG", MAG[level - 1]);
                actor.Status.mag_skill += MAG[level - 1];

                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {

                //DEX
                actor.Status.dex_skill -= (short)skill.Variable["PopMusic_DEX"];
                //INT
                actor.Status.int_skill -= (short)skill.Variable["PopMusic_INT"];
                //MAG
                actor.Status.mag_skill -= (short)skill.Variable["PopMusic_MAG"];

                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
            }
            void TimerEventHandler(Actor actor, DefaultBuff skill)
            {
                int ranges = Map.Distance(this.actor, actor);
                if (ranges > 200)
                {
                    skill.AdditionEnd();
                }
            }
        }
        #endregion
    }
}
