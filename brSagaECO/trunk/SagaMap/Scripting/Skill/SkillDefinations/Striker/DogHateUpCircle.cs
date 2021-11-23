
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Mob;
namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 野獸咆哮（エンミティーロア）
    /// </summary>
    public class DogHateUpCircle : ISkill
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
            if (SkillHandler.Instance.CheckMobType(pet, "ANIMAL"))
            {
                MobAI ai = SkillHandler.Instance.GetMobAI(pet);
                ai.CastSkill(6307, level, dActor);
            }
            
        }
        #endregion
    }
}