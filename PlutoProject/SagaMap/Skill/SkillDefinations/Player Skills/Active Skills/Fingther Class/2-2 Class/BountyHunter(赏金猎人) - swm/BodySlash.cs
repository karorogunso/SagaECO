using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 砍擊盔甲（ストレートスラッシュ）
    /// </summary>
    public class BodySlash : Slash, ISkill
    {
        #region ISkill Members
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.SkillProc(sActor, dActor, args, level, SagaLib.PossessionPosition.CHEST);
        }
        #endregion
    }
}
