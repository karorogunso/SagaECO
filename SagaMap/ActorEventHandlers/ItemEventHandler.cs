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
    public class ItemEventHandler : ActorEventHandler
    {
        
        public ItemEventHandler()
        {
            
        }

        #region ActorEventHandler Members

        public void OnActorAppears(Actor aActor)
        {
            
        }

        public void OnActorChangeEquip(Actor sActor, MapEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnActorChat(Actor cActor, MapEventArgs args)
        {
            
        }

        public void OnActorDisappears(Actor dActor)
        {
            
        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            
        }

        public void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            throw new NotImplementedException();
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

        public void OnDelete()
        {
            //TODO: add something

        }

        public void OnDie()
        {
            throw new NotImplementedException();
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

        public void OnTeleport(float x, float y)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
