using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 熔岩瀑流：7×7火属性设置多段魔法攻击，附带灼烧
    /// </summary>
    public class S31080 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ActorSpeak(sActor, "愤怒吧！大地！替我驱逐这群入侵者——！");
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
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
            float factor = 1.5f;
            int countMax = 10, count = 0;
            int TotalLv = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 1000;
                this.dueTime = 0;
            }

            public override void CallBack()
            {
                try
                {
                    if (count < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                                
                                if(!i.Status.Additions.ContainsKey("BOSS_大地之怒"))
                                {
                                    OtherAddition skill2 = new OtherAddition(null, i, "BOSS_大地之怒", 10000);
                                    skill2.OnAdditionEnd += (s, e) =>
                                    {
                                        i.TInt["BOSS_大地之怒层数"] = 0;
                                        Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("【大地之怒】层数清空了。");
                                    };
                                    SkillHandler.ApplyAddition(i, skill2);
                                }
                                if (i.TInt["BOSS_大地之怒层数"] < 4)
                                {
                                    SkillHandler.Instance.ShowEffectOnActor(i, 5041);
                                    i.TInt["BOSS_大地之怒层数"]++;
                                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("正在被大地吞噬！赶紧逃脱！[层数:" + i.TInt["BOSS_大地之怒层数"].ToString() + "/5]");
                                }
                                else
                                {
                                    SkillHandler.Instance.ShowEffectOnActor(i, 5042);
                                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("你被大地吞噬了。[层数:" + i.TInt["BOSS_大地之怒层数"].ToString() + "/5]");
                                    SkillHandler.Instance.CauseDamage(caster, i, (int)i.MaxHP);
                                    SkillHandler.Instance.ShowVessel(i, (int)i.MaxHP);
                                }
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Earth, factor);

                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        count++;
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
