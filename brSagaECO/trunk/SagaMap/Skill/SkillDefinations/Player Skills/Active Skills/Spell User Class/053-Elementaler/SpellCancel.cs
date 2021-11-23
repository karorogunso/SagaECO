using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    class SpellCancel : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
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
            if (myClinet.Character.Tasks.ContainsKey("SkillCast"))
            {
                if (myClinet.Character.Tasks["SkillCast"].Activated)
                {
                    myClinet.Character.Tasks["SkillCast"].Deactivate();
                    myClinet.Character.Tasks.Remove("SkillCast");
                }
            }
        }
        #endregion

    }
}
