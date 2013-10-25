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
    public class PCEventHandler : ActorEventHandler
    {
        public MapClient Client;

        public PCEventHandler(MapClient client)
        {
            Client = client;
        }

        #region ActorEventHandler Members

        public void OnActorAppears(Actor aActor)
        {
            if (Client == null) return;
            switch (aActor.type)
            {
                case ActorType.PC:
                    Packets.Server.SSMG_ACTOR_PC_APPEAR p = new SagaMap.Packets.Server.SSMG_ACTOR_PC_APPEAR();
                    ActorPC pc = (ActorPC)aActor;
                    p.ActorID = pc.ActorID;
                    p.Dir = (byte)(pc.Dir / 45);
                    p.HP = pc.HP;
                    p.MaxHP = pc.MaxHP;
                    p.PossessionActorID = 0xFFFFFFFF;
                    p.PossessionPosition = PossessionPosition.NONE;
                    p.Speed = pc.Speed;
                    p.X = Global.PosX16to8(pc.X);
                    p.Y = Global.PosY16to8(pc.Y);
                    this.Client.netIO.SendPacket(p);
                    break;
                case ActorType.ITEM:
                    Packets.Server.SSMG_ITEM_ACTOR_APPEAR p1 = new SagaMap.Packets.Server.SSMG_ITEM_ACTOR_APPEAR();
                    p1.Item = (ActorItem)aActor;
                    this.Client.netIO.SendPacket(p1);
                    break;
                default:
                    break;
            }
        }

        public void OnActorChangeEquip(Actor sActor, MapEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnActorChat(Actor cActor, MapEventArgs args)
        {
            if (Client == null) return;
            Packets.Server.SSMG_CHAT_PUBLIC p = new SagaMap.Packets.Server.SSMG_CHAT_PUBLIC();
            p.ActorID = cActor.ActorID;
            p.Message = ((ChatArg)args).content;
            this.Client.netIO.SendPacket(p);
        }

        public void OnActorDisappears(Actor dActor)
        {
            switch (dActor.type)
            {
                case ActorType.PC:
                    Packets.Server.SSMG_ACTOR_DELETE p = new SagaMap.Packets.Server.SSMG_ACTOR_DELETE();
                    p.ActorID = dActor.ActorID;
                    this.Client.netIO.SendPacket(p);
                    break;
                case ActorType.ITEM:
                    Packets.Server.SSMG_ITEM_ACTOR_DISAPPEAR p1 = new SagaMap.Packets.Server.SSMG_ITEM_ACTOR_DISAPPEAR();
                    ActorItem item = (ActorItem)dActor;
                    p1.ActorID = item.ActorID;
                    p1.Count = item.Item.stack;
                    p1.Looter = item.LootedBy;
                    this.Client.netIO.SendPacket(p1);
                    break;
            }
        }

        public void OnActorSkillUse(Actor sActor, MapEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnActorStartsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            Packets.Server.SSMG_ACTOR_MOVE p = new SagaMap.Packets.Server.SSMG_ACTOR_MOVE();
            p.ActorID = mActor.ActorID;
            p.X = pos[0];
            p.Y = pos[1];
            p.Dir = dir;
            p.MoveType = MoveType.RUN;
            this.Client.netIO.SendPacket(p);
        }

        public void OnActorStopsMoving(Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            throw new NotImplementedException();
        }

        public void OnCreate(bool success)
        {
            if (Client == null) return;
            if (success)
            {
                Client.SendActorID();
                Client.SendActorMode();
                Client.SendCharOption();
                Client.SendItems();
                Client.SendCharInfo();

            }
        }


        public void OnActorChangeEmotion(Actor aActor, MapEventArgs args)
        {
            ChatArg arg = (ChatArg)args;
            Packets.Server.SSMG_CHAT_EMOTION p = new SagaMap.Packets.Server.SSMG_CHAT_EMOTION();
            p.ActorID = aActor.ActorID;
            p.Emotion = arg.emotion;
            this.Client.netIO.SendPacket(p);
        }

        public void OnActorChangeMotion(Actor aActor, MapEventArgs args)
        {
            ChatArg arg = (ChatArg)args;
            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
            p.ActorID = aActor.ActorID;
            p.Motion = arg.motion;
            p.Loop = arg.loop;
            this.Client.netIO.SendPacket(p);
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
