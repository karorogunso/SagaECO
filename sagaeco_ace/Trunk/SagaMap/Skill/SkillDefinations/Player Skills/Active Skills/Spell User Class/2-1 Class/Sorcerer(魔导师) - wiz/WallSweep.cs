
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 解除魔法障壁（ウォールスイープ）
    /// </summary>
    public class WallSweep : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //解除已設置的魔法牆壁
            //解除別人的牆壁，PVP專用技能
        }
        #endregion
    }
}