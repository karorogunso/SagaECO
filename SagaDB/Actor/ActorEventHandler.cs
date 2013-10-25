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
        void OnCreate(bool success);

        void OnReSpawn();

        void OnMapLoaded();

        void OnDie();

        void OnKick();

        void OnDelete();

        void OnActorAppears(Actor aActor);

        void OnActorChangeEmotion(Actor aActor, MapEventArgs args);

        void OnActorChangeMotion(Actor aActor, MapEventArgs args);

        void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed);

        void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed);

        void OnActorChat(Actor cActor, MapEventArgs args);

        void OnActorDisappears(Actor dActor);

        void OnActorSkillUse(Actor sActor, MapEventArgs args);

        void OnActorChangeEquip(Actor sActor, MapEventArgs args);

        void OnTeleport(float x, float y);

        void OnSendWhisper(string name, string message, byte flag);

        void OnSendMessage(string from, string message);        
    }
}
