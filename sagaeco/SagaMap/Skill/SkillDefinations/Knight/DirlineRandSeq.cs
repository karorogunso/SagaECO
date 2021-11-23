
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 刺裂旋風（スピアサイクロン）
    /// </summary>
    public class DirlineRandSeq : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.SPEAR, SagaDB.Item.ItemType.RAPIER) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint DirlineRandSeq2_SkillID = 2382;
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(DirlineRandSeq2_SkillID, level, 0));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(DirlineRandSeq2_SkillID, level, 560));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(DirlineRandSeq2_SkillID, level, 560));
        }
        #endregion

    }
}
