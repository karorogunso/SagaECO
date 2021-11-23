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
    /// 寒冰湍流：7×7水属性设置多段魔法攻击，附带颤栗
    /// </summary>
    public class S42203
        : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("寒冰湍流CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
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
            float factor = 1.2f;
            int countMax = 8, count = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;

                ActorPC Me = (ActorPC)caster;

                if (caster.Status.Additions.ContainsKey("属性契约"))
                {
                    if (((OtherAddition)(caster.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Water)
                    {
                        factor = 1.8f;
                        countMax = 11;
                        caster.EP += 300;
                    }
                }

                caster.EP += 500;
                if (caster.EP > caster.MaxEP) caster.EP = caster.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);

                if (Me.Status.Additions.ContainsKey("元素解放"))
                {
                    factor = 2.4f;
                    countMax = 13;
                    DefaultBuff cd = new DefaultBuff(caster, "寒冰湍流CD", 5000);
                    SkillHandler.ApplyAddition(caster, cd);
                }
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
                                if(factor >= 2f)
                                {
                                    if (SkillHandler.Instance.CanAdditionApply(caster, i, "Frosen", 10))
                                    {
                                        Freeze f = new Freeze(this.skill.skill, i, 3000);
                                        SkillHandler.ApplyAddition(i, f);
                                    }
                                }
                                else if(SkillHandler.Instance.CanAdditionApply(caster, i,"颤栗",10))
                                {
                                    Chilling f = new Chilling(this.skill.skill, i, 10000, 1000);
                                    SkillHandler.ApplyAddition(i, f);
                                }
                            }
                        }

                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Water, factor);

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
