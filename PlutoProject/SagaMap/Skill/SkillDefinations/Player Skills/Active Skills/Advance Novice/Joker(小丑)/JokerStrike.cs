using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    ///  小丑连击
    /// </summary>
    public class JokerStrike : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;

        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.argType = SkillArg.ArgType.Attack;
            float factor = new float[] { 0, 1.8f, 2.2f, 2.6f, 3.0f, 3.4f }[level];

            int[] attackTimes = { 0, 2, 3, 4, 6, 8, 10 };
            
            List<Actor> dest = new List<Actor>();
            for (int i = 0; i < attackTimes[level]; i++)
                dest.Add(dActor);
            args.delayRate = 4.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, sActor.WeaponElement, factor);
        }
        #endregion
    }
}
