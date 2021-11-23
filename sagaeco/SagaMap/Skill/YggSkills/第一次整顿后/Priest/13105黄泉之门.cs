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
    public class S13105: ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("黄泉之门CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition cd = new OtherAddition(null, sActor, "黄泉之门CD", 1000);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);
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
                this.period = 150;
                this.dueTime = 0;
                countMax = 2 + 3 * level;
                factor = 1.5f;
                factor += factor * 0.5f * (caster.BeliefDark / 5000f);
                ActorPC Me = (ActorPC)caster;
            }
            
            public override void CallBack()
            {
                try
                {
                    count++;
                    if (count - 1 < countMax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        bool causedamage = false;
                        foreach (Actor i in actors)
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                causedamage = true;
                                if (!i.Status.Additions.ContainsKey("暗刻"))
                                    SkillHandler.Instance.MagicAttack(caster, i, skill, Elements.Dark, factor);
                                else
                                {
                                    SkillHandler.Instance.ShowEffectOnActor(i, 5003, caster);
                                    SkillHandler.Instance.MagicAttack(caster, i, skill, Elements.Dark, factor * 1.5f);
                                }
                            }

                        if (causedamage && !caster.Status.Additions.ContainsKey("意志坚定"))
                            caster.EP -= 70;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, caster, true);
                        //广播技能效果
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
                    Deactivate();
                    map.DeleteActor(actor);
                    Logger.ShowError(ex);
                }
            }
        }
        #endregion
    }
}
