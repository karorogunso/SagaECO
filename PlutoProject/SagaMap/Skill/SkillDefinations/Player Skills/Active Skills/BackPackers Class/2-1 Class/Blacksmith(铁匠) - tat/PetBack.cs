
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Mob;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 回來！（戻れ！）
    /// </summary>
    public class PetBack : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            MobAI ai = SkillHandler.Instance.GetMobAI(sActor);
            if (ai == null)
            {
                return;
            }
            ai.StopAttacking();
        }
        #endregion
    }
}