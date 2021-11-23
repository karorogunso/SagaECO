using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31054 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, args);
            timer.Activate();
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5360);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            SkillArg skill;
            float rate;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 500;
                this.skill = args;
            }
            public override void CallBack()
            {
                try
                {
                    int damage = 2000;
                    SkillHandler.Instance.ActorSpeak(caster, "姐姐大人说男孩子靠近都要切掉。");
                    List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 2000);
                    foreach (var item in actors)
                    {
                        SkillHandler.Instance.CauseDamage(caster, item, damage);
                        SkillHandler.Instance.ShowVessel(item, damage);
                        if (item.type != ActorType.PC) continue;
                        if (((ActorPC)item).Gender != PC_GENDER.MALE) continue;
                        Poison1 p = new Poison1(null, item, 10000, 500);
                        SkillHandler.ApplyAddition(item, p);
                        Activator2 tim = new Activator2(caster, item);
                        SkillHandler.Instance.ShowEffectOnActor(item, 8059);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
        }
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            Map map;
            float rate;
            int countMax = 3, count = 0;
            public Activator2(Actor caster, Actor dactor)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                period = 2000;
                dueTime = 1000;
                dActor = dactor;
            }
            public override void CallBack()
            {
                try
                {
                    if (count < countMax)
                    {
                        int damage = 1000;
                        SkillHandler.Instance.CauseDamage(caster, dActor, damage);
                        SkillHandler.Instance.ShowVessel(dActor, damage);
                        SkillHandler.Instance.ShowEffectOnActor(dActor, 8059);
                        count++;
                    }
                    else
                    {
                        Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
            }
        }
    }
}
