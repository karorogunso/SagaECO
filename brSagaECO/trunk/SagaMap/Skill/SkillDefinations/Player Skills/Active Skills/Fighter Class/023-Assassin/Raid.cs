
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
            float[] factors = new float[] { 0, 6.5f, 7.0f, 7.25f, 7.5f, 8.0f };
            float factor = factors[level];
            int crirate = 30 + 5 * level;
            List<Actor> affected = new List<Actor>();
            affected.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SkillHandler.DefType.Def, SagaLib.Elements.Neutral, 0, factor, false, 0, false, 0, crirate);
            int rate = 25 + 5 * level;
            int lifetime = 5000 + 1000 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stun, rate))
            {
                Stun skill = new Stun(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}