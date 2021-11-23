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
    /// 重力场：7×7地属性设置多段魔法攻击，附带顿足
    /// </summary>
    public class S42403 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("地震CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 5f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.type == ActorType.PC)
            {
                ActorPC Me = (ActorPC)sActor;
                sActor.EP += 500;
                if (Me.Status.Additions.ContainsKey("属性契约"))
                {
                    if (((OtherAddition)(Me.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Earth)
                    {
                        factor = 8f;
                        sActor.EP += 300;
                    }
                }

                if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

                if (Me.Status.Additions.ContainsKey("元素解放"))
                {
                    factor = 8.0f;
                    DefaultBuff cd = new DefaultBuff(sActor, "地震CD", 5000);
                    SkillHandler.ApplyAddition(sActor, cd);
                }
            }
            else
            {
                factor = 8.0f;
            }
            List<Actor> targets = map.GetActorsArea(dActor, 300, true);
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    SkillHandler.Instance.DoDamage(false, sActor, item, args, SkillHandler.DefType.MDef, Elements.Earth, 50, factor);
                    if (factor == 8.0f)
                    {
                        硬直 yz = new 硬直(args.skill, item, 1000);
                        SkillHandler.ApplyAddition(item, yz);
                    }
                }
            }
        }

        #endregion

        /* #region Timer

       private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 1.5f;
            int countMax = 9, count = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;

                ActorPC Me = (ActorPC)caster;

                actor.EP += 200;
                if (Me.Status.Additions.ContainsKey("属性契约"))
                {
                    if (((OtherAddition)(Me.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Earth)
                    {
                        factor = 2.0f;
                        actor.EP += 300;
                    }
                }

                if (actor.EP > actor.MaxEP) actor.EP = actor.MaxEP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);

                if (Me.Status.Additions.ContainsKey("元素解放"))
                {
                    factor = 3.0f;
                    DefaultBuff cd = new DefaultBuff(caster, "地震CD", 5000);
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
                                if (factor >= 3f)
                                {
                                    if (count < 1)
                                    {
                                        硬直 yz = new 硬直(skill.skill, i, 1000);
                                        SkillHandler.ApplyAddition(i, yz);
                                    }
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
        #endregion*/
    }
}
