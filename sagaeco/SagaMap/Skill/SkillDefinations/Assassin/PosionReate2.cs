using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    ///  硬化毒（硬化毒）
    /// </summary>
    public class PosionReate2 : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Stone stone = new Stone(args.skill, sActor, 5000);
            SkillHandler.ApplyAddition(sActor, stone);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PoisonReate2", 5000);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}



                                          