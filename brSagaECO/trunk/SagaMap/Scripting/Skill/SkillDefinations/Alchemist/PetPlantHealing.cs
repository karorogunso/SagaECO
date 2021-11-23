
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 植物停頓（プラントヒーリング）
    /// </summary>
    public class PetPlantHealing : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = -(1.0f + 0.4f * level);
            SkillHandler.Instance.MagicAttack(sActor, sActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, 50, factor);
        }
        #endregion
    }
}