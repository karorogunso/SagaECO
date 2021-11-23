using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S40104 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition oa1 = new OtherAddition(args.skill, sActor, "ResistDEBUFF", 5000);//内容未装
            OtherAddition oa2 = new OtherAddition(args.skill, sActor, "BUFFUP", 30000);//内容未装
            SkillHandler.ApplyAddition(sActor, oa1);
            SkillHandler.ApplyAddition(sActor, oa2);
        }
        #endregion
    }
}