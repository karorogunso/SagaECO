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
    /// 黑洞：2转终极技能
    /// </summary>
    public class S43102 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(pc.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                {
                    return 0;
                }
                return -2;
            }
            else
            {
                return -2;
            }
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
            float factor = 1.6f;
            int countMax = 9, count = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;
                if (caster.Status.Additions.ContainsKey("属性契约"))
                {
                    if (((OtherAddition)(caster.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                    {
                        countMax = 12;
                    }
                }
                ActorPC Me = (ActorPC)caster;
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
                        if (caster.Status.Additions.ContainsKey("属性契约"))
                        {
                            if (((OtherAddition)(caster.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Dark)
                            {
                                foreach (Actor i in actors)
                                {
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                                    {
                                        affected.Add(i);
                                        if (i.Darks == 1)
                                        {
                                            SkillHandler.Instance.ShowEffect(map, i, 5003);
                                            SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                                            int damaga = SkillHandler.Instance.CalcDamage(false, caster, i, skill, SkillHandler.DefType.MDef, Elements.Dark, 50, 2f,out res);
                                            SkillHandler.Instance.CauseDamage(caster, i, damaga);
                                            SkillHandler.Instance.ShowVessel(i, damaga,0,0,res);
                                            i.Darks = 0;
                                        }
                                    }
                                }
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Dark, factor);

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
                    this.Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
        #endregion
    }
}
