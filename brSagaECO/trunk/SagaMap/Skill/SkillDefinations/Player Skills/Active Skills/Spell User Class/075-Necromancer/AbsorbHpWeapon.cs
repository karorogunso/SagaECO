
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 噬血（ライフテイク）
    /// </summary>
    public class AbsorbHpWeapon : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            BloodLeech skill = new BloodLeech(args.skill, dActor, 50000, 0.1f * level);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}