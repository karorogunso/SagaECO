using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Network.Client;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class Resurrection:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)dActor;
                if (pc.Online)
                {
                    pc.Buff.紫になる = true;
                    MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);
                    pc.TInt["Revive"] = level;
                    MapClient.FromActorPC(pc).EventActivate(0xF1000000);
                }
            }
        }

        #endregion
    }
}
