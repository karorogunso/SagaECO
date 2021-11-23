using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31033 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Activator timer = new Activator(sActor,dActor);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            Map map;
            float rate;
            public Activator(Actor caster,Actor dactor)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 500;
                dActor = dactor;
            }
            public override void CallBack()
            {
                try
                {
                    int damage = 66666;
                    SkillHandler.Instance.ActorSpeak(caster, "姐姐大人，你看你看。");
                    List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 3000);
                    if (actors.Count > 0)
                        dActor = actors[SagaLib.Global.Random.Next(0, actors.Count - 1)];

                    SkillHandler.Instance.CauseDamage(caster, dActor, damage);
                    SkillHandler.Instance.ShowVessel(dActor, damage);
                    //SkillHandler.Instance.ShowEffectOnActor(dActor, 5368);
                    SkillHandler.Instance.ShowEffectOnActor(dActor, 5396);
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
