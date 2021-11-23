using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 魔法反射：反射下一次魔法伤害，物魔转换后改成反射下一次物理伤害 未实装
    /// </summary>
    public class S42509 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            OtherAddition oa = new OtherAddition(args.skill, pc, "ShieldReflect", 2000);
            SkillHandler.ApplyAddition(pc, oa);
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

        }

        #endregion
    }
}
