
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.BladeMaster
{
    /// <summary>
    /// 2段拔刀（二段抜刀）
    /// </summary>
    public class DoubleCutDown : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.SWORD, SagaDB.Item.ItemType.SHORT_SWORD, SagaDB.Item.ItemType.AXE) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            uint NextSkillID=2380;
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillID, level, 0));
            args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(NextSkillID, level, 500));
        }
        #endregion
    }
}