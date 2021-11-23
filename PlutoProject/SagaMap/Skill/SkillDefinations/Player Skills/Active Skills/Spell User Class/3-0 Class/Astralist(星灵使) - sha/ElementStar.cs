using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Astralist
{
    public class ElementStar : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            short range = 300;
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.CheckActorSkillInRange(dActor.X, dActor.Y, range))
            {
                return -17;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = dActor.MapID;
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
            Activator timer = new Activator(sActor, dActor, actor, args, level);
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
            int countMax = 0, count = 0;
            Actor dActor;
            float[] Firefactor = { 0, 3.5f, 0.5f, 0.5f, 0.5f, 1.8f };
            float[] Waterfactor = { 0, 0.5f, 3.5f, 0.5f, 0.5f, 1.8f };
            float[] Windfactor = { 0, 0.5f, 0.5f, 3.5f, 0.5f, 1.8f };
            float[] Earthfactor = { 0, 0.5f, 0.5f, 0.5f, 3.5f, 1.8f };
            public Activator(Actor caster, Actor theDActor, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = (int)(4000.0f / 13.0f);
                this.dueTime = 0;
                //int[] Counts = { 0, 3, 3, 4, 4, 5 };
                countMax = 13;

                //factor = 0.5f + 0.5f * level;
                dActor = theDActor;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    float anotherfactor = 0;
                    if (caster.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)caster;
                        if (pc.Skills2_2.ContainsKey(3319) || pc.DualJobSkill.Exists(x => x.ID == 3319))
                        {

                            //这里取副职的剑圣等级
                            var duallv = 0;
                            if (pc.DualJobSkill.Exists(x => x.ID == 3319))
                                duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3319).Level;

                            //这里取主职的剑圣等级
                            var mainlv = 0;
                            if (pc.Skills2_2.ContainsKey(3319))
                                mainlv = pc.Skills2_2[3319].Level;

                            //这里取等级最高的剑圣等级用来做居合的倍率加成
                            anotherfactor += 0.15f + Math.Max(duallv, mainlv) * 0.05f;
                        }
                    }

                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(dActor, 300, true);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor（即怪物）

                        //施加魔法伤害

                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                int FireDamage = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Fire, 100, Firefactor[skill.skill.Level] + anotherfactor);
                                int WaterDamage = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Water, 100, Waterfactor[skill.skill.Level] + anotherfactor);
                                int WindDamage = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Wind, 100, Windfactor[skill.skill.Level] + anotherfactor);
                                int EarthDamage = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Earth, 100, Earthfactor[skill.skill.Level] + anotherfactor);
                                int AttackAffect = FireDamage + WaterDamage + WindDamage + EarthDamage;
                                SkillHandler.Instance.CauseDamage(caster, i, AttackAffect);
                                SkillHandler.Instance.ShowVessel(i, AttackAffect);
                                //Additions.Global.Stiff Stiff = new SagaMap.Skill.Additions.Global.Stiff(skill.skill, i, 500);//Mob can not move as soon as attacked.
                                //SkillHandler.ApplyAddition(i, Stiff);
                                //affected.Add(i);
                            }
                        }
                        //SkillHandler.Instance.MagicAttack(caster, affected, skill, SagaLib.Elements.Neutral, factor);

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
