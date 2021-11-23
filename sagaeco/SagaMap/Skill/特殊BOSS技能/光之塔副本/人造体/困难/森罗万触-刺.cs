using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31053 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor, args);
            timer.Activate();
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5359);
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
                dueTime = 1500;
                this.skill = args;
            }
            public override void CallBack()
            {
                try
                {
                    
                    SkillHandler.Instance.ActorSpeak(caster, "姐姐大人说女孩子要施以调教。");
                    List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 2000);
                    foreach (var item in actors)
                    {
                        int damage = (int)(item.MaxHP - 500);
                        SkillHandler.Instance.CauseDamage(caster, item, damage);
                        SkillHandler.Instance.ShowVessel(item, damage);
                        if (item.type != ActorType.PC) continue;
                        if (((ActorPC)item).Gender != PC_GENDER.FEMALE) continue;
                        Stone st = new Stone(skill.skill, item, 3000);
                        SkillHandler.ApplyAddition(item, st);
                        Activator2 tim = new Activator2(caster, item);
                        tim.Activate();
                        SkillHandler.Instance.ShowEffectOnActor(item, 8022);
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
            public Activator2(Actor caster, Actor dactor)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 6000;
                dActor = dactor;
            }
            public override void CallBack()
            {
                try
                {
                    int damage = 1000;
                    ATKDOWN ad = new ATKDOWN(null, dActor, 15000, 30);
                    MATKDOWN md = new MATKDOWN(null, dActor, 15000, 30);
                    SkillHandler.ApplyAddition(dActor, ad);
                    SkillHandler.ApplyAddition(dActor, md);
                    SkillHandler.Instance.CauseDamage(caster, dActor, damage);
                    SkillHandler.Instance.ShowVessel(dActor, damage);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
        }
    }
}
