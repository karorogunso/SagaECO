using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations.X
{
    public class IceDef : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            short defadd = (short)(sActor.Status.def * 0.35f);
            sActor.Status.def_add_skill += defadd;

            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "冰封坚韧", 9000000);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}
