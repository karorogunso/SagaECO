using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.c1skill
{
    public class YumemiTest : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SagaMap.Packets.Server.TEST_YUMEMI_1 p = new Packets.Server.TEST_YUMEMI_1();
            //SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)dActor).SendNPCPlaySound netIO.SendPacket(p);
        }
    }
}
