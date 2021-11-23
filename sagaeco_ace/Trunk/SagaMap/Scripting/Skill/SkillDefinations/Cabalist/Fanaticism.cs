
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 聖曜光輝（ファナティシズム）
    /// </summary>
    public class Fanaticism : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 20 + 10 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "Berserk", rate))
            {
                Berserk skill = new Berserk(args.skill, dActor, 30000);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}
