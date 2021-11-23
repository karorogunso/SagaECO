
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Maestro
{
    /// <summary>
    /// レールガン
    /// </summary>
    public class RobotLaser : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            if (!sActor.Status.Additions.ContainsKey("RobotLaser"))
            {
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
            else return -30;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 8f * level;
            int lifetime = 35000 - 5000 * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "RobotLaser", lifetime);
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion
    }
}