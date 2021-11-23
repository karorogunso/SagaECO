
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 冥想（瞑想）
    /// </summary>
    public class PetMeditatioon : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Additions.Global.Meditatioon skill = new SagaMap.Skill.Additions.Global.Meditatioon(args.skill, dActor, 20000);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}