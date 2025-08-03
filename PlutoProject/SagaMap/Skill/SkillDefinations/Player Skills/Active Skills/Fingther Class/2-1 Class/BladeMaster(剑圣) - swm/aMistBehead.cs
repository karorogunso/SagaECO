using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 飛霞斬（霞斬り）
    /// </summary>
    public class aMistBehead : BeheadSkill, ISkill
    {
        #region ISkill Members
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.Proc(sActor, dActor, args, level, SagaDB.Mob.MobType.BIRD);
        }
        #endregion
    }
}
