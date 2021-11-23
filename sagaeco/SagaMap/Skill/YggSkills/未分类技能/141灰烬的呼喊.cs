using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S141 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //用常规办法不行，因为死亡时会清除全部buff，然后触发EndEventHandler

            //if (sActor.type == ActorType.PC)
            //{
            //    ActorPC pc = (ActorPC)sActor;
            //    DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "灰烬的呼喊", true);
            //    skill.OnAdditionStart += this.StartEventHandler;
            //    skill.OnAdditionEnd += this.EndEventHandler;
            //    SkillHandler.ApplyAddition(sActor, skill);
            //}
        }

        void StartEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //actor.TInt["灰烬的呼喊"] = 1;
        }

        void EndEventHandler(Actor actor, DefaultPassiveSkill skill)
        {
            //actor.TInt["灰烬的呼喊"] = 0;
        }

        #endregion
    }
}
