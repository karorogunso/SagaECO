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
    public class S31147 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            actor.MapID = sActor.MapID;
            actor.Name = "深蓝领域";
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            actor.Speed = 500;
            actor.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            actor.Stackable = false;
            Activator timer = new Activator(sActor, dActor, actor, args);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Actor dactor;
            SkillArg skill;
            Map map;
            int countMax = 25, count = 0;
            short[] pos;
            public Activator(Actor caster, Actor dActor, ActorSkill skillActor, SkillArg args)
            {
                this.actor = skillActor;
                this.caster = caster;
                this.skill = args.Clone();
                this.skill.dActor = 0xffffffff;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 2000;
                this.dueTime = 1000;
                this.dactor = dActor;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count == countMax || caster.Buff.Stun)//临界消除
                    {
                        Deactivate();
                        map.DeleteActor(actor);
                        count = countMax;
                    }
                    else
                    {
                        count++;
                        List<Actor> actors = map.GetActorsArea(actor, 400, false);
                        foreach (var item in actors)
                        {
                            if (item == caster)
                            {
                                uint heal = (uint)(caster.MaxHP * 0.05f);
                                caster.HP += heal;
                                if (caster.HP > caster.MaxHP)
                                    caster.HP = caster.MaxHP;
                                SkillHandler.Instance.ShowVessel(item, (int)-heal);
                            }
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                uint heal = (uint)(item.MaxHP * 0.03f);
                                item.HP += heal;
                                if (item.HP > item.MaxHP)
                                    item.HP = item.MaxHP;
                                SkillHandler.Instance.ShowVessel(item, (int)-heal);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                    map.DeleteActor(actor);
                }
                ClientManager.LeaveCriticalArea();

            }
        }
    }
}
