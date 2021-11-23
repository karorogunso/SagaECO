using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Network.Client;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Event
{
    public class MaxiMum : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
                DefaultBuff skill = new DefaultBuff(args.skill, sActor, "デカデカ", 600000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = actor as ActorPC;
            if (pc != null)
            {
                pc.Size = 2000;
                actor.e.OnPlayerSizeChange(actor);
            }
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            ActorPC pc = actor as ActorPC;
            if (pc != null)
            {
                pc.Size = 1000;
                actor.e.OnPlayerSizeChange(actor);
            }
        }
        #endregion
    }
}
