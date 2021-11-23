using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Striker
{
    /// <summary>
    /// 多連箭（バラージアロー）
    /// </summary>
    public class PluralityArrow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] times = { 0, 3, 4, 5, 5, 5 };
            float[] factor = { 0f, 1.2f, 1.2f, 1.2f, 1.2f, 1.25f };
            List<Actor> target = new List<Actor>();
            for (int i = 0; i < times[level]; i++)
            {
                target.Add(dActor);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, target, args, SagaLib.Elements.Neutral, factor[level]);
        }
        #endregion
    }
}
