using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 參擊無雙（斬撃無双）
    /// </summary>
    public class MuSoU : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = {0.2f, 0.45f, 0.7f,0.95f, 1.2f};
            float factor = factors[level-1];
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.BLOW;
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < 10; i++)
            {
                dest.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SagaLib.Elements.Neutral, factor);
            硬直 skill = new 硬直(args.skill, dActor, 1000 );
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
    }
}
