using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 砍擊裝飾品（オーナメントスラッシュ）
    /// </summary>
    public class ChestSlash : Slash, ISkill
    {
        #region ISkill Members
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            this.SkillProc(sActor, dActor, args, level, SagaLib.PossessionPosition.NECK );
        }
        #endregion
    }
}
