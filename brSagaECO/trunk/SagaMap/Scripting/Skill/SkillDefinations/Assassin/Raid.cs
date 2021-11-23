
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 奇襲（奇襲）
    /// </summary>
    public class Raid : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("Hiding") || sActor.Status.Additions.ContainsKey("Cloaking"))
            {
                return 0;
            }
            else
            {
                return -12;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.5f + 1.0f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            int rate = 20 + 5 * level;
            int lifetime=2000+1500*level;
            if (SkillHandler.Instance.CanAdditionApply(sActor,dActor, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Stun skill = new Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
            SkillHandler.Instance.PushBack(sActor, dActor, 4);
        }
        #endregion
    }
}