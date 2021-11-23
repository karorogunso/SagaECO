using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations
{
    public class S31004 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.DoDamage(true, sActor, dActor, args, SkillHandler.DefType.Def, Elements.Neutral, 0, 5f, 50f);
            SkillHandler.Instance.ShowEffectByActor(dActor, 5261);
            SkillHandler.Instance.ShowEffectByActor(dActor, 5366);
            Stun stun = new Stun(args.skill, dActor, 4000);
            SkillHandler.ApplyAddition(dActor, stun);

            Activator timer = new Activator(sActor, args);
            timer.Activate();
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                this.period = 0;
                this.dueTime = 4000;
            }
            public override void CallBack()
            {
                Actor dActor = caster;
                //if (caster.type == ActorType.MOB)
                //    dActor = ((ActorEventHandlers.MobEventHandler)((ActorMob)caster).e).AI.HighestActor();//找出最高仇恨者。该函数未测试，可能存在问题

                //List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(dActor, 300);
                List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor,500, false);

                List<Actor> affected = new List<Actor>();

                foreach (var item in actors)
                {
                    if (item.type == ActorType.PC && !item.Buff.Dead)
                        affected.Add(item);
                }

                float factor = 100f;
                factor = factor / affected.Count;

                foreach (var item in affected)
                {
                    SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, Elements.Neutral, 30, factor, 50f);
                    SkillHandler.Instance.ShowEffectByActor(item, 5286);
                    鈍足 speedCut = new 鈍足(skill.skill, item, 8000, 50);
                    SkillHandler.ApplyAddition(item, speedCut);
                }
                SkillHandler.Instance.ShowEffectByActor(dActor, 5266);
                this.Deactivate();
            }
        }
    }
}
