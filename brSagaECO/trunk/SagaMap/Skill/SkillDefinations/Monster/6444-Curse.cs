using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 天罰!
    /// </summary>
    public class Curse : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = (float)(dActor.MaxHP * 0.9f);
            SkillHandler.Instance.FixAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            Stiff skill = new Stiff(args.skill, dActor, 2000);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}


