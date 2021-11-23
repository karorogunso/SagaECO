using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 初級舞蹈（デッドリーアタック）
    /// </summary>
    public class WildDance : ISkill 
    {
         #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.5f + 1.0f * level;
            int rate = 20 * level;
            int lifetime = 7000;
            args.argType = SkillArg.ArgType.Attack;
            args.type = ATTACK_TYPE.STAB;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, sActor.WeaponElement, factor);
            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                Additions.Global.Stiff skill1 = new SagaMap.Skill.Additions.Global.Stiff(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill1);
            }
        }
         #endregion
    }
}
