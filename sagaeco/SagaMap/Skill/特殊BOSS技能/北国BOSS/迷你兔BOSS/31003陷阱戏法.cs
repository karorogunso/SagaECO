using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31003 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ShowEffectByActor(sActor, 5355);
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
                this.dueTime = 3000;
            }
            public override void CallBack()
            {
                SkillHandler.Instance.ShowEffectByActor(caster, 5320);
                List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 400);
                foreach (var item in actors)
                {
                    SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 0, 10f);
                    SkillHandler.Instance.ShowEffectByActor(item, 5003);
                    Stun stun = new Stun(skill.skill, item, 10000);
                    SkillHandler.ApplyAddition(item, stun);
                    SkillHandler.Instance.PushBack(caster, item, 10);

                    item.MP = 0;
                    item.SP = 0;
                    ((ActorPC)item).e.OnHPMPSPUpdate(item);
                }
                this.Deactivate();
            }
        }
    }
}
