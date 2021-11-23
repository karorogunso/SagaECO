
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 危險毒性（プラントポイズン）[接續技能]
    /// </summary>
    public class PetPlantPoison : ISkill
    {
        bool MobUse;
        public PetPlantPoison()
        {
            this.MobUse = false;
        }
        public PetPlantPoison(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            float factor = 0.9f + 0.2f * level;

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            int rate = 65 + 5 * level;
            int lifetime = 1000 + 1000 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Poison skill = new Poison(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        #endregion
    }
}