using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31163: ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 2000, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(dActor, item))
                    targets.Add(item);
            }
            if (targets.Count > 0)
            {
                Actor target = targets[SagaLib.Global.Random.Next(0, targets.Count - 1)];
                ActorSkill actor = new ActorSkill(args.skill, sActor);
                actor.Name = "狂暴曲·腥红之月";
                actor.MapID = sActor.MapID;
                actor.X = target.X;
                actor.Y = target.Y;
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

        private class Activator : MultiRunTask
        {
            Map map;
            Actor caster;
            ActorSkill skill;
            Actor dActor;
            int count = 0;
            int maxcount = 5000;
            public Activator(Actor sactor, ActorSkill skill)
            {
                caster = sactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 0;
                period = 200;
                this.skill = skill;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if ((caster.HP != caster.MaxHP || caster.type == ActorType.PC) && caster.HP > 0 && count < maxcount && skill.MapID == caster.MapID)
                    {
                        List<Actor> actors = map.GetActorsArea(skill, 100, false);
                        List<Actor> affected = new List<Actor>();
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                int damage = SkillHandler.Instance.CalcDamage(true, caster, item, null, SkillHandler.DefType.MDef, Elements.Neutral, 1, 20f);
                                SkillHandler.Instance.CauseDamage(caster, item, damage);
                                SkillHandler.Instance.ShowVessel(item, damage);
                                SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(map.ID), item, 5238);
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
