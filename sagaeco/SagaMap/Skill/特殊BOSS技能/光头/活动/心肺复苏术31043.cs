using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Network.Client;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31043
        : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor != null)
            {
                if (dActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)dActor;
                    if (pc.Online)
                    {
                        if (pc.Buff.Dead == true)
                        {
                            pc.Buff.紫になる = true;
                            MapClient.FromActorPC(pc).Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);
                            pc.TInt["Revive"] = 5;

                            if (dActor.type == ActorType.PC)
                                MapClient.FromActorPC((ActorPC)dActor).SendSystemMessage(string.Format("玩家 {0} 正在使你复活", sActor.Name));
                            pc.TStr["复活者"] = sActor.Name;

                            MapClient.FromActorPC(pc).EventActivate(0xF1000000);
                        }

                    }
                }
            }
        }

        #endregion
    }
}
