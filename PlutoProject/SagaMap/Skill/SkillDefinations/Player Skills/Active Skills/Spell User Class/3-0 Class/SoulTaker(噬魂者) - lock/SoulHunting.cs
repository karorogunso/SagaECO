using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    /// <summary>
    /// 灵魂狩猎(ソウルハント)
    /// </summary>
    public class SoulHunting : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float []factor = { 0,12.0f,3.0f,17.0f,3.0f,14.0f};

            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor[level]);
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2545, level, 1000));
        }

        #endregion
    }
}
