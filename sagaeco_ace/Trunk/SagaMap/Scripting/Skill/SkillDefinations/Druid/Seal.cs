
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 封印（封印）
    /// </summary>
    public class Seal : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 3000 + 1000 * level;
            鈍足 skill = new 鈍足(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}