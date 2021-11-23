
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Ranger
{
    /// <summary>
    /// 野營（ビバーク）
    /// </summary>
    public class Bivouac : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            HPRecovery skill = new HPRecovery(args.skill, sActor, 300000, 5000);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}