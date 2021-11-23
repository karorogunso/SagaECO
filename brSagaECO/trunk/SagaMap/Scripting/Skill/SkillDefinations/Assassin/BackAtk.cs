
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 死神晚宴（バックアタック）
    /// </summary>
    public class BackAtk : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //需作方向判斷
            float factor  =0;
            //背面以外
            //factor=0.1f+0.5f*level;
            //背面
            factor = 1.5f + 0.5f * level;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}