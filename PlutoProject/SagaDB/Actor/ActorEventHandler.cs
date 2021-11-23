using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Item;

namespace SagaDB.Actor
{
    public interface MapEventArgs { }
    public interface ActorEventHandler
    {
        void OnPlayerShopChange(Actor aActor);

        void OnPlayerShopChangeClose(Actor aActor);

        void OnCreate(bool success);
        void OnActorReturning(Actor sActor);

        void OnReSpawn();

        void OnMapLoaded();

        void OnDie();

        void OnKick();

        void OnDelete();

        void OnAttack(Actor aActor, MapEventArgs args);

        void OnCharInfoUpdate(Actor aActor);

        void OnPlayerSizeChange(Actor aActor);

        void OnActorAppears(Actor aActor);

        void OnActorChangeEmotion(Actor aActor, MapEventArgs args);

        void OnActorChangeMotion(Actor aActor, MapEventArgs args);

        void OnActorChangeWaitType(Actor aActor);

        void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed);

        void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed, MoveType moveType);

        void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed);
        
        void OnActorSpeedChange(Actor mActor);

        void OnActorChat(Actor cActor, MapEventArgs args);

        void OnActorDisappears(Actor dActor);

        void OnActorSkillUse(Actor sActor, MapEventArgs args);

        void OnActorChangeEquip(Actor sActor, MapEventArgs args);

        void OnActorChangeBuff(Actor sActor);

        void OnTeleport(short x, short y);

        void OnPlayerChangeStatus(ActorPC aActor);

        void OnSendWhisper(string name, string message, byte flag);

        void OnSendMessage(string from, string message);

        void OnHPMPSPUpdate(Actor sActor);

        void OnLevelUp(Actor sActor, MapEventArgs args);

        void OnPlayerMode(Actor aActor);

        void OnShowEffect(Actor aActor, MapEventArgs args);

        void OnActorPossession(Actor aActor, MapEventArgs args);

        void OnActorPartyUpdate(ActorPC aActor);

        void OnSignUpdate(Actor aActor);

        void PropertyUpdate(UpdateEvent arg, int para);

        void PropertyRead(UpdateEvent arg);

        void OnActorRingUpdate(ActorPC aActor);

        void OnActorWRPRankingUpdate(ActorPC aActor);

        void OnActorChangeAttackType(ActorPC aActor);

        void OnActorFurnitureSit(ActorPC aActor);

        void OnActorFurnitureList(Object obj);

        void OnUpdate(Actor aActor);

        void OnActorPaperChange(ActorPC aActor);

        void OnActorSkillCancel(Actor aActor);
    }
}
