using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31152 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 3000, false);
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ActorSkill actor = new ActorSkill(args.skill, sActor);
                        actor.Name = "召唤寒冰球";
                        actor.MapID = sActor.MapID;
                        short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 1000);
                        actor.X = pos[0];
                        actor.Y = pos[1];
                        actor.Speed = 600;
                        actor.e = new ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(actor);
                        actor.invisble = false;
                        map.OnActorVisibilityChange(actor);
                        actor.Stackable = false;
                        Activator timer = new Activator(sActor, actor);
                        timer.Activate();
                    }
                }
            }
        }
        private class Activator : MultiRunTask
        {
            Map map;
            Actor caster;
            ActorSkill skill;
            Actor dActor;
            int count = 0;
            int maxcount = 10000;
            public Activator(Actor sactor, ActorSkill skill)
            {
                caster = sactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 2000;
                period = 500;
                this.skill = skill;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID && ((Actor)skill).TInt["克苏鲁之怒"] != 1)
                    {
                        List<Actor> actors = map.GetActorsArea(skill, 100, false);
                        List<Actor> affected = new List<Actor>();
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                affected.Add(item);
                        }
                        if (affected.Count > 0)
                        {
                            SkillHandler.Instance.ShowEffectByActor(skill, 5078);
                            actors = map.GetActorsArea(skill, 200, false);
                            affected = new List<Actor>();
                            foreach (var item in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                                    affected.Add(item);
                            }
                            foreach (Actor j in actors)
                            {
                                if (SkillHandler.Instance.CheckValidAttackTarget(caster, j))
                                {
                                    int damage = SkillHandler.Instance.CalcDamage(true, caster, j, null, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 1, 2f);
                                    SkillHandler.Instance.CauseDamage(caster, j, damage);
                                    SkillHandler.Instance.ShowVessel(j, damage);
                                    SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), j, 4321);
                                    if (!j.Status.Additions.ContainsKey("Frosen"))
                                    {
                                        Freeze f = new Freeze(null, j, 1000);
                                        SkillHandler.ApplyAddition(j, f);
                                    }
                                }
                            }
                            Deactivate();
                            map.DeleteActor(skill);
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
