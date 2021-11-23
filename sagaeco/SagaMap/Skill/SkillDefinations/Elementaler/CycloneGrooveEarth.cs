
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    /// <summary>
    /// 元素四重奏（エレメンタルカルテット）
    /// </summary>
    public class CycloneGrooveEarth : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }
        #endregion
    }
}