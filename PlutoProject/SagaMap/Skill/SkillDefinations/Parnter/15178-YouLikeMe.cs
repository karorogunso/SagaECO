using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using static SagaMap.Skill.SkillHandler;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// ぱぴーみたいにどーん！
    /// </summary>
    public class YouLikeMe : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.1f;
            SkillHandler.Instance.PushBack(sActor, dActor, 6);
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, Elements.Dark, factor);
        }
        #endregion
    }
}