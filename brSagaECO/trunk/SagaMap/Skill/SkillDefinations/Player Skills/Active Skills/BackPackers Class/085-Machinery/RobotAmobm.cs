
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Machinery
{
    /// <summary>
    /// 發射導彈（アンチモブミサイル）
    /// </summary>
    public class RobotAmobm : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (pet == null)
            {
                return -53;//需回傳"需裝備寵物"
            }
            if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            {
                return 0;
            }
            return -53;//需回傳"需裝備寵物"
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f + 0.3f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}