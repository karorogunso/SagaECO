using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    ///  大地之怒（兜割り）
    /// </summary>
    public class aEarthAngry :BeheadSkill, ISkill
    {
        #region ISkill Members
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.Proc(sActor, dActor, args, level, SagaDB.Mob.MobType.INSECT);
        }
        #endregion
    }
}
