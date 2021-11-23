using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Network.Client;

using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Partner;

using SagaMap.Manager;

namespace SagaMap.ActorEventHandlers
{
    public class PartnerEventHandler : ActorEventHandler
    {
        ActorPartner partner;
        public Partner.PartnerAI AI;
        Scripting.PartnerCallback currentCall;
        ActorPC currentPC;
        public event Scripting.PartnerCallback Dying;
        public event Scripting.PartnerCallback Attacking;
        public event Scripting.PartnerCallback Moving;
        public event Scripting.PartnerCallback Defending;
        public event Scripting.PartnerCallback SkillUsing;
        public event Scripting.PartnerCallback Updating;

        public PartnerEventHandler(ActorPartner partner)
        {
            this.partner = partner;
            this.AI = new SagaMap.Partner.PartnerAI(partner);
        }

        #region ActorEventHandler Members
        public void OnActorSkillCancel(Actor sActor)
        {

        }
        public void OnActorReturning(Actor sActor)
        {

        }
        public void OnActorPaperChange(ActorPC aActor)
        {
        }
        public void OnActorAppears(Actor aActor)
        {
            if (!partner.VisibleActors.Contains(aActor.ActorID))
                partner.VisibleActors.Add(aActor.ActorID);
            if (aActor.ActorID == this.partner.Owner.ActorID && this.partner.type != ActorType.SHADOW)
            {
                if (!this.AI.Hate.ContainsKey(aActor.ActorID))
                    this.AI.Hate.Add(aActor.ActorID, 1);
            }
        }
        public void OnPlayerShopChange(Actor aActor)
        {

        }
        public void OnPlayerShopChangeClose(Actor aActor)
        {

        }
        public void OnActorChangeEquip(Actor sActor, MapEventArgs args)
        {
            
        }

        public void OnActorChat(Actor cActor, MapEventArgs args)
        {
            
        }

