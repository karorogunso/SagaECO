
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    /// <summary>
    /// 丟石頭（石つぶて）
    /// </summary>
    public class StoneThrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.6f + 0.1f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            int rate=18+2*level;
            if(SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.硬直 ,rate))
            {
                硬直 skill1 = new 硬直(args.skill, dActor, 3000);
                SkillHandler.ApplyAddition(dActor, skill1);
            }
        }
        #endregion
    }
}