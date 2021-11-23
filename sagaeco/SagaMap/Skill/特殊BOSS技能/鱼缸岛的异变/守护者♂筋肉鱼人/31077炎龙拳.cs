using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31077 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PhysicalAttack(sActor,dActor, args, Elements.Fire, 1f);
            SkillHandler.Instance.ActorSpeak(sActor, "吃我热情的一拳！！");
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            for (int i = 0; i < 5; i++)
            {
                short[] pos;
                pos = map.GetRandomPosAroundPos(dActor.X, dActor.Y, 150);
                ActorSkill actor = new ActorSkill(args.skill, sActor);

                actor.MapID = sActor.MapID;
                actor.X = pos[0];
                actor.Y = pos[1];
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                actor.Stackable = false;

                Activator timer = new Activator(sActor, actor, args);
                timer.Activate();
            }
        }

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            float factor = 2.0f;
            float factor_burn = 2.0f;
            int countMax = 5, count = 0,timecountmax = 20,timecount = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 600;
                this.dueTime = 2000;
            }



            public override void CallBack()
            {
                try
                {
                    if (timecount < timecountmax)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 50, true);
                        List<Actor> affected = new List<Actor>();
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))
                            {
                                affected.Add(i);
                                Burning burn = new Burning(this.skill.skill, i, 12000, (int)(this.caster.Status.min_matk * factor_burn));
                                SkillHandler.ApplyAddition(i, burn);
                            }
                        }
                        SkillHandler.Instance.MagicAttack(caster, affected, skill, Elements.Fire, factor);

                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        //count++;
                    }
                    else
                    {
                        this.Deactivate();
                        map.DeleteActor(actor);
                    }
                    timecount++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
