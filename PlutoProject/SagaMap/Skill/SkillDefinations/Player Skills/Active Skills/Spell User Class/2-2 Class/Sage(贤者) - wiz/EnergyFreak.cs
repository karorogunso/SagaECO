using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 能量轉移（エナジーフリーク）
    /// </summary>
    public class EnergyFreak : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = new int[] { 0, 4500, 5300, 6500, 7300, 8500 }[level];
            float factor = 1.0f + 0.4f * level;
            int rate = 10 * level;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);

            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Silence, rate))
            {
                Additions.Global.Silence skill3 = new SagaMap.Skill.Additions.Global.Silence(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill3);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Additions.Global.Poison skill5 = new SagaMap.Skill.Additions.Global.Poison(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill5);
            }
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Stone, rate))
            {
                Additions.Global.Stone skill4 = new SagaMap.Skill.Additions.Global.Stone(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill4);
            }
        }
        #endregion
    }
}
