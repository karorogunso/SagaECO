using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Scripting;

namespace SagaMap.Mob
{
    public class MobAttack : MultiRunTask
    {
        private MobAI mob;
        public Actor dActor;

        public MobAttack(MobAI mob, Actor dActor)
        {
            this.dueTime = 0;
            this.mob = mob;
            this.period = calcDelay(mob.Mob);
            this.dActor = dActor;
        }

        int calcDelay(Actor actor)
        {
            int aspd = 0;
            uint delay = 0;
            if (this.mob.Mob.type == ActorType.MOB)
            {
                ActorMob tar = (ActorMob)this.mob.Mob;
                aspd = tar.BaseData.aspd;
            }
            if (this.mob.Mob.type == ActorType.PET)
            {
                ActorPet pet = (ActorPet)this.mob.Mob;
                aspd = pet.BaseData.aspd;
            }
            if (this.mob.Mob.type == ActorType.SHADOW || this.mob.Mob.type == ActorType.GOLEM ||
                this.mob.Mob.type == ActorType.PC)
            {
                aspd = this.mob.Mob.Status.aspd;
            }

            aspd += (short)(actor.Status.aspd_skill);
            if (aspd > 960)
                aspd = 960;
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand)
                        delay = 2400 - (uint)(2400 * aspd * 0.001f);
                    else
                        delay = 2000 - (uint)(2000 * aspd * 0.001f);
                }
                else
                    delay = 2000 - (uint)(2000 * aspd * 0.001f);
            }
            else
                delay = 2000 - (uint)(2000 * aspd * 0.001f);
            if (actor.Status.aspd_skill_perc >= 1f)
                delay = (uint)(delay / actor.Status.aspd_skill_perc);
            return (int)delay;
        }

        public override void CallBack()
        {
            //ClientManager.EnterCriticalArea();
            try
            {
                if (!mob.CanAttack)
                {
                    //ClientManager.LeaveCriticalArea();
                    return;
                }
                if (mob.Mob.HP == 0 || dActor.HP == 0 || !mob.Hate.ContainsKey(dActor.ActorID) || mob.Mob.Tasks.ContainsKey("AutoCast"))
                {
                    if (mob.Hate.ContainsKey(dActor.ActorID)) mob.Hate.Remove(dActor.ActorID);
                    if (this.Activated) this.Deactivate();
                    //ClientManager.LeaveCriticalArea();
                    return;
                }
                if (mob.Mob.type == ActorType.PET)
                {
                    ActorPet pet = (ActorPet)mob.Mob;
                    if (pet.Owner.ActorID == dActor.ActorID)
                    {
                        if (this.Activated) this.Deactivate();
                        //ClientManager.LeaveCriticalArea();
                        return;
                    }
                }
                if (mob.Master != null)
                {
                    if (dActor.ActorID == mob.Master.ActorID)
                    {
                        //ClientManager.LeaveCriticalArea();
                        return;
                    }
                    if(dActor.type == ActorType.MOB)
                    {
                        if(((ActorEventHandlers.MobEventHandler)dActor.e).AI.Master != null)
                        {
                            if (((ActorEventHandlers.MobEventHandler)dActor.e).AI.Master.ActorID == mob.Master.ActorID)
                                return;
                        }
                    }
                }
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.HP == 0)
                    {
                        if (mob.Hate.ContainsKey(dActor.ActorID)) mob.Hate.Remove(dActor.ActorID);
                        if (this.Activated) this.Deactivate();
                        //ClientManager.LeaveCriticalArea();
                        return;
                    }
                }
                SkillArg arg = new SkillArg();
                Skill.SkillHandler.Instance.Attack(mob.Mob, dActor, arg);
                mob.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, mob.Mob, true);
                this.period = calcDelay(this.mob.Mob);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            //ClientManager.LeaveCriticalArea();
        }
    }
}
