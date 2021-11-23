
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 分身術（幻視形代）
    /// </summary>
    public class EventSumNinJa : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorPC pc = (ActorPC)sActor;
            if (level == 1)
            {
                if (sActor.Slave.Count >= 1)
                {
                    ActorEventHandlers.PetEventHandler eh2 = (ActorEventHandlers.PetEventHandler)sActor.Slave[0].e;
                    eh2.AI.Pause();
                    eh2.AI.map.DeleteActor(eh2.AI.Mob);
                    sActor.Slave.Remove(eh2.AI.Mob);
                    eh2.AI.Mob.Tasks["Shadow"].Deactivate();
                    eh2.AI.Mob.Tasks.Remove("Shadow");
                }
                ActorShadow actor = new ActorShadow(pc);
                actor.Name = Manager.LocalManager.Instance.Strings.SKILL_DECOY + pc.Name;
                actor.MapID = pc.MapID;
                actor.X = (short)(sActor.X + SagaLib.Global.Random.Next(1, 10));
                actor.Y = (short)(sActor.Y + SagaLib.Global.Random.Next(1, 10));
                ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(actor);
                actor.e = eh;
                actor.Int = pc.Int >= 10 ? (ushort)(pc.Int - 10) : (ushort)0;
                actor.Str = pc.Str >= 10 ? (ushort)(pc.Str - 10) : (ushort)0;
                actor.Mag = pc.Mag >= 10 ? (ushort)(pc.Mag - 10) : (ushort)0;
                actor.Dex = pc.Dex >= 10 ? (ushort)(pc.Dex - 10) : (ushort)0;
                actor.Agi = pc.Agi >= 10 ? (ushort)(pc.Agi - 10) : (ushort)0;
                actor.Vit = pc.Vit >= 10 ? (ushort)(pc.Vit - 10) : (ushort)0;
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
            else
            {
                if (sActor.Slave.Count >= (level - 1))
                {
                    sActor.Slave[0].ClearTaskAddition();
                    map.DeleteActor(sActor.Slave[0]);
                    sActor.Slave.Remove(sActor.Slave[0]);
                }
                ActorShadow actor = new ActorShadow(pc);
                actor.Name = Manager.LocalManager.Instance.Strings.SKILL_DECOY + pc.Name;
                actor.MapID = pc.MapID;
                actor.X = pc.X;
                actor.Y = pc.Y;
                actor.Int = pc.Int >= 10 ? (ushort)(pc.Int - 10) : (ushort)0;
                actor.Str = pc.Str >= 10 ? (ushort)(pc.Str - 10) : (ushort)0;
                actor.Mag = pc.Mag >= 10 ? (ushort)(pc.Mag - 10) : (ushort)0;
                actor.Dex = pc.Dex >= 10 ? (ushort)(pc.Dex - 10) : (ushort)0;
                actor.Agi = pc.Agi >= 10 ? (ushort)(pc.Agi - 10) : (ushort)0;
                actor.Vit = pc.Vit >= 10 ? (ushort)(pc.Vit - 10) : (ushort)0;
                actor.MaxHP = (uint)(pc.MaxHP * ( 0.5f * level));
                actor.HP = (uint)(pc.HP * (0.5f * level));
                actor.Speed = pc.Speed;
                actor.BaseData.mobSize = 1;
                ActorEventHandlers.PetEventHandler eh = new ActorEventHandlers.PetEventHandler(actor);
                actor.e = eh;

                eh.AI.Mode = new SagaMap.Mob.AIMode(1);
                eh.AI.Master = pc;
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

            public override void CallBack(object o)
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