
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Mob;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 猛鳥回音（シュリルボイス）
    /// </summary>
    public class PetBirdAtkRowCircle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet == null)
            {
                return;
            }
            if (SkillHandler.Instance.CheckMobType(pet, "BIRD"))
            {
                MobAI ai = SkillHandler.Instance.GetMobAI(pet);
                ai.CastSkill(6501, level, dActor);
            }
        }
        #endregion
    }
}