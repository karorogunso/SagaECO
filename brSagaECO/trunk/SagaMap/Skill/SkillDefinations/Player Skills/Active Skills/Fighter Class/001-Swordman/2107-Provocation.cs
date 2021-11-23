using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Swordman
{
    /// <summary>
    /// 挑釁
    /// </summary>
    public class Provocation:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Hate.ContainsKey(sActor.ActorID))
                {
                    eh.AI.Hate[sActor.ActorID] += (uint)(dActor.MaxHP / 5);
                }
                else
                {
                    eh.AI.Hate.Add(sActor.ActorID, (uint)(dActor.MaxHP / 5));
                }
            }
        }

        #endregion
    }
}
