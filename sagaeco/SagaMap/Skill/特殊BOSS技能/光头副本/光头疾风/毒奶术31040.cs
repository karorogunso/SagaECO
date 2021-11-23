using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    public class S31040 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factors = 5f;
            int damage = SkillHandler.Instance.CalcDamage(false, sActor, dActor, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, factors);
            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
            Poison2 skill = new Poison2(args.skill, dActor, 30000, 35);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}

