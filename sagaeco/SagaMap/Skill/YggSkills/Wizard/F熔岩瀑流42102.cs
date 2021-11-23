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
    public class S42102 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
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
            float factor = 1.0f;
            float factor_burn = 0f;
            int countMax = 5, count = 0;
            int TotalLv = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 600;
                this.dueTime = 0;

                actor.EP += 500;
                if (caster.Status.Additions.ContainsKey("属性契约"))
                {
                    if (((OtherAddition)(caster.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Fire)
                    {
                        factor = 2f;
                        countMax = 7;
                        factor_burn = 1.9f;
                        actor.EP += 300;
                    }
                }
                if (actor.EP > actor.MaxEP) actor.EP = actor.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
                ActorPC Me = (ActorPC)caster;
                if (Me.Status.Additions.ContainsKey("元素解放"))
                {
                    factor = 2.5f;
                    factor_burn = 2.0f;
                    countMax = 10;
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
                                if (i.type == ActorType.PC)
                                    factor = factor / 2;
                                affected.Add(i);
                                Burning burn = new Burning(this.skill.skill, i, 6000, (int)(this.caster.Status.min_matk * factor_burn));
                                SkillHandler.ApplyAddition(i, burn);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Fire, factor);

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
