using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42504 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            //if (pc.Status.Additions.ContainsKey("属性契约")) return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (sActor.type == ActorType.PC)
            {
                
                ActorPC pc = (ActorPC)sActor;
                if (pc.TInt["无属性状态"] != 1)
                {
                    pc.TInt["无属性状态"] = 1;
                    Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("无属性魔法已转换成『物理性』");
                }
                else
                {
                    pc.TInt["无属性状态"] = 0;
                    Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("无属性魔法已转换成『魔法性』");
                }
            }
        }
        #endregion
    }
}
