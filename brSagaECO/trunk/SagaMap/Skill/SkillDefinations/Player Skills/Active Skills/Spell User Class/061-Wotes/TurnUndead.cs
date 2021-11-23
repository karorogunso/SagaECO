
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    /// <summary>
    /// 聖光審判術（ターンアンデッド）
    /// </summary>
    public class TurnUndead : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float[] factors = new float[] { 0.0f, 2.4f, 2.9f, 3.5f, 4.2f, 5.0f };
            float factor = factors[level] - sActor.Status.Cardinal_Rank;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}