        public void OnActorDisappears(Actor dActor)
        {
            if (partner.VisibleActors.Contains(dActor.ActorID))
                partner.VisibleActors.Remove(dActor.ActorID);
            if (dActor.type == ActorType.PC && dActor.ActorID != this.partner.Owner.ActorID)
            {
                if (this.AI.Hate.ContainsKey(dActor.ActorID))
                    this.AI.Hate.Remove(dActor.ActorID);
            }
        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            SkillArg arg = (SkillArg)args;
            try
            {
                AI.OnSeenSkillUse(arg);
            }
            catch { }
            try
            {
                if (SkillUsing != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(SkillUsing, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(SkillUsing, null);
                    }
                    else
                        RunCallback(SkillUsing, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }

        }
        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {

        }
        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed, MoveType moveType)
        {
        }

        public void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            try
            {
                if (Moving != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(Moving, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(Moving, null);
                    }
                    else
                        RunCallback(Moving, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void OnCreate(bool success)
        {
           
        }


        public void OnActorChangeEmotion(Actor aActor, MapEventArgs args)
        {
           
        }

        public void OnActorChangeMotion(Actor aActor, MapEventArgs args)
        {
            
        }
        public void OnActorChangeWaitType(Actor aActor) { }
        public void OnDelete()
        {
            this.partner.VisibleActors.Clear();
            AI.Pause();
        }


        public void OnCharInfoUpdate(Actor aActor)
        {

        }


        public void OnPlayerSizeChange(Actor aActor)
        {

        }

        public void OnDie()
        {
            OnDie(true);
        }

        public void OnDie(bool loot)
        {
            this.partner.Buff.Dead = true;
            this.partner.ClearTaskAddition();
            if (this.partner.type != ActorType.SHADOW)
            {
                ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)partner.Owner.e;
                eh.Client.DeletePartner();
                if (partner.Owner.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                {
                    Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                    p.data = new byte[11];
                    Item item = partner.Owner.Inventory.Equipments[EnumEquipSlot.PET];
                    if (item.Durability != 0) item.Durability--;
                    eh.Client.SendItemInfo(item);
                    eh.Client.SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.PET_FRIENDLY_DOWN, partner.Name));
                    EffectArg arg = new EffectArg();
                    arg.actorID = eh.Client.Character.ActorID;
                    arg.effectID = 8044;
                    eh.OnShowEffect(eh.Client.Character, arg);
                    p.InventoryID = item.Slot;
                    p.Target = ContainerType.BODY;
                    p.Count = 1;
                    eh.Client.OnItemMove(p);
                }
            }
            else
            {
                this.partner.Owner.Slave.Remove(this.partner);
                this.AI.Pause();
                Tasks.Partner.DeleteCorpse task = new SagaMap.Tasks.Partner.DeleteCorpse(this.partner);
                this.partner.Tasks.Add("DeleteCorpse", task);
                task.Activate();
                if (this.partner.Tasks.ContainsKey("Shadow"))
                {
                    this.partner.Tasks["Shadow"].Deactivate();
                    this.partner.Tasks.Remove("Shadow");
                }
            }
        }

        public void OnKick()
        {
            throw new NotImplementedException();
        }

        public void OnMapLoaded()
        {
            throw new NotImplementedException();
        }

        public void OnReSpawn()
        {
            throw new NotImplementedException();
        }

        public void OnSendMessage(string from, string message)
        {
            throw new NotImplementedException();
        }

        public void OnSendWhisper(string name, string message, byte flag)
        {
            throw new NotImplementedException();
        }

        public void OnTeleport(short x, short y)
        {
            throw new NotImplementedException();
        }

        public void OnAttack(Actor aActor, MapEventArgs args)
        {
            SkillArg arg = (SkillArg)args;
            AI.OnSeenSkillUse(arg);
            try
            {
                if (Attacking != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                        RunCallback(Attacking, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(Attacking, null);
                    }
                    else
                        RunCallback(Attacking, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void OnHPMPSPUpdate(Actor sActor)
        {
            if (Defending != null)
            {
                if (AI.lastAttacker != null)
                {
                    if (AI.lastAttacker.type == ActorType.PC)
                        RunCallback(Defending, (ActorPC)AI.lastAttacker);
                    else
                        RunCallback(Defending, null);
                }
                else
                    RunCallback(Defending, null);
            }
        }

        public void OnPlayerChangeStatus(ActorPC aActor)
        {

        }

        public void OnActorChangeBuff(Actor sActor)
        {

        }
        public void OnLevelUp(Actor sActor, MapEventArgs args)
        {
        }
        public void OnPlayerMode(Actor aActor)
        {
        }

        public void OnShowEffect(Actor aActor, MapEventArgs args)
        {
        }

        public void OnActorPossession(Actor aActor, MapEventArgs args)
        {

        }
        public void OnActorPartyUpdate(ActorPC aActor)
        {

        }
        public void OnActorSpeedChange(Actor mActor)
        {

        }

        public void OnSignUpdate(Actor aActor)
        {

        }

        public void PropertyUpdate(UpdateEvent arg, int para)
        {
            switch (arg)
            {
                case UpdateEvent.SPEED:
                    AI.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, this.partner, true);
                    break;
            }
        }
        public void PropertyRead(UpdateEvent arg)
        {
        }

        public void OnActorRingUpdate(ActorPC aActor)
        { }

        public void OnActorWRPRankingUpdate(ActorPC aActor)
        { }

        public void OnActorChangeAttackType(ActorPC aActor)
        { }
        public void OnActorFurnitureSit(ActorPC aActor)
        { }

        void RunCallback(Scripting.PartnerCallback callback, ActorPC pc)
        {
            currentCall = callback;
            currentPC = pc;
            System.Threading.Thread th = new System.Threading.Thread(Run);
            th.Start();
        }

        void Run()
        {
            ClientManager.EnterCriticalArea();
            try
            {
                if (currentCall != null)
                {
                    if (currentPC != null)
                        currentCall.Invoke(this, currentPC);
                    else
                    {
                        if (this.AI.map.Creator != null)
                            currentCall.Invoke(this, this.AI.map.Creator);
                    }
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
        public void OnActorFurnitureList(Object obj)
        { }
        public void OnUpdate(Actor aActor)
        {
            try
            {
                if (Updating != null)
                {
                    if (AI.lastAttacker != null)
                    {
                        if (AI.lastAttacker.type == ActorType.PC)
                            RunCallback(Updating, (ActorPC)AI.lastAttacker);
                        else
                            RunCallback(Updating, null);
                    }
                    else
                        RunCallback(Updating, null);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        #endregion
    }
}
