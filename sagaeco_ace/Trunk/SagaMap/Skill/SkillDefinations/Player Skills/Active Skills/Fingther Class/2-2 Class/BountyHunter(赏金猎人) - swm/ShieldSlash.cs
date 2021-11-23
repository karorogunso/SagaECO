using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 砍擊盾（シールドスラッシュ）
    /// </summary>
    public class ShieldSlash : Slash, ISkill
    {
        #region ISkill Members
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.SkillProc(sActor, dActor, args, level, SagaLib.PossessionPosition.LEFT_HAND );
        }
        #endregion
    }
}
