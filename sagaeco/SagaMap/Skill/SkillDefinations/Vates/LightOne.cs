using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class LightOne:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1f;
            if (level == 6)
                SkillHandler.Instance.Seals(sActor, dActor,5);
            else
                SkillHandler.Instance.Seals(sActor, dActor);
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);

            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 70, 1);
            }
        }
        #endregion
    }
}
