using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Network.Client;

using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaDB.Item;

namespace SagaMap.ActorEventHandlers
{
    public class NullEventHandler : ActorEventHandler
    {
        
        public NullEventHandler()
        {
            
        }

        #region ActorEventHandler Members
        public void OnActorSkillCancel(Actor sActor)
        {

        }
        public void OnActorPaperChange(ActorPC aActor)
        {
        }
        public void OnActorReturning(Actor sActor)
        {

        }
        public void OnActorAppears(Actor aActor)
        {
            
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
            
        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            
        }

        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            
        }

        public void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            
        }
        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed, MoveType moveType)
        {
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
            //TODO: add something

        }


        public void OnCharInfoUpdate(Actor aActor)
        {

        }


        public void OnPlayerSizeChange(Actor aActor)
        {

        }

        public void OnDie()
        {
            
        }

        public void OnKick()
        {
            
        }

        public void OnMapLoaded()
        {
          
        }

        public void OnReSpawn()
        {
           
        }

        public void OnSendMessage(string from, string message)
        {
           
        }

        public void OnSendWhisper(string name, string message, byte flag)
        {
           
        }

        public void OnTeleport(short x, short y)
        {
           
        }

        public void OnAttack(Actor aActor, MapEventArgs args)
        {
           
        }

        public void OnHPMPSPUpdate(Actor sActor)
        {

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
        public void OnActorFurnitureList(Object obj)
        { }
        public void OnUpdate(Actor aActor)
        {
        }
        #endregion
    }
}
