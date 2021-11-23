using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class Decoy:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                if (sActor.Slave.Count >= 3)
                {
                    ActorEventHandlers.PetEventHandler eh2 = (ActorEventHandlers.PetEventHandler)sActor.Slave[0].e;
                    eh2.AI.Pause();
                    eh2.AI.map.DeleteActor(eh2.AI.Mob);
                    sActor.Slave.Remove(eh2.AI.Mob);
                    eh2.AI.Mob.Tasks["Shadow"].Deactivate();
                    eh2.AI.Mob.Tasks.Remove("Shadow");
                }
                ActorPC pc = (ActorPC)sActor;
                ActorShadow actor = new ActorShadow(pc);
                Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
                actor.Name = Manager.LocalManager.Instance.Strings.SKILL_DECOY + pc.Name;
                actor.MapID = pc.MapID;
                actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
                actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
                ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(actor);
                actor.e = eh;
                eh.AI.Mode = new SagaMap.Mob.AIMode(0);
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                map.SendVisibleActorsToActor(actor);
                eh.AI.Start();
                Activator task = new Activator(sActor, actor, level * 10000);
                actor.Tasks.Add("Shadow", task);
                task.Activate();
                sActor.Slave.Add(actor);
            }
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorShadow actor;
            Actor castor;
            Map map;
            public Activator(Actor castor, ActorShadow actor, int lifetime)
            {
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = lifetime;
                this.dueTime = lifetime;
                this.actor = actor;
                this.castor = castor;
            }

            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    ActorEventHandlers.PetEventHandler eh = (ActorEventHandlers.PetEventHandler)actor.e;
                    eh.AI.Pause();
                    map.DeleteActor(actor);
                    castor.Slave.Remove(actor);
                    actor.Tasks.Remove("Shadow");
                    this.Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
