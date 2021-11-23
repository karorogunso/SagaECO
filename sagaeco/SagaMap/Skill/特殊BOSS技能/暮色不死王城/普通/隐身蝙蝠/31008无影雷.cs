using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;


namespace SagaMap.Skill.SkillDefinations
{
    public class S31008 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            
            //SkillHandler.Instance.DoDamage(true, sActor, dActor, args, SkillHandler.DefType.Def, Elements.Neutral, 0, 4f, 50f);
            SkillHandler.Instance.ShowEffectByActor(dActor, 5261);
            SkillHandler.Instance.ShowEffectByActor(dActor, 5366);
            SkillHandler.Instance.ShowEffectByActor(dActor, 5442);
            Stun stun = new Stun(args.skill, dActor, 4000);
            SkillHandler.ApplyAddition(dActor, stun);

            Activator timer = new Activator(sActor, dActor,args);
            timer.Activate();
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Actor dActor;
            public Activator(Actor caster, Actor dA, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                this.period = 0;
                this.dueTime = 6000;
                dActor = dA;
            }
            public override void CallBack()
            {
                Deactivate();
                List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 700, false);

                List<Actor> affected = new List<Actor>();

                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item) && item != dActor)
                        affected.Add(item);
                }

                SkillHandler.Instance.ShowEffectByActor(dActor, 5304);
                SkillHandler.Instance.ShowEffectByActor(dActor, 5266);
                if (SkillHandler.Instance.CheckValidAttackTarget(caster,dActor))
                {
                    int damage = (int)(dActor.HP * 0.5f);
                    SkillHandler.Instance.CauseDamage(caster, dActor, damage);
                    SkillHandler.Instance.ShowVessel(dActor, damage);
                }

                foreach (var item in affected)
                {
                    SkillHandler.Instance.DoDamage(false, caster, item, skill, SkillHandler.DefType.IgnoreAll, Elements.Neutral, 30, 5f, 5f);
                    SkillHandler.Instance.ShowEffectByActor(item, 5286);
                    Stun stun = new Stun(null, item, 4000);
                    SkillHandler.ApplyAddition(item, stun);
                }
               
            }
        }
    }
}
