using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 巨木斷（巨木断ち）
    /// </summary>
    public class aWoodHack : BeheadSkill, ISkill
    {
        #region ISkill Members
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.Proc(sActor, dActor, args, level, SagaDB.Mob.MobType.TREE);
        }
        #endregion
    }
}
