using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42503 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约")) return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorEventHandlers.PCEventHandler _e;
            SagaMap.Network.Client.MapClient myClinet;
            ActorPC myActor = (ActorPC)sActor;
            _e = (ActorEventHandlers.PCEventHandler)sActor.e;
            myClinet = _e.Client;
            myClinet.SendSkillDummy();
        }
        #endregion
    }
}
