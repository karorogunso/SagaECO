using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 憤怒的錘子
    /// </summary>
    public class MobHpPerDown : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = (float)(dActor.HP * 0.1f);
            SkillHandler.Instance.FixAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}