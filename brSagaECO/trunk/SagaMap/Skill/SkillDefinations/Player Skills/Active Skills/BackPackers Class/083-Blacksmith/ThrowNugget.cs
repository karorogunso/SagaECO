
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 投擲礦物（ナゲット投げ）
    /// </summary>
    public class ThrowNugget : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f + 1.0f * level;
            //用等級判斷取走的道具
            uint[] itemIDs = { 0, 10015708, 10015700, 10015600, 10015800, 10015707 };
            uint itemID = itemIDs[level];
            ActorPC pc = (ActorPC)sActor;
            if (SkillHandler.Instance.CountItem(pc, itemID) > 0)
            {
                SkillHandler.Instance.TakeItem(pc, itemID, 1);
                SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            }
            
        }
        #endregion
    }
}