using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void OnChat(Packets.Client.CSMG_CHAT_PUBLIC p)
        {
            if (!AtCommand.Instance.ProcessCommand(this, p.Content))
            {
                ChatArg arg = new ChatArg();
                arg.content = p.Content;
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, this.Character, true);            
            }
        }

        public void OnMotion(Packets.Client.CSMG_CHAT_MOTION p)
        {
            ChatArg arg = new ChatArg();
            arg.motion = p.Motion;
            arg.loop = p.Loop;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);
        }

        public void OnEmotion(Packets.Client.CSMG_CHAT_EMOTION p)
        {
            ChatArg arg = new ChatArg();
            arg.emotion = p.Emotion;            
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.EMOTION, arg, this.Character, true);
        }

        public void OnSit(Packets.Client.CSMG_CHAT_SIT p)
        {
            ChatArg arg = new ChatArg();
            arg.motion = MotionType.SIT;
            arg.loop = 1;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, this.Character, true);
        }

        public void SendSystemMessage(string content)
        {
            Packets.Server.SSMG_CHAT_PUBLIC p = new SagaMap.Packets.Server.SSMG_CHAT_PUBLIC();
            p.ActorID = 0xFFFFFFFF;
            p.Message = content;
            this.netIO.SendPacket(p);
        }
    }
}
