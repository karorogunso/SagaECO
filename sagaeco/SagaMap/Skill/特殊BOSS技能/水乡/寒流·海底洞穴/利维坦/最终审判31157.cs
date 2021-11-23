using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31157 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.Name = "最终审判";
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            actor.Speed = 600;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(sActor, actor);
            timer.Activate();
        }

        private class Activator : MultiRunTask
        {
            Map map;
            Actor caster;
            ActorSkill skill;
            Actor dActor;
            int count = 0;
            int maxcount = 13;
            public Activator(Actor sactor, ActorSkill skill)
            {
                caster = sactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 0;
                period = 500;
                this.skill = skill;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID )
                    {
                        List<Actor> actors = map.GetActorsArea(skill, 100, false);
                        List<Actor> affected = new List<Actor>();
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                int damage = SkillHandler.Instance.CalcDamage(true, caster, item, null, SkillHandler.DefType.MDef, Elements.Neutral, 1, 5f);
                                SkillHandler.Instance.CauseDamage(caster, item, damage);
                                SkillHandler.Instance.ShowVessel(item, damage);
                                SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), item, 5275);
                            }
                        }
                    }
                    else
                    {
                        map.DeleteActor(skill);
                        Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    map.DeleteActor(skill);
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
