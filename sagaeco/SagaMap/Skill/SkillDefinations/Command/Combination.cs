
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Command
{
    /// <summary>
    /// 連打
    /// </summary>
    public class Combination : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            List<Actor> dactors = new List<Actor>();
            for (int i = 0; i < 5; i++)
            {
                dactors.Add(dActor);
            }
            float factor = 1f + 0.05f * level;
            if (dActor.Status.def < 50 || dActor.Status.def_add < 50)
                factor = factor * 1.5f;
            SkillHandler.Instance.PhysicalAttack(sActor, dactors, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}